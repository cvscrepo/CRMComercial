using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class Menu
{
    public int IdMenu { get; set; }

    public string? Nombre { get; set; }

    public string? Icono { get; set; }

    public string? Url { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UptadtedAt { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; set; } = new List<MenuRol>();
}
