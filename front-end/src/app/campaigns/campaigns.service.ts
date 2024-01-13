import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { campaignDTO } from './campaigns.models';
import { KeyValuePair } from '../productos/productos.models';

@Injectable({
  providedIn: 'root'
})
export class CampaignsService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'Campaign';
  private driveURL = environment.apiUrl + 'Drive';

  public obtenerCampañas(pagina: number, cantidadRegistrosAMostrar: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('pagina', pagina.toString());
    params = params.append('recordsPorPagina', cantidadRegistrosAMostrar.toString());
    return this.http.get<campaignDTO[]>(this.apiURL + '/getAllCampaigns', {observe: 'response', params});
  }

  public obtenerCampaña(id: string): Observable<any>{
    let params = new HttpParams();
    params = params.append('id',id);
    return this.http.get<campaignDTO>(this.apiURL + '/GetCampaign', {observe: 'response', params});
  }

  public crearCampaña(discountDTO: campaignDTO){
    return this.http.post<string>(this.apiURL + '/CreateCampaign', discountDTO);
  }

  public actualizarCampaña(discountDTO: campaignDTO){
    return this.http.put<string>(this.apiURL + '/UpdateCampaign', discountDTO, { responseType: 'text' as 'json'});
  }

  public borrarCampaña(id:string){
    let params = new HttpParams();
    return this.http.put<string>(this.apiURL + '/DeleteCampaign', {observe: 'response', params});
  }

  public subirImagenes(listadoProductos: File[], id :string){
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');

    let params = new HttpParams();
    params.append('id', id);

    const file = new FormData();

    for (let index = 0; index < listadoProductos.length; index++) {
      file.append('file', listadoProductos[index]);
      file.append('id',id);
    }

    return this.http.post<any>(this.driveURL + '/UploadCampaignFile', file, { params: params})
        .pipe(map(
          resp => {
          }
        ));
  }

  public obtenerImagenesCampañaBase64(id:string){
    let params = new HttpParams();
    params = params.append('id', id);
    return this.http.get<KeyValuePair<string, string>[]>(this.apiURL + '/getCampaignBase64Images', {observe: 'response', params});
  }

  public getAllProducts(): Observable<any>{
    return this.http.get<campaignDTO>(this.apiURL + '/GetAllProducts', {observe: 'response'});
  }
}
