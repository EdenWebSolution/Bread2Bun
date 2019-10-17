import { Address } from '../../profile/models/update-profile-model';

export class ViewProfileModel {
  userId: number;
  userName: string;
  fullName: string;
  email: string;
  profileImage: string;
  coverImage: string;
  country: string;
  countryCode: string;
  university: string;
  availableDays: Array<number>;
  aboutMe: string;
  languages: Array<string>;
  address: Address;
  instagram: string;
  twitter: string;
  constructor() {
    this.availableDays = new Array<number>();
    this.languages = new Array<string>();
    this.address = new Address();
  }
}
