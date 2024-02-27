using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class DetalleCotizacionVariableDTO
    {
        public int IdDetalleCotizacionVariables { get; set; }

        public int? IdDetalleCotizacion { get; set; }

        public int? IdVariablesEconomicas { get; set; }

        public string? Valor { get; set; }

        public string? CreatedAt { get; set; }

        public string? UpdatedAt { get; set; }

        public virtual VariablesEconomicaDTO? IdVariablesEconomicasNavigation { get; set; }
    }
}
