import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class UserResolverService {
  public userId: number;
  private decoder = new JwtHelperService();
  private decodedToken: any;

  constructor(
    private router: Router,
    private toastr: ToastrService
  ) {
    this.setUserId();
  }

  private setUserId() {
    const token =
      localStorage.getItem('bread2bun-TokenId') !== null
        ? localStorage.getItem('bread2bun-TokenId')
        : sessionStorage.getItem('bread2bun-TokenId');
    if (token !== null) {
      this.decodedToken = this.decoder.decodeToken(token);
      this.userId = +this.decodedToken.sub;
    } else {
      this.toastr.error('Your session has expired, please sign in again', 'Oops!!')
      this.router.navigate(['/authorize']);
    }
  }
}
