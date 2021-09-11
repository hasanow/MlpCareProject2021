import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
// import { SpinnerOverlayService } from './spinner-overlay.service';
import { Router } from '@angular/router';
import { TokenModel } from '../Models/TokenModel';

@Injectable()
export class WebApiService {

  public apiUrl =  "/api/";


  constructor(protected http: HttpClient,/*protected spinner:SpinnerOverlayService,*/private router: Router) { }

  public getToken(): string {
    var token = localStorage.getItem("tokenKey");
    return token;
  }

  public setToken(token: TokenModel): void {

    localStorage.setItem("tokenKey", token.token);   

    this.router.navigateByUrl("crudOperations");
  }

  public YetkiSifirla() {
    localStorage.removeItem("tokenKey");
    this.router.navigateByUrl("giris");
  }

  public isAuthenticated(): boolean {
    const token = this.getToken();
    return token != null;
  }

  public AktifGorunenAd(): string {
    return localStorage.getItem("gorunenAdi");
  }
}
