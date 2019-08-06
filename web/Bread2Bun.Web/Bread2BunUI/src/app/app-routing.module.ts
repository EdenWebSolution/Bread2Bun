import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: 'authorize',
    loadChildren: './modules/authorize/authorize.module#AuthorizeModule'
  },
  {
    path: '',
    redirectTo: 'authorize',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
