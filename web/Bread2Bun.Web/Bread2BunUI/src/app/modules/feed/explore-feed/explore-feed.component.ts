import { Component, OnInit } from '@angular/core';
import { slideFromUp } from 'src/app/animations';
import { ProfileService } from '../../profile/services/profile.service';
import { ViewProfileModel } from '../../shared/models/view-profile-model';
import { forkJoin } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ExploreFeedModel, ExploreFeedFood } from '../models/explore-feed-model';
import { Router } from '@angular/router';
import { LayoutService } from '../../layout/layout.service';

@Component({
  selector: 'app-explore-feed',
  templateUrl: './explore-feed.component.html',
  styleUrls: ['./explore-feed.component.scss'],
  animations: [slideFromUp]
})
export class ExploreFeedComponent implements OnInit {

  scrollDistance = 1;
  scrollUpDistance = 2;
  throttle = 300;
  skip = 0;
  take = 10;
  postFinished = false;
  DefaultProfileImage: string = '../../../../assets/images/default.jpg';

  profileImageUrl: string = '../../../../assets/images/default.jpg';
  coverImgUrl: string = '../../../../assets/images/cover.jpg';
  posts: Array<ExploreFeedFood> = new Array<ExploreFeedFood>();
  profileData: ViewProfileModel = new ViewProfileModel();
  isBlocked = false;
  isCompletedProfile = false;

  constructor(
    private profileService: ProfileService,
    private tstr: ToastrService,
    private router: Router,
    private layoutService: LayoutService
  ) { }

  ngOnInit() {
    this.getFeedData();
  }

  getFeedData() {
    this.isBlocked = true;
    forkJoin(
      this.profileService.getProfileView(),
      this.profileService.getExploreFeed(this.skip, this.take),
      this.profileService.getStatus()
    ).subscribe(result => {
      this.isBlocked = false;
      this.profileData = result[0];
      this.posts = result[1].items;
      this.isCompletedProfile = result[2];
      if (this.profileData.profileImage !== null) {
        this.profileImageUrl = this.profileData.profileImage;
      }
      if (this.profileData.coverImage !== null) {
        this.coverImgUrl = this.profileData.coverImage;
      }
    }, error => {
      this.isBlocked = false;
      this.tstr.error('Something went wrong', 'Error');
    });
  }

  getPosts() {
    if (!this.postFinished) {
      this.skip += 10;
      this.profileService.getExploreFeed(this.skip, this.take).subscribe(result => {
        this.posts = this.posts.concat(result.items);
        if (result.items.length === 0) {
          this.postFinished = true;
        }
      });
    }
  }
  onPostUp() {
  }

  editProfile() {
    this.router.navigate(['/app/profile/editProfile']);
  }

  goToThread(userId: number, userName: string, profileImage: string, ) {
    const connectedUsers = this.layoutService.userConnections;
    let connectionId = null;
    let isOnline = false;
    if (connectedUsers.some(a => a.userName === userName)) {
      const connectedUser = connectedUsers.find(user => user.userName === userName);
      connectionId = connectedUser.connectionId;
      isOnline = connectedUser.isOnline;
    }
    const userObj = {
      userName,
      userId,
      profileImage,
      connectionId,
      isOnline
    };
    const userObjStringified = JSON.stringify(userObj);
    localStorage.setItem('chatThread', userObjStringified);
    this.router.navigate(['/app/messages', userName]);
  }
}
