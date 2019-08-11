import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { slideFromUp, slideFromLeft, slideFromRight } from 'src/app/animations';
import { RegisterUserModel } from '../../models/register-user-model';
import { SharedService } from 'src/app/modules/shared/shared.service';
import { UniversititesModel } from 'src/app/modules/shared/universitites-model';
import { CountriesModel } from 'src/app/modules/shared/countries-model';
import { forkJoin } from 'rxjs';
import { AuthorizeService } from '../../services/authorize.service';

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
  universities: UniversititesModel[];
  countries: CountriesModel[];
  constructor(
    private fb: FormBuilder,
    private sharedService: SharedService,
    private authorizeService: AuthorizeService
  ) {
    this.registerUserModel = new RegisterUserModel();
    this.countries = new Array<CountriesModel>();
    this.universities = new Array<UniversititesModel>();
  }

  ngOnInit() {
    this.initiateForm();
  }

  initiateForm() {
    forkJoin(
      this.sharedService.getCountries(),
      this.sharedService.getUniversities()
    ).subscribe(result => {
      this.countries = result[0],
        this.universities = result[1];
    }, error => {
      console.log(error);
    });
    this.registerUserForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.pattern(this.namePattern)]],
      lastName: ['', [Validators.required, Validators.pattern(this.namePattern)]],
      userName: ['', [Validators.required, Validators.minLength(5)]],
      password: ['', [Validators.required, Validators.pattern(this.passwordPattern)]],
      email: ['', [Validators.required, Validators.email]],
      universityId: ['', [Validators.required]],
      gender: ['', [Validators.required]],
      countryId: ['', [Validators.required]]
    });
  }

  get firstName() {
    return this.registerUserForm.get('firstName');
  }

  get lastName() {
    return this.registerUserForm.get('lastName');
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

  get universityId() {
    return this.registerUserForm.get('universityId');
  }

  get gender() {
    return this.registerUserForm.get('gender');
  }

  get countryId() {
    return this.registerUserForm.get('countryId');
  }

  registerUser() {
    console.log(this.registerUserForm.value);
    this.registerUserModel = Object.assign({}, this.registerUserModel, this.registerUserForm.value);
    this.authorizeService.registerUser(this.registerUserModel).subscribe(result => {
      console.log(result);
    }, error => {
      console.log(error);
    });
  }

}
