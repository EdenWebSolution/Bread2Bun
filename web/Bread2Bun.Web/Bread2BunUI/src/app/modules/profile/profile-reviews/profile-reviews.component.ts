import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { slideFromUp } from 'src/app/animations';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { ProfileService } from '../services/profile.service';
import { ToastrService } from 'ngx-toastr';
import { CreateReviewModel } from '../models/create-review-model';
import { GetReviewListModel, ReviewList } from '../models/get-review-list-model';

@Component({
  selector: 'app-profile-reviews',
  templateUrl: './profile-reviews.component.html',
  styleUrls: ['./profile-reviews.component.scss'],
  animations: [slideFromUp]
})
export class ProfileReviewsComponent implements OnInit {

  createPostForm: FormGroup;
  maxCharacter = 1500;
  characterCount = 0;
  scrollDistance = 1;
  scrollUpDistance = 2;
  throttle = 300;
  reviews: Array<ReviewList> = new Array<ReviewList>();
  textAreaHeight: number = 35;
  imageUrl: any = '';
  removeUpload: boolean = false;
  reviewDate = new Date();

  skip = 0;
  take = 10;
  userId: number;
  reviewImage: FileList;
  createReviewModel: CreateReviewModel = new CreateReviewModel();
  userSub: number;
  isMyProfile = false;
  reviewFinished: boolean = false;


  constructor(
    private fb: FormBuilder,
    private cd: ChangeDetectorRef,
    private router: Router,
    private profileService: ProfileService,
    private activatedRoute: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.createPostForm = this.fb.group({
      postContent: ['', [Validators.required]]
    });

    this.activatedRoute.params.subscribe((params: Params) => {
      this.userId = params.userId;
      this.profileService.getPagedReview(this.userId, this.skip, this.take).subscribe(result => {
        this.reviews = result.items;
      }, error => {
        this.toastr.error('Couldn\'t load reviews', 'Error');
      });
    });

    this.userSub = +localStorage.getItem('user-sub');
    if (this.userSub === +this.userId) {
      this.isMyProfile = true;
    }

  }

  measureLength() {
    this.characterCount = this.createPostForm.get('postContent').value.length;
    if (this.characterCount > 0) {
      this.textAreaHeight = 160;
    }
  }

  growHeight() {
    this.textAreaHeight = 160;
  }

  resetHeight() {
    if (this.characterCount === 0) {
      this.textAreaHeight = 35;
    }
  }

  onFileChange(event) {
    const reader = new FileReader();

    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      this.reviewImage = event.target.files;
      reader.onload = () => {
        this.imageUrl = reader.result;
        this.removeUpload = true;

        // need to run CD since file load runs outside of zone
        this.cd.markForCheck();
      };
    }
  }

  removeImage() {
    this.imageUrl = '';
    this.removeUpload = false;
    this.reviewImage = null;
  }

  createPost() {
    this.createReviewModel.revieweeId = this.userId;
    this.createReviewModel.review = this.createPostForm.value.postContent;
    this.profileService.createFormData(this.reviewImage, this.createReviewModel).subscribe(result => {
      this.toastr.success('Review posted succesfully', 'Success');
      this.removeImage();
      this.skip = 0;
      this.profileService.getPagedReview(this.userId, this.skip, this.take).subscribe(result => {
        this.reviews = result.items;
      }, error => {
        this.toastr.error('Couldn\'t load reviews', 'Error');
      });
    }, error => {
      this.toastr.error('Couldn\'t post review', 'Error');
    });
  }

  getReviews() {
    if (!this.reviewFinished) {
      this.skip += 10;
      this.profileService.getPagedReview(this.userId, this.skip, this.take).subscribe(result => {
        this.reviews = this.reviews.concat(result.items);
        if (result.items.length === 0) {
          this.reviewFinished = true;
        }
      }, error => {
        this.toastr.error('Couldn\'t load reviews', 'Error');
      });
    }
  }
  onReviewUp() {
  }

  goBackToProfile() {
    this.router.navigate(['/app/profile']);
  }


}
