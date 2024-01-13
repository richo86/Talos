import { HttpParams } from '@angular/common/http';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { CategoriaDTO } from 'src/app/categorias/categorias.models';
import { descuentoDTO } from 'src/app/descuentos/descuentos.models';
import { dataURI, parsearErroresAPI, toBase64 } from 'src/app/utilidades/utilidades';
import Swal from 'sweetalert2';
import { crearProductoDTO, KeyValuePair, productoDTO } from '../productos.models';
import { ProductosService } from '../productos.service';
import { MatChipInputEvent } from '@angular/material/chips';
import {COMMA, ENTER, SPACE} from '@angular/cdk/keycodes';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';

@Component({
  selector: 'app-crear-productos',
  templateUrl: './crear-productos.component.html',
  styleUrls: ['./crear-productos.component.css']
})
export class CrearProductosComponent implements OnInit {

  errores: string[] = [];
  form: FormGroup;
  producto: productoDTO;
  crearProducto: crearProductoDTO = {
    id: null,
    nombre: null,
    descripcion: null,
    inventario: null,
    precio: null,
    fechaCreacion: null,
    fechaModificacion: null,
    categoriaId: null,
    categoriaDescripcion: null,
    subcategoriaId: null,
    subcategoriaDescripcion: null,
    descuentoId: null,
    codigo: null,
    moneda:null,
    keywords:[]
  };
  listadoImagenes: File[] = new Array(0);
  listadoImagenesBase64: KeyValuePair<string, string>[] = [];;
  listadoIds: string[];
  categorias: CategoriaDTO[];
  subcategorias: CategoriaDTO[];
  descuentos: descuentoDTO[];
  productoId: string;
  keywords: string[] = [];
  separatorKeysCodes: number[] = [ENTER, COMMA, SPACE];
  filteredKeywords: string[];

  @ViewChild('keywordInput') keywordInput: ElementRef<HTMLInputElement>;

  constructor(private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private productosService: ProductosService,
              private router: Router) { }

  ngOnInit(): void {
    this.InitializeForm();
    this.GetProduct();
    this.GetDiscounts();
  }

  InitializeForm(){
    this.form = this.formBuilder.group({
      nombre: ['',{validators: [Validators.required]}],
      descripcion: ['',{validators: [Validators.required]}],
      inventario:['',{validators:[Validators.required]}],
      precio: ['',{validators:[Validators.required]}],
      categoriaId: ['',{validators:[Validators.required]}],
      subcategoriaId: ['',{validators:[Validators.required]}],
      codigo: ['',{validators:[Validators.required]}],
      descuentoId: ['',{validators:[]}]
    });
  }

  GetProduct(){
    this.activatedRoute.params.subscribe((params) => {
      if(!!params.id){
        this.productosService.obtenerProducto(params.id).pipe(take(1))
        .subscribe({
          next: res => {
            this.productoId = params.id;
            this.producto = res.body;
            console.log(this.producto);
            this.form.patchValue(this.producto);
            this.keywords = this.producto.keywords;

            this.GetMainCategories();
            this.GetSubcategories();
            this.GetProductImagesBase64();
          },
          error: (error) =>{
            this.errores = parsearErroresAPI(error)
          }
        });
      }else{
        this.GetMainCategories();
        this.GetSubcategories();
      }
    });
  }

  GetProductImagesBase64(){
    this.productosService.obtenerImagenesProductoBase64(this.productoId).pipe(take(1))
      .subscribe({
        next: (res) => {
          this.listadoImagenesBase64 = res.body;
          this.listadoImagenesBase64.forEach(x=>{
            if(!x.value.includes("base64")){
              x.value = dataURI(x.value);
            }
          })
        },
        error: (error) =>{
          this.errores = parsearErroresAPI(error)
        }
      });
  }

  GetMainCategories(){
    this.productosService.obtenerCategoriasPrincipales().pipe(take(1))
      .subscribe({
        next: (res) => {
          this.categorias = res.body;
        },
        error: (errores) => {
          this.errores = parsearErroresAPI(errores);
        }
    });
  }

