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

        public int IdProducto { get; set; }

        public int IdSucursal { get; set; }

        public int? CantidadServicios { get; set; }

        public string? DetalleServicio { get; set; }

        public string Total { get; set; }

        public string? CreatedAt { get; set; }

        public string? UpdatedAt { get; set; }
    }
}
