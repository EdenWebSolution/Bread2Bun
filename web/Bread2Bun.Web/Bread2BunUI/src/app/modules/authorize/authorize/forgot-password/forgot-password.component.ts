import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { slideFromUp, slideFromLeft, slideFromRight } from 'src/app/animations';
import { AuthorizeService } from '../../services/authorize.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss'],
  animations: [slideFromRight]
})
export class ForgotPasswordComponent implements OnInit {

  resetPasswordForm: FormGroup;
  @Output() showLogin = new EventEmitter<boolean>();
  loading: boolean;
  showInstruction = false;

  constructor(
    private fb: FormBuilder,
    private authorizeService: AuthorizeService,
    private toastr: ToastrService
  ) {
    this.loading = false;
  }

  ngOnInit() {
    this.resetPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }


  get email() {
    return this.resetPasswordForm.get('email');
  }

  resetPassword() {
    this.loading = true;
    this.authorizeService.forgotPassword(this.resetPasswordForm.value).subscribe(result => {
      this.loading = false;
      this.showInstruction = true;
    }, error => {
      console.log(error);
      this.toastr.error(error.message, 'Oops!!', {
        progressBar: true
      });
      this.loading = false;
    });
  }

  showLoginFn() {
    this.showLogin.emit(true);
  }
}
