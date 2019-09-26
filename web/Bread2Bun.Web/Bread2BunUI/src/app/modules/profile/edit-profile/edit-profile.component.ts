import { Component, OnInit, ViewChild, ElementRef, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { UpdateProfileModel, Address } from '../models/update-profile-model';
import { SharedService } from '../../shared/services/shared.service';
import { ToastrService } from 'ngx-toastr';
import { UniversititesModel } from '../../shared/models/universitites-model';
import { CountriesModel } from '../../shared/models/countries-model';
import { forkJoin } from 'rxjs';
import { TypeaheadMatch } from 'ngx-bootstrap';
import { ProfileService } from '../services/profile.service';
import { Router } from '@angular/router';
import { FoodService } from '../../shared/services/food.service';
import { FoodsModel } from '../../shared/models/foods-model';
import { FoodDataModel } from '../../shared/models/food-data-model';

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {

  editProfileForm: FormGroup;
  profileUpdateModel: UpdateProfileModel;
  namePattern = '^[A-Za-z\s]+$';
  foodList: Array<FoodsModel> = new Array<FoodsModel>();
  imageUrl: any;
  isBlocked = false;

  @ViewChild('fileInput') el: ElementRef;
  ProfileImageUrl: any = 'https://cdn2.iconfinder.com/data/icons/user-management/512/add-512.png';
  editFile: boolean = true;
  removeUpload: boolean = false;
  coverPhotoId: number;
  coverPhotoUrl: string = 'data:image/jpeg;base64,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';
  days: Array<any> = [
    {
      id: 1,
      value: 'Monday'
    },
    {
      id: 2,
      value: 'Tuesday'
    },
    {
      id: 3,
      value: 'Wednesday'
    },
    {
      id: 4,
      value: 'Thursday'
    },
    {
      id: 5,
      value: 'Friday'
    },
    {
      id: 6,
      value: 'Saturday'
    },
    {
      id: 7,
      value: 'Sunday'
    },
  ];

  foodData: FoodDataModel = new FoodDataModel();
  selectedDays = [];
  dropdownSettings = {};
  foodDropdownSettings = {};
  initiated = false;
  selectedFood = [];
  universities: UniversititesModel[];
  countries: CountriesModel[];
  selectedCountryId: number;
  selectedUniversityId: number;
  langPlaceholder = 'Enter languages';
  languagesArr: Array<string> = new Array<string>();
  languagesAdded = [];
  loading = false;
  fileList: FileList;
  constructor(
    private fb: FormBuilder,
    private cd: ChangeDetectorRef,
    private sharedService: SharedService,
    private toastr: ToastrService,
    private profileService: ProfileService,
    private router: Router,
    private foodService: FoodService
  ) {
    this.countries = new Array<CountriesModel>();
    this.universities = new Array<UniversititesModel>();
    this.profileUpdateModel = new UpdateProfileModel();
    this.profileUpdateModel.address = new Address();
  }

  ngOnInit() {
    this.editProfileForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.pattern(this.namePattern)]],
      lastName: ['', [Validators.required, Validators.pattern(this.namePattern)]],
      universityId: ['', [Validators.required]],
      countryId: ['', [Validators.required]],
      profileImg: [null, []],
      coverFoodImage: ['', []],
      availableDays: ['', [Validators.required]],
      foodIds: ['', Validators.required],
      languages: ['', [Validators.required]],
      aboutMe: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      twitterUsername: [''],
      instagramUsername: [''],
      residentCity: ['', Validators.required],
      residentCountry: ['', Validators.required]
    });

    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'value',
      selectAllText: 'Check All',
      unSelectAllText: 'Un-Check all',
    };

    this.foodDropdownSettings = {
      singleSelection: false,
      idField: 'key',
      textField: 'value',
      selectAllText: 'Select All',
      unSelectAllText: 'Un Select All',
      allowSearchFilter: true
    };

    this.initiateForm();
  }

  initiateForm() {
    this.initiated = false;
    this.isBlocked = true;
    forkJoin(
      this.sharedService.getCountries(),
      this.sharedService.getUniversities(),
      this.profileService.getUserProfileToUpdate()
    ).subscribe(result => {
      this.countries = result[0],
        this.universities = result[1],
        this.getUserProfileToUpdate(result[2]);

    }, error => {
      this.initiated = true;
      this.isBlocked = false;
      this.toastr.error(error.message, 'Error');
    });
  }

  get firstName() {
    return this.editProfileForm.get('firstName');
  }

  get lastName() {
    return this.editProfileForm.get('lastName');
  }

  get email() {
    return this.editProfileForm.get('email');
  }

  get universityId() {
    return this.editProfileForm.get('universityId');
  }

  get countryId() {
    return this.editProfileForm.get('countryId');
  }

  get profileImg() {
    return this.editProfileForm.get('profileImg');
  }

  get coverFoodImage() {
    return this.editProfileForm.get('coverFoodImage');
  }

  get availableDays() {
    return this.editProfileForm.get('availableDays');
  }

  get aboutMe() {
    return this.editProfileForm.get('aboutMe');
  }

  get languages() {
    return this.editProfileForm.get('languages');
  }

  get twitterUsername() {
    return this.editProfileForm.get('twitterUsername');
  }

  get instagramUsername() {
    return this.editProfileForm.get('instagramUsername');
  }

  get foodIds() {
    return this.editProfileForm.get('foodIds');
  }

  get residentCity() {
    return this.editProfileForm.get('residentCity');
  }

  get residentCountry() {
    return this.editProfileForm.get('residentCountry');
  }


  uploadFile(event) {
    const reader = new FileReader(); // HTML5 FileReader API
    const file = event.target.files[0];
    this.fileList = event.target.files;
    if (event.target.files && event.target.files[0]) {
      reader.readAsDataURL(file);

      // When file uploads set it to file formcontrol
      reader.onload = () => {
        this.ProfileImageUrl = reader.result;
        this.editFile = false;
        this.removeUpload = true;
      };
      // ChangeDetectorRef since file is loading outside the zone
      this.cd.markForCheck();
    }
  }



  onDaySelect(item: any) {
  }

  onSelectAllDays(items: any) {
  }

  onDaysDeSelect(items: any) {
  }

  onFoodSelect(item: any) {
  }

  onSelectAllFoods(items: any) {
  }

  onFoodDeSelect(items: any) {
  }

  onLanguageRemoved(language: any) {
    this.languagesArr = this.languagesArr.filter(lang => lang !== language);

  }
  onLanguageAdded(language: any) {
    this.languagesArr.push(language.value);
  }

  onSelect(event: TypeaheadMatch): void {
    this.selectedCountryId = event.item.id;
    this.foodService.getFoodListByCountry(this.selectedCountryId).subscribe(res => {
      this.foodList = res;
      this.selectedFood = [];
    }, error => {
      this.toastr.error('Cannot get foods related to' + event.item.value, 'Error');
    });
  }

  onUniversitySelect(event: TypeaheadMatch): void {
    this.selectedUniversityId = event.item.id;
  }

  onCoverImageFoodSelect(event: any): void {
    this.coverPhotoId = event.item.key;
    this.foodService.getFoodImageByFoodId(this.coverPhotoId).subscribe(result => {
      this.foodData = result;
      this.coverPhotoUrl = this.foodData.defaultFoodImagepath;
    }, error => {
      this.toastr.error('No image found for this food', 'Error');
    });
  }

  updateProfile() {
    this.loading = true;
    const availableDaysArr: number[] = new Array<number>();
    const offeredFoodsArr: number[] = new Array<number>();
    this.selectedDays.forEach(days => {
      availableDaysArr.push(days.id);
    });
    this.selectedFood.forEach(food => {
      offeredFoodsArr.push(food.key);
    });
    this.profileUpdateModel.availableDays = availableDaysArr;
    this.profileUpdateModel.foodIds = offeredFoodsArr;
    this.profileUpdateModel.countryId = this.selectedCountryId;
    this.profileUpdateModel.universityId = this.selectedUniversityId;
    this.profileUpdateModel.coverFoodImageId = this.coverPhotoId;
    this.profileUpdateModel.languages = this.languagesArr;
    this.profileUpdateModel.firstName = this.editProfileForm.value.firstName;
    this.profileUpdateModel.lastName = this.editProfileForm.value.lastName;
    this.profileUpdateModel.aboutMe = this.editProfileForm.value.aboutMe;
    this.profileUpdateModel.email = this.editProfileForm.value.email;
    this.profileUpdateModel.twitter = this.editProfileForm.value.twitterUsername;
    this.profileUpdateModel.instagram = this.editProfileForm.value.instagramUsername;
    this.profileUpdateModel.address.city = this.editProfileForm.value.residentCity;
    this.profileUpdateModel.address.country = this.editProfileForm.value.residentCountry;

    this.profileService.updateUserProfile(this.profileUpdateModel).subscribe(result => {
      this.profileService.saveFile(this.fileList).subscribe(res => {
        this.loading = false;
        this.toastr.success('Profile updated successfully', 'Success');
        this.router.navigate(['/app/profile']);
      },
        error => {
          this.toastr.error('Something went wrong', 'Error');
        });
    },
      error => {
        this.toastr.error('Something went wrong', 'Error');
        this.loading = false;
      });
  }

  getUserProfileToUpdate(value: UpdateProfileModel) {

    const country = this.countries.find(c => c.id === value.countryId).name;
    const university = this.universities.find(u => u.id === value.universityId).name;
    this.selectedCountryId = this.countries.find(c => c.id === value.countryId).id;
    this.selectedUniversityId = this.universities.find(u => u.id === value.universityId).id;

    this.editProfileForm.patchValue({
      firstName: value.firstName,
      lastName: value.lastName,
      universityId: university,
      countryId: country,
      languages: value.languages,
      aboutMe: value.aboutMe,
      email: value.email,
      twitterUsername: value.twitter,
      instagramUsername: value.instagram,
      residentCity: value.address.city,
      residentCountry: value.address.country,
      // availableDays: result.availableDays
    });
    // this.languagesAdded = value.languages;
    this.languagesArr = value.languages;
    this.ProfileImageUrl = value.profileImage;
    this.getSelectedDays(value.availableDays);
    this.getFoods(value.countryId, value.foodIds, value.coverFoodImageId);
  }

  getSelectedDays(days: Array<number>) {
    days.forEach(day => {
      this.selectedDays.push(this.days.find(dayArr => dayArr.id === day));
    });
    this.editProfileForm.patchValue({
      availableDays: this.selectedDays
    });
  }

  getSelectedFoods(foodIds: Array<number>) {
    foodIds.forEach(id => {
      this.selectedFood.push(this.foodList.find(foodArr => foodArr.key === id));
    });
    this.editProfileForm.patchValue({
      foodIds: this.selectedFood
    });
  }

  getFoods(countryId: number, foodsIds: Array<number>, coverImageId: number) {
    this.foodService.getFoodListByCountry(countryId).subscribe(result => {
      this.foodList = result;
      this.getSelectedFoods(foodsIds);
      this.initiated = true;
      this.setCoverImageFood(coverImageId);
    }, error => {
      this.toastr.error('Something went wrong', 'Error');
      this.initiated = true;
      this.isBlocked = false;
    });
  }

  setCoverImageFood(countryId: number): void {
    if (countryId !== 0) {
      this.foodService.getFoodImageByFoodId(countryId).subscribe(result => {
        this.coverPhotoUrl = result.defaultFoodImagepath;
        this.coverPhotoId = result.id;
        this.isBlocked = false;
      }, error => {
        this.isBlocked = false;
        this.toastr.error('No image found for this food', 'Error');
      });
    }
  }
}


