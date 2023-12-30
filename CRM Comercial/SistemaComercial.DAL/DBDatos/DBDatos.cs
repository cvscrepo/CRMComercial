using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaComercial.DAL.DBDatos.Contrato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DAL.DBDatos
{
    public class DBDatos<TModel> : IDBDatos<TModel> where TModel : class
    {
        private readonly DbcomercialContext _dbcomercialContext;

        public DBDatos(DbcomercialContext dbcomercialContext)
        {
            _dbcomercialContext = dbcomercialContext;
        }

        private List<PropertyInfo> ObtenerPropiedadesDeNavegacion()
        {
            //Obtener todas las propiedades de la clase
            var propiedades = typeof(TModel).GetProperties();

            //Filtrar propiedades que son tipo clase (propiedades de navegación)
            var propiedadesNavegacion = propiedades.Where(p => p.PropertyType.IsClass).ToList();

            return propiedadesNavegacion;
        }
        public int Ejecutar(string nombreProcedimiento, SqlParameter[] parametros = null)
        {
            try
            {
                var parametroSql = parametros?.ToArray() ?? Array.Empty<SqlParameter>();
                var resultados = _dbcomercialContext.Set<TModel>().FromSqlRaw(nombreProcedimiento, parametroSql);
                var propiedadesNavegacion = ObtenerPropiedadesDeNavegacion();

                foreach(var propiedad in propiedadesNavegacion)
                {
                    resultados.Include(propiedad.Name);
                }

                var resultado = resultados.AsEnumerable().ToList();
                return resultado.Count;
            }
            catch
            {
                throw;
            }
        }

        public IQueryable<TModel> Listar(string nombreProcedimiento, List<SqlParameter> parametros = null)
        {
            try
            {
                var parametroSql = parametros?.ToArray() ?? Array.Empty<SqlParameter>();
                IQueryable<TModel> resultados = _dbcomercialContext.Set<TModel>().FromSqlRaw(nombreProcedimiento, parametroSql);
                return resultados;
            }
            catch
            {
                throw;
            }
        }

        public List<TModel> ListarTablas(string procedimiento, List<SqlParameter> parametros = null, Expression<Func<TModel, object>> includeExpression = null)
        {
            try
            {
                var parametroSql = parametros?.ToArray() ?? Array.Empty<SqlParameter>();
                var query = _dbcomercialContext.Set<TModel>().FromSqlRaw(procedimiento, parametroSql).AsEnumerable().ToList();
                
                return query;

            }
            catch
            {
                throw;
            }
        }
    }
}
