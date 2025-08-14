import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { productoDTO } from '../productos/productos.models';
import { collectionDTO } from './landing.models';

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

  public GetDiscountedProducts(countryCode:string){
    return this.http.get<any>(this.apiURL + '/GetDiscountedProducts?countryCode=' + countryCode, {observe: 'response'});
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

  public GetProductsFromSpecificArea(countryCode,area){
    return this.http.get<any>(this.apiURL + '/GetProductsFromSpecificArea?countryCode=' + countryCode + '&area=' + area,
    {observe: 'response'});
  }

  public GetProductsFromSpecificCategory(countryCode,category){
    return this.http.get<any>(this.apiURL + '/GetProductsFromSpecificCategory?countryCode=' + countryCode + '&category=' + category, {observe: 'response'});
  }

  public GetProductsFromSpecificSubcategory(countryCode,subcategory){
    return this.http.get<any>(this.apiURL + '/GetProductsFromSpecificSubcategory?countryCode=' + countryCode + '&subcategory=' + subcategory, {observe: 'response'});
  }

  public GetAreas(){
    return this.http.get<any>(this.categoryURL + '/GetMainAreas', {observe: 'response'});
  }

  public GetTopSubcategories(countryCode:string){
    return this.http.get<any>(this.apiURL + '/GetTopSubcategories?countryCode=' + countryCode, {observe: 'response'});
  }

  public GetStoreProducts(countryCode:string){
    return this.http.get<productoDTO[]>(this.apiURL + '/GetStoreProducts?countryCode=' + countryCode, {observe: 'response'});
  }

  public GetStoreAreas(){
    return this.http.get<collectionDTO[]>(this.apiURL + '/GetStoreAreas', {observe: 'response'});
  }

  public GetStoreCategories(){
    return this.http.get<collectionDTO[]>(this.apiURL + '/GetStoreCategories', {observe: 'response'});
  }

  public GetStoreSubcategories(){
    return this.http.get<collectionDTO[]>(this.apiURL + '/GetStoreSubcategories', {observe: 'response'});
  }
}
