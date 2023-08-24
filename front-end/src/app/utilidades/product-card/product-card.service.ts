import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CartDTO } from 'src/app/cart/cartDTO.models';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductCardService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'Cart';

  public CreateCart(cartDTO:CartDTO){
    return this.http.post<CartDTO>(this.apiURL + '/CreateCart', cartDTO, { responseType: 'text' as 'json'});
  }
}
