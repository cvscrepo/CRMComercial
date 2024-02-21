using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IDetalleCotizacionVariableService
    {
        public Task<List<DetalleCotizacionVariableDTO>> ListarDetalleVariables();
        public Task<DetalleCotizacionVariableDTO> ListarDetalleVariable(int idDetalleCotizacion);
        public Task<DetalleCotizacionVariableDTO> CrearDetalleVariable(DetalleCotizacionVariableDTO detalle);
        public Task<DetalleCotizacionVariableDTO> EditarDetalleVariable(DetalleCotizacionVariableDTO detalle);
        public Task<bool> EliminarDetalleVariable(int idDetalleCotizacion);
    }
}
