﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaComercial.BLL.Servicios;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DAL;
using SistemaComercial.DAL.DBDatos;
using SistemaComercial.DAL.DBDatos.Contrato;
using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DAL.Repositorios;
using SistemaComercial.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SistemaComercial.BLL.Servicios_Tareas.Contrato;
using SistemaComercial.BLL.Servicios_Tareas;

namespace SistemaComercial.IOC
{
    public static class Dependencia
    {
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbcomercialContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQl"))
            );
            services.AddTransient(typeof(IDBDatos<>), typeof(DBDatos<>));
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddScoped<IRolService, RoleService>();
            services.AddScoped<IUsurioService, UsuarioService>();
            services.AddScoped<ITipoService, TipoService>();
            services.AddScoped<IVariablesEconomicasService, VariablesEconomicasService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<ISucursalService, SucursalService>();
            services.AddScoped<ICotizacionService, CotizacionService>();
            services.AddScoped<IContratoService, ContratoService>();
            services.AddScoped<ICategoriaInventarioService, CategoriaInventaroService>();
            services.AddScoped<IInventarioService, InventarioService>();
            services.AddScoped<IServicioService, ServicioService>();
            services.AddScoped<IDetalleCotizacionService, DetalleCotizacionService>();
            services.AddScoped<IDetalleCotizacionInventarioService, DetalleCotizacionInventarioService>();
            services.AddScoped<IDetalleCotizacionVariableService, DetalleCotizacionVariableService>();
            services.AddScoped<ICotizacionLogica, CotizacionLogicaService>();
            services.AddScoped<ILoginService, LoginService>();
        }
    }
}
