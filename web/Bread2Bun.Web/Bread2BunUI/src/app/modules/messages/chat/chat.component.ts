import {
  Component,
  OnInit,
  ElementRef,
  NgZone,
  OnDestroy
} from '@angular/core';
import { MessageModel } from '../Models/MessageModel';
import { Subscription, Subject } from 'rxjs/Rx';
import { slideFromLeft } from 'src/app/animations';
import { ChatListModel } from '../Models/chat-list-model';
import { ChatService } from '../service/chat.service';
import { TypeaheadMatch } from 'ngx-bootstrap';
import { SharedService } from '../../shared/services/shared.service';
import { Users } from '../../shared/models/users';
import { UserService } from '../../shared/services/user.service';
import { ToastrService } from 'ngx-toastr';
import { UserConnectionModel } from '../Models/UserConnectionModel';
import { LayoutService } from '../../layout/layout.service';

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
  chatDate = new Date();
  showThread = false;
  userData = {
    userId: 0,
    userName: '',
    connectionId: ''
  };
  chats: Array<ChatListModel>;
  users: Array<Users>;
  userSearchTerm = '';
  userSearch = new Subject<string>();
  userSelected: string;
  isNewMessage = false;
  nullImagePath = '../../../../assets/images/default.jpg';
  isBlocked = false;

  constructor(
    private layoutService: LayoutService,
    private ngZone: NgZone,
    private chatService: ChatService,
    private userService: UserService,
    private eleRef: ElementRef,
    private toastr: ToastrService
  ) {
    this.message = new MessageModel();
    this.chats = new Array<ChatListModel>();
    this.users = new Array<Users>();
    this.subscribeToEvents();
    this.userSearch
      .debounceTime(300)
      .distinctUntilChanged()
      .subscribe(data => {
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

  ngOnDestroy() {
    if (this.layoutService.userConnected) {
      this.subscription.unsubscribe();
    }
  }
  showThisThread(id: number, userName: string, connectionId: string) {
    this.userData.userId = id;
    this.userData.userName = userName;
    this.userData.connectionId = connectionId;
    this.showThread = true;
  }

  showList() {
    this.getMyChats();
    this.showThread = false;
  }

  getMyChats() {
    this.isBlocked = true;
    this.chatService.getMyChats().subscribe(result => {
      this.chats = result.details;
      this.isBlocked = false;
    }, error=>{
      this.isBlocked = false;
      this.toastr.error('Couldn\'t load your chats', 'Error');
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
    this.showThisThread(
      event.item.id,
      event.item.userName,
      event.item.connectionId
    );
    this.userSelected = '';
  }

  toggleNewMessage() {
    this.isNewMessage = !this.isNewMessage;
  }

  private subscribeToEvents(): void {
    this.subscription = this.layoutService.userConnected.subscribe(
      (user: UserConnectionModel) => {
        this.ngZone.run(() => {
          const isUserAvailable = this.chats.some(
            x => x.userName === user.userName
          );

          if (isUserAvailable) {
            const newuser = this.chats.find(x => x.userName === user.userName);
            newuser.isOnline = user.isOnline;
            newuser.connectionId = user.connectionId;
          }
        });
      }
    );
  }
}
