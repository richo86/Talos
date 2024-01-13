import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CampaignsService } from '../campaigns.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { campaignDTO } from '../campaigns.models';
import { Observable, map, startWith, take } from 'rxjs';
import Swal from 'sweetalert2';
import { dataURI, parsearErroresAPI, toBase64 } from 'src/app/utilidades/utilidades';
import { CategoriaDTO } from 'src/app/categorias/categorias.models';
import { ProductosService } from 'src/app/productos/productos.service';
import { KeyValuePair, productoDTO } from 'src/app/productos/productos.models';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';

@Component({
  selector: 'app-cec',
  templateUrl: './cec.component.html',
  styleUrls: ['./cec.component.css']
})
export class CreateCampaignComponent implements OnInit {

  constructor(private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private campañasService: CampaignsService,
    private productosService: ProductosService,
    private router: Router) {
      this.filteredProducts = this.myControl.valueChanges.pipe(
        startWith(null),
        map((product: string | null) => (product ? this._filter(product) : null)),
      );
    }

  form: FormGroup;
  errores: string[] = [];
  categorias: CategoriaDTO[];
  subcategorias: CategoriaDTO[];
  campaignId:string;
  campaign: campaignDTO = {
    id: null,
    nombre: null,
    descripcion: null,
    estado: null,
    porcentajeDescuento: null,
    fechaCreacion: new Date(),
    fechaEdicion: new Date(),
    fechaInicioVigencia: null,
    fechaFinVigencia: null,
    categoria: '',
    subcategoria: '',
    imagen: '',
    imagenBase64: '',
    productos: []
  };
  listadoImagenes: File[] = new Array(0);
  listadoImagenesBase64: KeyValuePair<string, string>[] = [];
  products: string[] = [];
  productIds: string[] = [];
  separatorKeysCodes: number[] = [];
  filteredProducts: Observable<productoDTO[]>;
  myControl = new FormControl('');
  allProducts:productoDTO[] = [];

  @ViewChild('keywordInput') keywordInput: ElementRef<HTMLInputElement>;

  ngOnInit(): void {
    this.initForm();
    this.GetCampaign();
    this.GetAllProducts();
  }

  initForm(){
    this.form = this.formBuilder.group({
      nombre: ['',{validators: [Validators.required]}],
      descripcion: ['',{validators: [Validators.required]}],
      estado:['',{validators:[Validators.required]}],
      fechaInicioVigencia:['',{validators:[Validators.required]}],
      fechaFinVigencia:['',{validators:[Validators.required]}],
      categoria:['',{validators:[]}],
      subcategoria:['',{validators:[]}],
      porcentajeDescuento: ['',{validators:[Validators.required]}]
    });
  }

  GetAllProducts(){
    this.campañasService.getAllProducts().subscribe({
      next:(res) => {
        this.allProducts = res.body;
      }
    })
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

  GetSubcategories(){
    let categoryId = this.form.get('categoria').value;
    this.productosService.obtenerCategoriasSecundarias(!!this.campaign.categoria ? this.campaign.categoria : !!categoryId ? categoryId : null).pipe(take(1))
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


  GetCampaign(){
    this.activatedRoute.params.subscribe((params) => {
      if(!!params.id){
        this.campañasService.obtenerCampaña(params.id).pipe(take(1))
        .subscribe({
          next: res => {
            this.campaignId = params.id;
            this.campaign = res.body;
            this.form.patchValue(this.campaign);
            this.productIds = this.campaign.productos;
            this.productIds.forEach(x=>this.products.push(this.allProducts.find(p=>p.id == x).nombre));
            
            this.GetMainCategories();
            this.GetSubcategories();
            this.GetCampaignImagesBase64();
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

  GetCampaignImagesBase64(){
    this.campañasService.obtenerImagenesCampañaBase64(this.campaignId).pipe(take(1))
      .subscribe({
        next: (res) => {
          this.listadoImagenesBase64 = res.body;
          this.listadoImagenesBase64.forEach(x=>{
            if(!x.value.includes("base64")){
              x.value = dataURI(x.value);
            }
          });
        },
        error: (error) =>{
          this.errores = parsearErroresAPI(error)
        }
      });
  }

  create(){
    this.activatedRoute.params.subscribe((params) => {
      this.campaign = Object.assign(this.campaign,this.form.value);
      if(!!params.id){
        this.campaign.productos = this.productIds;
        this.campañasService.actualizarCampaña(this.campaign).pipe(take(1))
        .subscribe({
          next: (res) => {
            this.deleteImage(this.campaign.imagen);
            if(this.listadoImagenes.length > 0){
              this.campañasService.subirImagenes(this.listadoImagenes,params.id).pipe(take(1))
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
            };
            Swal.fire({
              text: '¡Operación exitosa!',
              icon: 'success'
            }).then(res =>{
              if(res.isConfirmed)
                this.router.navigate(['/campañas']);
            });
          },
          error: (error) => this.errores = parsearErroresAPI(error)
        });
      }else{
        this.campaign.productos = this.productIds;
        this.campañasService.crearCampaña(this.campaign).pipe(take(1))
        .subscribe({
          next: (res) => {
            if(this.listadoImagenes.length > 0){
              this.campañasService.subirImagenes(this.listadoImagenes,res).pipe(take(1))
              .subscribe({
                next: (res) =>{
                },
                error: (error) =>{
                  this.errores = parsearErroresAPI(error)
                }
              });
            };
            Swal.fire({
              text: '¡Operación exitosa!',
              icon: 'success'
              }).then(res =>{
                if(res.isConfirmed)
                  this.router.navigate(['/campañas']);
            });
          },
          error: (error) => this.errores = parsearErroresAPI(error)
        });
      }
    });
  }

  archivoSeleccionado(file){
    this.listadoImagenes.pop();
    this.listadoImagenesBase64.pop();
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
      this.products.push(value);
    }

    // Clear the input value
    event.chipInput!.clear();
  }

  remove(product: string): void {
    const index = this.products.indexOf(product);

    if (index >= 0) {
      this.products.splice(index, 1);
      this.productIds.splice(index, 1);
    }
  }

  selected(event: MatAutocompleteSelectedEvent): void {
    this.products.push(event.option.viewValue);
    this.productIds.push(event.option.value);
    this.keywordInput.nativeElement.value = '';
  }

  private _filter(value: string): productoDTO[] {
    const filterValue = value.toLowerCase();

    return this.allProducts.filter(product => product.nombre.toLowerCase().includes(filterValue));
  }

}
