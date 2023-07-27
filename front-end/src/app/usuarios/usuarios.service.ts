import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { usuarioDTO } from '../seguridad/seguridad';

@Injectable({
  providedIn: 'root'
})
export class UsuariosService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'account';

  public obtenerTodos(pagina: number, cantidadRegistrosAMostrar: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('pagina', pagina.toString());
    params = params.append('recordsPorPagina', cantidadRegistrosAMostrar.toString());
    return this.http.get<usuarioDTO[]>(this.apiURL + '/users', {observe: 'response', params});
  }

  public hacerAdmin(id: string){
    return this.http.post<any>(this.apiURL + '/makeAdmin?id=' + id, {observe: 'response'});
  }

  public quitarAdmin(id: string){
    return this.http.post<any>(this.apiURL + '/deleteAdmin?id=' + id, {observe: 'response'})
  }

  public borrarUsuario(id:string){
    return this.http.delete<any>(this.apiURL + '/deleteUser?id=' + id, {observe: 'response'});
  }
}
