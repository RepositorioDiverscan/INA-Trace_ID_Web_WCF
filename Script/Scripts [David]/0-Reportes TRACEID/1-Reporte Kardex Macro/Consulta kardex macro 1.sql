SELECT
	 ADMMA.idInterno
	,ADMMA.Nombre
	,CONVERT(DATE, AROC.FechaRegistro)
	,AROC.Cantidad
	,*
FROM
	OPEArticulosRecibidosOC AROC
	INNER JOIN ADMMaestroArticulo ADMMA
	ON AROC.idArticulo = ADMMA.idArticulo
