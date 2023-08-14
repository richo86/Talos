import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MaterialModule } from './material/material.module';
import { MenuComponent } from './menu/menu.component';
import { MostrarErroresComponent } from './utilidades/mostrar-errores/mostrar-errores.component';
import { RatingComponent } from './utilidades/rating/rating.component';
import { AutorizacionComponent } from './seguridad/autorizacion/autorizacion.component';
import { LoginComponent } from './login/login.component';
import { RegistroComponent } from './seguridad/registro/registro.component';
import { AutenticacionComponent } from './seguridad/autenticacion/autenticacion.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormularioRegistroComponent } from './seguridad/formulario-registro/formulario-registro.component';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { InterceptorService } from './seguridad/interceptor.service';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatMenuModule } from '@angular/material/menu';
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { PerfilComponent } from './perfil/perfil.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { ProductosComponent } from './productos/productos.component';
import { DescuentosComponent } from './descuentos/descuentos.component';
import { NotificacionesComponent } from './notificaciones/notificaciones.component';
import { ListadoGenericoComponent } from './utilidades/listado-generico/listado-generico.component';
import { CrearComponent } from './usuarios/crear/crear/crear.component';
import { EditarComponent } from './usuarios/editar/editar/editar.component';
import { CabeceraGenericaComponent } from './utilidades/cabecera-generica/cabecera-generica.component';
import { CrearProductosComponent } from './productos/crear-productos/crear-productos.component';
import { CargarImagenComponent } from './utilidades/cargar-imagen/cargar-imagen.component';
import { CEDComponent } from './descuentos/ced/ced.component';
import { CategoriasComponent } from './categorias/categorias.component';
import { CecComponent } from './categorias/cec/cec.component';
import { MensajesComponent } from './mensajes/mensajes.component';
import { VentasComponent } from './ventas/ventas.component';
import { MatDialogModule } from '@angular/material/dialog';
import { DetalleVentaComponent } from './ventas/detalle-venta/detalle-venta.component';
import { EditarVentaComponent } from './ventas/editar/editar.component';
import { RegionesComponent } from './regiones/regiones.component';
import { CerComponent } from './regiones/cer/cer.component';
import { CategoriesDisplayComponent } from './categories-display/categories-display.component';
import {NgxGalleryModule} from 'ngx-gallery';
import { ImageGalleryComponent } from './utilidades/image-gallery/image-gallery.component';
import { CarouselComponent } from './utilidades/carousel/carousel.component';
import { AreasDisplayComponent } from './areas-display/areas-display.component';
import { SubcategoriesDisplayComponent } from './subcategories-display/subcategories-display.component';
import { AboutComponent } from './about/about.component';
import { ReturnPolicyComponent } from './return-policy/return-policy.component';
import { ProductPageComponent } from './product-page/product-page.component';
import { ProductCardComponent } from './utilidades/product-card/product-card.component';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import { AllProductsComponent } from './all-products/all-products.component';
import {MatGridListModule} from '@angular/material/grid-list';
import { FooterComponent } from './footer/footer.component';

@NgModule({
  declarations: [
    AppComponent,
    LandingPageComponent,
    MenuComponent,
    MostrarErroresComponent,
    RatingComponent,
    AutorizacionComponent,
    LoginComponent,
    RegistroComponent,
    AutenticacionComponent,
    FormularioRegistroComponent,
    CartComponent,
    CheckoutComponent,
    PerfilComponent,
    UsuariosComponent,
    ProductosComponent,
    DescuentosComponent,
    NotificacionesComponent,
    ListadoGenericoComponent,
    CrearComponent,
    EditarComponent,
    CabeceraGenericaComponent,
    CrearProductosComponent,
    CargarImagenComponent,
    CEDComponent,
    CategoriasComponent,
    CecComponent,
    MensajesComponent,
    VentasComponent,
    DetalleVentaComponent,
    EditarVentaComponent,
    RegionesComponent,
    CerComponent,
    CategoriesDisplayComponent,
    ImageGalleryComponent,
    CarouselComponent,
    AreasDisplayComponent,
    SubcategoriesDisplayComponent,
    AboutComponent,
    ReturnPolicyComponent,
    ProductPageComponent,
    ProductCardComponent,
    AllProductsComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    ReactiveFormsModule,
    HttpClientModule,
    SweetAlert2Module.forRoot(),
    MatSidenavModule,
    FormsModule,
    MatMenuModule,
    MatDialogModule,
    NgxGalleryModule,
    MatCardModule,
    MatButtonModule,
    MatGridListModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: InterceptorService,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
