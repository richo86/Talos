import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'StoreFront';

  public obtenerMenuProductos(){
    return this.http.get<any>(this.apiURL + '/GetAllProducts', {observe: 'response'});
  }

}
