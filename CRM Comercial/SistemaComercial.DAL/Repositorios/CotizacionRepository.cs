using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SistemaComercial.Model;
using System.Runtime.InteropServices;
using SistemaComercial.DTO;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;

namespace SistemaComercial.DAL.Repositorios
{
    public class CotizacionRepository : GenericRepository<Cotizacion>, ICotizacionRepository
    {
        private readonly DbcomercialContext _dbComercialContext;

        public CotizacionRepository(DbcomercialContext dbComercialContext) : base(dbComercialContext) 
        {
            _dbComercialContext = dbComercialContext;
        }

        public Task<Cotizacion> RegistrarDetalleCotización(CotizacionDTO cotizacion)
        {
            Cotizacion cotizacionGenerada = new Cotizacion();

            using (var transaction = _dbComercialContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (DetalleCotizacionDTO detalle in cotizacion.DetalleCotizacions)
                    {
                        //Aquí se genera la creación del detalleCotizacion junto con la lógica de calcular el total
                        var formula = detalle.IdServicioNavigation.TipoServicioNavigation.Formula;
                        if (detalle.IdServicioNavigation.TipoServicioNavigation.Nombre.ToLower() == "servicio vigilancia")
                        {
                          
                        }
                    }
                }
                catch
                {
                    throw;
                }
                throw new NotImplementedException();
            }
        }
    }
}
