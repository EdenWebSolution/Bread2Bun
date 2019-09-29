import { Injectable, EventEmitter } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { BaseService } from '../../core/services/base.service';
import { MessageModel } from '../Models/MessageModel';

@Injectable({
  providedIn: 'root'
})
export class ChatService extends BaseService {
  messageReceived: EventEmitter<MessageModel>;
  connectionEstablished = new EventEmitter<boolean>();

  private connectionIsEstablished = false;
  private hubConnection: HubConnection;

  constructor() {
    super();
    this.messageReceived = new EventEmitter<MessageModel>();

    this.createConnection();
    this.registerOnServerEvents();
    this.startConnection();
  }

  private createConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.baseEndPoint}/chat`,
      {
        accessTokenFactory: () =>'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyIiwianRpIjoiMDAxZTMyY2MtM2Q2ZC00NzI4LWE2NjAtNGZiYmQ0YjE0YTMxIiwidW5pcXVlX25hbWUiOiJiaXJpenphIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRmFsc2UiLCJyZW1lbWJlck1lIjoiRmFsc2UiLCJleHAiOjE2MDEzOTYwMTEsImlzcyI6IkJyZWFkMkJ1biIsImF1ZCI6InVzZXJzIn0.Vm20LGHRnEOHcPtgmE4gw63PdVNnjM8N9AsAQcE5L7w'
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
        }, 1000);
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
  }
}
