import { Injectable } from '@angular/core';
import { BaseService } from '../../core/services/base.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class NavbarService extends BaseService {
  constructor(private http: HttpClient) {
    super();
  }

  getAllUnreadMessageCOunt() {
    return this.http
      .get<number>(
        `${this.baseEndPoint}/api/chat/allunreadcount`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }
}
