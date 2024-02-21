using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class ServicioDTO
    {
        public int IdServicio { get; set; }

        public int? TipoServicio { get; set; }

        public string? Nombre { get; set; }

        public string? DescripcionCategoria { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual TipoServicioDTO? TipoServicioNavigation { get; set; }
    }
}
