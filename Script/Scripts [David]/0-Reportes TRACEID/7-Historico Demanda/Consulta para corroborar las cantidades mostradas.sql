


EXEC SP_ObtenerHistoricoDemandaArticuloFechasV2 '01-03-2018', '08-03-2018', 49, 886, 0 
EXEC SP_ObtenerHistoricoDemandaArticuloFechasV2 '01-03-2018', '08-03-2018', 49, 340, 1 
--Ver cantidad solicitada hoy 
SELECT 
	SUM(OPDS.Cantidad)
FROM 
	OPESALPreDetalleSolicitud OPDS 
	
	INNER JOIN OPESALMaestroSolicitud OPMS
	ON  OPDS.idMaestroSolicitud = OPMS.idMaestroSolicitud
WHERE 
	CONVERT(DATE, OPDS.FechaRegistro) = CONVERT(DATE, '09-03-2018')
    AND
	OPDS.idInternoArticulo = 1002
SELECT 
	* 
FROM OPESALPreDetalleSolicitud OPDS 
WHERE 
	CONVERT(DATE, OPDS.FechaRegistro) = CONVERT(DATE, '09-03-2018')
    AND
	OPDS.idInternoArticulo = 1002