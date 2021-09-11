import { Injectable, ɵCodegenComponentFactoryResolver } from "@angular/core";
import { Subject } from 'rxjs';



@Injectable()
export class SpinnerOverlayService{
    
    isLoading=new Subject<boolean>();

    show(){
        this.isLoading.next(true);
        console.log("Acildi");
    }
    hide(){
        this.isLoading.next(false);
        console.log("Kapandi");
    }   

}