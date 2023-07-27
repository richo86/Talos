import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { paisDTO } from '../seguridad/formulario-registro/registro';
import { regionesProductoDTO } from './regiones.models';

@Injectable({
  providedIn: 'root'
})
export class RegionesService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'Regions';

  public obtenerRegiones(pagina: number, cantidadRegistrosAMostrar: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('pagina', pagina.toString());
    params = params.append('recordsPorPagina', cantidadRegistrosAMostrar.toString());
    return this.http.get<regionesProductoDTO[]>(this.apiURL + '/getAllRegions', {observe: 'response', params});
  }

  public obtenerRegionesProducto(id:string){
    let params = new HttpParams();
    params = params.append('id', id);
    return this.http.get<regionesProductoDTO>(this.apiURL + '/getRegionProduct', {observe: 'response', params })
  }

  public obtenerPaises(){
    return this.http.get<paisDTO[]>(this.apiURL + '/getAllCountries', {observe: 'response' })
  }

  public crearRegiones(region: regionesProductoDTO){
    return this.http.post<regionesProductoDTO>(this.apiURL + '/CreateRegion', region);
  }

  public actualizarRegiones(region: regionesProductoDTO){
    return this.http.put<string>(this.apiURL + '/UpdateRegion', region);
  }

  public borrarRegion(id:string){
    let params = new HttpParams();
    params = params.append('id',id);
    return this.http.delete<string>(this.apiURL + '/DeleteRegion', {observe: 'response' , responseType: 'text' as 'json', params});
  }
}
