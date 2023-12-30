using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class TipoServicioDTO
    {
        public int IdTipoServicio { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Formula { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdateAt { get; set; }
    }
}
