import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CouponsRoutingModule } from './coupons-routing.module';
import { CouponFeedComponent } from './coupon-feed/coupon-feed.component';

@NgModule({
  declarations: [CouponFeedComponent],
  imports: [
    CommonModule,
    CouponsRoutingModule
  ]
})
export class CouponsModule { }
