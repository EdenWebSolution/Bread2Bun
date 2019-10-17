export class ExploreFeedModel {
  items: ExploreFeedFood[];
  skip: number;
  take: number;
  timeTaken: number;
  totalRecordCount: number;
  constructor() {

  }
}

export class ExploreFeedFood {
  country: string;
  countryCode: string;
  foodImage: string;
  foodName: string;
  profileImage: string;
  userId: number;
  username: string;
  fullName: string;
  isVegeterian: boolean;
  constructor() {

  }
}
