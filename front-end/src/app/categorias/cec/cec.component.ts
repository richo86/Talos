import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { KeyValuePair } from 'src/app/productos/productos.models';
import { ProductosService } from 'src/app/productos/productos.service';
import { dataURI, parsearErroresAPI, toBase64 } from 'src/app/utilidades/utilidades';
import Swal from 'sweetalert2';
import { CategoriaDTO, TipoCategoria } from '../categorias.models';
import { CategoriasService } from '../categorias.service';

@Component({
  selector: 'app-cec',
  templateUrl: './cec.component.html',
  styleUrls: ['./cec.component.css']
})
export class CecComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private categoriasService: CategoriasService,
              private productosService: ProductosService,
              private router: Router) { }

  form: FormGroup;
  errores: string[] = [];
  paginaActual = 1;
  cantidadRegistrosMostrados = 12;
  categoriasPrincipales: CategoriaDTO[];
  areasNegocio: CategoriaDTO[];
  categoria: CategoriaDTO = {
    id:null,
    descripcion:null,
    codigo: null,
    tipoCategoria:null,
    area:null,
    areaDescripcion:null,
    categoriaPrincipal:null,
    categoriaPrincipalDescripcion:null,
    imagen:null,
    imagenBase64:null,
    areaId:null
  };
  tipoCategorias:TipoCategoria[] = [
    {value : 0, descripcion : 'Área de negocio'},
    {value : 1, descripcion : 'Principal'},
    {value : 2, descripcion : 'Secundario'}
  ];
  selectedType: number = 0;
  imagenCategoria: File;
  imagenActual: string;

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      descripcion: ['',{validators: [Validators.required]}],
      tipoCategoria: ['',{validators:[Validators.required]}],
      area: ['',{validators:[Validators.required]}],
      categoriaPrincipal: ['',{validators:[Validators.required]}]
    });

    this.form.get('categoriaPrincipal').setValue('ninguna');
    this.form.get('area').setValue('ninguna');

    this.activatedRoute.params.subscribe((params) => {
      if(!!params.id){
        this.categoriasService.obtenerArea(params.id).pipe(take(1))
        .subscribe({
          next: res => {
            this.imagenActual = !!res.body.imagenBase64 ? dataURI(res.body.imagenBase64) : dataURI(res.body.imagen);
            this.categoria = res.body;
            this.categoria.categoriaPrincipal = 'ninguna';
            this.categoria.area = 'ninguna';

            this.form.patchValue(this.categoria);
          },
          error: () => {
            this.categoriasService.obtenerCategoria(params.id).pipe(take(1))
            .subscribe({
              next: (categoryres) => {
                this.categoriasService.obtenerAreas(this.paginaActual,this.cantidadRegistrosMostrados).pipe(take(1))
                .subscribe({
                  next: (result) => {
                    this.areasNegocio = result.body;
                    this.imagenActual = !!categoryres.body.imagenBase64 ? dataURI(categoryres.body.imagenBase64) : dataURI(categoryres.body.imagen);
                    this.categoria = categoryres.body;
                    this.categoria.categoriaPrincipal = 'ninguna';
                    this.categoria.areaDescripcion = this.areasNegocio.find(x=>x.id == this.categoria.areaId.toUpperCase())?.descripcion;
                    this.categoria.area = this.areasNegocio.find(x=>x.id == this.categoria.areaId.toUpperCase())?.id;
                    this.form.patchValue(this.categoria);
                  }
                });
              },
              error: () => {
                this.categoriasService.obtenerCategoriaSecundaria(params.id).pipe(take(1))
                  .subscribe({
                    next: res => {
                      this.productosService.obtenerCategoriasPrincipales().pipe(take(1))
                      .subscribe({
                        next: (result) => {
                          this.categoriasPrincipales = result.body;
                          this.imagenActual = !!res.body.imagenBase64 ? dataURI(res.body.imagenBase64) : dataURI(res.body.imagen);
                          this.categoria = res.body;
                          this.categoria.area = 'ninguna';
                          this.categoria.categoriaPrincipalDescripcion = this.categoriasPrincipales.find(x=>x.id == this.categoria.categoriaPrincipal)?.id;
                          this.form.patchValue(this.categoria);
                        }
                      });
                    },
                    error: errores =>{
                      this.errores = parsearErroresAPI(errores);
                    }
                  });
              }
            });
          }
        });
      }else{
        this.categoriasService.obtenerAreas(this.paginaActual,this.cantidadRegistrosMostrados).pipe(take(1))
        .subscribe({
          next: (result) => {
            this.areasNegocio = result.body;
          }
        });
        this.productosService.obtenerCategoriasPrincipales().pipe(take(1))
        .subscribe({
          next: (result) => {
            this.categoriasPrincipales = result.body;
          }
        });
      }
    });

  }

  create(){
    this.activatedRoute.params.subscribe((params) => {
      this.categoria = Object.assign(this.categoria,this.form.value);
      if(!!params.id){
        if(!!this.categoria.imagen || !!this.imagenCategoria){
          this.categoriasService.actualizarCategoria(this.categoria).pipe(take(1))
          .subscribe({
            next: (res) => {
              if(!!this.imagenCategoria){
                if(!!this.categoria.imagen)
                  this.categoriasService.borrarImagen(this.categoria.imagen).pipe(take(1)).subscribe();
                  
                this.categoriasService.subirImagen(this.imagenCategoria,this.categoria.id).pipe(take(1))
                .subscribe({
                  next: (res) =>{
                  },
                  error: (error) =>{
                    this.errores = parsearErroresAPI(error)
                  }
                });
              }
              Swal.fire({
                text: '¡Operación exitosa!',
                icon: 'success'
              }).then(res =>{
                if(res.isConfirmed)
                  this.router.navigate(['/categorias']);
              });
            },
            error: (error) => this.errores = parsearErroresAPI(error)
          });
        }else{
          this.errores.push("Es necesario incluir una imagen")
        }
      }else{
        if(!!this.categoria.imagen || !!this.imagenCategoria){
          this.categoriasService.crearCategoria(this.categoria).pipe(take(1))
          .subscribe({
            next: (res) => {
              if(!!this.imagenCategoria.size){
                this.categoriasService.subirImagen(this.imagenCategoria,res.id).pipe(take(1))
                .subscribe({
                  next: (res) =>{
                  },
                  error: (error) =>{
                    this.errores = parsearErroresAPI(error)
                  }
                });
              }
              else{
                this.errores.push("Es necesario incluir una imagen")
              }
              Swal.fire({
                text: '¡Operación exitosa!',
                icon: 'success'
              }).then(res =>{
                if(res.isConfirmed)
                  this.router.navigate(['/categorias']);
              });
            },
            error: (error) => this.errores = parsearErroresAPI(error)
          });
        }else{
          this.errores.push('Es necesario incluir una imagen');
        }
      }
    });
  }

  archivoSeleccionado(file){
    this.imagenCategoria = file;
    let newBase64 = "";
    let newImage: KeyValuePair<string, string>;
    toBase64(file).then((value: string) => {
      newBase64 = value;
      this.imagenActual = newBase64;
    }).catch(error => console.log(error));
  }
}
