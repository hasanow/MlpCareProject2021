import { Component, Input, OnInit } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';
import { SpinnerOverlayService } from 'src/app/Services/spinner-overlay.service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.css']
})
export class SpinnerComponent implements OnInit {

  isLoading:Subject<boolean>=this.spinnerService.isLoading;

  
  constructor(private spinnerService:SpinnerOverlayService) { }
  @Input() mesaj:string='';
  ngOnInit() {
  }

}
