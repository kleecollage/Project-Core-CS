namespace Persistencia.DapperConexion.Paginacion
{
    public class PaginacionModel
    {
        public List<IDictionary<string, object>> ListaRecords { get; set; }
        // [ { "cursoId": "123", "Titulo": "Asp.Net", ...}, {"cursoId": "1234", "Titulo: "React", ...}, {...}]
        public int TotalRecords { get; set; }
        public int NumeroPaginas { get; set; }
    }
}