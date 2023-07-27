import { Component, OnInit } from '@angular/core';
import { NotificacionDTO } from './notificaciones.model';

@Component({
  selector: 'app-notificaciones',
  templateUrl: './notificaciones.component.html',
  styleUrls: ['./notificaciones.component.css']
})
export class NotificacionesComponent implements OnInit {

  notifications: NotificacionDTO[];

  constructor() { }

  ngOnInit(): void {
  }

}
