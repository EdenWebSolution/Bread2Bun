import { Injectable } from '@angular/core';
import { BaseService } from '../../core/services/base.service';
import { HttpClient } from '@angular/common/http';
import { ChatListModel } from '../Models/chat-list-model';
import { PaginationModel } from '../../shared/models/pagination-model';
import { ChatThread } from '../Models/chat-thread';


@Injectable({
  providedIn: 'root'
})
export class ChatService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getMyChats() {
    return this.http
      .get<PaginationModel<ChatListModel>>(
        `${this.baseEndPoint}/api/chat/list`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  getMyThread(toId: number) {
    return this.http
      .get<PaginationModel<ChatThread>>(
        `${this.baseEndPoint}/api/chat?to=${toId}`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }


}
