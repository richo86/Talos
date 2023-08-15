import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LandingService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'StoreFront';
  private categoryURL = environment.apiUrl + 'Category';

  public GetLatestProducts(countryCode:string){
    return this.http.get<any>(this.apiURL + '/GetLatestProducts?countryCode=' + countryCode, {observe: 'response'});
  }

  public GetBestSellers(countryCode:string){
    return this.http.get<any>(this.apiURL + '/GetBestSellers?countryCode=' + countryCode, {observe: 'response'});
  }

  public GetLowestCost(countryCode:string){
    return this.http.get<any>(this.apiURL + '/GetLowestCost?countryCode=' + countryCode, {observe: 'response'});
  }

  public GetProductsFromCategory(){
    return this.http.get<any>(this.apiURL + '/GetProductsFromCategory', {observe: 'response'});
  }

  public GetProductsFromSubcategory(){
    return this.http.get<any>(this.apiURL + '/GetProductsFromSubcategory', {observe: 'response'});
  }

  public GetProductsFromArea(){
    return this.http.get<any>(this.apiURL + '/GetProductsFromArea', {observe: 'response'});
  }

  public GetProductsFromSpecificArea(){
    return this.http.get<any>(this.apiURL + '/GetProductsFromSpecificArea', {observe: 'response'});
  }

  public GetProductsFromSpecificCategory(){
    return this.http.get<any>(this.apiURL + '/GetProductsFromSpecificCategory', {observe: 'response'});
  }

  public GetProductsFromSpecificSubcategory(){
    return this.http.get<any>(this.apiURL + '/GetProductsFromSpecificSubcategory', {observe: 'response'});
  }

  public GetAreas(){
    return this.http.get<any>(this.categoryURL + '/GetMainAreas', {observe: 'response'});
  }

  public GetTopSubcategories(countryCode:string){
    return this.http.get<any>(this.apiURL + '/GetTopSubcategories?countryCode=' + countryCode, {observe: 'response'});
  }
}
