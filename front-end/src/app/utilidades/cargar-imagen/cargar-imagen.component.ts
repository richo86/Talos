import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { toBase64 } from '../utilidades';

@Component({
  selector: 'app-cargar-imagen',
  templateUrl: './cargar-imagen.component.html',
  styleUrls: ['./cargar-imagen.component.css']
})
export class CargarImagenComponent implements OnInit {

  constructor() { }

  imagenBase64: string;

  @Input()
  imagenActual: string;

  @Output()
  archivoSeleccionado: EventEmitter<File> = new EventEmitter<File>();

  ngOnInit(): void {
  }

  change(e){
    if(e.target.files.length > 0){
      const file: File = e.target.files[0];
      toBase64(file).then((value: string) => this.imagenBase64 = "")
        .catch(error => console.log(error));

      this.imagenActual = null;
      this.archivoSeleccionado.emit(file);
    }
  }

}
