import { Component, OnInit } from '@angular/core';
import { slideFromUp } from 'src/app/animations';
import { MenuItem } from '../models/menu-item';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  animations: [slideFromUp]
})
export class NavbarComponent implements OnInit {

  show: boolean;
  showDropdown: boolean;
  menuItem: MenuItem[];

  constructor(
    private t: ToastrService
  ) {
    this.show = false;
    this.showDropdown = false;
    this.menuItem = new Array<MenuItem>();
  }

  ngOnInit() {
    this.menuItem = [
      {
        path: '/app/feed',
        title: 'Feed',
        icon: '',
        class: ''
      },
      {
        path: '/app/explore',
        title: 'Explore',
        icon: '',
        class: ''
      },
      {
        path: '/app/messages',
        title: 'Messages',
        icon: '',
        class: ''
      },
      {
        path: '/app/profile',
        title: 'Profile',
        icon: '',
        class: ''
      }
    ];
  }

  logout() {
    // implementation
    // localStorage.removeItem('bread2bun-TokenId');
    // sessionStorage.removeItem('bread2bun-TokenId');
  }

}
