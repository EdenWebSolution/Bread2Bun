import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { TypeaheadModule } from 'ngx-bootstrap';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ToastrModule.forRoot(),
    TypeaheadModule.forRoot(),
  ],
  exports: [
    ReactiveFormsModule,
    FormsModule,
    ToastrModule,
    TypeaheadModule
  ]
})
export class SharedModule { }
