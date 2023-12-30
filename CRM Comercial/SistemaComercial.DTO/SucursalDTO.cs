using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class SucursalDTO
    {
        public int IdSucursal { get; set; }

        public int? IdCliente { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Direccion { get; set; }

        public string? CreatedAt { get; set; }

        public string? UpdatedAt { get; set; }
    }
}
