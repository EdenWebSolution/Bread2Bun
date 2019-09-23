import { Component, OnInit } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  connection: HubConnection;
  msgs: Array<string>;

  constructor() {
    this.msgs = new Array<string>();
  }

  ngOnInit() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(environment.signalREndPoint)
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.connection
      .start()
      .then(connection => this.connection.invoke('SendMessage', 'hello'));
  }
}
