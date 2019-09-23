import { ViewProfileModel } from './view-profile-model';
import { UserFoodModel } from '../../profile/models/user-food-model';

export class ViewOtherProfileModel {
  profile: ViewProfileModel;
  foods: Array<UserFoodModel>;
  constructor() {
    // this.profile = new ViewProfileModel();
    // this.foods = new Array<UserFoodModel>();
  }
}
