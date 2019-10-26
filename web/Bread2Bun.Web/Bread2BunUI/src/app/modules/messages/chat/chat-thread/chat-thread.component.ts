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
  constructor(private chatService: LayoutService, private ngZone: NgZone) {
    this.isSending = false;
    this.subscribeToEvents();
    this.chatThread = new Array<MessageModel>();
    this.message = null;
  }

  ngOnInit() { }

  ngOnDestroy() {
    if (this.chatService.messageReceived) {
      this.subscription.unsubscribe();
      // this.chatService.stopConnection();
    }
  }

  sendMessage() {
    this.isSending = true;

    const messageModel = new MessageModel();
    if (this.message !== null) {
      messageModel.text = this.message;
      messageModel.toId = this.toId;
      this.chatService.sendMessage(messageModel);
    }
  }

  private subscribeToEvents(): void {
    this.isSending = false;
    this.subscription = this.chatService.messageReceived.subscribe(
      (message: MessageModel) => {
        this.ngZone.run(() => {
          this.message = null;
          console.log(message);
          this.chatThread.push(message);
        });
      }
    );

    // this.subscription = this.chatService.userConnected.subscribe(
    //   (userConnection: UserConnectionModel) => {
    //     this.ngZone.run(() => {
    //       console.log(userConnection);
    //     });
    //   }
    // );
  }
}
