import { Injectable, Injector } from '@angular/core';
import { BaseService } from '../../core/services/base.service';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { CountriesModel } from '../models/countries-model';
import { UniversititesModel } from '../models/universitites-model';

@Injectable({
  providedIn: 'root'
})
export class SharedService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getCountries() {
    return this.http
      .get<CountriesModel[]>(
        `${this.baseEndPoint}/api/shared/countries`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }

  getUniversities() {
    return this.http
      .get<UniversititesModel[]>(
        `${this.baseEndPoint}/api/shared/universities`,
        this.httpOptions
      )
      .catch(this.server4xxError);
  }
}
