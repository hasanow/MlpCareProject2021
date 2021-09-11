import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/Models/UserModel';
import AlertifyService from 'src/app/Services/alertify.service';
import { UserManagerService } from 'src/app/Services/usermanager.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

users:Array<UserModel>;

constructor(
              private userManager:UserManagerService,
              private alertify:AlertifyService,
              private router:Router
    ) {
      this.users=new Array<UserModel>();     
      
    }
ngOnInit(): void {
  this.GetUsers(); 
  this.userManager.SetUserId(0); 
}
    GetUsers(){
      this.userManager.GetUsers().subscribe(sonuc=>{
        this.users=sonuc;
        console.log(this.users);
      });
    }

    DeleteUser(user:UserModel){
      this.alertify.Dogrulama(user.firstName+' '+user.lastName +'kullanıcısı silinecek. Onaylıyor musunuz?',()=>{
        this.userManager.DeleteUser(user).subscribe(sonuc=>{
          this.GetUsers();
        });
      },null,'Sil');
      
    }

    UpdateUser(user:UserModel){
      if(user)
        this.userManager.SetUserId(user.userId);
      this.router.navigateByUrl("/UserRegister")
    }
}
