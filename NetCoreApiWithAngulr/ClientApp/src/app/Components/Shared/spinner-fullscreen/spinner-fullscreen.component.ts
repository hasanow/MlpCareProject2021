import { Component, OnInit } from '@angular/core';
import { SpinnerOverlayService } from 'src/app/Services/spinner-overlay.service';
import { Subject } from 'rxjs/internal/Subject';

@Component({
  selector: 'spinner-fullscreen',
  templateUrl: './spinner-fullscreen.component.html',
  styleUrls: ['./spinner-fullscreen.component.css']
})
export class SpinnerFullscreenComponent implements OnInit {

  isLoading:Subject<boolean>=this.spinnerService.isLoading;

  
  constructor(private spinnerService:SpinnerOverlayService) { }

  ngOnInit() {
  }

}
