using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int IdRol { get; set; }

    public int? UsuarioRegistro { get; set; }

    public string NombreCompleto { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string? UrlFoto { get; set; }

    public bool? EsActivo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UptadedAt { get; set; }

    public DateTime? LastConnection { get; set; }

    public virtual ICollection<Cliente> ClienteCreatedByNavigations { get; set; } = new List<Cliente>();

    public virtual ICollection<Cliente> ClienteUptadtedByNavigations { get; set; } = new List<Cliente>();

    public virtual ICollection<Cotizacion> CotizacionEditadoPorNavigations { get; set; } = new List<Cotizacion>();

    public virtual ICollection<Cotizacion> CotizacionIdUsuarioNavigations { get; set; } = new List<Cotizacion>();

    public virtual ICollection<DetalleCotizacionInventario> DetalleCotizacionInventarios { get; set; } = new List<DetalleCotizacionInventario>();

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<Usuario> InverseUsuarioRegistroNavigation { get; set; } = new List<Usuario>();

    public virtual Usuario? UsuarioRegistroNavigation { get; set; }

    public virtual ICollection<VariablesEconomica> VariablesEconomicaCreatedByNavigations { get; set; } = new List<VariablesEconomica>();

    public virtual ICollection<VariablesEconomica> VariablesEconomicaUpdatedByNavigations { get; set; } = new List<VariablesEconomica>();
}
