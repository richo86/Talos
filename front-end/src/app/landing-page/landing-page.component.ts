import { Component, OnInit } from '@angular/core';
import { productoDTO } from '../productos/productos.models';
import { SeguridadService } from '../seguridad/seguridad.service';
import { carouselDTO } from '../utilidades/carousel/carousel.models';
import { collectionDTO } from './landing.models';
import { LandingService } from './landing.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {

  carouselDTO: carouselDTO[] = [{
    image: "//www.teasetea.com/cdn/shop/files/Shopify_Website_Banner_PAGE_4_1296x.png?v=1682089410",
    title: "Blends with Benefits",
    description: "Functional self care to elevate your everyday wellness rituals.",
    linkRoute: "/cards",
    linkDescription: "SHOP OUR BEST SELLERS"
  }];
  countryCode :string;
  collectionList: collectionDTO[];
  economyList: productoDTO[];
  topSubcategories: collectionDTO[];
  bestSellers: productoDTO[];

  constructor(private securityService: SeguridadService,
              private landingService: LandingService) { }

  ngOnInit(): void {
    this.GetUserLocation();
  }

  GetUserLocation(){
    this.securityService.getUserLocation()
    .subscribe({
      next: res =>{
        this.countryCode = res.body.countryCode;

        this.GetProductsFromArea();

        this.GetBestSellers();

        this.GetLowestCost();

        this.GetSubcategories();
      }
    });
  }

  GetProductsFromArea(){
    this.landingService.GetProductsFromArea()
    .subscribe({
      next: (result) => {
        this.collectionList = result.body;
      }
    });
  }

  GetBestSellers(){
    this.landingService.GetBestSellers(this.countryCode)
    .subscribe({ 
      next: (result) =>{
        this.bestSellers = result.body;
        if(this.bestSellers.length <3){
          this.GetLatestProducts();
        }
      }
    });
  }

  GetLatestProducts(){
    this.landingService.GetLatestProducts(this.countryCode)
      .subscribe({
        next: (result)=>{
          this.bestSellers = result.body;
        }
      });
  }

  GetLowestCost(){
    this.landingService.GetLowestCost(this.countryCode)
    .subscribe({
      next: (result)=>{
        this.economyList = result.body;
      }
    });
  }

  GetSubcategories(){

  }

}
