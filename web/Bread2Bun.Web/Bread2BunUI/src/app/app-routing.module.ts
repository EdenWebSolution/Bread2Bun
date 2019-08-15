import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './modules/layout/layout/layout.component';
import { AuthorizeComponent } from './modules/authorize/authorize/authorize.component';

const routes: Routes = [
  {
    path: '',
    component: AuthorizeComponent
  },
  {
    path: 'app',
    component: LayoutComponent,
    children: [
      {
        path: 'timeline',
        loadChildren: './modules/timeline/timeline.module#TimelineModule'
      },
      {
        path: 'profile',
        loadChildren: './modules/profile/profile.module#ProfileModule'
      },
      {
        path: 'explore',
        loadChildren: './modules/explore/explore.module#ExploreModule'
      },
      {
        path: 'messages',
        loadChildren: './modules/messages/messages.module#MessagesModule'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
