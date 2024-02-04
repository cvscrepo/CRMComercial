using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios
{
    public class ClienteService : IClienteService
    {
        private readonly IGenericRepository<Cliente> _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IGenericRepository<Cliente> clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }
        public async Task<List<ClienteDTO>> ListarClientes()
        {
            try
            {
                var listaClientesBD = await _clienteRepository.Consultar();
                var listaClientes = listaClientesBD.Include("CreatedByNavigation").AsEnumerable().ToList();
                return _mapper.Map<List<ClienteDTO>>(listaClientes);
            }
            catch
            {
                throw;
            }
        }
        public async Task<ClienteDTO> ListarCliente(int id)
        {
            try
            {
                var listaClienteDB = await _clienteRepository.Consultar((c) => c.IdCliente == id);
                var clienteObtenido = listaClienteDB.Include("CreatedByNavigation").FirstOrDefault();
                return _mapper.Map<ClienteDTO>(clienteObtenido);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClienteDTO> CrearCliente(ClienteDTO cliente)
        {
            try
            {
                var clienteModelo = _mapper.Map<Cliente>(cliente);
                var clienteCreado = await _clienteRepository.Crear(clienteModelo);
                if(clienteCreado == null)
                {
                    throw new TaskCanceledException("No se pudo crear el cliente");
                }
                var query = await _clienteRepository.Consultar((c) => c.IdCliente == clienteCreado.IdCliente);
                var clienteAdicionado = query.Include("CreatedByNavigation").FirstOrDefault();
                return _mapper.Map<ClienteDTO>(clienteAdicionado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<ClienteDTO> EditarCliene(ClienteDTO cliente)
        {
            try
            {
                var clienteEncontrado = await _clienteRepository.Obtener((c) => c.IdCliente == cliente.IdCliente);
                if(clienteEncontrado == null){
                    throw new TaskCanceledException("Cliente no encontrado");
                };
                clienteEncontrado.UptadtedBy = cliente.UptadtedBy;
                clienteEncontrado.Nit = cliente.Nit;
                clienteEncontrado.NombreCompleto = cliente.NombreCompleto;
                clienteEncontrado.Email = cliente.Email;
                clienteEncontrado.Prospecto = cliente.Prospecto;
                clienteEncontrado.NombreContacto = cliente.NombreContacto;
                clienteEncontrado.Telefono = cliente.Telefono;
                clienteEncontrado.UptadtedAt = DateTime.Now;
                var clienteEditado = await _clienteRepository.Editar(clienteEncontrado);
                return _mapper.Map<ClienteDTO>(clienteEncontrado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarCliente(int id)
        {
            try
            {
                var clienteConsultado = await _clienteRepository.Obtener((c)=> c.IdCliente == id);
                if( clienteConsultado == null)
                {
                    throw new TaskCanceledException("Cliente a eliminar no encontrado");
                }
                var clienteEliminado = await _clienteRepository.Eliminar(clienteConsultado);
                return clienteEliminado;
            }
            catch
            {
                throw;
            }
        }
    }
}
