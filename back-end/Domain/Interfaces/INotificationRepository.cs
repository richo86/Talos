using Models;
using Models.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface INotificationRepository
    {
        Task<List<Notificaciones>> GetNotifications();
        Task<Notificaciones> GetNotification(string id);
        Task<Notificaciones> CreateNotification(Notificaciones notificacion);
        Task<List<Notificaciones>> UpdateNotifications(List<Notificaciones> notificaciones);
        Task<string> DeleteNotification(string id);
        IQueryable<Mensajes> GetAllMessages();
        Task<List<Mensajes>> GetMessages(string email);
        Task<Mensajes> SendMessage(Mensajes mensaje);

    }
}
