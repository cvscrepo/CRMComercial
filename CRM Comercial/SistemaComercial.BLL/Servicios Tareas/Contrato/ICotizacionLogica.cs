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
        public Task<decimal> CalculoDetalleCotizacion(CotizacionDTO cotizacion);
        public Task<decimal> CalculoDetalleCotizacion(DetalleCotizacionDTO detalle);
        public Task<CotizacionDTO> RegistrarCotizacion(CotizacionDTO cotizacion);
    }
}
