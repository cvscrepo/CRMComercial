﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SistemaComercial.DTO;
using SistemaComercial.Model;

namespace SistemaComercial.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // origen - destino
            #region Rol
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<RolDTO, Rol>().ReverseMap();
            #endregion

            #region Menu
            CreateMap<Menu, MenuDTO>().ReverseMap();
            #endregion

            #region Usuario
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(destino=>
                destino.RolDescription,
                opt => opt.MapFrom(origen => origen.IdRolNavigation.Nombre)
                )
                .ForMember(destino => 
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == true ? 1 : 0)
                );

            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(destino =>
                   destino.IdRolNavigation,
                   opt => opt.Ignore()
                )                
                .ForMember(destino =>
                destino.EsActivo,
                opt => opt.MapFrom(origen => origen.EsActivo == 1 ? true : false)
                );

            CreateMap<Usuario, SesionDTO>()
                .ForMember(destino =>
                    destino.RolDescription,
                    opt => opt.MapFrom(origen => origen.IdRolNavigation)
                );
            #endregion

            #region Servicio
            CreateMap<Servicio, ServicioDTO>()
                .ForMember(destino =>
                   destino.DescripcionCategoria,
                   opt => opt.MapFrom(origen => origen.TipoServicioNavigation.Nombre)
                );
            CreateMap<ServicioDTO, Servicio>()
                .ForMember(destino =>
                    destino.Descripcion,
                    opt => opt.MapFrom(origen => origen.DescripcionCategoria)
                );
            #endregion

            #region Tipo servicio
            CreateMap<TipoServicio, TipoServicioDTO>()
                .ReverseMap();
            CreateMap<TipoServicioDTO, TipoServicio>()
                .ReverseMap();
            #endregion

            #region Cotización
            CreateMap<Cotizacion, CotizacionDTO>()
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-CO")))
                )
                .ForMember(destino =>
                    destino.CreatedAt,
                    opt => opt.MapFrom(origen => origen.CreatedAt.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destino =>
                    destino.UpdatedAt,
                    opt => opt.MapFrom(origen => origen.UpdatedAt.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<CotizacionDTO, Cotizacion>()
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-CO")))
                )
                .ForMember(destino =>
                    destino.CreatedAt,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.UpdatedAt,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.DetalleCotizacions,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.IdClienteNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino => 
                    destino.IdUsuarioNavigation,
                    opt => opt.Ignore()
                )
                ;

            #endregion

            #region Cliente
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<ClienteDTO, Cliente>().ReverseMap();

            #endregion

            #region Contrato
            CreateMap<Contrato, ContratoDTO>()
                .ForMember(destino =>
                    destino.Nit,
                    opt => opt.MapFrom(origen => origen.IdCotizacionNavigation.IdClienteNavigation.Nit)
                )
                .ForMember(destino =>
                    destino.Cotizacion,
                    opt => opt.MapFrom(origen => origen.IdCotizacionNavigation)
                );

            #endregion

            #region Categoria inventario
            CreateMap<CategoriaInventario, CategoriaInventarioDTO>().ReverseMap();
            CreateMap<CategoriaInventarioDTO, CategoriaInventario>().ReverseMap();
            #endregion

            #region Detalle Cotización
            CreateMap<DetalleCotizacion, DetalleCotizacionDTO>()
                .ForMember(destino =>
                    destino.DetalleServicio,
                    opt => opt.MapFrom(origen => origen.IdServicioNavigation.Descripcion)
                )
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => origen.Total.ToString())
                )
                .ForMember(destino =>
                    destino.CreatedAt,
                    opt => opt.MapFrom(origen => origen.CreatedAt.Value.ToString())
                )
                .ForMember(destino =>
                    destino.UpdatedAt,
                    opt => opt.MapFrom(origen => origen.UpdatedAt.Value.ToString())
                );
            CreateMap<DetalleCotizacionDTO, DetalleCotizacion>()
                .ForMember(destino =>
                    destino.Total,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Total, new CultureInfo("es-CO")))
                )
                .ForMember(destino =>
                    destino.CreatedAt,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.UpdatedAt,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.DetalleCotizacionInventarios,
                    opt => opt.Ignore() 
                )
                .ForMember(destino => 
                    destino.DetalleCotizacionVariables,
                    opt => opt.Ignore()
                )
                .ForMember(destino => 
                    destino.IdCotizacionNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.IdServicioNavigation,
                    opt => opt.Ignore()
                )
                .ForMember(destino => 
                    destino.IdSucursalNavigation,
                    opt => opt.Ignore()
                )
                ;
            #endregion

            #region Detalle cotizacion inventario
            CreateMap<DetalleCotizacionInventario, DetalleCotizacionInventarioDTO>()
                .ReverseMap();
            CreateMap<DetalleCotizacionInventarioDTO, DetalleCotizacionInventario>()
                .ReverseMap();
            #endregion

            #region Detalle Cotizacion Variable
            CreateMap<DetalleCotizacionVariable, DetalleCotizacionVariableDTO>()
                .ForMember(destino =>
                    destino.Valor,
                    opt => opt.MapFrom(origen => origen.Valor.ToString())
                )
                .ForMember(destino =>
                    destino.CreatedAt,
                    opt => opt.MapFrom(origen => origen.CreatedAt)
                  )
                .ForMember(destino => 
                    destino.UpdatedAt,
                    opt => opt.MapFrom(origen => origen.UpdatedAt)
                );
            CreateMap<DetalleCotizacionVariableDTO, DetalleCotizacionVariable>()
                .ForMember(destino =>
                    destino.CreatedAt,
                    opt => opt.Ignore()
                    )
                .ForMember(destino =>
                    destino.UpdatedAt,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.Valor,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Valor))
                )
                .ForMember(destino =>
                    destino.IdVariablesEconomicasNavigation,
                    opt => opt.Ignore()
                ); 
            #endregion

            #region Inventario
            CreateMap<Inventario, InventarioDTO>()
                .ForMember(destino =>
                    destino.Valor,
                    opt => opt.MapFrom(origen => origen.Valor.Value.ToString())
                )
                .ForMember(destino =>
                    destino.NombreCategoria,
                    opt => opt.MapFrom(origen => origen.IdCategoriaInventarioNavigation.Nombre)
                )
                .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(origen => origen.Estado == true ? 1 : 0)
                )
                .ForMember(destino =>
                    destino.CreateAt,
                    opt => opt.MapFrom(origen => origen.CreateAt.Value.ToString())
                )
                .ForMember(destino =>
                    destino.UptadedAt,
                    opt => opt.MapFrom(origen => origen.UptadedAt.Value.ToString())
                );

            CreateMap<InventarioDTO, Inventario>()
                .ForMember(destino =>
                    destino.Valor,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Valor))
                )
                .ForMember(destino =>
                    destino.Estado,
                    opt => opt.MapFrom(origen => origen.Estado == 1)
                );
            #endregion

            #region Sucursal 
            CreateMap<Sucursal, SucursalDTO>()
                .ForMember(destino =>
                    destino.CreatedAt,
                    opt => opt.MapFrom(origen => origen.CreatedAt.ToString())
                )
                .ForMember(destino =>
                    destino.UpdatedAt,
                    opt => opt.MapFrom(origen => origen.UpdatedAt.ToString())
                );
            CreateMap<SucursalDTO, Sucursal>()
                .ForMember(destino =>
                    destino.CreatedAt,
                    opt => opt.Ignore()
                )
                .ForMember(destino =>
                    destino.UpdatedAt,
                    opt => opt.Ignore()
                );
            #endregion

            #region variables economicas
            CreateMap<VariablesEconomicas, VariablesEconomicaDTO>()
                .ForMember(destino => 
                    destino.Valor,
                    opt => opt.MapFrom(origen => origen.Valor.ToString())
                )

                .ForMember(destino =>
                    destino.CreatedAt,
                    opt => opt.MapFrom(origen => origen.CreatedAt.Value.ToString())
                )
                .ForMember(destino =>
                    destino.UpdatedAt,
                    opt => opt.MapFrom(origen => origen.UpdatedAt.Value.ToString())
                );
            CreateMap<VariablesEconomicaDTO, VariablesEconomicas>()
                .ForMember(destino =>
                    destino.Valor,
                    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Valor))
                )
                .ForMember(destino =>
                    destino.CreatedAt,
                    opt => opt.Ignore()
                    //opt => opt.MapFrom(origen => DateTime.ParseExact(origen.CreatedAt, "dd/MM/yyyy", CultureInfo.InvariantCulture))
                )
                .ForMember(destino =>
                    destino.UpdatedAt,
                    opt => opt.Ignore()
                );
            #endregion

        }
    }
}
