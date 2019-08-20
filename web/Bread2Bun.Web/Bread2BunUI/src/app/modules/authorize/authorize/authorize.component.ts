import { Component, OnInit } from '@angular/core';
import { slideFromBottom, slideFromUp, slideFromLeft, slideFromRight } from 'src/app/animations';

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

  constructor() { }

  ngOnInit() {
    this.login = true;
    this.register = false;
    this.forgotPassword = false;
  }

  showLogin() {
    this.login = true;
    this.register = false;
    this.forgotPassword = false;
  }
  showRegister() {
    this.login = false;
    this.register = true;
    this.forgotPassword = false;
  }

  showForgotPassword() {
    this.login = false;
    this.register = false;
    this.forgotPassword = true;
  }
}

