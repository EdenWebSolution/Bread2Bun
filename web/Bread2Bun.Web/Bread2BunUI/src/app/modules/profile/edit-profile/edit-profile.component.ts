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
  coverPhotoUrl: string = '../../../../assets/images/cover.jpg';
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
      fullName: ['', [Validators.required]],
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

  get fullName() {
    return this.editProfileForm.get('fullName');
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
    this.profileUpdateModel.fullName = this.editProfileForm.value.fullName;
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
          this.loading = false;
        });
    },
      error => {
        this.toastr.error('Something went wrong', 'Error');
        this.loading = false;
      });
  }

  getUserProfileToUpdate(value: UpdateProfileModel) {
    let country = '';
    let university = '';
    if (value.countryId !== null) {
      country = this.countries.find(c => c.id === value.countryId).name;
      this.selectedCountryId = this.countries.find(c => c.id === value.countryId).id;
    }
    if (value.universityId !== null) {
      university = this.universities.find(u => u.id === value.universityId).name;
      this.selectedUniversityId = this.universities.find(u => u.id === value.universityId).id;
    }

    this.editProfileForm.patchValue({
      fullName: value.fullName,
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
    this.languagesAdded = this.languagesArr;
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
    if (countryId !== null) {
      this.foodService.getFoodListByCountry(countryId).subscribe(result => {
        this.foodList = result;
        this.getSelectedFoods(foodsIds);
        this.initiated = true;
        this.setCoverImageFood(coverImageId);
        this.isBlocked = false;
      }, error => {
        this.toastr.error('Couldn\'t update profile', 'Error');
        this.initiated = true;
        this.isBlocked = false;
      });
    } else {
      this.initiated = true;
      this.isBlocked = false;
    }
  }

  setCoverImageFood(countryId: number): void {
    if (countryId !== 0) {
      this.foodService.getFoodImageByFoodId(countryId).subscribe(result => {
        this.coverPhotoUrl = result.defaultFoodImagepath;
        this.coverPhotoId = result.id;
      }, error => {
        this.toastr.error('No image found for this food', 'Error');
      });
    }
  }
}


