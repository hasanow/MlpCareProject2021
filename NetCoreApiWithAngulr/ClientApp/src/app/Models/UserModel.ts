export class UserModel {
  public userId: number;
  public email: string;
  public password: string;
  public firstName: string;
  public lastName: string;
  public tcKimlikNo: string;
  public status: boolean;

  constructor() {
    this.userId = 0;
  }
}
