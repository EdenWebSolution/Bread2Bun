import { Component, OnInit, OnDestroy } from '@angular/core';
import { slideFromUp } from 'src/app/animations';
import { MenuItem } from '../models/menu-item';
import { ToastrService } from 'ngx-toastr';
import { AuthorizeService } from '../../authorize/services/authorize.service';
import { Router } from '@angular/router';
import { LayoutService } from '../layout.service';
import { NavbarService } from './navbar.service';
import { SubSink } from 'subsink';
import { BaseService } from '../../core/services/base.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  animations: [slideFromUp]
})
export class NavbarComponent implements OnInit, OnDestroy {
  show: boolean;
  showDropdown: boolean;
  menuItem: MenuItem[];
  rate = 5;
  max = 5;
  isReadonly = false;
  allUnreadMessageCount: number;
  subsink: SubSink;
  userName: string;

  constructor(
    private t: ToastrService,
    private authorizeService: AuthorizeService,
    private router: Router,
    private layoutService: LayoutService,
    private navbarService: NavbarService,
    private baseService: BaseService
  ) {
    this.subsink = new SubSink();
    this.show = false;
    this.showDropdown = false;
    this.menuItem = new Array<MenuItem>();
  }

  ngOnInit() {
    this.loadMenu();
    this.getAllUnreadMessageCount();
    this.initSubjects();
    this.getUserName();
  }

  ngOnDestroy() {
    this.subsink.unsubscribe();
  }

  loadMenu() {
    this.menuItem = [
      {
        path: '/app/feed',
        title: 'Explore',
        icon: 'fa-house-damage',
        class: ''
      },
      {
        path: '/app/messages',
        title: 'Messages',
        icon: 'fa-comment',
        class: ''
      },
      {
        path: '/app/profile',
        title: 'Profile',
        icon: 'fa-id-badge',
        class: ''
      },
      {
        path: '/app/coupons',
        title: 'Coupons',
        icon: 'fa-clipboard-list',
        class: ''
      }
    ];
  }

  logout() {
    this.authorizeService.logout().subscribe(
      result => {
        localStorage.removeItem('bread2bun-TokenId');
        sessionStorage.removeItem('bread2bun-TokenId');
        this.router.navigate(['/authorize']);
      },
      error => {
        this.t.error("Can't logout", 'Error');
      }
    );
  }

  getRating() {}

  getAllUnreadMessageCount() {
    this.navbarService.getAllUnreadMessageCOunt().subscribe(
      result => {
        this.allUnreadMessageCount = result;
      },
      error => {}
    );
  }

  initSubjects() {
    this.subsink.sink = this.layoutService.allUnreadMessageCount$.subscribe(
      count => {
        this.allUnreadMessageCount = count;
      },
      error => {}
    );
  }

  getUserName(){
    this.userName = this.baseService.getUserName();
  }
}
