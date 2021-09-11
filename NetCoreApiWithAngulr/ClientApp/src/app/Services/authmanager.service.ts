import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { TokenModel } from "../Models/TokenModel";
import { UserForLoginModel } from "../Models/UserForLoginModel";
import { WebApiService } from "./webapi.service";






@Injectable({
    providedIn:"root"
})
export class AuthManagerService extends WebApiService{

    Login(userForLogin:UserForLoginModel){
        return this.http.post<TokenModel>("Auth/Login",userForLogin);
    }

}