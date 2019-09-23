import { FoodComponent } from './food/food.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FeedRoutingModule } from './feed-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ExploreFeedComponent } from './explore-feed/explore-feed.component';

@NgModule({
  declarations: [ExploreFeedComponent, FoodComponent],
  imports: [
    CommonModule,
    FeedRoutingModule,
    SharedModule
  ]
})
export class FeedModule { }
