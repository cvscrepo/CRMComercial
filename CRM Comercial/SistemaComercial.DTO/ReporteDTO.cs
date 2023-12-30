using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class ReporteDTO
    {
        public string? NumeroDocumento { get; set; }
        public string? FechaRegistro { get; set; }
        public string? TotalVenta { get; set; }
        public string? Servicio { get; set; }
        public string? Cantidad { get; set; }
        public string? Total {  get; set; }
    }
}
