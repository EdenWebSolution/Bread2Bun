import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FeedRoutingModule } from './feed-routing.module';
import { SharedModule } from '../shared/shared.module';
import { FoodComponent } from './food/food.component';

@NgModule({
  declarations: [FoodComponent],
  imports: [
    CommonModule,
    FeedRoutingModule,
    SharedModule
  ]
})
export class FeedModule { }
