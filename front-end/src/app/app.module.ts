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
import { DisplayItemsComponent } from './display-items/display-items.component';
import { AppearDirective } from './utilidades/appear';
import { CardSliderComponent } from './utilidades/card-slider/card-slider.component';
import { AreaCardComponent } from './area-card/area-card.component';
import { SearchStoreComponent } from './search-store/search-store.component';
import {MatDividerModule} from '@angular/material/divider';
import { CategoriesNavigationComponent } from './categories-navigation/categories-navigation.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from 'src/environments/environment';
import { cartReducers } from './utilidades/Redux/Cart/cartReducer';
import { productReducers, productsFromAreaReducers, productsFromCategoryReducers, productsFromSubcategoryReducers } from './utilidades/Redux/Products/productReducer';
import { bestReducers } from './utilidades/Redux/BestSellers/bestReducer';
import { areasReducers } from './utilidades/Redux/Areas/areasReducer';
import { categoriesReducers } from './utilidades/Redux/Categories/categoriesReducer';
import { discountsReducers } from './utilidades/Redux/Discounts/discountsReducer';
import { lowestCostReducers } from './utilidades/Redux/LowestCost/lowestCostReducer';
import { subcategoriesReducers } from './utilidades/Redux/Subcategories/subcategoriesReducer';
import { CampaignsComponent } from './campaigns/campaigns.component';
import { CreateCampaignComponent } from './campaigns/cec/cec.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatChipsModule } from '@angular/material/chips';
import { EffectsModule } from '@ngrx/effects';
import { CartEffects } from './utilidades/Redux/Cart/cartEffects';
import { AreasEffects } from './utilidades/Redux/Areas/areasEffects';
import { BestSellersEffects } from './utilidades/Redux/BestSellers/bestEffects';
import { CategoriesEffects } from './utilidades/Redux/Categories/categoriesEffects';
import { DiscountsEffects } from './utilidades/Redux/Discounts/discountsEffects';
import { LowestCostEffects } from './utilidades/Redux/LowestCost/lowestCostEffects';
import { ProductsEffects, ProductsFromAreaEffects, ProductsFromCategoryEffects, ProductsFromSubcategoryEffects } from './utilidades/Redux/Products/productEffects';
import { SubcategoriesEffects } from './utilidades/Redux/Subcategories/SubcategoriesEffects';
import { SocialLoginComponent } from './social-login/social-login.component';

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
    FooterComponent,
    DisplayItemsComponent,
    AppearDirective,
    CardSliderComponent,
    AreaCardComponent,
    SearchStoreComponent,
    CategoriesNavigationComponent,
    FavoritesComponent,
    CampaignsComponent,
    CreateCampaignComponent,
    SocialLoginComponent,
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
    MatFormFieldModule,
    MatMenuModule,
    MatDialogModule,
    MatCardModule,
    MatButtonModule,
    MatGridListModule,
    MatDividerModule,
    MatChipsModule,
    StoreModule.forRoot({ 
      areas: areasReducers,
      productsFromArea: productsFromAreaReducers,
      productsFromCategory: productsFromCategoryReducers,
      productsFromSubcategory: productsFromSubcategoryReducers,
    }),
    EffectsModule.forRoot(),
    StoreDevtoolsModule.instrument({
      maxAge:25,
      logOnly:environment.production,
      autoPause:true
    }),
    StoreModule.forFeature('Cart',cartReducers),
    StoreModule.forFeature('Products', productReducers),
    StoreModule.forFeature('Best Sellers', bestReducers),
    StoreModule.forFeature('Areas', areasReducers),
    StoreModule.forFeature('Categories', categoriesReducers),
    StoreModule.forFeature('Discounts', discountsReducers),
    StoreModule.forFeature('Lowest Cost', lowestCostReducers),
    StoreModule.forFeature('Subcategories', subcategoriesReducers),
    StoreModule.forFeature('ProductsFromArea', productsFromAreaReducers),
    StoreModule.forFeature('ProductsFromCategory', productsFromCategoryReducers),
    StoreModule.forFeature('ProductsFromSubcategory', productsFromSubcategoryReducers),
    EffectsModule.forFeature([CartEffects]),
    EffectsModule.forFeature([AreasEffects]),
    EffectsModule.forFeature([BestSellersEffects]),
    EffectsModule.forFeature([CategoriesEffects]),
    EffectsModule.forFeature([DiscountsEffects]),
    EffectsModule.forFeature([LowestCostEffects]),
    EffectsModule.forFeature([ProductsEffects]),
    EffectsModule.forFeature([SubcategoriesEffects]),
    EffectsModule.forFeature([ProductsFromAreaEffects]),
    EffectsModule.forFeature([ProductsFromCategoryEffects]),
    EffectsModule.forFeature([ProductsFromSubcategoryEffects]),
  ],
  providers: [
    {
    provide: HTTP_INTERCEPTORS,
    useClass: InterceptorService,
    multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
