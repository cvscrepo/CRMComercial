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
        public async Task<CategoriaInventarioDTO> ListarCategorias()
        {
            try
            {
                var listarCategorias = await _categoriaRepository.Consultar();
                return _mapper.Map<CategoriaInventarioDTO>(listarCategorias);
            }
            catch
            {
                throw;
            }
        }

        public Task<CategoriaInventarioDTO> CrearCategoria(CategoriaInventarioDTO categoria)
        {
            throw new NotImplementedException();
        }

        public Task<CategoriaInventarioDTO> EditarCategoria(CategoriaInventarioDTO categoria)
        {
            throw new NotImplementedException();
        }

        public Task<CategoriaInventarioDTO> EliminarCategoria(int id)
        {
            throw new NotImplementedException();
        }

    }
}
