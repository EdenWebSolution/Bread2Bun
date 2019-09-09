import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UpdateProfileModel } from '../models/update-profile-model';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {

  editProfileForm: FormGroup;
  profileUpdateModel: UpdateProfileModel;

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.editProfileForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      universityId: ['', [Validators.required]],
      countryId: ['', [Validators.required]],
      profileImg: [null, [Validators.required]],
      coverFoodImage: ['', [Validators.required]],
      availableDays: ['', [Validators.required]],
      aboutMe: ['', [Validators.required]],
      languages: ['', [Validators.required]],
      email: ['', [Validators.required]],
      socialmedia: this.fb.group({
        twitterUsername: [''],
        instagramUsername: ['']
      }),
      foodIds: ['', Validators.required]
    });
  }

  get firstName() {
    return this.editProfileForm.get('firstName');
  }

  get lastName() {
    return this.editProfileForm.get('lastName');
  }

  get email() {
    return this.editProfileForm.get('email');
  }

  get universityId() {
    return this.editProfileForm.get('universityId');
  }

  get countryId() {
    return this.editProfileForm.get('countryId');
  }

  get profileImg() {
    return this.editProfileForm.get('profileImg');
  }

  get coverFoodImage() {
    return this.editProfileForm.get('coverFoodImage');
  }

  get availableDays() {
    return this.editProfileForm.get('availableDays');
  }

  get aboutMe() {
    return this.editProfileForm.get('aboutMe');
  }

  get languages() {
    return this.editProfileForm.get('languages');
  }

  get twitterUsername() {
    return this.editProfileForm.get('socialmedia.twitterUsername');
  }

  get instagramUsername() {
    return this.editProfileForm.get('socialmedia.instagramUsername');
  }

  get foodIds() {
    return this.editProfileForm.get('foodIds');
  }

  updateProfile() {
    console.log(this.editProfileForm.value);
  }
}
