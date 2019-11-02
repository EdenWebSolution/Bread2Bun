import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '../../core/services/base.service';
import { Users } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getUsers(searchTerm: string) {
    return this.http
      .get<Users[]>(
        `${this.baseEndPoint}/api/shared/users?searchTerm=${searchTerm}`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }
}
