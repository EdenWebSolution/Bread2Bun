import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { slideFromUp } from 'src/app/animations';
import { Router } from '@angular/router';

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
  scrollDistance = 2;
  scrollUpDistance = 2;
  throttle = 1500;
  reviews = [false, false, false];
  textAreaHeight: number = 35;
  imageUrl: any = '';
  removeUpload: boolean = false;
  profileImgUrl: string = 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTZtt-8JagsbCAhDa02YU8dEhABmIUIUaMEyq__-O6eEBo20DIwvw';
  reviewDate = new Date();
  landscapeImg = 'https://images.unsplash.com/photo-1515003197210-e0cd71810b5f?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1950&q=80';



  constructor(
    private fb: FormBuilder,
    private cd: ChangeDetectorRef,
    private router: Router
  ) { }

  ngOnInit() {
    this.createPostForm = this.fb.group({
      postContent: ['', [Validators.required]],
      postImage: [null]
    });
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

      reader.onload = () => {
        this.imageUrl = reader.result;
        this.createPostForm.patchValue({
          postImage: reader.result
        });
        this.removeUpload = true;

        // need to run CD since file load runs outside of zone
        this.cd.markForCheck();
      };
    }
  }

  removeImage() {
    this.imageUrl = '';
    this.createPostForm.patchValue({
      postImage: null
    });
    this.removeUpload = false;
  }

  createPost() {
    console.log(this.createPostForm.value);
  }

  getReviews() {
    console.log('adsa');
    this.reviews.push(false);
  }
  onReviewUp() {
    console.log('up psts');
  }

  goBackToProfile() {
    this.router.navigate(['/app/profile']);
  }


}
