
import { Injectable } from '@angular/core';
import {
    HttpEvent, HttpInterceptor, HttpHandler, HttpRequest,HttpClient
} from '@angular/common/http';
import { SpinnerOverlayService } from '../Services/spinner-overlay.service';
import { finalize } from 'rxjs/operators';

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {   
    constructor(public spinnerService:SpinnerOverlayService) {} 
    
    intercept(req: HttpRequest<any>, next: HttpHandler) {       
        console.log("Yüklendi");
        this.spinnerService.show();
        return next.handle(req).pipe(
            finalize(()=>{
                this.spinnerService.hide();
                console.log("kapandi");
            })
            
        );
    }
}