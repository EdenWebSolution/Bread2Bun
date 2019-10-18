import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CouponFeedComponent } from './coupon-feed/coupon-feed.component';

const routes: Routes = [
  {
    path: '',
    component: CouponFeedComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CouponsRoutingModule { }
