import { Component, OnInit } from '@angular/core';
import { slideFromBottom, slideFromUp, slideFromLeft, slideFromRight } from 'src/app/animations';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.scss'],
  animations: [slideFromBottom, slideFromUp, slideFromLeft, slideFromRight]
})
export class AuthorizeComponent implements OnInit {
  register: boolean;
  login: boolean;
  forgotPassword: boolean;
  passwordReset: boolean;
  showResetPassword: boolean;

  constructor(
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.login = true;
    this.register = false;
    this.forgotPassword = false;
    this.activatedRoute.params.subscribe((params: Params) => {
      this.passwordReset = params.isReset === 'resetpassword' ? true : false;
      if (this.passwordReset) {
        this.showPasswordReset();
      }
    });
  }

  showLogin() {
    this.login = true;
    this.register = false;
    this.forgotPassword = false;
    this.showResetPassword = false;
  }
  showRegister() {
    this.login = false;
    this.register = true;
    this.forgotPassword = false;
    this.showResetPassword = false;
  }

  showForgotPassword() {
    this.login = false;
    this.register = false;
    this.forgotPassword = true;
    this.showResetPassword = false;
  }

  showPasswordReset() {
    this.showResetPassword = true;
    this.login = false;
    this.register = false;
    this.forgotPassword = false;
  }
}

