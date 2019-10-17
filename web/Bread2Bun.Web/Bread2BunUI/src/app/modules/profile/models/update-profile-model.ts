export class UpdateProfileModel {
  fullName: string;
  universityId: number;
  countryId: number;
  profileImage: string;
  coverFoodImageId: number;
  availableDays: Array<number>;
  aboutMe: string;
  languages: Array<string>;
  email: string;
  instagram: string;
  twitter: string;
  foodIds: Array<number>;
  address: Address = new Address();
  constructor() { }
}

export class Address {
  city: string;
  country: string;
}
