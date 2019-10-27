import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { ProfileService } from '../services/profile.service';
import { ViewOtherProfileModel } from '../../shared/models/view-other-profile-model';
import { ToastrService } from 'ngx-toastr';
import { ClipboardService } from 'ngx-clipboard';
import { ReviewList } from '../models/get-review-list-model';
import { forkJoin } from 'rxjs';
import { ViewProfileModel } from '../../shared/models/view-profile-model';
import { BaseService } from '../../core/services/base.service';

@Component({
  selector: 'app-view-profile',
  templateUrl: './view-profile.component.html',
  styleUrls: ['./view-profile.component.scss']
})
export class ViewProfileComponent implements OnInit {

  username: string;
  profileData: ViewOtherProfileModel = new ViewOtherProfileModel();
  coverImgUrl: string = '../../../../assets/images/cover.jpg';
  profileImgUrl: string = '../../../../assets/images/default.jpg';
  noImage: string = '../../../../assets/images/default.jpg';
  languagesString: string;
  skip = 0;
  take = 10;
  userId: number;
  myReviews: Array<ReviewList> = new Array<ReviewList>();
  profileDetails: ViewProfileModel = new ViewProfileModel();
  isBlocked = false;


  days: Array<any> = [
    {
      id: 1,
      value: 'Monday',
      className: ''
    },
    {
      id: 2,
      value: 'Tuesday',
      className: ''
    },
    {
      id: 3,
      value: 'Wednesday',
      className: ''
    },
    {
      id: 4,
      value: 'Thursday',
      className: ''
    },
    {
      id: 5,
      value: 'Friday',
      className: ''
    },
    {
      id: 6,
      value: 'Saturday',
      className: ''
    },
    {
      id: 7,
      value: 'Sunday',
      className: ''
    },
  ];

  constructor(
    private activatedRoute: ActivatedRoute,
    private profileService: ProfileService,
    private toastr: ToastrService,
    private clipBoardService: ClipboardService,
    private baseService: BaseService
  ) { }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.username = params.username;
      this.getProfileData(this.username);
    });
    this.userId = +this.baseService.getUserId();
  }


  getProfileData(username: string) {
    this.isBlocked = true;
    forkJoin(
      this.profileService.getProfileViewByUserName(username),

    ).subscribe(result => {
      this.profileData = result[0];
      this.setProfileData(this.profileData);
    }, error => {
      this.isBlocked = false;
      this.toastr.error('Couldn\'t load profile details', 'Error');
    });

  }

  setProfileData(profileData) {
    this.profileDetails = profileData.profile;
    if (this.profileDetails.coverImage != null) {
      this.coverImgUrl = this.profileDetails.coverImage;
    }
    if (this.profileDetails.profileImage != null) {
      this.profileImgUrl = this.profileDetails.profileImage;
    }
    this.languagesString = profileData.profile.languages.join(', ');
    profileData.profile.availableDays.forEach(a => {
      this.days.forEach(d => {
        if (d.id === a) {
          d.className = 'highlight';
        }
      });
    });
    this.profileService.getPagedReview(profileData.profile.userId, this.skip, this.take).subscribe(result => {
      this.myReviews = result.items;
      this.isBlocked = false;
    }, error => {
      this.isBlocked = false;
      this.toastr.error('Couldn\'t load profile details', 'Error');
    });
  }

  copyToShare() {
    this.clipBoardService.copyFromContent(window.location.href);
    this.toastr.info('Profile URL copied to clipboard', 'Share');
  }

  openTwitter() {
    window.open('https://twitter.com/' + this.profileData.profile.twitter, '_blank');
  }
  openInstagram() {
    window.open('   https://www.instagram.com/' + this.profileData.profile.instagram, '_blank');
  }

}
