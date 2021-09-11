import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { UserForLoginModel } from "../Models/UserForLoginModel";
import { UserForRegisterModel } from "../Models/UserForRegisterModel";
import { UserModel } from "../Models/UserModel";
import { WebApiService } from "./webapi.service";




@Injectable({
    providedIn:"root"
})
export class UserManagerService extends WebApiService{
    
private  selectedUserId:number;

IsExistUserId(){
    return this.selectedUserId!=undefined && this.selectedUserId!=null && this.selectedUserId!=0;
}

GetUsers():Observable<Array<UserModel>>{
    return this.http.get<Array<UserModel>>("User/getlist");
}
DeleteUser(user:UserModel):Observable<any>{
    return this.http.delete<any>("User/delete?userId="+user.userId);
}

SetUserId(userId:number){
    this.selectedUserId=userId;
}

GetUser():Observable<UserForRegisterModel>{
    return this.http.get<UserForRegisterModel>("User/get?userId="+this.selectedUserId);
}
SaveUser(userForRegister:UserForRegisterModel):Observable<any>{
    return this.http.put<any>("user/save",userForRegister);
}
}