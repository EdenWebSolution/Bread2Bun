import { Component, OnInit } from '@angular/core';
import { slideFromUp, slideFromRight, slideFromLeft } from 'src/app/animations';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LoginUserModel } from '../../models/login-user-model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  animations: [slideFromUp, slideFromRight, slideFromLeft]
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  loginUserModel: LoginUserModel;

  constructor(
    private fb: FormBuilder,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      userName: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
    this.toastr.success('Hello world!', 'Toastr fun!');
  }

  get userName() {
    return this.loginForm.get('userName');
  }
  get password() {
    return this.loginForm.get('password');
  }

  loginUser(){
    console.log(this.loginForm.value);
  }

}
