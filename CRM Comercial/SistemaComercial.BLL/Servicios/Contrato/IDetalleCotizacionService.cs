using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IDetalleCotizacionService
    {
        public Task<List<DetalleCotizacionDTO>> ListarDetalleCotizacion(int id);
        public Task<DetalleCotizacionDTO> CrearDetalleCotizacion(DetalleCotizacionDTO detalleCotizacion);
        public Task<DetalleCotizacionDTO> EditarDetalleCotizacion(DetalleCotizacionDTO detalleCotizacion);
        public Task<bool> EliminarDetalleCotizacion(int id);
    }
}
