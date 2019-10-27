import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  Input,
  NgZone,
  OnDestroy
} from '@angular/core';
import { slideFromLeft } from 'src/app/animations';
import { MessageModel } from '../../Models/MessageModel';
import { LayoutService } from 'src/app/modules/layout/layout.service';
import { Subscription } from 'rxjs';
import { ChatService } from '../../service/chat.service';
import { ChatThread } from '../../Models/chat-thread';
import { BaseService } from 'src/app/modules/core/services/base.service';
import { MessageStatus } from 'src/app/modules/core/enums/MessageStatus';

@Component({
  selector: 'app-chat-thread',
  templateUrl: './chat-thread.component.html',
  styleUrls: ['./chat-thread.component.scss'],
  animations: [slideFromLeft]
})
export class ChatThreadComponent implements OnInit, OnDestroy {
  @Output() showList = new EventEmitter();
  @Input() toId: number;
  chatThread: Array<MessageModel>;

  subscription: Subscription;
  message: string;
  isSending: boolean;
  chatMessages: Array<ChatThread>;
  myUserId: number;
  constructor(
    private layoutService: LayoutService,
    private ngZone: NgZone,
    private chatService: ChatService,
    private baseService: BaseService
  ) {
    this.isSending = false;
    this.subscribeToEvents();
    this.chatThread = new Array<MessageModel>();
    this.chatMessages = Array<ChatThread>();
    this.message = null;
  }

  ngOnInit() {
    this.toggleMessageReadStatus();
    this.getThread(this.toId);
    this.myUserId = +this.baseService.getUserId();
  }

  ngOnDestroy() {
    if (this.layoutService.messageReceived) {
      this.subscription.unsubscribe();
    }
  }

  sendMessage() {
    this.isSending = true;

    const messageModel = new MessageModel();
    if (this.message !== null) {
      messageModel.text = this.message;
      messageModel.toId = this.toId;
      this.layoutService.sendMessage(messageModel);
    }
  }

  private subscribeToEvents(): void {
    this.isSending = false;
    this.subscription = this.layoutService.messageReceived.subscribe(
      (message: ChatThread) => {
        this.ngZone.run(() => {
          this.message = null;
          this.chatMessages.push(message);
        });
      }
    );
  }

  getThread(id: number) {
    this.chatService.getMyThread(id).subscribe(result => {
      this.chatMessages = result.details;
    });
  }

  toggleMessageReadStatus() {
    this.chatService
      .toggleMessageReadStatus(this.toId, MessageStatus.read)
      .subscribe(() => {}, error => {});
  }
}
