using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Classes;
using Talos.API.User;

namespace Domain.Utilities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<Models.Areas> Areas { get; set; }
        public DbSet<Models.Categorias> Categorias { get; set; }
        public DbSet<Models.Subcategorias> Subcategorias { get; set; }
        public DbSet<Descuentos> Descuentos { get; set; }
        public DbSet<DetallePedidos> DetallePedidos { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Pagos> Pagos { get; set; }
        public DbSet<ItemsPedido> ItemsPedido { get; set; }
        public DbSet<Pagos> Transacciones { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Sesion> Sesion { get; set; }
        public DbSet<TipoVenta> TipoVenta { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Notificaciones> Notificaciones { get; set; }
        public DbSet<Mensajes> Mensajes { get; set; }
        public DbSet<Imagenes> Imagenes { get; set; }
        public DbSet<RegionesProducto> RegionesProductos { get; set; }
        public DbSet<ProductosRelacionados> ProductosRelacionados { get; set; }
    }
}
