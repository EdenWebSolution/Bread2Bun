import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorizeRoutingModule } from './authorize-routing.module';
import { AuthorizeComponent } from './authorize/authorize.component';
import { LoginComponent } from './authorize/login/login.component';
import { RegisterComponent } from './authorize/register/register.component';

@NgModule({
  declarations: [AuthorizeComponent, LoginComponent, RegisterComponent],
  imports: [
    CommonModule,
    AuthorizeRoutingModule
  ]
})
export class AuthorizeModule { }
