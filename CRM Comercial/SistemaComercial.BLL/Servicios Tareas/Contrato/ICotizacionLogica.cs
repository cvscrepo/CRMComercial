using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios_Tareas.Contrato
{
    public interface ICotizacionLogica
    {
        public Task<List<decimal>> CalculoDetalleCotizacion(CotizacionDTO cotizacion);
    }
}
