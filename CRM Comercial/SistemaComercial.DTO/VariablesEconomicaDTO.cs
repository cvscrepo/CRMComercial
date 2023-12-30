using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class VariablesEconomicaDTO
    {
        public int IdVariablesEconomicas { get; set; }

        public int? CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public string Valor { get; set; }

        public string? CreatedAt { get; set; }

        public string? UpdatedAt { get; set; }
    }
}
