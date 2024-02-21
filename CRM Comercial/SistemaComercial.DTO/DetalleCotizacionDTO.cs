using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class DetalleCotizacionDTO
    {
        public int IdDetalleCotizacion { get; set; }

        public int IdCotizacion { get; set; }

        public int IdServicio { get; set; }

        public int IdSucursal { get; set; }

        public int? CantidadServicios { get; set; }

        public string? DetalleServicio { get; set; }

        public string Total { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<DetalleCotizacionInventarioDTO>? DetalleCotizacionInventarios { get; set; } = new List<DetalleCotizacionInventarioDTO>();

        public virtual ICollection<DetalleCotizacionVariableDTO>? DetalleCotizacionVariables { get; set; } = new List<DetalleCotizacionVariableDTO>();

        public virtual ServicioDTO? IdServicioNavigation { get; set; } = null!;

        public virtual SucursalDTO? IdSucursalNavigation { get; set; } = null!;
    }
}
