using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public int? CreatedBy { get; set; }

    public int? UptadtedBy { get; set; }

    public int Nit { get; set; }

    public string? NombreCompleto { get; set; }

    public string Email { get; set; } = null!;

    public bool? Prospecto { get; set; }

    public string? NombreContacto { get; set; }

    public string? Telefono { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UptadtedAt { get; set; }

    public virtual ICollection<Cotizacion> Cotizacions { get; set; } = new List<Cotizacion>();

    public virtual Usuario? CreatedByNavigation { get; set; }

    public virtual ICollection<Sucursal> Sucursals { get; set; } = new List<Sucursal>();

    public virtual Usuario? UptadtedByNavigation { get; set; }
}
