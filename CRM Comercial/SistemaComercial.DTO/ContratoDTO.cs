using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class ContratoDTO
    {
        public int id { get; set; }

        public string? Nombre { get; set; }

        public int? IdCotizacion { get; set; }

        public string? RepresentanteLegal { get; set; }

        public string? FechaCreacion { get; set; }

        public virtual CotizacionDTO? IdCotizacionNavigation { get; set; }
    }
}
