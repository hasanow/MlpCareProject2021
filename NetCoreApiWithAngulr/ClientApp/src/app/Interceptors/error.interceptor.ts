
import { retry, catchError } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import {
  HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpClient, HttpErrorResponse
} from '@angular/common/http';
import { WebApiService } from '../Services/webapi.service';
import { throwError } from 'rxjs';
import { Router } from '@angular/router';
import AlertifyService from '../Services/alertify.service';
import { Location } from '@angular/common';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private webapi: WebApiService,
    private alertify: AlertifyService,
    private location: Location) { }

  intercept(req: HttpRequest<any>, next: HttpHandler) {

    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        console.log(error);
       
        if (error.status === 401) {
          this.alertify.Alert("Oturum süreniz dolmuştur. Giriş ekranına yönlendiriliyorsunuz...", () => {
            this.router.navigateByUrl("Login");
          });
        }else if (error.status === 406) {          
          return next.handle(req);
        }
         else {
          this.alertify.Alert(error.error);
        } 
        return throwError(error);
      })
    );
  }
}
