import { Component, OnInit, ElementRef } from '@angular/core';
import { MessageModel } from '../Models/MessageModel';
import { Subscription, Subject } from 'rxjs/Rx';
import { slideFromLeft } from 'src/app/animations';
import { ChatListModel } from '../Models/chat-list-model';
import { ChatService } from '../service/chat.service';
import { TypeaheadMatch } from 'ngx-bootstrap';
import { SharedService } from '../../shared/services/shared.service';
import { Users } from '../../shared/models/users';
import { UserService } from '../../shared/services/user.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
  animations: [slideFromLeft]
})
export class ChatComponent implements OnInit {
  subscription: Subscription;
  uniqueID: string = new Date().getTime().toString();
  message: MessageModel;
  txtMessage: string;
  chatDate = new Date();
  showThread = false;
  userData = {
    userId: 0,
    userName: ''
  };
  chats: Array<ChatListModel>;
  users: Array<Users>;
  userSearchTerm = '';
  userSearch = new Subject<string>();
  userSelected: string;
  isNewMessage = false;
  nullImagePath = '../../../../assets/images/default.jpg';

  constructor(
    private chatService: ChatService,
    private userService: UserService,
    private eleRef: ElementRef
  ) {
    this.message = new MessageModel();
    this.chats = new Array<ChatListModel>();
    this.users = new Array<Users>();
    this.userSearch.debounceTime(300)
      .distinctUntilChanged().subscribe(data => {
        if (data !== '') {
          this.userSearchTerm = data;
          this.getUsers();
        } else {
          this.users = [];
        }
      });
  }

  ngOnInit() {
    this.getMyChats();
  }

  showThisThread(id: number, userName: string) {
    this.userData.userId = id;
    this.userData.userName = userName;
    this.showThread = true;
  }

  showList() {
    this.getMyChats();
    this.showThread = false;
  }

  getMyChats() {
    this.chatService.getMyChats().subscribe(result => {
      this.chats = result.details;
      console.log(this.chats);
    });
  }

  getUsers() {
    this.userService.getUsers(this.userSearchTerm).subscribe(result => {
      this.users = result;
      const input = this.eleRef.nativeElement.querySelector('#searchUsers');
      input.dispatchEvent(new Event('click'));
    });
  }

  onUserSelect(event: TypeaheadMatch) {
    this.showThisThread(event.item.id, event.item.userName);
    this.userSelected = '';
  }

  toggleNewMessage() {
    this.isNewMessage = !this.isNewMessage;
  }
}
