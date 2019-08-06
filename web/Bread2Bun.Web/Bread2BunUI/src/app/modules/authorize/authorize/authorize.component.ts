import { Component, OnInit } from '@angular/core';
import { slideFromBottom } from 'src/app/animations';

@Component({
  selector: 'app-authorize',
  templateUrl: './authorize.component.html',
  styleUrls: ['./authorize.component.scss'],
  animations: [slideFromBottom]
})
export class AuthorizeComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
