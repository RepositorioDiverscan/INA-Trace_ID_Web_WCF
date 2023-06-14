/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2012 (11.0.7001)
    Source Database Engine Edition : Microsoft SQL Server Standard Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2012
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [TEST_TRACEID_V2]
GO

/****** Object:  View [dbo].[Vista_PreDetalleSolicitudPorBodega]    Script Date: 19/3/2018 15:45:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Vista_PreDetalleSolicitudPorBodegaV2]
  /*===========================================
    AUTOR.......: Nelson Calderón S.
	Fecha......: 01-sep-2017
	Descripción: Se válida que pertenescan a alguna
	             de las compañias definidas.
	--------------------------------------------
	AUTOR......: Alejandro Calvo.
	Descripción: Despliega las solicitudes pendientes
	             de procesar
	--------------------------------------------
	AUTOR......: David G.
	Descripción: Se mejoró el rendimiento de la vista
	             

	============================================*/
AS
SELECT ms.idMaestroSolicitud,
       ms.Nombre as Solicitud,
	   ma.idBodega, 
       ISNULL(CASE ma.idBodega 
                WHEN 1 THEN 'Seco' 
                WHEN 2 THEN 'Frio' 
                WHEN 3 THEN 'Cong'
				WHEN 6 THEN 'Quimico'
              END, 'Seco') AS 'Bodega',
       de.Nombre as Destino,
       ms.idInternoSAP as IdInterno,
	   pds.IdCompania,	   
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
		   ma.idBodega,		   		   
		   ms.idInternoSAP,
           --ISNULL(CASE ma.idBodega WHEN 1 THEN 'Seco' WHEN 2 THEN 'Frio' WHEN 3 THEN 'Cong' WHEN 6 THEN 'Quimico' END, 'Seco'),
		   pds.IdCompania,
		   ms.FechaCreacion
GO





