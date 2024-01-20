using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface ITipoService
    {
        public Task<List<TipoServicioDTO>> ListarTiposServicio();
        public Task<TipoServicioDTO> ListarTipoServicio();
        public Task<TipoServicioDTO> CrearTipoServicio(TipoServicioDTO tipoServicio);
        public Task<bool> EditarTipoServicio(TipoServicioDTO tipoServicio);
        public Task<bool> EliminarTipoServicio(TipoServicioDTO tipoServicio);
    }
}
