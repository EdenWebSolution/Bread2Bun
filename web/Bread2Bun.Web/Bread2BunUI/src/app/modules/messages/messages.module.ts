import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MessagesRoutingModule } from './messages-routing.module';
import { SharedModule } from '../shared/shared.module';
import { ChatComponent } from './chat/chat.component';

@NgModule({
  declarations: [ChatComponent],
  imports: [
    CommonModule,
    MessagesRoutingModule,
    SharedModule
  ]
})
export class MessagesModule { }
