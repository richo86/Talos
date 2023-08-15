import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoriaDTO } from 'src/app/categorias/categorias.models';
import { ProductosService } from 'src/app/productos/productos.service';
import { paisDTO } from 'src/app/seguridad/formulario-registro/registro';
import { parsearErroresAPI } from 'src/app/utilidades/utilidades';
import Swal from 'sweetalert2';
import { productosRelacionadosDTO, regionesProductoDTO } from '../regiones.models';
import { RegionesService } from '../regiones.service';

@Component({
  selector: 'app-cer',
  templateUrl: './cer.component.html',
  styleUrls: ['./cer.component.css']
})
export class CerComponent implements OnInit {

  regionesProducto : regionesProductoDTO;
  errores: string[] = [];
  form: FormGroup;
  precioEstandar: number;
  categorias: CategoriaDTO[];
  subcategorias: CategoriaDTO[];
  paises: paisDTO[];
  listadoProductosRelacionados : productosRelacionadosDTO[];
  regionesSeleccionadas : string[];
  productosSeleccionados : string[];

  constructor(private formBuilder: FormBuilder,
              private activatedRoute: ActivatedRoute,
              private regionesService: RegionesService,
              private productosService: ProductosService,
              private router: Router) { }

  ngOnInit(): void {
    this.InitializeForm();
    this.GetRegionsProduct();
    this.GetCountries();
    this.GetMainCategories();
    this.GetSecondaryCategories();
  }

  InitializeForm(){
    this.form = this.formBuilder.group({
      nombre: ['',{validators: [Validators.required]}],
      regiones: ['',{validators: [Validators.required]}],
      precio: [''],
      productosRelacionados: ['',{validators: [Validators.required]}],
      inventario:['',{validators:[Validators.required]}]
    });
  }

  GetRegionsProduct(){
    this.activatedRoute.params.subscribe((params) => {
      if(!!params.id){
        this.regionesService.obtenerRegionesProducto(params.id)
        .subscribe({
          next: res => {
            this.regionesProducto = res.body;
            this.form.patchValue(this.regionesProducto);
          },
          error: (error) =>{
            this.errores = parsearErroresAPI(error)
          }
        });
      }
      if(!!params.productoId)
        this.regionesProducto.producto = params.productoId;
    });
  }

  GetCountries(){
    this.regionesService.obtenerPaises()
    .subscribe({
      next: (res) => {
        this.paises = res.body;
      },
      error: (errors) =>{
        this.errores = parsearErroresAPI(errors);
      }
    });
  }

  GetMainCategories(){
    this.productosService.obtenerCategoriasPrincipales()
    .subscribe({
      next: (res) => {
        this.categorias = res.body;
      },
      error: (errors) => {
        this.errores = parsearErroresAPI(errors);
      }
    });
  }

  GetSecondaryCategories(){
    this.productosService.obtenerCategoriasSecundarias()
    .subscribe({
      next: (res) => {
        this.subcategorias = res.body;
      },
      error: (errores) => {
        this.errores = parsearErroresAPI(errores);
      }
    });
  }

  create(){
    this.activatedRoute.params.subscribe((params) => {
      if(!!params.id){
        this.regionesProducto = Object.assign(this.regionesProducto,this.form.value);
        this.regionesProducto.productosRelacionados = this.productosSeleccionados;
        this.regionesProducto.regiones = this.regionesSeleccionadas;
        this.regionesService.actualizarRegiones(this.regionesProducto)
        .subscribe({
          next: (res) => {
            Swal.fire({
              text: '¡Operación exitosa!',
              icon: 'success'
            }).then(res =>{
              if(res.isConfirmed)
                this.router.navigate(['/regiones']);
            });
          },
          error: (error) => this.errores = parsearErroresAPI(error)
        });
      }else{
        this.regionesProducto = Object.assign(this.regionesProducto,this.form.value);
        this.regionesProducto.productosRelacionados = this.productosSeleccionados;
        this.regionesProducto.regiones = this.regionesSeleccionadas;
        this.regionesService.crearRegiones(this.regionesProducto)
        .subscribe({
          next: (res) => {
            Swal.fire({
              text: '¡Operación exitosa!',
              icon: 'success'
            }).then(res =>{
              if(res.isConfirmed)
                this.router.navigate(['/regiones']);
            });
          },
          error: (error) => this.errores = parsearErroresAPI(error)
        });
      }
    });
  }

}
