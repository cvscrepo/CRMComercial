using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class DetalleCotizacionInventarioDTO
    {
        public int Id { get; set; }

        public int? IdDetalleCotizacion { get; set; }

        public int? CreatedBy { get; set; }

        public int IdInventario { get; set; }

        public int CantidadAsignada { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual InventarioDTO? IdInventarioNavigation { get; set; }
    }
}
