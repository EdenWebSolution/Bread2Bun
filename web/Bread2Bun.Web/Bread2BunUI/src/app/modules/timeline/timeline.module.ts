import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TimelineRoutingModule } from './timeline-routing.module';
import { MyTimelineComponent } from './my-timeline/my-timeline.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [MyTimelineComponent],
  imports: [
    CommonModule,
    TimelineRoutingModule,
    SharedModule
  ]
})
export class TimelineModule { }
