import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { slideFromUp, slideFromLeft } from 'src/app/animations';
import { AuthorizeService } from '../../services/authorize.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
  animations: [slideFromLeft]
})
export class ForgotPasswordComponent implements OnInit {

  resetPasswordForm: FormGroup;
  @Output() showLogin = new EventEmitter<boolean>();

  constructor(
    private fb: FormBuilder,
    private authorizeService: AuthorizeService
  ) { }

  ngOnInit() {
    this.resetPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }


  get email() {
    return this.resetPasswordForm.get('email');
  }

  resetPassword() {
    this.authorizeService.forgotPassword(this.resetPasswordForm.value).subscribe(result => {
      console.log(result);
    }, error => {
      console.log(error);
    });
  }

  showLoginFn() {
    this.showLogin.emit(true);
  }
}
