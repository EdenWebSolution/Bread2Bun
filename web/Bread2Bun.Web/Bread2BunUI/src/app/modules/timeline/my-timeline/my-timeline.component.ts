import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-my-timeline',
  templateUrl: './my-timeline.component.html',
  styleUrls: ['./my-timeline.component.scss']
})
export class MyTimelineComponent implements OnInit {

  constructor(
    private t: ToastrService
  ) { }

  ngOnInit() {
    this.t.success('everything is broken', 'Major Error', {
      progressBar: true,
      toastClass: 'ngx-toastr my-success'
    });

    this.t.show('everything is broken', 'Major Error', {
      progressBar: true,
    });
    this.t.error('everything is broken', 'Major Error', {
      progressBar: true,
    });
    this.t.info('everything is broken', 'Major Error', {
      progressBar: true,
    });
    this.t.warning('everything is broken', 'Major Error', {
      progressBar: true,
    });
  }

}
