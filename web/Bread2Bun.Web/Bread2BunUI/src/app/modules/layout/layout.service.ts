import { Injectable, EventEmitter } from '@angular/core';
import { MessageModel } from '../messages/Models/MessageModel';
import { UserConnectionModel } from '../messages/Models/UserConnectionModel';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import { BaseService } from '../core/services/base.service';
import { ChatThread } from '../messages/Models/chat-thread';
import { UserConnection } from '../shared/models/UserConnection';
import { Subject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LayoutService extends BaseService {
  messageReceived: EventEmitter<ChatThread>;
  userConnections: Array<UserConnectionModel>;
  userConnected: EventEmitter<UserConnectionModel>;
  // connectionEstablished = new EventEmitter<boolean>();

  private unreadMessageCount$: Subject<number>;
  allUnreadMessageCount$: Observable<number>;

  private connectionIsEstablished = false;
  private hubConnection: HubConnection;

  constructor() {
    super();
    this.userConnections = new Array<UserConnectionModel>();
    this.messageReceived = new EventEmitter<ChatThread>();
    this.userConnected = new EventEmitter<UserConnectionModel>();

    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  private createConnection() {
    const authToken =
      localStorage.getItem('bread2bun-TokenId') === null
        ? sessionStorage.getItem('bread2bun-TokenId')
        : localStorage.getItem('bread2bun-TokenId');

    if (environment.production) {
      this.hubConnection = new HubConnectionBuilder()
        .withUrl(`${this.baseEndPoint}/chat`, {
          accessTokenFactory: () => authToken
        })
        .configureLogging(LogLevel.None)
        .build();
    } else {
      this.hubConnection = new HubConnectionBuilder()
        .withUrl(`${this.baseEndPoint}/chat`, {
          accessTokenFactory: () => authToken
        })
        .configureLogging(LogLevel.Information)
        .build();
    }
    this.hubConnection.serverTimeoutInMilliseconds = 960000;
    this.hubConnection.keepAliveIntervalInMilliseconds = 600000;
  }

  private startConnection(): void {
    this.hubConnection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        // this.connectionEstablished.emit(this.connectionIsEstablished);
      })
      .catch(err => {
        setTimeout(function() {
          this.startConnection();
        }, 2000);
      });
  }

  stopConnection(): void {
    this.hubConnection.stop().catch(err => {});
  }
  sendMessage(message: MessageModel) {
    this.hubConnection.invoke('SendMessage', message);
  }

  updateAllMessageCount() {
    this.hubConnection.invoke('GetMyGlobalMessageNotification');
  }

  private registerOnServerEvents(): void {
    this.unreadMessageCount$ = new Subject();
    this.allUnreadMessageCount$ = this.unreadMessageCount$.asObservable();

    this.hubConnection.on('ReceiveMessage', (data: ChatThread) => {
      this.messageReceived.emit(data);
    });

    this.hubConnection.on(
      'newMessageNotification',
      (unreadMessageCount: number) => {
        this.unreadMessageCount$.next(unreadMessageCount);
      }
    );

    this.hubConnection.on(
      'UserConnected',
      (data: UserConnectionModel, connectUsers: UserConnectionModel[]) => {
        this.userConnected.emit(data);
        this.userConnections = new Array<UserConnectionModel>();
        this.userConnections = connectUsers;
      }
    );

    this.hubConnection.on(
      'UserDisconntected',
      (data: UserConnectionModel, connectUsers: UserConnectionModel[]) => {
        this.userConnected.emit(data);
        this.userConnections = new Array<UserConnectionModel>();
        this.userConnections = connectUsers;
      }
    );
  }
}
