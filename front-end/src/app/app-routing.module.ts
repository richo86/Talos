import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutComponent } from './about/about.component';
import { AllProductsComponent } from './all-products/all-products.component';
import { AreasDisplayComponent } from './areas-display/areas-display.component';
import { CategoriasComponent } from './categorias/categorias.component';
import { CecComponent } from './categorias/cec/cec.component';
import { CategoriesDisplayComponent } from './categories-display/categories-display.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { CEDComponent } from './descuentos/ced/ced.component';
import { DescuentosComponent } from './descuentos/descuentos.component';
import { EsAdminGuard } from './es-admin.guard';
import { EsVendorGuard } from './es-vendor.guard';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { LoginComponent } from './login/login.component';
import { PerfilComponent } from './perfil/perfil.component';
import { ProductPageComponent } from './product-page/product-page.component';
import { CrearProductosComponent } from './productos/crear-productos/crear-productos.component';
import { ProductosComponent } from './productos/productos.component';
import { CerComponent } from './regiones/cer/cer.component';
import { RegionesComponent } from './regiones/regiones.component';
import { RegistroComponent } from './seguridad/registro/registro.component';
import { SubcategoriesDisplayComponent } from './subcategories-display/subcategories-display.component';
import { CrearComponent } from './usuarios/crear/crear/crear.component';
import { EditarComponent } from './usuarios/editar/editar/editar.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { ProductCardComponent } from './utilidades/product-card/product-card.component';
import { EditarVentaComponent } from './ventas/editar/editar.component';
import { VentasComponent } from './ventas/ventas.component';

const routes: Routes = [
  {path:'',component:LandingPageComponent},
  {path:'login',component:LoginComponent},
  {path:'registro', component:RegistroComponent},
  {path:'checkout',component:CheckoutComponent},
  {path:'descuentos',component:DescuentosComponent,canActivate:[EsAdminGuard]},
  {path:'perfil',component:PerfilComponent},
  {path:'perfil/:id',component:PerfilComponent},
  {path:'productos',component:ProductosComponent,canActivate:[EsAdminGuard]},
  {path:'usuarios',component:UsuariosComponent,canActivate:[EsAdminGuard]},
  {path:'crear', component: CrearComponent},
  {path:'usuarios/editar/:id',component: EditarComponent},
  {path:'productos/CAE',component:CrearProductosComponent,canActivate:[EsAdminGuard,EsVendorGuard]},
  {path:'productos/CAE/:id',component:CrearProductosComponent,canActivate:[EsAdminGuard,EsVendorGuard]},
  {path:'descuentos',component:DescuentosComponent,canActivate:[EsAdminGuard,EsVendorGuard]},
  {path:'descuentos/CED',component:CEDComponent,canActivate:[EsAdminGuard,EsVendorGuard]},
  {path:'descuentos/CED/:id',component:CEDComponent,canActivate:[EsAdminGuard,EsVendorGuard]},
  {path:'categorias',component:CategoriasComponent,canActivate:[EsAdminGuard,EsVendorGuard]},
  {path:'categorias/CEC',component:CecComponent,canActivate:[EsAdminGuard,EsVendorGuard]},
  {path:'categorias/CEC/:id',component:CecComponent,canActivate:[EsAdminGuard,EsVendorGuard]},
  {path:'ventas',component:VentasComponent,canActivate:[EsAdminGuard,EsVendorGuard]},
  {path:'ventas/editar',component:EditarVentaComponent,canActivate:[EsAdminGuard]},
  {path:'ventas/editar/:id',component:EditarVentaComponent,canActivate:[EsAdminGuard]},
  {path:'regiones',component:RegionesComponent,canActivate:[EsAdminGuard,EsVendorGuard]},
  {path:'regiones/cer',component:CerComponent,canActivate:[EsAdminGuard]},
  {path:'about-us',component:AboutComponent},
  {path:'areas',component:AreasDisplayComponent},
  {path:'categories',component:CategoriesDisplayComponent},
  {path:'subcategories',component:SubcategoriesDisplayComponent},
  {path:'product-page/:id',component:ProductPageComponent},
  {path:'all-products',component:AllProductsComponent},
  {path:'cards',component:ProductCardComponent},
  {path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
