using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public int IdRol { get; set; }

        public int? UsuarioRegistro { get; set; }

        public string NombreCompleto { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Contrasena { get; set; } = null!;

        public string? UrlFoto { get; set; }

        public string? RolDescription { get; set; }

        public int? EsActivo { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UptadedAt { get; set; }

        public DateTime? LastConnection { get; set; }
    }
}
