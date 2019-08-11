import { Injectable } from '@angular/core';
import { BaseService } from '../../core/services/base.service';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { RegisterUserModel } from '../models/register-user-model';

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
}
