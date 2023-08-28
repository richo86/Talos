using AutoMapper;
using Models;
using Models.Classes;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Talos.API.User;

namespace PeliculasAPI.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UsuarioDTO, ApplicationUser>().ReverseMap();
            CreateMap<UsuarioDTO, Credenciales>();
            CreateMap<CalificacionDTO, Calificacion>();
            CreateMap<GeneroDTO,Genero>().ReverseMap();
            CreateMap<PaisDTO,Pais>().ReverseMap();
            CreateMap<Producto, ProductoDTO>()
                .ForMember(to => to.Id, from => from.MapFrom(src => src.Id.ToString()))
                .ForMember(to => to.Imagenes, from => from.MapFrom(src => ExtractImages(src.Imagenes)))
                .ForMember(to => to.ImagenesBase64, from => from.MapFrom(src => ExtractBase64Images(src.Imagenes)))
                .ForMember(to => to.DescuentoId, from => from.MapFrom(src => src.DescuentoId.ToString()))
                .ForMember(to => to.CategoriaDescripcion, from => from.MapFrom(src => src.Categoria.Descripcion))
                .ForMember(to => to.SubcategoriaDescripcion, from => from.MapFrom(src => src.Subcategoria.Descripcion));
            CreateMap<CrearProductoDTO, Producto>()
                .ForMember(to => to.DescuentoId, from => from.MapFrom(src => src.DescuentoId != null ? src.DescuentoId : null))
                .AfterMap((input, output) =>
                {
                    output.Id = Guid.NewGuid();
                    output.FechaCreacion = DateTime.Now;
                });
            CreateMap<Categorias, CategoriaDTO>()
                .ForMember(to => to.Id, from => from.MapFrom(src => src.Id.ToString()))
                .ForMember(to => to.AreaId, from => from.MapFrom(src => src.Area.ToString()));
            CreateMap<CategoriaDTO, Categorias>()
                .AfterMap((input, output) =>
                {
                    output.Id = Guid.NewGuid();
                });
            CreateMap<Categorias, CategoriaActDTO>()
                .ForMember(to => to.Id, from => from.MapFrom(src => src.Id.ToString()));
            CreateMap<CategoriaActDTO, Categorias>()
                .ForMember(to => to.Id, from => from.MapFrom(src => Guid.Parse(src.Id)));
            CreateMap<CategoriaActDTO, Subcategorias>()
                .ForMember(to => to.Id, from => from.MapFrom(src => Guid.Parse(src.Id)));
            CreateMap<Subcategorias, CategoriaDTO>()
                .ForMember(to => to.Id, from => from.MapFrom(src => src.Id.ToString()));
            CreateMap<CategoriaDTO, Subcategorias>()
                .ForMember(to => to.CategoriaPrincipal, from => from.MapFrom(src => Guid.Parse(src.CategoriaPrincipal)))
                .AfterMap((input, output) =>
                {
                    output.Id = Guid.NewGuid();
                });
            CreateMap<Areas, CategoriaDTO>()
                .ForMember(to => to.Id, from => from.MapFrom(src => src.Id.ToString()));
            CreateMap<CategoriaDTO, Areas>()
                .AfterMap((input, output) =>
                {
                    output.Id = Guid.NewGuid();
                });
            CreateMap<CategoriaActDTO, Areas>()
                .ForMember(to => to.Id, from => from.MapFrom(src => Guid.Parse(src.Id)));
            CreateMap<Descuentos, DescuentoDTO>();
            CreateMap<DescuentoDTO, Descuentos>();
            CreateMap<Carrito, CarritoDTO>();
            CreateMap<CarritoDTO, Carrito>()
                .ForMember(to => to.Id, from => from.MapFrom(src => src.Id == null ? Guid.NewGuid() : Guid.Parse(src.Id)))
                .AfterMap((input, output) =>
                {
                    output.Id = Guid.NewGuid();
                });
            CreateMap<Notificaciones, NotificationDTO>()
                .ForMember(to => to.Id, from => from.MapFrom(src => src.Id.ToString()))
                .ForMember(to => to.Id, from => from.MapFrom(src => src.UserId.ToString()));
            CreateMap<NotificationDTO, Notificaciones>()
                .ForMember(to => to.Estado, from => from.MapFrom(src => src.Estado == null ? true : src.Estado))
                .ForMember(to => to.Id, from => from.MapFrom(src => src.Id == null ? Guid.NewGuid() : Guid.Parse(src.Id)))
                .ForMember(to => to.Id, from => from.MapFrom(src => src.UserId == null ? Guid.NewGuid() : Guid.Parse(src.UserId)));
            CreateMap<MensajesDTO, Mensajes>()
                .ForMember(to => to.UsuarioReceptorEmail, from => from.MapFrom(src => src.UsuarioReceptorEmail == null ? Models.Utilities.Constantes.AdminEmail : src.UsuarioReceptorEmail))
                .AfterMap((input, output) =>
                {
                    output.Id = Guid.NewGuid();
                    output.FechaRegistro = DateTime.Now;
                });
            CreateMap<Mensajes, MensajesDTO>();
        }

        private object ExtractImages(List<Imagenes> imagenes)
        {
            List<string> imagesCodes = new List<string>();
            if (imagenes.Any())
            {
                foreach (var item in imagenes)
                {
                    imagesCodes.Add(item.ImagenUrl);
                }
            }

            return imagesCodes;
        }

        private object ExtractBase64Images(List<Imagenes> imagenes)
        {
            List<KeyValuePair<string,string>> images = new List<KeyValuePair<string, string>>();
            if (imagenes.Any())
            {
                foreach (var item in imagenes)
                {
                    images.Add(new KeyValuePair<string, string>(item.ImagenUrl, item.ImagenBase64));
                }
            }

            return images;
        }
    }
}
