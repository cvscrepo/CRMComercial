using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IClienteService
    {
        public Task<List<ClienteDTO>> ListarClientes();
        public Task<ClienteDTO> ListarCliente(int id);
        public Task<ClienteDTO> CrearCliente(ClienteDTO cliente);
        public Task<ClienteDTO> EditarCliene(ClienteDTO cliente);
        public Task<bool> EliminarCliente(int id);
    }
}
