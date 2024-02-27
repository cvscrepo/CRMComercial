using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class CotizacionDTO
    {
        public int IdCotizacion { get; set; }

        public int? IdCliente { get; set; }

        public int? IdUsuario { get; set; }

        public string? Nombre { get; set; }

        public int? EditadoPor { get; set; }

        public int? Estado { get; set; }

        public string? Total { get; set; }

        public string? CreatedAt { get; set; }

        public string? UpdatedAt { get; set; }

        public virtual ICollection<DetalleCotizacionDTO> DetalleCotizacions { get; set; } = new List<DetalleCotizacionDTO>();

        public virtual ClienteDTO? IdClienteNavigation { get; set; }

        public virtual UsuarioDTO? IdUsuarioNavigation { get; set; }
    }
}
