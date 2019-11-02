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
import { ToastrService } from 'ngx-toastr';
import { SubSink } from 'subsink';

@Component({
  selector: 'app-chat-thread',
  templateUrl: './chat-thread.component.html',
  styleUrls: ['./chat-thread.component.scss'],
  animations: [slideFromLeft]
})
export class ChatThreadComponent implements OnInit, OnDestroy {
  @Output() showList = new EventEmitter();
  @Input() userData: any;
  chatThread: Array<MessageModel>;
  message: string;
  isSending: boolean;
  chatMessages: Array<ChatThread>;
  myUserId: number;
  userName: string;
  isBlocked = false;
  connectionId: string;
  myUserName: string;
  subsink: SubSink;

  constructor(
    private layoutService: LayoutService,
    private ngZone: NgZone,
    private chatService: ChatService,
    private baseService: BaseService,
    private toastr: ToastrService
  ) {
    this.subsink = new SubSink();
    this.isSending = false;
    this.subscribeToEvents();
    this.chatThread = new Array<MessageModel>();
    this.chatMessages = Array<ChatThread>();
    this.message = null;
  }

  ngOnInit() {
    this.toggleMessageReadStatus();
    this.getThread(this.userData.userId);
    this.userName = this.userData.userName;
    this.myUserId = +this.baseService.getUserId();
    this.connectionId = this.userData.connectionId;
    this.myUserName = this.baseService.getUserName();
  }

  ngOnDestroy() {
    this.subsink.unsubscribe();
  }

  sendMessage() {
    this.isSending = true;

    const messageModel = new MessageModel();
    if (this.message !== null) {
      // push to array
      const myMessage = new ChatThread();
      myMessage.message = this.message;
      myMessage.date = new Date();
      myMessage.fromUserName = this.myUserName;
      myMessage.fromId = this.myUserId;
      myMessage.toId = this.userData.userId;
      this.chatMessages.push(myMessage);
      // sending message
      messageModel.text = this.message;
      messageModel.toId = this.userData.userId;
      const user = this.layoutService.userConnections.find(
        a => a.userName === this.userData.userName
      );
      messageModel.clientUniqueId =
        user === undefined || user === null ? null : user.connectionId;
      this.layoutService.sendMessage(messageModel);
      this.message = null;
    }
  }

  private subscribeToEvents(): void {
    this.isSending = false;
    this.subsink.sink = this.layoutService.messageReceived.subscribe(
      (message: ChatThread) => {
        this.ngZone.run(() => {
          if (message.fromId !== this.myUserId) {
            this.chatMessages.push(message);
          }
        });
      }
    );
  }

  getThread(id: number) {
    this.isBlocked = true;
    this.chatService.getMyThread(id).subscribe(
      result => {
        this.chatMessages = result.details;
        this.isBlocked = false;
      },
      error => {
        this.isBlocked = false;
        this.toastr.error('Failed to retrieve messages', 'Error');
      }
    );
  }

  toggleMessageReadStatus() {
    this.chatService
      .toggleMessageReadStatus(this.userData.userId, MessageStatus.read)
      .subscribe(
        () => {
          const user = this.layoutService.userConnections.find(
            a => a.userName === this.userData.userName
          );
          const clientUniqueId =
            user === undefined || user === null ? null : user.connectionId;
          this.layoutService.updateAllMessageCount();
        },
        error => {}
      );
  }

  goBack() {
    this.chatService
      .toggleMessageReadStatus(this.userData.userId, MessageStatus.read)
      .subscribe(
        () => {
          this.showList.emit(true);
        },
        error => {}
      );
  }
}
