alter VIEW [dbo].[Vista_PreDetalleSolicitudPorBodega]
  /*===========================================
    AUTOR.......: Nelson Calderón S.
	Fecha......: 01-sep-2017
	Descripción: Se válida que pertenescan a alguna
	             de las compañias definidas.
	--------------------------------------------
	AUTOR......: Alejandro Calvo.
	Descripción: Despliega las solicitudes pendientes
	             de procesar
	============================================*/
AS
--SELECT * FROM Vista_SalidasAprobadas
SELECT ms.idMaestroSolicitud,
       ms.Nombre as Solicitud,
       ISNULL(CASE ma.idBodega 
                WHEN 3 THEN 2 else ma.idBodega   
              END, '1') AS 'idBodega',
       ISNULL(CASE 
                CASE ma.idBodega 
                  WHEN 3 THEN 2 else ma.idBodega 
                END
                WHEN 1 THEN 'Seco' 
                WHEN 2 THEN 'Frio' 
                WHEN 3 THEN 'Frio'
				WHEN 6 THEN 'Quimico'
              END, 'Seco') AS 'Bodega',
       de.Nombre as Destino,
       ms.idInternoSAP as IdInterno,
	   pds.IdCompania,
	   --CONVERT(nvarchar(10),ms.FechaCreacion,103) AS Fecha2,
	   CONVERT(DATE,ms.FechaCreacion,103) AS Fecha
  FROM OPESALMaestroSolicitud AS ms
    INNER JOIN OPESALDetalleSolicitud AS pds ON (ms.idMaestroSolicitud = pds.idMaestroSolicitud)
	INNER JOIN ADMMaestroArticulo        AS ma  ON (pds.idArticulo = ma.idArticulo)
	INNER JOIN ADMDestino                AS de  ON (de.idDestino = ms.idDestino)
	INNER JOIN ADMCompania               AS Co  ON (pds.IdCompania = Co.IdCompania)
  WHERE pds.Procesado =0
  GROUP BY ms.idMaestroSolicitud,
           ms.Nombre,
		   de.Nombre, 
		   ISNULL(CASE ma.idBodega WHEN 3 THEN 2 else ma.idBodega END, '1'),
		   ms.idInternoSAP,
           ISNULL(CASE CASE ma.idBodega WHEN 3 THEN 2 else ma.idBodega END WHEN 1 THEN 'Seco' WHEN 2 THEN 'Frio' WHEN 3 THEN 'Frio' WHEN 6 THEN 'Quimico' END, 'Seco'),
		   pds.IdCompania,
		   ms.FechaCreacion



GO