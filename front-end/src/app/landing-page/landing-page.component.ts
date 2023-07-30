import { Component, OnInit } from '@angular/core';
import { carouselDTO } from '../utilidades/carousel/carousel.models';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {

  carouselDTO: carouselDTO[] = [{
    image: "https://images.pexels.com/photos/17054468/pexels-photo-17054468/free-photo-of-moda-hombre-en-pie-retrato.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
    title: "Title",
    description: "New description",
    linkRoute: "/"
  }];

  constructor() { }

  ngOnInit(): void {
  }

}
