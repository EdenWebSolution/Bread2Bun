import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { TypeaheadModule, RatingModule } from 'ngx-bootstrap';

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
    RatingModule
  ],
  exports: [
    ReactiveFormsModule,
    FormsModule,
    ToastrModule,
    TypeaheadModule,
    RatingModule
  ]
})
export class SharedModule { }
