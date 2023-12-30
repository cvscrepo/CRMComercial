using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class DetalleCotizacionVariable
{
    public int IdDetalleCotizacionVariables { get; set; }

    public int? IdDetalleCotizacion { get; set; }

    public int? IdParametrosVariables { get; set; }

    public decimal? Valor { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual DetalleCotizacion? IdDetalleCotizacionNavigation { get; set; }

    public virtual VariablesEconomica? IdParametrosVariablesNavigation { get; set; }
}
