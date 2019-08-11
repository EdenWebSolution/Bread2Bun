import { Gender } from '../../core/enums/genderEnum';

export class RegisterUserModel {
  firstName: string;
  lastName: string;
  userName: string;
  password: string;
  countryId: number;
  universityId: number;
  gender: Gender;
  email: string;
}
