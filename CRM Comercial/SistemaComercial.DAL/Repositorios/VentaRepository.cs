using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SistemaComercial.Model;

namespace SistemaComercial.DAL.Repositorios
{
    public class VentaRepository : GenericRepository<Cotizacion>, IVentaRepository
    {
        private readonly DbcomercialContext _dbComercialContext;

        public VentaRepository(DbcomercialContext dbComercialContext) : base(dbComercialContext) 
        {
            _dbComercialContext = dbComercialContext;
        }

        public Task<Cotizacion> Registrar(Cotizacion cotizacion)
        {
            throw new NotImplementedException();
        }
    }
}
