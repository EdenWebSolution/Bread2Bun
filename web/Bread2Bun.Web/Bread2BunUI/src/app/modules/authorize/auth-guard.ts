import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import { BaseService } from '../core/services/base.service';
@Injectable({
  providedIn: 'root'
})
export class AuthGuard extends BaseService implements CanActivate {
  constructor(
    private router: Router,
  ) {
    super();
  }
  canActivate(route: ActivatedRouteSnapshot): Observable<boolean> | boolean {
    if (
      (localStorage.getItem('bread2bun-TokenId') === null || localStorage.getItem('bread2bun-TokenId') === undefined) ||
      (sessionStorage.getItem('bread2bun-TokenId') === null || sessionStorage.getItem('bread2bun-TokenId') === undefined)
    ) {
      localStorage.removeItem('bread2bun-TokenId');
      sessionStorage.removeItem('bread2bun-TokenId');
      console.log('error');

      this.router.navigate(['/authorize']);
      return false;
    } else {
      return true;
    }
  }
}
