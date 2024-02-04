using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class Contrato
{
    public int id { get; set; }

    public int Nit {  get; set; }

    public string? Nombre { get; set; }

    public int? IdCotizacion { get; set; }

    public string? RepresentanteLegal { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaEdicion { get; set; }

    public virtual Cotizacion? IdCotizacionNavigation { get; set; }
}
