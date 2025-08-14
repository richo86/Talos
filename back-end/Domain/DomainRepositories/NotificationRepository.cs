using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Models.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.DomainRepositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext context;

        public NotificationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Notificaciones> CreateNotification(Notificaciones notificacion)
        {
            await context.Notificaciones.AddAsync(notificacion);

            var result = context.SaveChanges();

            if (result > 0)
                return notificacion;
            else
                return new Notificaciones();
        }

        public async Task<string> DeleteNotification(string id)
        {
            var notificacion = await context.Notificaciones.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (notificacion != null)
            {
                context.Notificaciones.Remove(notificacion);
                var result = context.SaveChanges();
                if (result > 0)
                    return "Operación exitosa";
                else
                    return "Ocurrió un error al eliminar la notificación";
            }
            else
                return "No se encontró la notificación que se desea eliminar";
        }

        public IQueryable<Mensajes> GetAllMessages()
        {
            var mensajes = context.Mensajes.Where(x => x.Mensaje != null).GroupBy(x=>x.UsuarioEmail).FirstOrDefault().AsQueryable();

            if (mensajes.Count() > 0)
                return mensajes;
            else
                return new List<Mensajes>().AsQueryable();
        }

        public async Task<List<Mensajes>> GetMessages(string email)
        {
            var mensajes = context.Mensajes.Where(x => x.UsuarioEmail.Equals(email)).ToList();

            return mensajes;
        }

        public async Task<Notificaciones> GetNotification(string id)
        {
            var notification = await context.Notificaciones.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (notification != null)
                return notification;
            else
                return new Notificaciones();
        }

        public async Task<List<Notificaciones>> GetNotifications()
        {
            var notificaciones = context.Notificaciones.Where(x => x.Mensaje != null).ToList();

            if (notificaciones.Count() > 0)
                return notificaciones;
            else
                return new List<Notificaciones>();
        }

        public async Task<Mensajes> SendMessage(Mensajes mensaje)
        {
            await context.Mensajes.AddAsync(mensaje);
            var result = context.SaveChanges();

            if (result > 0)
                return mensaje;
            else
                return new Mensajes();
        }

        public async Task<List<Notificaciones>> UpdateNotifications(List<Notificaciones> notificaciones)
        {
            foreach (var item in notificaciones)
            {
                var notificacion = await context.Notificaciones.FirstOrDefaultAsync(x => x.Id.Equals(item.Id));

                context.Entry(notificacion).CurrentValues.SetValues(item);
            }

            var result = context.SaveChanges();

            if (result > 0)
                return notificaciones;
            else
                return new List<Notificaciones>();
        }
    }
}
