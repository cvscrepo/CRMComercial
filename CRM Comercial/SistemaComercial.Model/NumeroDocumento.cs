using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class NumeroDocumento
{
    public int IdNumeroDocumento { get; set; }

    public int UltimoNumero { get; set; }

    public DateTime? CreatedAt { get; set; }
}
