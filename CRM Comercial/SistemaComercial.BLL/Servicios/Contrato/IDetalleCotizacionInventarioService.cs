using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IDetalleCotizacionInventarioService
    {
        public Task<List<DetalleCotizacionInventarioDTO>> ListarDetalleCotizacionInventario(int idDetalleCotizacion = 0, int idInventario = 0);
        public Task<DetalleCotizacionInventarioDTO> CrearDetalleCotizacionInventario(DetalleCotizacionInventarioDTO detalleCotizacionInventario);
        public Task<DetalleCotizacionInventarioDTO> EditarDetalleCotizacionInventario(DetalleCotizacionInventarioDTO detalleCotizacionInventario);
        public Task<bool> EliminarDetalleCotizacionInventario(int idDetalleCotizacionInventario);
    }
}
