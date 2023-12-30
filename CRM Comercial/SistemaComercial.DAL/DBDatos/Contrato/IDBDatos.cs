using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DAL.DBDatos.Contrato
{
    public interface IDBDatos<TModel> where TModel : class
    {
        public List<TModel> ListarTablas(string procedimiento, List<SqlParameter> parametros = null, Expression<Func<TModel, object>> includeExpression = null);
        public IQueryable<TModel> Listar(string nombreProcedimiento, List<SqlParameter> parametros = null);
        public int Ejecutar(string nombreProcedimiento, SqlParameter[] parametros = null);
    }
}
