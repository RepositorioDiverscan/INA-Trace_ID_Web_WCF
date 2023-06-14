
DECLARE 
	@IdMaestroSolicitud BIGINT

SET @IdMaestroSolicitud = 
	--35279**	
	35266
---OBTIENE LOS ARTÍCULOS QUE ESTÁN Y NO EN DS Y PDS 
SELECT 
	*
FROM
(
	SELECT DISTINCT
		 DS.IdArticulo
		,ADMMA.Nombre
		,ADMMA.IdInterno IdInternoDetalleSolicitud
	FROM
		OPESALDetalleSolicitud DS
		INNER JOIN ADMMaestroArticulo ADMMA
		ON DS.IdArticulo = ADMMA.IdArticulo

	WHERE	
		DS.IdMaestroSolicitud = @IdMaestroSolicitud
	--ORDER BY ADMMA.Nombre

) AS DS

FULL OUTER JOIN 
(
	SELECT DISTINCT
 
		 PDS.IdInternoArticulo IdInterno
		,ADMMA.Nombre
	 
	FROM 

	OPESALPreDetalleSolicitud PDS

	INNER JOIN ADMMaestroArticulo ADMMA
	ON ADMMA.IdInterno = PDS.IdInternoArticulo

	WHERE 
		PDS.idMaestroSolicitud = @IdMaestroSolicitud	
) AS PDS

	ON DS.IdInternoDetalleSolicitud = PDS.IdInterno

	ORDER BY PDS.Nombre
GO


--=================================================================>
--Maestros Solicitud con articulos que no llegaron a Detalle
--=================================================================>
--[35279] -- 1016 SALSA DE TOMATE
--SELECT * FROM ADMMaestroArticulo ADMMA WHERE ADMMA.IdInterno = 1016
--SELECT * FROM ADMMA