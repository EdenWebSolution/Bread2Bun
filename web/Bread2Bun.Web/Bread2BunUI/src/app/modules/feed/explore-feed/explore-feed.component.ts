import { Component, OnInit } from '@angular/core';
import { slideFromUp } from 'src/app/animations';
import { ProfileService } from '../../profile/services/profile.service';
import { ViewProfileModel } from '../../shared/models/view-profile-model';
import { forkJoin } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ExploreFeedModel, ExploreFeedFood } from '../models/explore-feed-model';
import { Router } from '@angular/router';

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
  postFinished: boolean = false;
  DefaultProfileImage: string = 'https://t3.ftcdn.net/jpg/00/64/67/80/240_F_64678017_zUpiZFjj04cnLri7oADnyMH0XBYyQghG.jpg';

  profileImageUrl: string = 'https://t3.ftcdn.net/jpg/00/64/67/80/240_F_64678017_zUpiZFjj04cnLri7oADnyMH0XBYyQghG.jpg';

  posts: Array<ExploreFeedFood> = new Array<ExploreFeedFood>();
  profileData: ViewProfileModel = new ViewProfileModel();

  constructor(
    private profileService: ProfileService,
    private tstr: ToastrService,
    private router: Router
  ) { }

  ngOnInit() {
    this.profileService.getStatus().subscribe(result => {
      if (result) {
        this.getFeedData();
      } else {
        this.router.navigate(['/app/profile/editProfile']);
      }
    });
  }

  getFeedData() {
    forkJoin(
      this.profileService.getProfileView(),
      this.profileService.getExploreFeed(this.skip, this.take)
    ).subscribe(result => {
      this.profileData = result[0];
      this.posts = result[1].items;
      if (this.profileData.profileImage !== null) {
        this.profileImageUrl = this.profileData.profileImage;
      }
    }, error => {
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
}
