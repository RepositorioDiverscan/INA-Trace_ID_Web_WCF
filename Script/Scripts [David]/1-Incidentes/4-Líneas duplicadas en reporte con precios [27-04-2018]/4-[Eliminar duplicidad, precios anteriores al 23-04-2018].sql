--=======================================
--CANTIDAD DE LÍNEAS POR FECHA
--=======================================
SELECT --DISTINCT
	 COUNT(CONVERT(DATE, CA.FechaRegistro)) CantidadLineas
	,CONVERT(DATE, CA.FechaRegistro) Fecha
	--,* 
FROM 
	ADMCostoArticulos CA
 
--WHERE 
	
	--CA.FechaRegistro >= CONVERT(DATETIME, '23-04-2018')
GROUP BY 
	CONVERT(DATE, CA.FechaRegistro)

--HAVING 
--	COUNT(CONVERT(DATE, CA.FechaRegistro))  > 10
ORDER BY 
	COUNT(CONVERT(DATE, CA.FechaRegistro)) DESC
	,CONVERT(DATE, CA.FechaRegistro)
	--CONVERT(DATE, CA.FechaRegistro)

GO
---CONSULTA UTILIZADA EN DELETE
SELECT 
	*
FROM 
	ADMCostoArticulos
WHERE
	CONVERT(DATE, FechaRegistro) <  CONVERT(DATE, '23-04-2018')
ORDER BY 
	CONVERT(DATE, FechaRegistro) DESC
GO
------[ELIMINACIÓN DE DATOS]---------------->
----------SELECT * FROM ADMCostoArticulos
----------GO
----------DELETE FROM ADMCostoArticulos
----------WHERE
----------	CONVERT(DATE, FechaRegistro) <  CONVERT(DATE, '23-04-2018')
----------GO
----------SELECT * FROM ADMCostoArticulos
