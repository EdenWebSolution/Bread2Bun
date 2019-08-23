import { Component, OnInit } from '@angular/core';
import { slideFromUp } from 'src/app/animations';
import { UserResolverService } from '../../shared/services/user-resolver.service';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  constructor() {}

  ngOnInit() {}
}
