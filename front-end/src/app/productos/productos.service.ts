import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CategoriaDTO } from '../categorias/categorias.models';
import { descuentoDTO } from '../descuentos/descuentos.models';
import { crearProductoDTO, KeyValuePair, productoDTO } from './productos.models';

@Injectable({
  providedIn: 'root'
})
export class ProductosService {

  constructor(private http:HttpClient) { }

  private apiURL = environment.apiUrl + 'Product';
  private driveURL = environment.apiUrl + 'Drive';
  private categoriasURL = environment.apiUrl + 'Category';
  private descuentosURL = environment.apiUrl + 'Discount';

  public obtenerProductos(pagina: number, cantidadRegistrosAMostrar: number): Observable<any>{
    let params = new HttpParams();
    params = params.append('pagina', pagina.toString());
    params = params.append('recordsPorPagina', cantidadRegistrosAMostrar.toString());
    return this.http.get<productoDTO[]>(this.apiURL + '/getAllProducts', {observe: 'response', params});
  }

  public obtenerProducto(id: string): Observable<any>{
    let params = new HttpParams();
    params = params.append('id',id);
    return this.http.get<productoDTO>(this.apiURL + '/getProduct', {observe: 'response', params});
  }

  public obtenerCategoriasPrincipales(){
    return this.http.get<CategoriaDTO[]>(this.categoriasURL + '/GetMainCategories', {observe: 'response'});
  }

  public obtenerCategoriasSecundarias(){
    return this.http.get<CategoriaDTO[]>(this.categoriasURL + '/GetSecondaryCategories', {observe: 'response'});
  }

  public obtenerDescuentos(){
    return this.http.get<descuentoDTO[]>(this.descuentosURL + '/getAllDiscountsList', {observe: 'response'});
  }

  public obtenerListadoImagenesProducto(id:string){
    let params = new HttpParams();
    params = params.append('id', id);
    return this.http.get<string[]>(this.apiURL + '/getProductIds', {observe: 'response', params});
  }

  public obtenerImagenesProductoBase64(id:string){
    let params = new HttpParams();
    params = params.append('id', id);
    return this.http.get<KeyValuePair<string, string>[]>(this.apiURL + '/getProductBase64Images', {observe: 'response', params});
  }

  public obtenerTodasImagenes(){
    return this.http.get<any>(this.driveURL + '/GetAllFiles', {observe: 'response'});
  }

  public obtenerImagenPorId(id:string){
    return this.http.get<string>(this.driveURL + `/GetFileById/${id}`, {observe: 'response'});
  }

  public obtenerImagenesProducto(fileIds: string[]){
    return this.http.post<KeyValuePair<string, string>[]>(this.driveURL + '/GetFilesByIds', fileIds);
  }

  public crearProducto(producto: crearProductoDTO){
    return this.http.post<productoDTO>(this.apiURL + '/CreateProduct', producto);
  }

  public subirImagenes(listadoProductos: File[], id :string){
    let headers = new HttpHeaders();
    headers.append('Content-Type', 'multipart/form-data');
    headers.append('Accept', 'application/json');

    let params = new HttpParams();
    params.append('id', id);

    const files = new FormData();

    for (let index = 0; index < listadoProductos.length; index++) {
      files.append('files', listadoProductos[index]);
      files.append('id',id);
    }

    return this.http.post<any>(this.driveURL + '/UploadImage', files, { params: params})
        .pipe(map(
          resp => {
            console.log(resp);
          }
        ));
  }

  public borrarImagen(fileId:string){
    let params = new HttpParams();
    params = params.append('fileId', fileId);
    return this.http.delete<any>(this.driveURL + "/DeleteImage",{observe: 'response', responseType: 'text' as 'json', params});
  }

  public actualizarProducto(producto: productoDTO){
    return this.http.put<string>(this.apiURL + '/UpdateProduct', producto);
  }

  public borrarProducto(id:string){
    let params = new HttpParams();
    params = params.append('id',id);
    return this.http.delete<string>(this.apiURL + '/DeleteProduct', {observe: 'response' , responseType: 'text' as 'json', params});
  }
}
