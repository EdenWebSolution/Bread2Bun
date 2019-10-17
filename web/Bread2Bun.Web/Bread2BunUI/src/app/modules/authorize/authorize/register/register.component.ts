import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { slideFromUp, slideFromLeft, slideFromRight } from 'src/app/animations';
import { RegisterUserModel } from '../../models/register-user-model';
import { SharedService } from 'src/app/modules/shared/services/shared.service';
import { UniversititesModel } from 'src/app/modules/shared/models/universitites-model';
import { CountriesModel } from 'src/app/modules/shared/models/countries-model';
import { forkJoin } from 'rxjs';
import { AuthorizeService } from '../../services/authorize.service';
import { ToastrService } from 'ngx-toastr';
import { TypeaheadMatch } from 'ngx-bootstrap/typeahead/typeahead-match.class';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
  animations: [slideFromUp, slideFromLeft, slideFromRight]
})
export class RegisterComponent implements OnInit {

  registerUserForm: FormGroup;
  namePattern = '^[A-Za-z\s]+$';
  passwordPattern = '^(?=.*?[0-9])(?=.*?[A-Z])(?=.*?[#?!@$%^&*()\\-_]).{8,}$'; // Strong password
  registerUserModel: RegisterUserModel;
  loading = false;
  registered: boolean;
  showContent = false;
  constructor(
    private fb: FormBuilder,
    private authorizeService: AuthorizeService,
    private toastr: ToastrService
  ) {
    this.registerUserModel = new RegisterUserModel();
    this.registered = false;
  }

  ngOnInit() {
    this.initiateForm();
  }

  initiateForm() {
    this.registerUserForm = this.fb.group({
      fullName: ['', [Validators.required]],
      userName: ['', [Validators.required, Validators.minLength(5)]],
      password: ['', [Validators.required, Validators.pattern(this.passwordPattern)]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  get fullName() {
    return this.registerUserForm.get('fullName');
  }

  get password() {
    return this.registerUserForm.get('password');
  }

  get userName() {
    return this.registerUserForm.get('userName');
  }

  get email() {
    return this.registerUserForm.get('email');
  }


  registerUser() {
    this.loading = true;
    this.registerUserModel = Object.assign({}, this.registerUserModel, this.registerUserForm.value);
    this.authorizeService.registerUser(this.registerUserModel).subscribe(result => {
      this.loading = false;
      this.registered = true;
    }, error => {
      this.toastr.error(error.message, 'Error');
      this.loading = false;
    });
  }

  loadContent() {
    this.showContent = true;
  }

}
