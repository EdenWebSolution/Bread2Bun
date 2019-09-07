import { Component, OnInit, ChangeDetectorRef, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { bloom, slideFromUp } from 'src/app/animations';
import { ClipboardService } from 'ngx-clipboard';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

declare let window;

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.scss'],
  animations: [bloom, slideFromUp]
})
export class MyProfileComponent implements OnInit {

  profileImgUrl: string = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTZtt-8JagsbCAhDa02YU8dEhABmIUIUaMEyq__-O6eEBo20DIwvw';
  max = 5;
  rate = 4;
  title = 'Food name';
  // tslint:disable-next-line: max-line-length
  coverImgUrl: string = 'data:image/jpeg;base64,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';





  landscapeImg = 'https://images.unsplash.com/photo-1515003197210-e0cd71810b5f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80';

  @ViewChild('fileInput') el: ElementRef;
  editFile: boolean = true;
  postDate = new Date();
  commentDate = new Date();

  constructor(
    private fb: FormBuilder,
    private clipBoardService: ClipboardService,
    private toastr: ToastrService,
  ) {

  }

  ngOnInit() {

  }


  copyToShare() {
    this.clipBoardService.copyFromContent(window.location.href);
    console.log(window.location.href);
    this.toastr.info('Profile URL copied to clipboard', 'Share')
  }
}
