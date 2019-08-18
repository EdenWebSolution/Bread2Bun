import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { slideFromUp, slideFromRight, slideFromLeft } from 'src/app/animations';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LoginUserModel } from '../../models/login-user-model';
import { AuthorizeService } from '../../services/authorize.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { decode } from '@angular/router/src/url_tree';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  animations: [slideFromUp, slideFromRight, slideFromLeft]
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loginUserModel: LoginUserModel;
  @Output() forgotPassword = new EventEmitter<boolean>();

  decoder = new JwtHelperService();

  constructor(
    private fb: FormBuilder,
    private authorizeService: AuthorizeService
  ) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]],
      rememberMe: [false]
    });
  }

  get userName() {
    return this.loginForm.get('userName');
  }
  get password() {
    return this.loginForm.get('password');
  }

  loginUser() {
    this.loginUserModel = Object.assign({}, this.loginUserModel, this.loginForm.value);
    this.authorizeService.loginUser(this.loginUserModel).subscribe(result => {
      console.log(result);
      const decodedToken = this.decoder.decodeToken(result.token);
      if (decodedToken.rememberMe === 'True') {
        localStorage.setItem('bread2bun-TokenId', result.token);
        sessionStorage.removeItem('bread2bun-TokenId');
      } else {
        sessionStorage.setItem('bread2bun-TokenId', result.token);
        localStorage.removeItem('bread2bun-TokenId');
      }
    }, error => {
      console.log('err', error);
    });
  }

  showForgotPassword() {
    this.forgotPassword.emit(true);
  }

}
