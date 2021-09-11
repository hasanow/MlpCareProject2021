import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TokenModel } from 'src/app/Models/TokenModel';
import { UserForLoginModel } from 'src/app/Models/UserForLoginModel';
import { AuthManagerService } from 'src/app/Services/authmanager.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

userForLogin:UserForLoginModel=new UserForLoginModel();
errors:string="";

  constructor(
      private authService:AuthManagerService,
      private router: Router
      ) { }

  ngOnInit() {
  }

  Login(){
    this.errors="";
    if(this.userForLogin.Email==null || this.userForLogin.Email=="" || this.userForLogin.Password==null || this.userForLogin.Password==""){
      this.errors="Email ve Password alanlarını doldurunuz.";
    }else{
      this.authService.Login(this.userForLogin).subscribe(sonuc=>{
        console.log(sonuc);
        this.authService.setToken(sonuc);
        this.router.navigateByUrl("Home");
      });
    }
  }

}
