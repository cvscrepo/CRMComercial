using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface ISucursalService
    {
        public Task<List<SucursalDTO>> ListarSucursales();
        public Task<SucursalDTO> ListarSucursal(int id);
        public Task<SucursalDTO> CrearSucursal(SucursalDTO sucursal);
        public Task<SucursalDTO> EditarSucursal(SucursalDTO sucursal);
        public Task<SucursalDTO> EliminarSucursal(int id);
    }
}
