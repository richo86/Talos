import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { usuarioDTO } from '../seguridad/seguridad';
import { PaymentType, ventasDTO } from './ventas.models';

@Injectable({
  providedIn: 'root'
})
export class VentasService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'Payment';

  public obtenerTodos(pagina: number, cantidadRegistrosAMostrar: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('pagina', pagina.toString());
    params = params.append('recordsPorPagina', cantidadRegistrosAMostrar.toString());
    return this.http.get<usuarioDTO[]>(this.apiURL + '/GetPayments', {observe: 'response', params});
  }

  public GetPaymentTypes(){
    return this.http.get<PaymentType[]>(this.apiURL + '/GetPaymentsTypes',{observe: 'response'});
  }

  public GetSale(id:string){
    let params = new HttpParams();
    params = params.append('id', id.toString());
    return this.http.get<ventasDTO>(this.apiURL + "GetPayment", {observe: 'response'});
  }

  public Editar(paymentDTO:ventasDTO){
    return this.http.put<ventasDTO>(this.apiURL + "UpdatePayment", paymentDTO);
  }
}