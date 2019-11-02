import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MessagesRoutingModule } from './messages-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ChatComponent } from './chat/chat.component';
import { TimeAgoPipe } from 'time-ago-pipe';
import { ChatThreadComponent } from './chat/chat-thread/chat-thread.component';

@NgModule({
  declarations: [ChatComponent, ChatThreadComponent],
  imports: [
    CommonModule,
    MessagesRoutingModule,
    SharedModule
  ]
})
export class MessagesModule { }
