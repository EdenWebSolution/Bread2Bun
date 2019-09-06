import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProfileRoutingModule } from './profile-routing.module';
import { SharedModule } from '../shared/shared.module';
import { MyProfileComponent } from './my-profile/my-profile.component';
import {TimeAgoPipe} from 'time-ago-pipe';


@NgModule({
  declarations: [MyProfileComponent, TimeAgoPipe],
  imports: [
    CommonModule,
    ProfileRoutingModule,
    SharedModule
  ]
})
export class ProfileModule { }
