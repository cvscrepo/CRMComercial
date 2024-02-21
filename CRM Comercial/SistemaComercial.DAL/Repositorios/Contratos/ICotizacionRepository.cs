using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DAL.Repositorios.Contratos
{
    public interface ICotizacionRepository: IGenericRepository<Cotizacion>
    {
        Task<Cotizacion> RegistrarDetalleCotización(Cotizacion cotizacion);
    }
}
