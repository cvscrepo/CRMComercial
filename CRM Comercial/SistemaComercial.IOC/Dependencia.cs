using Microsoft.EntityFrameworkCore;
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
        }
    }
}
