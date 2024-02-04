using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface ICotizacionService
    {
        public Task<List<CotizacionDTO>> ListarCotizaciones();
        public Task<CotizacionDTO> ListarCotizacion(int id);
        public Task<CotizacionDTO> CrearCotizacion(CotizacionDTO cotizacion);
        public Task<CotizacionDTO> EditarCotizacion(CotizacionDTO cotizacion);
        public Task<bool> EliminarCotizacion(int id);
    }
}
