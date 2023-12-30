using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class VariablesEconomica
{
    public int IdVariablesEconomicas { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Valor { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Usuario? CreatedByNavigation { get; set; }

    public virtual ICollection<DetalleCotizacionVariable> DetalleCotizacionVariables { get; set; } = new List<DetalleCotizacionVariable>();

    public virtual Usuario? UpdatedByNavigation { get; set; }
}
