import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MyProfileComponent } from './my-profile/my-profile.component';
import { ProfileReviewsComponent } from './profile-reviews/profile-reviews.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { ViewProfileComponent } from './view-profile/view-profile.component';

const routes: Routes = [
  {
    path: '',
    component: MyProfileComponent
  },
  {
    path: 'reviews/:userId',
    component: ProfileReviewsComponent
  },
  {
    path: 'editProfile',
    component: EditProfileComponent
  },
  {
    path: 'view/:username',
    component: ViewProfileComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfileRoutingModule { }
