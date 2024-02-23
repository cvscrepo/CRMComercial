using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class InformacionDetalleCotizacionDTO
    {
        public int CantidadServicios {  get; set; }
        public int DiasServicio { get; set; }
        public int MinutosInicioServicio { get; set; }
        public int MinutosFinServicio { get; set; }
        public bool Armado {  get; set; }
        public decimal Smlv { get; set; }
    }
}
