
import { Injectable } from '@angular/core';
import {
    HttpEvent, HttpInterceptor, HttpHandler, HttpRequest,HttpClient, HttpHeaders
} from '@angular/common/http';
import { WebApiService } from '../Services/webapi.service';

@Injectable()
export class SetUrlInterceptor implements HttpInterceptor {   
    constructor(public api:WebApiService) {}

    intercept(req: HttpRequest<any>, next: HttpHandler) {
        const headers=new HttpHeaders();

        
        
        if(!req.url.includes("http")){
            req=req.clone({            
                url:this.api.apiUrl+req.url                            
            });
        }        
        return next.handle(req);
    }
}