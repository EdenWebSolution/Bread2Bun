import { Component, OnInit, ChangeDetectorRef, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { bloom, slideFromUp } from 'src/app/animations';
import { ClipboardService } from 'ngx-clipboard';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ViewProfileModel } from '../../shared/models/view-profile-model';
import { ProfileService } from '../services/profile.service';
import { forkJoin } from 'rxjs';
import { FoodService } from '../../shared/services/food.service';
import { UserFoodModel } from '../models/user-food-model';
import { ReviewList } from '../models/get-review-list-model';

declare let window;

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.scss'],
  animations: [bloom, slideFromUp]
})
export class MyProfileComponent implements OnInit {


  profileData: ViewProfileModel = new ViewProfileModel();
  noImage: string = '../../../../assets/images/default.jpg';
  profileImgUrl: string = '../../../../assets/images/default.jpg';
  // tslint:disable-next-line: max-line-length
  coverImgUrl: string = '../../../../assets/images/cover.jpg';
  isBlocked = false;

  uniqueName: string;
  myProfile: boolean = true;
  languagesString: string;
  skip = 0;
  take = 50;
  myReviews: Array<ReviewList> = new Array<ReviewList>();
  isCompletedProfile: boolean = false;

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

  userFoods: Array<UserFoodModel> = new Array<UserFoodModel>();

  constructor(
    private fb: FormBuilder,
    private clipBoardService: ClipboardService,
    private toastr: ToastrService,
    private router: Router,
    private profileService: ProfileService,
    private foodService: FoodService
  ) {
  }

  ngOnInit() {
    this.getProfileData();
  }

  copyToShare() {
    this.clipBoardService.copyFromContent(window.location.href + '/' + this.profileData.userName);
    this.toastr.info('Profile URL copied to clipboard', 'Share');
  }

  editProfile() {
    this.router.navigate(['/app/profile/editProfile']);
  }

  getProfileData() {
    this.isBlocked = true;
    forkJoin(
      this.profileService.getProfileView(),
      this.foodService.getUserFoodList(),
      this.profileService.getStatus()

    ).subscribe(result => {
      this.profileData = result[0];
      this.userFoods = result[1],
      this.isCompletedProfile = result[2];
      this.setProfileDate(this.profileData);
    }, error => {
      this.isBlocked = false;
      this.toastr.error('Couldn\'t load profile details', 'Error');

    });

  }

  setProfileDate(profileData: ViewProfileModel) {
    if (profileData.coverImage != null) {
      this.coverImgUrl = profileData.coverImage;
    }
    if (this.profileData.profileImage != null) {
      this.profileImgUrl = profileData.profileImage;
    }
    this.languagesString = profileData.languages.join(', ');
    profileData.availableDays.forEach(a => {
      this.days.forEach(d => {
        if (d.id === a) {
          d.className = 'highlight';
        }
      });
    });
    this.profileService.getPagedReview(profileData.userId, this.skip, this.take).subscribe(result => {
      this.myReviews = result.items;
      this.isBlocked = false;
    }, error => {
      this.isBlocked = false;
      this.toastr.error('Couldn\'t load profile details', 'Error');
    })
  }

  openTwitter() {
    window.open('https://twitter.com/' + this.profileData.twitter, '_blank');
  }
  openInstagram() {
    window.open('   https://www.instagram.com/' + this.profileData.instagram, '_blank');
  }
}
