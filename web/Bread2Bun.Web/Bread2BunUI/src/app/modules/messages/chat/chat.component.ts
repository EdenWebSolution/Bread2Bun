import { Component, OnInit, NgZone, OnDestroy } from '@angular/core';
import { ChatService } from '../service/chat.service';
import { MessageModel } from '../Models/MessageModel';
import { Subscription } from 'rxjs/Rx';
import { UserConnectionModel } from '../Models/UserConnectionModel';
import { slideFromLeft } from 'src/app/animations';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
  animations: [slideFromLeft]

})
export class ChatComponent implements OnInit, OnDestroy {
  subscription: Subscription;
  uniqueID: string = new Date().getTime().toString();
  message: MessageModel;
  txtMessage: string;
  profileImgUrl =
    'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTZtt-8JagsbCAhDa02YU8dEhABmIUIUaMEyq__-O6eEBo20DIwvw';

  chatDate = new Date();
  showThread = false;
  constructor(private chatService: ChatService, private ngZone: NgZone) {
    this.message = new MessageModel();
    this.subscribeToEvents();
  }

  ngOnInit() {}

  ngOnDestroy() {
    if (this.chatService.messageReceived) {
      this.subscription.unsubscribe();
      // this.chatService.stopConnection();
    }
  }

  sendMessage(): void {
    if (this.txtMessage) {
      this.message = new MessageModel();
      this.message.clientUniqueId = this.uniqueID;
      this.message.text = this.txtMessage;

      this.chatService.sendMessage(this.message);
    }
  }

  private subscribeToEvents(): void {
    this.subscription = this.chatService.messageReceived.subscribe(
      (message: MessageModel) => {
        this.ngZone.run(() => {
          console.log(message);
        });
      }
    );

    this.subscription = this.chatService.userConnected.subscribe(
      (userConnection: UserConnectionModel) => {
        this.ngZone.run(() => {
          console.log(userConnection);
        });
      }
    );
  }

  showThisThread(){
    this.showThread = true;
  }

  showList(){
    this.showThread = false;
  }
}
