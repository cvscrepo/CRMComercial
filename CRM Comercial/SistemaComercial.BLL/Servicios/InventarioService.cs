using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
    public class InventarioService : IInventarioService
    {
        private readonly IGenericRepository<Inventario> _inventarioRepository;
        private readonly IMapper _mapper;

        public InventarioService(IGenericRepository<Inventario> inventarioRepository, IMapper mapper)
        {
            _inventarioRepository = inventarioRepository;
            _mapper = mapper;
        }

        public async Task<List<InventarioDTO>> ListarInventario()
        {
            try
            {
                var itemsInventario = await _inventarioRepository.Consultar();
                var query = itemsInventario.Include(c=> c.IdCategoriaInventarioNavigation).Include(i => i.CreatedByNavigation).AsEnumerable().ToList();
                return _mapper.Map<List<InventarioDTO>>(query);
            }
            catch
            {
                throw;
            }
        }
        public async Task<InventarioDTO> ListarItemInventario(int? id)
        {
            try
            {
                var listarItemInventario = await _inventarioRepository.Consultar( i => i.IdInventario == id);
                var query = listarItemInventario.Include(i => i.IdCategoriaInventarioNavigation).Include(i => i.CreatedByNavigation).First();
                return _mapper.Map<InventarioDTO>(query);
            }
            catch
            {
                throw;
            }
        }
        public async Task<InventarioDTO> CrearItemInventrio(InventarioDTO inventario)
        {
            try
            {
                var ItemInventarioCreado = await _inventarioRepository.Crear(_mapper.Map<Inventario>(inventario)) ?? throw new TaskCanceledException("No se pudo crear el item");
                return _mapper.Map<InventarioDTO>(ItemInventarioCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EditarItemInventario(InventarioDTO inventario)
        {
            try
            {
                var itemConsultado = await _inventarioRepository.Obtener(c => c.IdInventario == inventario.IdInventario);
                if (itemConsultado == null) throw new TaskCanceledException("No se encontró el item");
                itemConsultado.IdCategoriaInventario = inventario.IdCategoriaInventario;
                itemConsultado.Nombre = inventario.Nombre;
                itemConsultado.CantidadDisponible = inventario.CantidadDisponible;
                itemConsultado.CantidadAsignada = inventario.CantidadAsignada;
                itemConsultado.CantidadTotal = inventario.CantidadTotal;
                itemConsultado.Estado = inventario.Estado == 1;
                itemConsultado.Valor = Convert.ToDecimal(inventario.Valor);
                itemConsultado.UptadedAt = DateTime.Now;
                var itemEditado = await _inventarioRepository.Editar(itemConsultado);
                return itemEditado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarItemInventario(int id)
        {
            try
            {
                var itemConsultado = await _inventarioRepository.Obtener( i => i.IdInventario == id);
                if (itemConsultado == null) { throw new TaskCanceledException("No se encontró el item a eliminar"); }
                var itemEliminado = await _inventarioRepository.Eliminar(itemConsultado);
                return itemEliminado;
            }
            catch
            {
                throw;
            }
        }


    }
}
