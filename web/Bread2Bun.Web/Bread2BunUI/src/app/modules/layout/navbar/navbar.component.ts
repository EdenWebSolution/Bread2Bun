import { Component, OnInit } from '@angular/core';
import { slideFromUp } from 'src/app/animations';
import { MenuItem } from '../models/menu-item';
import { ToastrService } from 'ngx-toastr';
import { AuthorizeService } from '../../authorize/services/authorize.service';
import { Router } from '@angular/router';

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
  rate = 5;
  max = 5;
  isReadonly: boolean = false;

  constructor(
    private t: ToastrService,
    private authorizeService: AuthorizeService,
    private router: Router
  ) {
    this.show = false;
    this.showDropdown = false;
    this.menuItem = new Array<MenuItem>();
  }

  ngOnInit() {
    this.loadMenu();
  }

  loadMenu() {
    this.menuItem = [
      {
        path: '/app/feed',
        title: 'Explore',
        icon: '',
        class: ''
      },
      // {
      //   path: '/app/messages',
      //   title: 'Messages',
      //   icon: '',
      //   class: ''
      // },
      {
        path: '/app/profile',
        title: 'Profile',
        icon: '',
        class: ''
      },
      {
        path: '/app/coupons',
        title: 'Coupons',
        icon: '',
        class: ''
      }
    ];
  }

  logout() {
    this.authorizeService.logout().subscribe(result => {
      localStorage.removeItem('bread2bun-TokenId');
      sessionStorage.removeItem('bread2bun-TokenId');
      this.router.navigate(['/authorize']);
      console.log('Logout done');
    }, error => {
      this.t.error('Can\'t logout', 'Error');
    });
  }

  getRating() {
  }

}
