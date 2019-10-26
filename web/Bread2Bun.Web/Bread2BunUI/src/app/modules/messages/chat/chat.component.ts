import { Component, OnInit } from '@angular/core';
import { MessageModel } from '../Models/MessageModel';
import { Subscription, Subject } from 'rxjs/Rx';
import { slideFromLeft } from 'src/app/animations';
import { ChatListModel } from '../Models/chat-list-model';
import { ChatService } from '../service/chat.service';
import { TypeaheadMatch } from 'ngx-bootstrap';

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
  profileImgUrl =
    'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTZtt-8JagsbCAhDa02YU8dEhABmIUIUaMEyq__-O6eEBo20DIwvw';

  chatDate = new Date();
  showThread = false;
  userId: number;
  chats: Array<ChatListModel>;
  users: string[] = [];
  userSearchTerm = '';
  userSearch = new Subject<string>();
  userSelected: string;
  isNewMessage = false;

  constructor(
    private chatService: ChatService
  ) {
    this.message = new MessageModel();
    this.chats = new Array<ChatListModel>();
    this.userSearch.debounceTime(300)
      .distinctUntilChanged().subscribe(data => {
        if (data !== '' && data.length > 2) {
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

  showThisThread(id: number) {
    this.userId = id;
    this.showThread = true;
  }

  showList() {
    this.getMyChats();
    this.showThread = false;
  }

  getMyChats() {
    this.chatService.getMyChats().subscribe(result => {
      this.chats = result.details;
    });
  }

  getUsers() {
    this.users = ['akmal', 'thanzeel', 'haajiyaar', 'buriyaani']
  }

  onUserSelect(event: TypeaheadMatch) {
    console.log(event);
  }

  toggleNewMessage() {
    this.isNewMessage = !this.isNewMessage;
  }
}
