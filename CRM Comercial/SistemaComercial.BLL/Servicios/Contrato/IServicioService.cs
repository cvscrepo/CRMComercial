using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IServicioService
    {
        public Task<List<ServicioDTO>> ListarServicios();
        public Task<ServicioDTO> ListarServicio(int id);
        public Task<ServicioDTO> CrearServicio(ServicioDTO servicio);
        public Task<ServicioDTO> EditarServicio(ServicioDTO servicio);
        public Task<bool> EliminarServicio(int id);

    }
}