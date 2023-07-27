using Models;
using Models.Classes;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IPaymentRepository
    {
        Task<List<TipoVenta>> GetPaymentTypes();
        Task<IQueryable<DetallePedidosDTO>> GetPayments();
        Task<DetallePedidosDTO> GetPayment(string id);
        Task<string> CreatePayment(PaymentDTO paymentDTO);
        Task<PaymentDTO> UpdatePayment(PaymentDTO paymentDTO);
    }
}
