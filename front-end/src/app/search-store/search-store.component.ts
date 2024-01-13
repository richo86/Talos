import { Component, OnInit } from '@angular/core';
import { SearchStoreService } from './search-store.service';
import { Observable, Subscription, take } from 'rxjs';
import { SeguridadService } from '../seguridad/seguridad.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-search-store',
  templateUrl: './search-store.component.html',
  styleUrls: ['./search-store.component.css']
})
export class SearchStoreComponent implements OnInit {

  showSuggestions:boolean = false;
  showClose:boolean = false;
  showCategories:boolean = false;
  searchString:string = null;
  suggestedItems:string[] = [];
  searchSubscription:Subscription;
  countryCode :string;
  userId:string;

  constructor(private searchService:SearchStoreService,
              private securityService: SeguridadService,
              private router: Router) { }

  ngOnInit(): void {
    this.GetUserLocation();
  }

  GetUserLocation(){
    this.securityService.getUserLocation().pipe(take(1))
    .subscribe({
      next: res =>{
        this.userId = this.securityService.setUserId();
        this.countryCode = res.body.countryCode;
      }
    });
  }

  searchChange(event){
    if(event.srcElement.value.length > 2){
      this.searchSubscription = this.searchService.GetItems(event.srcElement.value,this.countryCode).subscribe((results)=>{
        this.suggestedItems = results.body;
        if(this.suggestedItems.length > 0)
          this.showSuggestions = true;
      });
      
    }
    else{
      this.showSuggestions = false;
    }
  }

  resetSearch(){
    this.showSuggestions = false;
    this.searchString = null;
  }

  itemSelected(event:string){
    this.showSuggestions = false;
    this.searchString = null;
    this.router.navigate([event])
  }

  onEnter(){
    this.showSuggestions = false;
    this.searchString = null;
    if(!!this.suggestedItems[0])
      this.router.navigate([this.suggestedItems[0].link]);
  }

  ngOnDestroy(){
    this.searchSubscription.unsubscribe();
  }

}
