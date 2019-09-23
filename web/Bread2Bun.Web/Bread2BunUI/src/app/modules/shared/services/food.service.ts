import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '../../core/services/base.service';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { FoodDataModel } from '../models/food-data-model';
import { UserFoodModel } from '../../profile/models/user-food-model';


@Injectable({
  providedIn: 'root'
})
export class FoodService extends BaseService {

  constructor(
    private http: HttpClient
  ) {
    super();
  }

  getFoodListByCountry(countryId: number) {
    return this.http
      .get<any[]>(
        `${this.baseEndPoint}/api/food/country/${countryId}`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  getFoodImageByFoodId(foodId: number) {
    return this.http
      .get<FoodDataModel>(`${this.baseEndPoint}/api/food/${foodId}/image`)
      .catch(this.server4xxError);
  }

  getUserFoodList() {
    return this.http
      .get<UserFoodModel[]>(
        `${this.baseEndPoint}/api/food/userfoods`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }
}

