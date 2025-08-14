using AutoMapper;
using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talos.API.User;

namespace Talos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;
        private readonly INotificationRepository notificationRepository;

        public NotificationController(UserManager<ApplicationUser> userManager, IMapper mapper, INotificationRepository notificationRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.notificationRepository = notificationRepository;
        }

        [HttpGet("GetNotification")]
        public async Task<ActionResult> GetNotification(string id)
        {
            try
            {
                var notification = await notificationRepository.GetNotification(id);

                if (notification.Mensaje != null)
                {
                    var notificationDTO = mapper.Map<Notificaciones, NotificationDTO>(notification);
                    return Ok(notificationDTO);
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar obtener la notificación: {ex.Message}");
            }
        }

        [HttpGet("getAllNotifications")]
        public async Task<ActionResult<List<NotificationDTO>>> GetAllNotifications()
        {
            try
            {
                var notifications = await notificationRepository.GetNotifications();

                if (notifications.Count() != 0)
                {
                    var notificationDTOs = mapper.Map<List<Notificaciones>, List<NotificationDTO>>(notifications);
                    return Ok(notificationDTOs);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al recuperar las notificaciones: {ex.Message}");
            }
        }


        [HttpPost("CreateNotification")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult> CreateNotification(List<NotificationDTO> notificationDTO)
        {
            try
            {
                var errors = validateNotification(notificationDTO);
                if (errors.Count() > 0)
                    return BadRequest(errors);

                Notificaciones notification = mapper.Map<NotificationDTO, Notificaciones>(notificationDTO.FirstOrDefault());

                notification = await notificationRepository.CreateNotification(notification);

                if (notification.Mensaje != null)
                {
                    var result = mapper.Map<Notificaciones, NotificationDTO>(notification);
                    return Ok(result);
                }
                else
                    return BadRequest("No fue posible crear la notificación");
            }
            catch(Exception ex)
            {
                return BadRequest($"Error al intentar crear la notificación: {ex.Message}");
            }
        }

        [HttpPut("UpdateNotification")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<List<NotificationDTO>>> UpdateNotification(List<NotificationDTO> notificationDTO)
        {
            try
            {
                var validation = validateNotification(notificationDTO);
                if (validation.Count() > 0)
                    return BadRequest(validation);

                var updateNotifications = mapper.Map<List<NotificationDTO>, List<Notificaciones>>(notificationDTO);

                var notifications = await notificationRepository.UpdateNotifications(updateNotifications);

                if (notifications.Count() > 0)
                {
                    var result = mapper.Map<List<Notificaciones>, List<NotificationDTO>>(notifications);
                    return Ok(result);
                }
                else
                    return BadRequest("No fue posible actualizar las notificaciones");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar las notificaciones: {ex.Message}");
            }
        }

        [HttpDelete("DeleteNotification")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public async Task<ActionResult<string>> DeleteNotification(string id)
        {
            try
            {
                var result = await notificationRepository.DeleteNotification(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar la notificación: {ex.Message}");
            }
        }

        [HttpGet("GetAllMessages")]
        public async Task<IActionResult> GetAllMessages([FromQuery] PaginacionDTO paginacionDTO)
        {
            try 
            {
                var queryable = notificationRepository.GetAllMessages();
                if (queryable == null)
                    return NotFound();

                await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
                var messages = queryable.OrderBy(x => x.FechaRegistro).Paginar(paginacionDTO).ToList();

                if (messages.Count() != 0)
                {
                    var messagesDTOs = mapper.Map<List<Mensajes>, List<MensajesDTO>>(messages);
                    return Ok(messagesDTOs.GroupBy(x=>x.UsuarioEmail));
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los mensajes: {ex.Message}");
            }
        }

        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages(string email)
        {
            try
            {
                var messages = await notificationRepository.GetMessages(email);

                if (messages.Count() > 0)
                    return Ok(messages);
                else
                    return NotFound("No se encontraron mensajes para el usuario indicado");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener los mensajes: {ex.Message}");
            }
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(MensajesDTO mensajeDTO)
        {
            try
            {
                var errors = validateMessage(mensajeDTO);
                if (errors.Count() > 0)
                    return BadRequest(errors);

                Mensajes message = mapper.Map<MensajesDTO, Mensajes>(mensajeDTO);

                message = await notificationRepository.SendMessage(message);

                if (message.Mensaje != null)
                {
                    var result = mapper.Map<Mensajes, MensajesDTO>(message);
                    return Ok(result);
                }
                else
                    return BadRequest("No fue posible crear la notificación");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al enviar el mensaje: {ex.Message}");
            }
        }

        private List<string> validateNotification(List<NotificationDTO> notifications)
        {
            List<string> errorList = new List<string>();

            foreach (var notification in notifications)
            {
                if (notification.Mensaje == null)
                    errorList.Add("Es necesario especificar el mensaje de la notificación");

                if (notification.UserId == null)
                    errorList.Add("Es necesario incluir el Id del usuario");
            }

            return errorList;
        }

        private List<string> validateMessage(MensajesDTO mensaje)
        {
            List<string> errorList = new List<string>();

            if (mensaje.Mensaje == null)
                errorList.Add("Es necesario especificar el mensaje de la conversación");

            if (mensaje.UsuarioEmail == null)
                errorList.Add("Es necesario incluir el email del usuario");

            return errorList;
        }
    }
}
