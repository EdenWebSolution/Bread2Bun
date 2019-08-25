import { Injectable } from '@angular/core';
import { BaseService } from '../../core/services/base.service';
import { HttpClient } from '@angular/common/http';
import { ProfileBasicModel } from '../models/profile-basic-model';
import { CreateReviewModel } from '../models/create-review-model';
import { ReviewModel } from '../models/review-model';
import { UpdateReviewModel } from '../models/update-review-model';
import { PaginationModel } from '../../shared/models/pagination-model';
import { PaginationBase } from '../../shared/models/pagination-base';
import { UserProfileModel } from '../models/user-profile-model';

@Injectable({
  providedIn: 'root'
})
export class ProfileService extends BaseService {
  constructor(private http: HttpClient) {
    super();
  }

  // profile app services

  getProfileBasics() {
    return this.http
      .get<ProfileBasicModel>(
        `${this.baseEndPoint}/api/profile/basic`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  getProfileBasicsById(id: number) {
    return this.http
      .get<ProfileBasicModel>(
        `${this.baseEndPoint}/api/profile/basic/${id}`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  setUserProfile(userProfileModel: UserProfileModel) {
    return this.http
      .post<UserProfileModel>(
        `${this.baseEndPoint}/api/profile/userprofile`,
        userProfileModel,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  updateUserProfile(userProfileModel: UserProfileModel) {
    return this.http
      .put<UserProfileModel>(
        `${this.baseEndPoint}/api/profile/userprofile`,
        userProfileModel,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }


  // review app services

  postReview(createReviewModel: CreateReviewModel) {
    return this.http
      .post<ReviewModel>(
        `${this.baseEndPoint}/api/review`,
        createReviewModel,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  updateReview(updateReviewModel: UpdateReviewModel) {
    return this.http
      .put<ReviewModel>(
        `${this.baseEndPoint}/api/review`,
        updateReviewModel,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  deleteReview(id: number) {
    return this.http
      .delete<ReviewModel>(
        `${this.baseEndPoint}/api/review/${id}`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  getAllReview(pagination: PaginationBase) {
    return this.http
      .get<PaginationModel<ReviewModel>>(
        `${this.baseEndPoint}/api/review?skip=${pagination.skip}&take=${
          pagination.take
        }&searchQuery=${pagination.searchQuery}`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }
}
