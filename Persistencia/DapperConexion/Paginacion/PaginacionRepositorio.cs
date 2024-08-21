using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionRepositorio : IPaginacion
    {
        private readonly IFactoryConnection _factoryConnection;
        public PaginacionRepositorio(IFactoryConnection factoryConnection) {
            _factoryConnection = factoryConnection;
        }
        public async Task<PaginacionModel> DevolverPaginacion(
            string storeProcedure,
            int numeroPagina,
            int cantidadElementos,
            IDictionary<string, object>
            parametrosFiltro,
            string ordenamientoColumna
        ) {
            PaginacionModel paginacionModel = new PaginacionModel();
            List<IDictionary<string, object>> listaReporte = null;
            int totalRecords = 0;
            int totalPaginas = 0;
            try
            {
                var connection = _factoryConnection.GetConneciton();
                DynamicParameters parametros = new DynamicParameters();

                foreach (var param in parametrosFiltro)
                {
                    parametros.Add("@" + param.Key, param.Value);
                }

                // parametros de entrada
                parametros.Add("@NumeroPagina", numeroPagina);
                parametros.Add("@CantidadElementos", cantidadElementos);
                parametros.Add("@Ordenamiento", ordenamientoColumna);
                // parametros de salida
                parametros.Add("@TotalRecords", totalRecords, DbType.Int32, ParameterDirection.Output);
                parametros.Add("@TotalPaginas", totalPaginas, DbType.Int32, ParameterDirection.Output);

                var result = await connection.QueryAsync(storeProcedure, parametros, commandType: CommandType.StoredProcedure);
                listaReporte = result.Select(x => (IDictionary<string, object>)x).ToList();
                paginacionModel.ListaRecords = listaReporte;
                paginacionModel.TotalRecords = parametros.Get<int>("@TotalRecords");
                paginacionModel.NumeroPaginas = parametros.Get<int>("@TotalPaginas");
            }
            catch (Exception e)
            {
                throw new Exception("No se pudo ejecutar el procedimiento almacenado", e);
            }
            finally 
            {
                _factoryConnection.CloseConnection();
            }
            
            return paginacionModel;
        }
    }
}