import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { TypeaheadModule, RatingModule, PopoverModule } from 'ngx-bootstrap';

@NgModule({
  declarations: [],
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
    PopoverModule.forRoot()
  ],
  exports: [
    ReactiveFormsModule,
    FormsModule,
    ToastrModule,
    TypeaheadModule,
    RatingModule,
    PopoverModule
  ]
})
export class SharedModule { }
