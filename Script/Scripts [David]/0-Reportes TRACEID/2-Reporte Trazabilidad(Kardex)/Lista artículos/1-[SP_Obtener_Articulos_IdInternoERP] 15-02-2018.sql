ALTER PROCEDURE SP_Obtener_Articulos_IdInternoERP
AS
BEGIN
	SELECT
		 MA.idInterno			AS IdArticuloERP
		,MA.Nombre				AS NombreArticulo
		,(MA.Nombre + ' -- ' + MA.idInterno)
								AS NombreArticuloIDInterno 
		,COUNT(MA.idInterno)	AS CantidadArticulosPorIdInterno 

	FROM 
		 ADMMaestroArticulo MA
	WHERE
		 MA.Activo = 1	
	GROUP BY
		 MA.idInterno
		,MA.Nombre
	ORDER BY 
		MA.Nombre ASC
END
GO
EXEC SP_Obtener_Articulos_IdInternoERP