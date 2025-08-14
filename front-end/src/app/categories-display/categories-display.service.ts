import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CategoriaDTO } from '../categorias/categorias.models';
import { productoDTO } from '../productos/productos.models';

@Injectable({
  providedIn: 'root'
})
export class CategoriesDisplayService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'StoreFront';
  private categoryURL = environment.apiUrl + 'Category';

  public getCategoryProducts(countryCode:string,category:string){
    let params = new HttpParams();
    params = params.append('countryCode', countryCode.toString());
    params = params.append('category', category.toString());
    return this.http.get<productoDTO[]>(this.apiURL + '/GetProductsFromSpecificCategory', {observe: 'response', params});
  }

  public getSubcategories(id:string){
    let params = new HttpParams();
    params = params.append('id', id.toString());
    return this.http.get<CategoriaDTO[]>(this.categoryURL + '', {observe: 'response', params});
  }
}