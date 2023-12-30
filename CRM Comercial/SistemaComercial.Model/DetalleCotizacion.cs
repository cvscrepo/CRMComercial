using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class DetalleCotizacion
{
    public int IdDetalleCotizacion { get; set; }

    public int IdCotizacion { get; set; }

    public int IdProducto { get; set; }

    public int IdSucursal { get; set; }

    public int? CantidadServicios { get; set; }

    public string? DetalleServicio { get; set; }

    public decimal Total { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<DetalleCotizacionInventario> DetalleCotizacionInventarios { get; set; } = new List<DetalleCotizacionInventario>();

    public virtual ICollection<DetalleCotizacionVariable> DetalleCotizacionVariables { get; set; } = new List<DetalleCotizacionVariable>();

    public virtual Cotizacion IdCotizacionNavigation { get; set; } = null!;

    public virtual Servicio IdProductoNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
