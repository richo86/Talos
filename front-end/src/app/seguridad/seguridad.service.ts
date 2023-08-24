import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GUID } from '../utilidades/guid';
import { generoDTO, paisDTO } from './formulario-registro/registro';
import { credencialesUsuario, respuestaAutenticacion, usuarioDTO } from './seguridad';

@Injectable({
  providedIn: 'root'
})
export class SeguridadService {

  constructor(private httpClient: HttpClient) { }

  apiUrl : string = environment.apiUrl + 'account';
  private readonly llaveToken = 'token';
  private readonly llaveExpiracion = 'token-expiracion';
  private readonly campoRol = 'role';
  private readonly userGuid = 'userGuid';

  isLoggedIn() : boolean {
    const token = localStorage.getItem(this.llaveToken);

    if(!token)
      return false;

    const expiracion = localStorage.getItem(this.llaveExpiracion);
    const expiracionFecha = new Date(expiracion);

    if(expiracionFecha <= new Date()){
      this.logOut();
      return false;
    }
      

    return true;
  }

  logOut(){
    localStorage.removeItem(this.llaveExpiracion);
    localStorage.removeItem(this.llaveToken);
  }

  getRole() :string{
    return this.obtenerCampoJwt(this.campoRol);
  }

  registrar(usuario: usuarioDTO): Observable<respuestaAutenticacion>{
    return this.httpClient.post<respuestaAutenticacion>(this.apiUrl + '/create',usuario);
  }

  editar(usuario: usuarioDTO){
    return this.httpClient.put<any>(this.apiUrl + "/updateUser",usuario);
  }

  getUser(id: string){
    return this.httpClient.get<usuarioDTO>(this.apiUrl + "/usuario?id=" + id);
  }

  getUserID(email: string){
    let HTTPOptions:Object = {

      headers: new HttpHeaders({
          'Content-Type': 'application/json'
      }),
      responseType: 'text'
   }
    return this.httpClient.get<string>(this.apiUrl + "/getUserID?email=" + email, HTTPOptions);
  }

  login(credenciales: credencialesUsuario): Observable<respuestaAutenticacion>{
    return this.httpClient.post<respuestaAutenticacion>(this.apiUrl + '/signin', credenciales);
  }

  guardarToken(respuestaAutenticacion: respuestaAutenticacion){
    localStorage.setItem(this.llaveToken, respuestaAutenticacion.token);
    localStorage.setItem(this.llaveExpiracion, respuestaAutenticacion.expiracion.toString());
  }

  saveUserId(id:string){
    localStorage.setItem(this.userGuid,id);
  }

  getUserId(){
    return localStorage.getItem(this.userGuid);
  }

  setUserId(){
    const token = this.obtenerToken();
    if(token)
      this.getUserID(this.obtenerCampoJwt('email')).pipe(take(1)).subscribe((res)=>{
        return res;
      });
    else{
      const findLocalId = this.getUserId();
      if(findLocalId)
        return findLocalId;
      else{
        const guid = new GUID().toString();
        this.saveUserId(guid);
        return guid;
      }
    }
  }

  obtenerToken(){
    return localStorage.getItem(this.llaveToken);
  }

  obtenerCampoJwt(campo: string): string{
    const token = localStorage.getItem(this.llaveToken);

    if(!token)
     return '';

    var dataToken = JSON.parse(atob(token.split('.')[1]));
    return dataToken[campo];
  }

  obtenerGeneros(){
    return this.httpClient.get<generoDTO[]>(this.apiUrl + '/generos',{observe: 'response'});
  }

  obtenerPaises(){
    return this.httpClient.get<paisDTO[]>(this.apiUrl + '/paises',{observe: 'response'});
  }

  getUserLocation(){
    return this.httpClient.get<any>("http://ip-api.com/json/",{observe: 'response'});
  }
}