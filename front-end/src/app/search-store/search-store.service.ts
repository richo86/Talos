import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SearchStoreService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'StoreFront';

  public GetItems(search:string,countryCode:string){
    let params = new HttpParams();
    params = params.append('search', search);
    params = params.append('countryCode', countryCode);
    return this.http.get<any[]>(this.apiURL + '/SearchItems', {observe: 'response',params});
  }
}
