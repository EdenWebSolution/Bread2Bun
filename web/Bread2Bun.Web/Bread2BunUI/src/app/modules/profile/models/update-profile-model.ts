export class UpdateProfileModel {
  firstName: string;
  lastName: string;
  universityId: number;
  countryId: number;
  profileImg: string;
  coverFoodImage: string;
  availableDays: Array<number>;
  aboutMe: string;
  languages: Array<string>;
  email: string;
  socialmedia: {
    twitterUsername: string,
    instagramUsername: string
  };
  foodIds: Array<number>;

}
