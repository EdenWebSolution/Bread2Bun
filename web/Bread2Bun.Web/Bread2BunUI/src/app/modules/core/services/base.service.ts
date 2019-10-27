import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { throwError } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';



@Injectable({
  providedIn: 'root'
})
export abstract class BaseService {
  errorMessage: { status: any; message: string };
  protected baseEndPoint = environment.baseEndPoint;
  protected httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  decoder = new JwtHelperService();
  token: string;
  userName: string;
  userId: number;
  decodedToken: any;
  constructor(
  ) {
    if (localStorage.getItem('bread2bun-TokenId') !== null) {
      this.token = localStorage.getItem('bread2bun-TokenId');
    } else if (sessionStorage.getItem('bread2bun-TokenId') !== null) {
      this.token = sessionStorage.getItem('bread2bun-TokenId');
    }
  }

  server4xxError(error: Response | any) {
    let isLogin = false;

    if (error.url !== undefined && error.url !== null) {
      isLogin = error.url.includes('api/security/login');
    }

    if (error.status === 0) {
      this.errorMessage = {
        message: 'Please check your internet connection',
        status: error.status
      };
    } else if (error.status === 401 && isLogin) {
      localStorage.removeItem('bread2bun-TokenId');
      sessionStorage.removeItem('bread2bun-TokenId');
      this.errorMessage = {
        message: 'Invalid username or password',
        status: error.status
      };
    } else if (
      error.status === 401 ||
      error.status === 408
    ) {
      localStorage.removeItem('bread2bun-TokenId');
      sessionStorage.removeItem('bread2bun-TokenId');
      this.errorMessage = {
        message: 'Your login time has been expired, login again',
        status: error.status
      };
    } else {
      this.errorMessage = {
        message: error.error,
        status: error.status
      };
    }
    const errorMsg = Object.assign({}, this.errorMessage);
    return throwError(errorMsg);
  }

  getUserName() {
    this.decodedToken = this.decoder.decodeToken(this.token);
    this.userName = this.decodedToken.unique_name;
    return this.userName;
  }

  getUserId() {
    this.decodedToken = this.decoder.decodeToken(this.token);
    this.userId = this.decodedToken.sub;
    return this.userId;
  }
}
