using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IVariablesEconomicasService
    {
        public Task<List<VariablesEconomicaDTO>> ListarVariables();
        public Task<VariablesEconomicaDTO> ListarVariable(int id);
        public Task<decimal> ListarVariable(string nombre);
        public Task<VariablesEconomicaDTO> CrearVariable(VariablesEconomicaDTO variable);
        public Task<VariablesEconomicaDTO> EditarVariable(VariablesEconomicaDTO variable);
        public Task<bool> EliminarVariable(int id);
    }
}
