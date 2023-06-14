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

/****** Object:  View [dbo].[Vista_DisponibilidadArticulos]    Script Date: 14/2/2018 12:21:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO









CREATE view [dbo].[Vista_DisponibilidadArticulos]
AS
   SELECT TOP 100 PERCENT ROW_NUMBER() OVER(ORDER BY A.idArticulo)  AS registros,      
          ROW_NUMBER() OVER(PARTITION BY A.idUbicacion ORDER BY A.idArticulo) AS RegUbic,
		  A.idarticulo,
		  A.IdUbicacion,
		  SUBSTRING(C.ETIQUETA,5,30) AS Etiqueta,
		  B.Nombre AS NombreArticulo,
		  B.idInterno,
		  A.FV,
		  A.Lote,
		  (A.Cantidad * B.Contenido) AS SUMACantidadEstado,
		 SUBSTRING(B.Unidad_Medida,CHARINDEX('-',B.Unidad_Medida)+1, 100) AS Unidad_Medida,
		  B.Granel,
		  c.idBodega,
		  C.Bodega,
		  c.idZona,
		  C.Zona,
		  c.Secuencia
	  FROM  Vista_ArticulosDisponiblestransito AS A
	    INNER JOIN ADMMaestroArticulo  AS B  ON (A.idarticulo = B.idArticulo)
		INNER JOIN Vista_CodigosMaestroUbicacion AS C ON (A.IdUbicacion = C.idUbicacion)
	  WHERE B.idCompania = 'AMCO'
	  ORDER BY C.Secuencia
	 



GO


