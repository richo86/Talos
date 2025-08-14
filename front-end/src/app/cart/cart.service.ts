import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CartDTO } from './cartDTO.models';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'Cart';

  public getCart(id:string){
    let params = new HttpParams();
    params = params.append('id', id.toString());
    return this.http.get<CartDTO[]>(this.apiURL + '', {observe: 'response', params});
  }
}