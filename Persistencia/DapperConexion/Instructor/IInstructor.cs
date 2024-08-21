using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public interface IInstructor
    {
        Task<IEnumerable<InstructorModel>> ObtenerLista();
        Task<InstructorModel> ObtenerPorId(Guid id);
        Task<int> Nuevo(string nombre, string apellido, string titulo);
        Task<int> Actualizar(Guid instructorId, string nombre, string apellido, string titulo);
        Task<int> Eliminar(Guid id);
    }
}

/*  EXAMPLE OF STOREPROCEDURE UPDATE ON DB

CREATE PROCEDURE usp_instructor_editar(
    @InstructorId UNIQUEIDENTIFIER,
    @Nombre NVARCHAR(500),
    @Apellidos NVARCHAR(500),
    @Titulo NVARCHAR(100)
)
AS
    BEGIN

        UPDATE Instructor
        SET 
            Nombre = @Nombre,
            Apellidos = @Apellidos,
            Grado = @Titulo
        WHERE InstructorId = @InstructorId

    END
*/