import { Component, OnInit } from '@angular/core';
import { slideFromBottom, slideFromUp } from 'src/app/animations';

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.scss'],
  animations: [slideFromBottom, slideFromUp]
})
export class AuthorizeComponent implements OnInit {
  register: boolean;
  login: boolean;

  constructor() { }

  ngOnInit() {
    this.login = true;
    this.register = false;
  }

  showLogin() {
    this.login = true;
    this.register = false;
  }
  showRegister() {
    this.login = false;
    this.register = true;
  }
}

