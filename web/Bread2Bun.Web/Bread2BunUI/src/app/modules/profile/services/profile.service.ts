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
import { UpdateProfileModel } from '../models/update-profile-model';
import { ViewProfileModel } from '../../shared/models/view-profile-model';
import { ViewOtherProfileModel } from '../../shared/models/view-other-profile-model';
import { ExploreFeedModel } from '../../feed/models/explore-feed-model';
import { catchError, map } from 'rxjs/operators';
import { GetReviewListModel } from '../models/get-review-list-model';

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

  getUserProfileToUpdate() {
    return this.http
      .get<UpdateProfileModel>(
        `${this.baseEndPoint}/api/profile/userprofile`,
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

  updateUserProfile(updateProfileModel: UpdateProfileModel) {
    return this.http
      .put<UpdateProfileModel>(
        `${this.baseEndPoint}/api/profile/userprofile`,
        updateProfileModel,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  saveFile(files: FileList) {
    let formData: FormData = new FormData();
    if (files != null && files != undefined && files.length > 0) {
      for (let i = 0; i < files.length; i++) {
        formData.append('uploadFile' + i, files[i]);
      }
    }
    return this.http.put(`${this.baseEndPoint}/api/profile/userprofile/profileimage`, formData).map(res => res).catch(this.server4xxError);
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

  getPagedReview(userId: number, skip: number, take: number) {
    return this.http
      .get<GetReviewListModel>(
        `${this.baseEndPoint}/api/review/list/${userId}?Skip=${skip}&Take=${take}`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  getProfileView() {
    return this.http
      .get<ViewProfileModel>(
        `${this.baseEndPoint}/api/profile/userprofile/userprofileviewinfo`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  getProfileViewByUserName(userName: string) {
    return this.http
      .get<ViewOtherProfileModel>(
        `${this.baseEndPoint}/api/profile/userprofile/${userName}`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  getExploreFeed(skip: number, take: number) {
    return this.http
      .get<ExploreFeedModel>(
        `${this.baseEndPoint}/api/profile/userprofile/feeds?skip=${skip}&take=${take}`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  getStatus() {
    return this.http
      .get<boolean>(
        `${this.baseEndPoint}/api/profile/userprofile/status`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  createFormData(files, entity: any) {
    let formData: FormData = new FormData();

    if (files != null && files != undefined && files.length > 0) {
      for (let i = 0; i < files.length; i++) {
        formData.append('imageUpload' + i, files[i]);
      }
    }

    formData.append('entityData', JSON.stringify(entity));

    return this.http.post(`${this.baseEndPoint}/api/review`, formData)
      .pipe(map((response) => JSON.stringify(response)),
        catchError(this.server4xxError));
  }

}
