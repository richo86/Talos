import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { MessagesDTO } from './mensajes.models';

@Injectable({
  providedIn: 'root'
})
export class MensajesService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'Mail';

  public GetAllMessages(pagina: number, cantidadRegistrosAMostrar: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('pagina', pagina.toString());
    params = params.append('recordsPorPagina', cantidadRegistrosAMostrar.toString());
    return this.http.get<any>(this.apiURL + '/GetAllMessages', {observe: 'response', params});
  }

  public GetMessages(email: string){
    let params = new HttpParams();
    params = params.append('email',email);
    return this.http.get<MessagesDTO[]>(this.apiURL + '/GetMessages', {observe: 'response', params});
  }

  public SendMessage(mensajeDTO: MessagesDTO){
    return this.http.post<MessagesDTO>(this.apiURL + '/SendMessage', mensajeDTO);
  }

}
