import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { slideFromUp, slideFromRight, slideFromLeft } from 'src/app/animations';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LoginUserModel } from '../../models/login-user-model';
import { AuthorizeService } from '../../services/authorize.service';

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
    }, error => {
      console.log('err', error);
    });
  }

  showForgotPassword() {
    this.forgotPassword.emit(true);
  }

}
