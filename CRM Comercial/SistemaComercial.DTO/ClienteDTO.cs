using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class ClienteDTO
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
    }
}
