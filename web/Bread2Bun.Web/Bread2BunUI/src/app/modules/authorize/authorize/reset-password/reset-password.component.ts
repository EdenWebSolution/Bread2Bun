import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { slideFromRight } from 'src/app/animations';
import { ResetPasswordModel } from '../../models/reset-password-model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { passwordValidator } from './validator';
import { AuthorizeService } from '../../services/authorize.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss'],
  animations: [slideFromRight]
})
export class ResetPasswordComponent implements OnInit {

  resetPasswordModel: ResetPasswordModel;
  resetPasswordForm: FormGroup;
  passwordPattern = '^(?=.*?[0-9])(?=.*?[A-Z])(?=.*?[#?!@$%^&*()\\-_]).{8,}$'; // Strong password
  loading: boolean;



  constructor(
    private activatedRoute: ActivatedRoute,
    private fb: FormBuilder,
    private authorizeService: AuthorizeService,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.loading = false;
    this.resetPasswordModel = new ResetPasswordModel();
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe((params: Params) => {
      this.resetPasswordModel.token = params.token;
      this.resetPasswordModel.email = params.email;
    });
    this.resetPasswordForm = this.fb.group({
      password: ['', [Validators.required, Validators.pattern(this.passwordPattern)]],
      confirmPasssword: ['', [passwordValidator, Validators.required]],
    });
  }

  get password() {
    return this.resetPasswordForm.get('password');
  }

  get confirmPasssword() {
    return this.resetPasswordForm.get('confirmPasssword');
  }

  resetPassword() {
    this.loading = true;
    this.resetPasswordModel.newPassword = this.resetPasswordForm.value.password;
    this.authorizeService.resetPassword(this.resetPasswordModel).subscribe(result => {
      this.loading = false;
      this.toastr.success('Password reset successful', 'Success');
      this.router.navigate(['/authorize']);
    }, error => {
      this.loading = false;
      this.toastr.error('Could not reset password', 'Error');
    });
  }

}
