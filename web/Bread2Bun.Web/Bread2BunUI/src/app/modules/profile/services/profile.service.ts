import { Injectable } from '@angular/core';
import { BaseService } from '../../core/services/base.service';
import { HttpClient } from '@angular/common/http';
import { ProfileBasicModel } from '../models/profile-basic-model';
import { CreateReviewModel } from '../models/create-review-model';
import { ReviewModel } from '../models/review-model';
import { UpdateReviewModel } from '../models/update-review-model';


@Injectable({
  providedIn: 'root'
})
export class ProfileService extends BaseService {
  constructor(private http: HttpClient) {
    super();
  }

  getProfileBasics() {
    return this.http
      .get<ProfileBasicModel>(
        `${this.baseEndPoint}/api/profile/basic`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  postReview(createReviewModel: CreateReviewModel) {
    return this.http
      .post<ReviewModel>(
        `${this.baseEndPoint}/api/profile/review`,
        createReviewModel,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  updateReview(updateReviewModel: UpdateReviewModel) {
    return this.http
      .put<ReviewModel>(
        `${this.baseEndPoint}/api/profile/review`,
        updateReviewModel,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  deleteReview(id: number) {
    return this.http
      .delete<ReviewModel>(
        `${this.baseEndPoint}/api/profile/review/${id}`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

}
