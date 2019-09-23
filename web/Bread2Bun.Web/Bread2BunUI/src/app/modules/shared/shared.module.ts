import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { TypeaheadModule, RatingModule, PopoverModule } from 'ngx-bootstrap';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { ClipboardModule } from 'ngx-clipboard';
import { TimeAgoPipe } from 'time-ago-pipe';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { TagInputModule } from 'ngx-chips';

@NgModule({
  declarations: [TimeAgoPipe],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(
      {
        progressBar: true
      }
    ),
    TypeaheadModule.forRoot(),
    RatingModule,
    PopoverModule.forRoot(),
    InfiniteScrollModule,
    ClipboardModule,
    NgMultiSelectDropDownModule.forRoot(),
    TagInputModule
  ],
  exports: [
    ReactiveFormsModule,
    FormsModule,
    ToastrModule,
    TypeaheadModule,
    RatingModule,
    PopoverModule,
    InfiniteScrollModule,
    ClipboardModule,
    TimeAgoPipe,
    NgMultiSelectDropDownModule,
    TagInputModule
  ]
})
export class SharedModule { }
