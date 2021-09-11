import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { UserForRegisterModel } from 'src/app/Models/UserForRegisterModel';
import AlertifyService from 'src/app/Services/alertify.service';
import { UserManagerService } from 'src/app/Services/usermanager.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['../login/login.component.css']
})
export class UserRegisterComponent implements OnInit {

public userForRegister:UserForRegisterModel;
public validationErrors:Array<string>;

  constructor(
        private userService:UserManagerService,
        private router:Router,
        private alertify:AlertifyService
  ) { 
    this.validationErrors=new Array<string>();
  }

  ngOnInit() {
      if(this.userService.IsExistUserId()){
        this.userService.GetUser().subscribe(sonuc=>{
            this.userForRegister=sonuc;
        });
        }
        else{
            this.userForRegister=new UserForRegisterModel();
        }
  }

  SaveUser(){
    if(this.userForRegister.userId>0){
      this.alertify.Dogrulama("Kullanıcı bilgileri güncellenecek Onaylıyor musunuz?",()=>{
        this.SaveUserFunc();        
      },null,'Güncelle');
    }else{
        this.SaveUserFunc()
    }
  }

  private SaveUserFunc(){
    if(this.PasswordCheck())
    this.userService.SaveUser(this.userForRegister).pipe(
     catchError((error:HttpErrorResponse)=>{
       this.validationErrors=new Array<string>();
       error.error.Errors.forEach(e => {
          this.validationErrors.push(e);
       }); 
      return throwError(error);
     })
    ).subscribe(sonuc=>{
        this.router.navigateByUrl("/Home");      
    })
  }

  private PasswordCheck():boolean{
    var u=this.userForRegister;
    if((u.password || u.rePassword) && u.password!=u.rePassword){
        return false;
    }
    return true;
  }

}
