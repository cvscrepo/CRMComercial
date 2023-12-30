using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class Inventario
{
    public int IdInventario { get; set; }

    public int? IdCategoriaInventario { get; set; }

    public int? CreatedBy { get; set; }

    public string Nombre { get; set; } = null!;

    public int CantidadDisponible { get; set; }

    public int CantidadAsignada { get; set; }

    public int CantidadTotal { get; set; }

    public bool? Estado { get; set; }

    public decimal? Valor { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UptadedAt { get; set; }

    public virtual Usuario? CreatedByNavigation { get; set; }

    public virtual ICollection<DetalleCotizacionInventario> DetalleCotizacionInventarios { get; set; } = new List<DetalleCotizacionInventario>();

    public virtual CategoriaInventario? IdCategoriaInventarioNavigation { get; set; }
}
