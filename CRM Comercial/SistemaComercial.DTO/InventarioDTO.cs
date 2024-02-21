using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class InventarioDTO
    {
        public int IdInventario { get; set; }

        public int? IdCategoriaInventario { get; set; }

        public string? NombreCategoria { get; set; }

        public int? CreatedBy { get; set; }

        public string Nombre { get; set; } = null!;

        public int CantidadDisponible { get; set; }

        public int CantidadAsignada { get; set; }

        public int CantidadTotal { get; set; }

        public int? Estado { get; set; }

        public string? Valor { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UptadedAt { get; set; }

        public virtual UsuarioDTO? CreatedByNavigation { get; set; }
        public virtual CategoriaInventarioDTO? IdCategoriaInventarioNavigation { get; set; }
    }
}
