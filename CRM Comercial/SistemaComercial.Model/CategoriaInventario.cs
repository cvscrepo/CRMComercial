﻿using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class CategoriaInventario
{
    public int IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
