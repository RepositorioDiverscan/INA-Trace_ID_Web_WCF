SELECT
	--COUNT(*)
	--MA.IdInterno,
	--COUNT(MA.IdInterno)  AS CANTIDADREGISTROS
	* 
FROM 
	RELArticuloXSolicitudAjuste RSA
	INNER JOIN SolicitudAjusteInventario SA
	ON RSA.IdSolicitudAjusteInventario = SA.IdSolicitudAjusteInventario
	INNER JOIN ADMMaestroArticulo MA
	ON RSA.IdArticulo = MA.idArticulo

WHERE 
	SA.IdSolicitudAjusteInventario IN(5,6,10,11,12)
	--SA.IdSolicitudAjusteInventario IN(12)
	AND
	SA.Estado = 2
	AND 
	CONVERT(DATE, SA.FechaAprobado) = CONVERT(DATE, '20-02-2018') 
GROUP BY
	MA.IdInterno
	
having 
	COUNT(MA.IdInterno) > 1
GO
