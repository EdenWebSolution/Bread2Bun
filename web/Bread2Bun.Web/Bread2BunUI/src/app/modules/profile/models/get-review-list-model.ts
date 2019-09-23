export class GetReviewListModel {
  items: ReviewList[];
  constructor() {

  }
}

export class ReviewList {
  review: string;
  createdOn: Date;
  reviewImage: string;
  reviewer: ReviewerModel;
  isShowMore = false;
  constructor() {

  }
}

export class ReviewerModel {
  firstName: string;
  lastName: string;
  userName: string;
  profilePictureImagePath: string;
  constructor() {

  }
}
