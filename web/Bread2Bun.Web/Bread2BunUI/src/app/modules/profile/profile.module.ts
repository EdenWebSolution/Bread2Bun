import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { SharedModule } from '../shared/shared.module';
import { MyProfileComponent } from './my-profile/my-profile.component';
import {TimeAgoPipe} from 'time-ago-pipe';
import { ProfileReviewsComponent } from './profile-reviews/profile-reviews.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { ViewProfileComponent } from './view-profile/view-profile.component';


@NgModule({
  declarations: [MyProfileComponent, ProfileReviewsComponent, EditProfileComponent, ViewProfileComponent],
  imports: [
    CommonModule,
    ProfileRoutingModule,
    SharedModule
  ]
})
export class ProfileModule { }
