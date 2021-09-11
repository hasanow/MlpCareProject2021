
import { Injectable } from '@angular/core';
import {
    HttpEvent, HttpInterceptor, HttpHandler, HttpRequest,HttpClient
} from '@angular/common/http';
import { WebApiService } from '../Services/webapi.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {   
    constructor(public api:WebApiService) {}
  
    intercept(req: HttpRequest<any>, next: HttpHandler) {
            if(!req.url.includes("Auth/Login")){

                var token=this.api.getToken();
                console.log(token);
                if(token){
                    req=req.clone({            
                        setHeaders:{
                            "Authorization":`Bearer ${token}`
                        }
                    });
                }
            }
        return next.handle(req);
    }
}