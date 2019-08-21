import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorizeRoutingModule } from './authorize-routing.module';
import { AuthorizeComponent } from './authorize/authorize.component';
import { LoginComponent } from './authorize/login/login.component';
import { RegisterComponent } from './authorize/register/register.component';
import { SharedModule } from '../shared/shared.module';
import { ForgotPasswordComponent } from './authorize/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './authorize/reset-password/reset-password.component';

@NgModule({
  declarations: [AuthorizeComponent, LoginComponent, RegisterComponent, ForgotPasswordComponent, ResetPasswordComponent],
  imports: [
    CommonModule,
    AuthorizeRoutingModule,
    SharedModule
  ]
})
export class AuthorizeModule { }
