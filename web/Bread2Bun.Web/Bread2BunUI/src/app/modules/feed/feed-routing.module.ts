import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ExploreFeedComponent } from './explore-feed/explore-feed.component';

const routes: Routes = [
  {
    path: '',
    component: ExploreFeedComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FeedRoutingModule { }
