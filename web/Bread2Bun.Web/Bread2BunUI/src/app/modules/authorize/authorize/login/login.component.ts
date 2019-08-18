import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { slideFromUp, slideFromRight, slideFromLeft } from 'src/app/animations';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LoginUserModel } from '../../models/login-user-model';
import { AuthorizeService } from '../../services/authorize.service';
import { JwtHelperService } from '@auth0/angular-jwt';
import { decode } from '@angular/router/src/url_tree';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

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
  loading = false;

  constructor(
    private fb: FormBuilder,
    private authorizeService: AuthorizeService,
    private router: Router,
    private toastr: ToastrService
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
    this.loading = true;
    this.loginUserModel = Object.assign({}, this.loginUserModel, this.loginForm.value);
    this.authorizeService.loginUser(this.loginUserModel).subscribe(result => {
      const decodedToken = this.decoder.decodeToken(result.token);
      if (decodedToken.rememberMe === 'True') {
        localStorage.setItem('bread2bun-TokenId', result.token);
        sessionStorage.removeItem('bread2bun-TokenId');
      } else {
        sessionStorage.setItem('bread2bun-TokenId', result.token);
        localStorage.removeItem('bread2bun-TokenId');
      }
      this.loading = false;
      this.router.navigate(['/app/timeline']);
    }, error => {
      this.loading = false;
      this.toastr.error(error.message, 'Login Error', {
        progressBar: true
      });
    });
  }

  showForgotPassword() {
    this.forgotPassword.emit(true);
  }

}