  GetDiscounts(){
    this.productosService.obtenerDescuentos().pipe(take(1))
      .subscribe({
        next: (res) => {
          this.descuentos = res.body;
        },
        error: (errores) => {
          this.errores = parsearErroresAPI(errores);
        }
    });

    //Default selected discount
    this.form.get('descuentoId').setValue("ninguno");
  }

  GetSubcategories(){
    let categoryId = this.form.get('categoriaId').value;
    this.productosService.obtenerCategoriasSecundarias(this.producto ? this.producto.categoriaId : !!categoryId? categoryId : null).pipe(take(1))
      .subscribe({
        next: (res) => {
          this.subcategorias = res.body;
        },
        error: (errores) => {
          this.errores = parsearErroresAPI(errores);
        }
    }); 
  }

  updateSubcategories(event:any){
    this.GetSubcategories();
  }

  create(){
    this.activatedRoute.params.subscribe((params) => {
      if(!!params.id){
        this.producto = Object.assign(this.producto,this.form.value);
        this.producto.keywords = this.keywords;
        this.productosService.actualizarProducto(this.producto).pipe(take(1))
        .subscribe({
          next: (res) => {
            if(this.listadoImagenes.length > 0){
              this.productosService.subirImagenes(this.listadoImagenes,params.id).pipe(take(1))
              .subscribe({
                next: (res) =>{
                  Swal.fire({
                    text: '¡Operación exitosa!',
                    icon: 'success'
                  });
                },
                error: (error) =>{
                  this.errores = parsearErroresAPI(error);
                }
              });
            }
            Swal.fire({
              text: '¡Operación exitosa!',
              icon: 'success'
            }).then(res =>{
              if(res.isConfirmed)
                this.router.navigate(['/productos']);
            });
          },
          error: (error) => this.errores = parsearErroresAPI(error)
        });
      }else{
        this.crearProducto = Object.assign(this.crearProducto,this.form.value);
        this.crearProducto.keywords = this.keywords;
        if(this.crearProducto.descuentoId == 'ninguno')
          this.crearProducto.descuentoId = null;
        
        this.productosService.crearProducto(this.crearProducto).pipe(take(1))
        .subscribe({
          next: (res) => {
            if(this.listadoImagenes.length > 0){
              this.productosService.subirImagenes(this.listadoImagenes,res.id).pipe(take(1))
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
                this.router.navigate(['/productos']);
            });
          },
          error: (error) => this.errores = parsearErroresAPI(error)
        });
      }
    });
  }

  archivoSeleccionado(file){
    this.listadoImagenes.push(file);
    let newBase64 = "";
    let newImage: KeyValuePair<string, string>;
    toBase64(file).then((value: string) => {
      newBase64 = value;
      newImage = { key: file.name, value: newBase64 };
      this.listadoImagenesBase64.push(newImage);
    }).catch(error => console.log(error));
  }

  deleteImage(id:string){
    this.productosService.borrarImagen(id).pipe(take(1))
      .subscribe({
        next: (res) =>{
          this.listadoImagenesBase64 = this.listadoImagenesBase64.filter(item => item.key !== id);
          this.listadoImagenes = this.listadoImagenes.filter(x=>x.name !== id);
          Swal.fire({
            text: '¡Operación exitosa!',
            icon: 'success'
          });
        },
        error: (error) =>{
          this.listadoImagenesBase64 = this.listadoImagenesBase64.filter(item => item.key !== id);
          this.listadoImagenes = this.listadoImagenes.filter(x=>x.name !== id);
        }
      })
  }

  add(event: MatChipInputEvent): void {
    const value = (event.value || '').trim();

    if (value) {
      this.keywords.push(value);
    }

    // Clear the input value
    event.chipInput!.clear();
  }

  remove(fruit: string): void {
    const index = this.keywords.indexOf(fruit);

    if (index >= 0) {
      this.keywords.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.keywords.push(event.option.viewValue);
    this.keywordInput.nativeElement.value = '';
  }

}
