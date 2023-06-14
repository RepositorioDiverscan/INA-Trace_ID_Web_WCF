SELECT 
	* 
FROM 
	ADMMaestroArticulo MA 
	
WHERE 
	--MA.Nombre  LIKE '%HONGOS%'
	--OR MA.idInterno = 10002
	MA.idInterno IN(2003, 10002)
	AND MA.Activo = 1
GO
--Id Interno 
SELECT 
	(SELECT TOP 1 MA.idInterno FROM ADMMaestroArticulo MA WHERE MA.idArticulo = T.IdArticulo AND MA.Activo = 1)
					AS IdInterno
	,*	
FROM 
	Trazabilidad T
GO	
SELECT * FROM Trazabilidad T

--LISTA DE ARTÍCULOS POR IdInterno
SELECT
	 MA.idInterno			AS IdInternoArticulo
	,MA.Nombre				AS NombreArticulo
	,COUNT(MA.idInterno)	AS CantidadArticulosPorIdInterno 

	--,*
FROM 
	ADMMaestroArticulo MA
WHERE
		MA.Activo = 1
	AND MA.idInterno = '10002'
GROUP BY
	 MA.idInterno
	,MA.Nombre
