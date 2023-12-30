using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class DetalleCotizacionInventario
{
    public int Id { get; set; }

    public int? IdDetalleCotizacion { get; set; }

    public int? CreatedBy { get; set; }

    public int? IdInventario { get; set; }

    public int CantidadAsignada { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Usuario? CreatedByNavigation { get; set; }

    public virtual DetalleCotizacion? IdDetalleCotizacionNavigation { get; set; }

    public virtual Inventario? IdInventarioNavigation { get; set; }
}
