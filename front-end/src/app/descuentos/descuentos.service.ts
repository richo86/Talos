import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { descuentoDTO } from './descuentos.models';

@Injectable({
  providedIn: 'root'
})
export class DescuentosService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'Discount';

  public obtenerDescuentos(pagina: number, cantidadRegistrosAMostrar: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('pagina', pagina.toString());
    params = params.append('recordsPorPagina', cantidadRegistrosAMostrar.toString());
    return this.http.get<descuentoDTO[]>(this.apiURL + '/getAllDiscounts', {observe: 'response', params});
  }

  public obtenerDescuento(id: string): Observable<any>{
    let params = new HttpParams();
    params = params.append('id',id);
    return this.http.get<descuentoDTO>(this.apiURL + '/GetDiscount', {observe: 'response', params});
  }

  public crearDescuento(discountDTO: descuentoDTO){
    return this.http.post<string>(this.apiURL + '/CreateDiscount', discountDTO);
  }

  public actualizarDescuento(discountDTO: descuentoDTO){
    return this.http.put<string>(this.apiURL + '/UpdateDiscount', discountDTO, { responseType: 'text' as 'json'});
  }

  public borrarDescuento(id:string){
    let params = new HttpParams();
    return this.http.put<string>(this.apiURL + '/DeleteDiscount', {observe: 'response', params});
  }
}
