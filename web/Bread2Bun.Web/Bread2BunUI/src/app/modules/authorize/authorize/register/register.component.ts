import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { slideFromUp, slideFromLeft, slideFromRight } from 'src/app/animations';

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

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.initiateForm();
  }

  initiateForm() {
    this.registerUserForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.pattern(this.namePattern)]],
      lastName: ['', [Validators.required, Validators.pattern(this.namePattern)]],
      foodieName: ['', [Validators.required, Validators.minLength(5)]],
      password: ['', [Validators.required, Validators.pattern(this.passwordPattern)]],
      email: ['', [Validators.required, Validators.email]],
      university: ['', [Validators.required]],
      gender: ['', [Validators.required]],
      country: ['', [Validators.required]]
    });
  }

  get firstName() {
    return this.registerUserForm.get('name');
  }

  get lastName() {
    return this.registerUserForm.get('name');
  }

  get password() {
    return this.registerUserForm.get('password');
  }

  get foodieName() {
    return this.registerUserForm.get('foodieName');
  }

  get email() {
    return this.registerUserForm.get('email');
  }

  get university() {
    return this.registerUserForm.get('university');
  }

  get gender() {
    return this.registerUserForm.get('gender');
  }

  get country() {
    return this.registerUserForm.get('country');
  }

  registerUser() {
    console.log(this.registerUserForm.value);
  }

}
