import { Injectable, EventEmitter } from '@angular/core';
import { MessageModel } from '../messages/Models/MessageModel';
import { UserConnectionModel } from '../messages/Models/UserConnectionModel';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { BaseService } from '../core/services/base.service';

@Injectable({
  providedIn: 'root'
})
export class LayoutService extends BaseService {

  messageReceived: EventEmitter<MessageModel>;
  userConnected: EventEmitter<UserConnectionModel>;
  connectionEstablished = new EventEmitter<boolean>();

  private connectionIsEstablished = false;
  private hubConnection: HubConnection;

  constructor() {
    super();
    this.messageReceived = new EventEmitter<MessageModel>();
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
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.baseEndPoint}/chat`, {
        accessTokenFactory: () => authToken
      })
      .build();
  }

  private startConnection(): void {
    this.hubConnection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        this.connectionEstablished.emit(this.connectionIsEstablished);
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

  private registerOnServerEvents(): void {
    this.hubConnection.on('ReceiveMessage', (data: MessageModel) => {
      this.messageReceived.emit(data);
    });

    this.hubConnection.on('UserConnected', (data: UserConnectionModel) => {
      this.userConnected.emit(data);
    });
  }
}