using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IContratoService
    {
        public Task<List<ContratoDTO>> ListarContratos();
        public Task<ContratoDTO> ListarContrato(int id);
        public Task<ContratoDTO> CrearContrato(ContratoDTO contratoDTO);
        public Task<ContratoDTO> EditarContrato(ContratoDTO contratoDTO);
        public Task<bool> EliminarContrato(int id);
    }
}
