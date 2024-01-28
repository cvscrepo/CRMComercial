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
    public class VariablesEconomicasService : IVariablesEconomicasService
    {
        private readonly IGenericRepository<VariablesEconomicas> _variablesRepositorio;
        private readonly IMapper _mapper;

        public VariablesEconomicasService(IGenericRepository<VariablesEconomicas> variablesRepositorio, IMapper mapper)
        {
            _variablesRepositorio = variablesRepositorio;
            _mapper = mapper;
        }
        public async Task<List<VariablesEconomicaDTO>> ListarVariables()
        {
            try
            {
                var listaVariablesDB = await _variablesRepositorio.Consultar();
                return _mapper.Map<List<VariablesEconomicaDTO>>(listaVariablesDB);
            }
            catch
            {
                throw;
            }
        }

        public async Task<VariablesEconomicaDTO> ListarVariable(int id)
        {
            try
            {
                var listarVariable = await _variablesRepositorio.Obtener((v) => v.IdVariablesEconomicas == id);
                if (listarVariable == null)
                {
                    throw new TaskCanceledException("Variable no encontrada");
                }
                return _mapper.Map<VariablesEconomicaDTO>(listarVariable);
            }
            catch
            {
                throw;
            }
        }

        public async Task<VariablesEconomicaDTO> CrearVariable(VariablesEconomicaDTO variable)
        {
            try
            {
                var variableModelo = _mapper.Map<VariablesEconomicas>(variable);
                var variableCreada = await _variablesRepositorio.Crear(variableModelo);
                if(variableCreada == null)
                {
                    throw new TaskCanceledException("Variable no creada");
                }
                var query = await _variablesRepositorio.Consultar(u => u.IdVariablesEconomicas == variable.IdVariablesEconomicas);
                return _mapper.Map<VariablesEconomicaDTO>(variableModelo);
            }
            catch
            {
                throw;
            }
        }

        public async Task<VariablesEconomicaDTO> EditarVariable(VariablesEconomicaDTO variable)
        {
            try
            {
                var variableEncontrada = await _variablesRepositorio.Obtener((v) => v.IdVariablesEconomicas == variable.IdVariablesEconomicas);
                if(variableEncontrada == null)
                {
                    throw new TaskCanceledException("Variable no encontrada");
                }
                variableEncontrada.Nombre = variable.Nombre;
                variableEncontrada.CreatedBy = variable.CreatedBy;
                variableEncontrada.UpdatedBy = variable.UpdatedBy;
                variableEncontrada.Descripcion = variable.Descripcion;
                variableEncontrada.Valor = Convert.ToDecimal(variable.Valor);
                variableEncontrada.UpdatedAt = DateTime.Now;
                var variableEditada = await _variablesRepositorio.Editar(variableEncontrada);
                return _mapper.Map<VariablesEconomicaDTO>(variableEncontrada);

            }
            catch 
            {
                throw;
            }
        }

        public async Task<bool> EliminarVariable(int id)
        {
            try
            {
                var variableEncontrada = await _variablesRepositorio.Obtener((v) => v.IdVariablesEconomicas == id);
                if (variableEncontrada == null)
                {
                    throw new TaskCanceledException("Variable no encontrada");
                }
                var variableEliminada = await _variablesRepositorio.Eliminar(variableEncontrada);
                
                return variableEliminada;
            }
            catch
            {
                throw;
            }
        }

    }
}
