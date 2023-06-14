SELECT --TOP 10
	* 
FROM 
	ADMCostoArticulos CA
 
--WHERE 
	--CA.IdArticulo = 25
	--CA.FechaRegistro >= CONVERT(DATETIME, '23-04-2018')

ORDER BY
	CA.IdArticulo, CA.FechaRegistro


	 select count(idArticulo) from ADMCostoArticulos where CAST(FechaRegistro as date) = '20180423'
  select count(idArticulo) from admmaestroarticulo where Activo = 1
GO




SELECT --TOP 10
	* 
FROM 
	ADMCostoArticulos CA
 
WHERE 
	CA.IdArticulo IN(35, 54, 58)
	--CA.FechaRegistro >= CONVERT(DATETIME, '23-04-2018')
ORDER BY
	CA.IdArticulo, CA.FechaRegistro

GO
SELECT * FROM ADMMaestroArticulo ADMMA WHERE idInterno = '2001'