using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IInventarioService
    {
        public Task<List<InventarioDTO>> ListarInventario();
        public Task<InventarioDTO> ListarItemInventario(int? id);
        public Task<InventarioDTO> CrearItemInventrio(InventarioDTO inventario);
        public Task<bool> EditarItemInventario(InventarioDTO inventario);
        public Task<bool> EliminarItemInventario(int id);
    }
}
