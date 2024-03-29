﻿using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface ICategoriaInventarioService
    {
        public Task<List<CategoriaInventarioDTO>> ListarCategorias();
        public Task<CategoriaInventarioDTO> CrearCategoria(CategoriaInventarioDTO categoria);
        public Task<CategoriaInventarioDTO> EditarCategoria(CategoriaInventarioDTO categoria);
        public Task<bool> EliminarCategoria(int id);
    }
}
