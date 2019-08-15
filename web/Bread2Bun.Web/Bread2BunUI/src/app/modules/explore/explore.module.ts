import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExploreRoutingModule } from './explore-routing.module';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ExploreRoutingModule,
    SharedModule
  ]
})
export class ExploreModule { }
