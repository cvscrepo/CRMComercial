using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DAL.Repositorios.Contratos
{
    public interface IVentaRepository: IGenericRepository<Cotizacion>
    {
        Task<Cotizacion> Registrar(Cotizacion cotizacion);
    }
}
