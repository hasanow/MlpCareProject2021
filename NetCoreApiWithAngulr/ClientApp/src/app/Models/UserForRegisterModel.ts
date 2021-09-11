export class UserForRegisterModel {
  public userId: number;
  public email: string;
  public password: string;
  public rePassword:string;
  public firstName: string;
  public lastName: string;
  public tcKimlikNo: string;

  constructor() {
    this.userId = 0;
    this.email="";
    this.password="";
    this.firstName="";
    this.lastName="";
    this.tcKimlikNo="";
    this.rePassword="";
  }
}
