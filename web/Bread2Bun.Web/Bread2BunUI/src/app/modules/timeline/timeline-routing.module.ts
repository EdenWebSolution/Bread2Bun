import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MyTimelineComponent } from './my-timeline/my-timeline.component';

const routes: Routes = [
  {
    path: '',
    component: MyTimelineComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TimelineRoutingModule { }
