using AutoMapper;
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
    public class CategoriaInventaroService : ICategoriaInventarioService
    {
        private readonly IGenericRepository<CategoriaInventario> _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaInventaroService(IGenericRepository<CategoriaInventario> categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }
        public async Task<List<CategoriaInventarioDTO>> ListarCategorias()
        {
            try
            {
                var listarCategorias = await _categoriaRepository.Consultar();
                return _mapper.Map<List<CategoriaInventarioDTO>>(listarCategorias);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoriaInventarioDTO> CrearCategoria(CategoriaInventarioDTO categoria)
        {
            try
            {
                var CrearCategoria = await _categoriaRepository.Crear(_mapper.Map<CategoriaInventario>(categoria)) ?? throw new TaskCanceledException("No se pudo crear la categoría");
                return _mapper.Map<CategoriaInventarioDTO>(CrearCategoria);
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoriaInventarioDTO> EditarCategoria(CategoriaInventarioDTO categoria)
        {
            try
            {
                var listarCategorias = await _categoriaRepository.Obtener(c => c.IdCategoria == categoria.IdCategoria);
                if (listarCategorias == null) throw new TaskCanceledException("No se encontró la categoría a editar");
                listarCategorias.Nombre = categoria.Nombre;
                var categoríaEditada = await _categoriaRepository.Editar(listarCategorias);
                if(!categoríaEditada) throw new TaskCanceledException("No se pudo editar la categoría");
                return _mapper.Map<CategoriaInventarioDTO>(listarCategorias);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarCategoria(int id)
        {
            try
            {
                var listarCategorias = await _categoriaRepository.Obtener(c => c.IdCategoria == id);
                if (listarCategorias == null) throw new TaskCanceledException("No se encontró la categoría a eliminar");
                var categoraiEliminada = await _categoriaRepository.Eliminar(listarCategorias);
                return categoraiEliminada;
            }
            catch
            {
                throw;
            }
        }

    }
}
