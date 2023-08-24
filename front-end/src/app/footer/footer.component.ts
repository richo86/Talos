import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FadeInOff } from '../utilidades/animation';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css'],
  animations:[FadeInOff(1000)]
})
export class FooterComponent implements OnInit {

  animationStart:boolean = false;

  constructor(private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
  }

  animStart(event: AnimationEvent) {
    console.log('Animation Started', event);
  }
  
  animDone(event: AnimationEvent) {
    console.log('Animation Ended', event);
  }

  onAppear(){
    this.cdr.detectChanges();
    this.animationStart = true;
  }

}
