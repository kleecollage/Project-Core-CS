using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Paginacion
{
    public interface IPaginacion
    {
        Task<PaginacionModel> DevolverPaginacion(
            string storeProcedure,
            int numeroPagina,
            int cantidadElementos,
            IDictionary<string, object> parametrosFiltro,
            string ordenamientoColumna
        );
    }
}


/* EJEMPLO DE PROCEDIMIENTO ALMACENADO PARA PAGINACION EN MSSQL

CREATE PROCEDURE usp_obtener_paginacion(
    @NombreCurso NVARCHAR(500),
    @Ordenamiento NVARCHAR(500),
    @NumeroPagina INT,
    @CantidadElementos INT,
    @TotalRecords INT OUTPUT,
    @TotalPaginas INT OUTPUT
) AS
BEGIN

    SET NOCOUNT ON
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

    DECLARE @Inicio INT
    DECLARE @Fin INT

    IF @NumeroPagina = 1
        BEGIN
            SET @Inicio = (@NumeroPagina * @CantidadElementos) - @CantidadElementos
            SET @Fin = @NumeroPagina * @CantidadElementos
        END
    ELSE 
        BEGIN
            SET @Inicio = ( (@NumeroPagina * @CantidadElementos) - @CantidadElementos ) + 1
            SET @Fin = @NumeroPagina * @CantidadElementos
        END

    CREATE TABLE #TMP(
        rowNumber INT IDENTITY(1,1),
        ID UNIQUEIDENTIFIER
    )

    DECLARE @SQL NVARCHAR(max)
    SET @SQL = ' SELECT CursoId FROM Curso '
    
    IF @NombreCurso IS NOT NULL
        BEGIN
            SET @SQL = @SQL + ' WHERE Titulo LIKE ''%' + @NombreCurso + '%'' '
        END

    IF @Ordenamiento IS NOT NULL
        BEGIN 
            SET @SQL = @SQL + ' ORDER BY ' + @Ordenamiento
        END

    -- SELECT CursoId FROM Curso WHERE Titulo LIKE % ASP % ORDER BY Titulo

    INSERT INTO #TMP(ID)
    EXEC sp_executesql @SQL

    SELECT @TotalRecords = COUNT(*) FROM #TMP

    IF @TotalRecords > @CantidadElementos
        BEGIN
            SET @TotalPaginas = @TotalRecords / @CantidadElementos
            IF (@TotalRecords % @CantidadElementos) > 0
                BEGIN
                    SET @TotalPaginas = @TotalPaginas + 1
                END
        END
    ELSE
        BEGIN
            SET @TotalPaginas = 1
        END

    SELECT 
        c.CursoId,
        c.Titulo,
        c.Descripcion,
        c.FechaPublicacion,
        c.FotoPortada,
        c.FechaCreacion
    FROM #TMP t INNER JOIN dbo.Curso c 
                    ON t.ID = c.CursoId
                LEFT JOIN Precio p
                    ON c.CursoId = p.CursoId
    WHERE t.rowNumber >= @Inicio AND t.rowNumber <= @Fin

END


*/