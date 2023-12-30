using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public int? IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<DetalleCotizacion> DetalleCotizacions { get; set; } = new List<DetalleCotizacion>();

    public virtual Cliente? IdClienteNavigation { get; set; }
}
