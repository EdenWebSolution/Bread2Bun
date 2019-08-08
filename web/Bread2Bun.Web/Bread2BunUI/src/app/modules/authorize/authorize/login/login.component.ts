import { Component, OnInit } from '@angular/core';
import { slideFromUp, slideFromRight, slideFromLeft } from 'src/app/animations';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  animations: [slideFromUp, slideFromRight, slideFromLeft]
})
export class LoginComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
