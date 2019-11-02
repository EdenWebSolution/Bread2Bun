export class UserConnectionModel {
  public connectionId: string;
  public userName: string;
  public isOnline: boolean;

  constructor() {
    this.connectionId = this.userName = null;
    this.isOnline = false;
  }
}
