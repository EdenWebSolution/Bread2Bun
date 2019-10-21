import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { slideFromLeft } from 'src/app/animations';

@Component({
  selector: 'app-chat-thread',
  templateUrl: './chat-thread.component.html',
  styleUrls: ['./chat-thread.component.scss'],
  animations: [slideFromLeft]
})
export class ChatThreadComponent implements OnInit {

  @Output() showList = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

}
