import { Component, OnInit } from '@angular/core';
import { slideFromUp } from 'src/app/animations';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
  animations: [slideFromUp]
})
export class NavbarComponent implements OnInit {

  show: boolean;
  showDropdown: boolean;

  constructor() {
    this.show = false;
    this.showDropdown = false;
  }

  ngOnInit() {
  }

}
