import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MyProfileComponent } from './my-profile/my-profile.component';
import { ProfileReviewsComponent } from './profile-reviews/profile-reviews.component';

const routes: Routes = [
  {
    path: '',
    component: MyProfileComponent
  },
  {
    path: 'reviews',
    component: ProfileReviewsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfileRoutingModule { }
