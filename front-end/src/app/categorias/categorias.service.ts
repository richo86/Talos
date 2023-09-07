import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CategoriaDTO } from './categorias.models';

@Injectable({
  providedIn: 'root'
})
export class CategoriasService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'Category';
  private driveURL = environment.apiUrl + 'Drive';

  public obtenerAreas(pagina: number, cantidadRegistrosAMostrar: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('pagina', pagina.toString());
    params = params.append('recordsPorPagina', cantidadRegistrosAMostrar.toString());
    return this.http.get<CategoriaDTO[]>(this.apiURL + '/getAllAreas', {observe: 'response', params});
  }

  public obtenerCategorias(pagina: number, cantidadRegistrosAMostrar: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('pagina', pagina.toString());
    params = params.append('recordsPorPagina', cantidadRegistrosAMostrar.toString());
    return this.http.get<CategoriaDTO[]>(this.apiURL + '/getAllCategories', {observe: 'response', params});
  }

  public obtenerSubcategorias(pagina: number, cantidadRegistrosAMostrar: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('pagina', pagina.toString());
    params = params.append('recordsPorPagina', cantidadRegistrosAMostrar.toString());
    return this.http.get<CategoriaDTO[]>(this.apiURL + '/getAllSubcategories', {observe: 'response', params});
  }

  public obtenerCategoria(id: string): Observable<any>{
    let params = new HttpParams();
    params = params.append('id',id);
    return this.http.get<CategoriaDTO>(this.apiURL + '/GetCategory', {observe: 'response', params});
  }

  public obtenerCategoriaSecundaria(id: string): Observable<any>{
    let params = new HttpParams();
    params = params.append('id',id);
    return this.http.get<CategoriaDTO>(this.apiURL + '/GetSecondaryCategory', {observe: 'response', params});
  }

  public obtenerArea(id: string): Observable<any>{
    let params = new HttpParams();
    params = params.append('id',id);
    return this.http.get<CategoriaDTO>(this.apiURL + '/GetArea', {observe: 'response', params});
  }

  public obtenerAreasNegocio(){
    return this.http.get<CategoriaDTO[]>(this.apiURL + '/GetMainAreas', {observe: 'response'});
  }

  public crearCategoria(producto: CategoriaDTO){
    return this.http.post<CategoriaDTO>(this.apiURL + '/CreateCategory', producto);
  }

  public actualizarCategoria(producto: CategoriaDTO){
    return this.http.put<string>(this.apiURL + '/UpdateCategory', producto);
  }

  public borrarCategoria(id:string){
    let params = new HttpParams();
    params = params.append('id',id);
    return this.http.delete<string>(this.apiURL + '/DeleteCategory', {observe: 'response', responseType: 'text' as 'json', params});
  }

  public subirImagen(imagenCategoria: File, id :string){
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');

    let params = new HttpParams();
    params.append('id', id);

    const file = new FormData();

    file.append('file', imagenCategoria);
    file.append('id',id);

    return this.http.post<any>(this.driveURL + '/UploadCategoryFile', file, { params: params})
        .pipe(map(
          resp => {
          }
        ));
  }

  public borrarImagen(fileId:string){
    let params = new HttpParams();
    params = params.append('fileId', fileId);
    return this.http.delete<any>(this.driveURL + "/DeleteImage",{observe: 'response', responseType: 'text' as 'json', params});
  }
}
