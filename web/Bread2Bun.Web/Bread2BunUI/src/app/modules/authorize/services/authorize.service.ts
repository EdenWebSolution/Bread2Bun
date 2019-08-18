import { Injectable } from '@angular/core';
import { BaseService } from '../../core/services/base.service';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { RegisterUserModel } from '../models/register-user-model';
import { LoginUserModel } from '../models/login-user-model';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  registerUser(registerUserModel: RegisterUserModel) {
    return this.http
      .post<RegisterUserModel>(
        `${this.baseEndPoint}/api/security/user/new`,
        registerUserModel,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  loginUser(userLoginModel: LoginUserModel) {
    return this.http
      .post<string>(
        `${this.baseEndPoint}/api/security/login`,
        userLoginModel,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  forgotPassword(emailAddress: string) {
    return this.http
      .post<string>(
        `${this.baseEndPoint}/api/security/forgotpassword`,
        emailAddress,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }
}
