import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './modules/layout/layout/layout.component';
import { AuthorizeComponent } from './modules/authorize/authorize/authorize.component';
import { AuthGuard } from './modules/authorize/auth-guard';
import { MyTimelineComponent } from './modules/timeline/my-timeline/my-timeline.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'app/timeline',
    pathMatch: 'full'
  },
  {
    path: 'authorize',
    component: AuthorizeComponent
  },
  {
    path: ':isReset/:token/:email',
    component: AuthorizeComponent
  },
  {
    path: 'app',
    component: LayoutComponent,
    children: [
      {
        path: 'timeline',
        loadChildren: './modules/timeline/timeline.module#TimelineModule',
        canActivate: [AuthGuard]
      },
      {
        path: 'profile',
        loadChildren: './modules/profile/profile.module#ProfileModule',
        canActivate: [AuthGuard]
      },
      {
        path: 'explore',
        loadChildren: './modules/explore/explore.module#ExploreModule',
        canActivate: [AuthGuard]
      },
      {
        path: 'messages',
        loadChildren: './modules/messages/messages.module#MessagesModule',
        canActivate: [AuthGuard]
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
