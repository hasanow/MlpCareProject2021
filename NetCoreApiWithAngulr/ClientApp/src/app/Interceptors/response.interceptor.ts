
import { Injectable } from '@angular/core';
import {
    HttpEvent, HttpInterceptor, HttpHandler, HttpRequest,HttpClient, HttpResponse
} from '@angular/common/http';
import { tap } from 'rxjs/operators';

@Injectable()
export class ResponseInterceptor implements HttpInterceptor {   
    constructor() {} 
    
    intercept(req: HttpRequest<any>, next: HttpHandler) {       
        return next.handle(req).pipe(
           tap(evt=>{
if(evt instanceof HttpResponse){
    if(evt.body && evt.body.success){
        console.log(evt.body);
    }
}
           })
        );
    }
}