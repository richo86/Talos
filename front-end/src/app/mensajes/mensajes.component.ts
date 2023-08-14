import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SeguridadService } from '../seguridad/seguridad.service';
import { MessagesDTO } from './mensajes.models';
import { MensajesService } from './mensajes.service';

@Component({
  selector: 'app-mensajes',
  templateUrl: './mensajes.component.html',
  styleUrls: ['./mensajes.component.css']
})
export class MensajesComponent implements OnInit {

  constructor(private securityService: SeguridadService,
              private mensajesService: MensajesService,
              private activatedRoute: ActivatedRoute) { }

  @Input()
  emailReceiver: string;
  messages: MessagesDTO[];
  email: string = this.securityService.obtenerCampoJwt('email');
  messageText: string;

  //Interval function
  findMessages = setInterval(() => this.getMessages(), 10000);

  ngOnInit(): void {
    this.GetEmailReceiver();
  }

  GetEmailReceiver(){
    if(this.emailReceiver == undefined){
      this.activatedRoute.params.subscribe((params) => {
        this.emailReceiver = params.emailReceiver;
      });
    }
  }

  ngOnDestroy() {
    clearInterval(this.findMessages);
  }

  getMessages(){
    this.mensajesService.GetMessages(this.email)
      .subscribe({
        next: res => {
          this.messages = res.body;
        }
      });
  }

  sendMessage(){
    let messageDTO: MessagesDTO = {
      id: null,
      mensaje: this.messageText,
      fechaRegistro: null,
      usuarioEmail: this.email,
      usuarioReceptorEmail: this.emailReceiver
    };

    this.mensajesService.SendMessage(messageDTO)
  }

}
