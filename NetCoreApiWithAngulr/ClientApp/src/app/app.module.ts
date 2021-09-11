import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './Components/nav-menu/nav-menu.component';
import { HomeComponent } from './Components/home/home.component';
import { WebApiService } from './Services/webapi.service';
import AlertifyService from './Services/alertify.service';
import { ErrorInterceptor } from './Interceptors/error.interceptor';
import { LoginComponent } from './Components/login/login.component';
import { AuthManagerService } from './Services/authmanager.service';
import { SpinnerComponent } from './Components/Shared/spinner/spinner.component';
import { LoaderInterceptor } from './Interceptors/loader.interceptor';
import { ResponseInterceptor } from './Interceptors/response.interceptor';
import { SetUrlInterceptor } from './Interceptors/seturl.interceptor';
import { TokenInterceptor } from './Interceptors/token.interceptor';
import { SpinnerOverlayService } from './Services/spinner-overlay.service';
import { UserRegisterComponent } from './Components/user-register/user-register.component';
import { SpinnerFullscreenComponent } from './Components/Shared/spinner-fullscreen/spinner-fullscreen.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    SpinnerComponent,
    UserRegisterComponent,
    SpinnerFullscreenComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      {path:"Login",component:LoginComponent,pathMatch:"full"},
      {path:"Home",component:HomeComponent,pathMatch:"full"},
      {path:"UserRegister",component:UserRegisterComponent,pathMatch:"full"}
    ])
  ],
  providers:
    [
      WebApiService,
      AuthManagerService,
      AlertifyService,
      SpinnerOverlayService,
      {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
      {provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true },
      {provide: HTTP_INTERCEPTORS, useClass: ResponseInterceptor, multi: true },
      {provide: HTTP_INTERCEPTORS, useClass: SetUrlInterceptor, multi: true },
      {provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
    ],
  bootstrap: [AppComponent]
})
export class AppModule { }
