import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpHeaders
} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptor implements HttpInterceptor {
  constructor() {}
  intercept(req: HttpRequest<any>, next: HttpHandler) {
    // tslint:disable-next-line: max-line-length
    const authToken = localStorage.getItem('bread2bun-TokenId') === null ? sessionStorage.getItem('bread2bun-TokenId') : localStorage.getItem('bread2bun-TokenId');
    const headers = new HttpHeaders({
      Authorization: 'Bearer ' + authToken
    });
    const cloneRequest = req.clone({ headers });
    return next.handle(cloneRequest);
  }
}
