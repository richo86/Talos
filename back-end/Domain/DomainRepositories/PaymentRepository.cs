using Domain.Interfaces;
using Domain.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talos.API.User;

namespace Domain.DomainRepositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public PaymentRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<string> CreatePayment(PaymentDTO paymentDTO)
        {
            Pagos pago = new Pagos();
            DetallePedidos detalle = new DetallePedidos();
            List<ItemsPedido> itemsPedido = new List<ItemsPedido>();

            //Buscar usuario o crearlo
            var persona = await userManager.FindByEmailAsync(paymentDTO.Carrito.FirstOrDefault().Email);

            if(persona.Email != null)
            {
                detalle.UsuarioId = persona.Id;
            }
            else
            {
                var user = new ApplicationUser();
                user.Id = Guid.NewGuid().ToString();
                user.Address = paymentDTO.Usuario.Address;
                user.Email = paymentDTO.Usuario.Email;
                user.FirstName = paymentDTO.Usuario.FirstName;
                user.MiddleName = paymentDTO.Usuario.MiddleName;
                user.FirstLastName = paymentDTO.Usuario.FirstLastName;
                user.SecondLastName = paymentDTO.Usuario.SecondLastName;
                user.Country = paymentDTO.Usuario.Country;
                user.UserIP = paymentDTO.Usuario.UserIP;
                user.PhoneNumber = paymentDTO.Usuario.PhoneNumber;

                var creacionUsuario = await userManager.CreateAsync(user);

                if (!creacionUsuario.Succeeded)
                    return "Error al crear el usuario";

                detalle.UsuarioId = user.Id;
            }

            //Crear el pago
            pago.Id = Guid.NewGuid();
            pago.MetodoPago = paymentDTO.MetodoPago;
            pago.Observaciones = paymentDTO.Observaciones;
            pago.FechaEstimada = DateTime.Now.AddDays(14);

            //Obtener productos
            decimal totalPago = 0;
            detalle.Id = Guid.NewGuid();
            detalle.PagoId = pago.Id;
            detalle.FechaCreacion = DateTime.Now;
            detalle.FechaModificacion = DateTime.Now;

            foreach (var item in paymentDTO.Carrito)
            {
                var producto = context.Producto.FirstOrDefault(x => x.Id.Equals(item.ProductoId));
                totalPago = totalPago + (producto.Precio * item.Cantidad);
                itemsPedido.Add(new ItemsPedido()
                {
                    Id = Guid.NewGuid(),
                    DetallePedidosId = detalle.Id,
                    ProductoId = producto.Id,
                    Cantidad = item.Cantidad,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                });
            }

            detalle.ValorTotal = totalPago;
            pago.Valor = totalPago;

            var result = context.SaveChanges();

            if (result > 0)
                return "¡Operación exitosa!";
            else
                return "Error al procesar el pago";
        }

        public async Task<PaymentDTO> UpdatePayment(PaymentDTO paymentDTO)
        {
            var payment = await context.Pagos.FirstOrDefaultAsync(x => x.Id.Equals(paymentDTO.Id));

            if (payment != null)
            {
                payment.Valor = paymentDTO.Valor;
                payment.Observaciones = paymentDTO.Observaciones;
                payment.MetodoPago = paymentDTO.MetodoPago;
                payment.FechaModificacion = DateTime.Now;

                context.Pagos.Update(payment);

                var result = context.SaveChanges();

                if (result > 0)
                    return paymentDTO;
                else
                    return new PaymentDTO();
            }
            else
                return new PaymentDTO();
        }

        public async Task<DetallePedidosDTO> GetPayment(string id)
        {
            var detailsId = Guid.Parse(id);

            var detallesPedido = new DetallePedidosDTO();
            var pedido = (from a in context.DetallePedidos
                          join b in context.ItemsPedido on a.Id equals b.DetallePedidosId
                          join c in context.Pagos on a.PagoId equals c.Id
                          join d in context.Users on a.UsuarioId equals d.Id
                          join e in context.TipoVenta on c.TipoVenta equals e.Id
                          where a.Id == detailsId
                          select new DetallePedidosDTO
                          {
                              Id = a.Id.ToString(),
                              NombreUsuario = d.FirstName + " " + d.MiddleName + " " + d.FirstLastName + " " + d.SecondLastName,
                              EmailUsuario = d.Email,
                              TotalVenta = a.ValorTotal,
                              MetodoPago = c.MetodoPago,
                              Estado = c.Estado,
                              Observaciones = c.Observaciones,
                              TipoVenta = e.Descripcion,
                              FechaActualizacion = a.FechaModificacion
                          }).FirstOrDefault();

            if (pedido != null)
            {
                detallesPedido.ItemsPedido = await GetItems(pedido.Id);

                return detallesPedido;
            }
            else
                return new DetallePedidosDTO();
        }

        public async Task<List<ItemsPedidoDTO>> GetItems(string Id)
        {
            var detailId = Guid.Parse(Id);

            var itemsPedido = await (from a in context.DetallePedidos
                               join b in context.ItemsPedido on a.Id equals b.DetallePedidosId
                               join c in context.Pagos on a.PagoId equals c.Id
                               join d in context.Producto on b.ProductoId equals d.Id
                               join e in context.Imagenes on d.Id equals e.ProductoId
                               where a.Id == detailId
                               select new ItemsPedidoDTO
                               {
                                   ProductoId = b.ProductoId,
                                   Producto = d.Nombre,
                                   Imagen = e.ImagenUrl,
                                   Valor = d.Precio
                               }).ToListAsync();

            return itemsPedido;
        }

        public async Task<IQueryable<DetallePedidosDTO>> GetPayments()
        {
            var detalles = new List<DetallePedidosDTO>();
            var pedidos = (from a in context.DetallePedidos
                          join b in context.ItemsPedido on a.Id equals b.DetallePedidosId
                          join c in context.Pagos on a.PagoId equals c.Id
                          join d in context.Users on a.UsuarioId equals d.Id
                          join e in context.TipoVenta on c.TipoVenta equals e.Id
                          select new DetallePedidosDTO
                          {
                              Id = a.Id.ToString(),
                              NombreUsuario = d.FirstName + " " + d.MiddleName + " " + d.FirstLastName + " " + d.SecondLastName,
                              EmailUsuario = d.Email,
                              TotalVenta = a.ValorTotal,
                              MetodoPago = c.MetodoPago,
                              Estado = c.Estado,
                              Observaciones = c.Observaciones,
                              TipoVenta = e.Descripcion,
                              FechaActualizacion = a.FechaModificacion
                          });

            foreach (var pedido in pedidos)
            {
                if (pedido != null)
                {
                    foreach (var item in pedidos)
                    {
                        item.ItemsPedido = await GetItems(item.Id);
                    }
                    return pedidos;
                }
                else
                    return new List<DetallePedidosDTO>().AsQueryable();
            }

            return detalles.AsQueryable();
        }

        public async Task<List<TipoVenta>> GetPaymentTypes()
        {
            List<TipoVenta> tiposVenta = new List<TipoVenta>();

            tiposVenta = await context.TipoVenta.Where(x => x.Descripcion != null).ToListAsync();

            return tiposVenta;
        }
    }
}
