import { Component, OnInit } from '@angular/core';
import { productoDTO } from '../productos/productos.models';
import { SeguridadService } from '../seguridad/seguridad.service';
import { carouselDTO } from '../utilidades/carousel/carousel.models';
import { collectionDTO } from './landing.models';
import { LandingService } from './landing.service';
import { Observable, take } from 'rxjs';
import { Store, select } from '@ngrx/store';
import { getAreas } from '../utilidades/Redux/Areas/areasActions';
import { selectAreas } from '../utilidades/Redux/Areas/areasSelectors';
import { BestSellers } from '../utilidades/Redux/BestSellers/bestSelectors'
import { getBestSellers } from '../utilidades/Redux/BestSellers/bestActions';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.css']
})
export class LandingPageComponent implements OnInit {

  userId:string;
  carouselDTO: carouselDTO[] = [{
    image: "//www.teasetea.com/cdn/shop/files/Shopify_Website_Banner_PAGE_4_1296x.png?v=1682089410",
    title: "Blends with Benefits",
    description: "Functional self care to elevate your everyday wellness rituals.",
    linkRoute: "/cards",
    linkDescription: "SHOP OUR BEST SELLERS"
  }];
  countryCode :string;
  collectionList: collectionDTO[] = [];
  economyList: productoDTO[] = [];
  topSubcategories: collectionDTO[] = [];
  bestSellers: productoDTO[] = [];
  

  constructor(private securityService: SeguridadService,
              private landingService: LandingService,
              private store:Store) { }

  ngOnInit(): void {
    this.GetUserLocation();
  }

  GetUserLocation(){
    this.securityService.getUserLocation().pipe(take(1))
    .subscribe({
      next: res =>{
        this.userId = this.securityService.setUserId();

        this.countryCode = res.body.countryCode;

        this.GetAreas();

        this.GetBestSellers();

        this.GetDiscountedProducts();

        this.GetSubcategories();

      }
    });
  }

  GetAreas(){
    this.store.dispatch(getAreas());
    this.store.select(selectAreas).subscribe(res => {
      console.log("collection",res)
      this.collectionList = res
    });
  }

  GetProductsFromArea(){
    this.landingService.GetProductsFromArea().pipe(take(1))
    .subscribe({
      next: (result) => {
        this.collectionList = result.body;
      }
    });
  }

  GetBestSellers(){
    this.store.dispatch(getBestSellers({countryCode:this.countryCode}));
    this.store.select(BestSellers).subscribe({
      next: (res)=>{
        console.log("best Sellers",res);
        this.bestSellers = res;
        if(this.bestSellers.length <3){
          this.GetLatestProducts();
        }
      },
      error: (res)=>{
        this.GetLatestProducts();
      }
    });
  }

  GetLatestProducts(){
    this.landingService.GetLatestProducts(this.countryCode).pipe(take(1))
      .subscribe({
        next: (result)=>{
          this.bestSellers = result.body;
        }
      });
  }

  GetDiscountedProducts(){
    this.landingService.GetDiscountedProducts(this.countryCode).pipe(take(1))
    .subscribe({
      next: (result) => {
        if(result.body.length == 0)
          this.GetLowestCost();
        else{
          this.economyList = result.body;
        }
      }
    })
  }

  GetLowestCost(){
    this.landingService.GetLowestCost(this.countryCode).pipe(take(1))
    .subscribe({
      next: (result)=>{
        this.economyList = result.body;
      }
    });
  }

  GetSubcategories(){
    this.landingService.GetTopSubcategories(this.countryCode).pipe(take(1))
    .subscribe((result)=> {
      this.topSubcategories = result.body;
    });
  }

}
