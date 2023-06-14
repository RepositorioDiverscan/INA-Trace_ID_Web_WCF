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

/****** Object:  View [dbo].[Vista_SSCCNoTrasladado]    Script Date: 14/2/2018 12:35:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[Vista_SSCCNoTrasladado]
AS
	WITH SSCCNoTrasladado(IdArticulo,
							   IdUbicacion, 
							   Cantidad,
							   Lote,
							   FechaVencimiento)
	AS(
		SELECT IdArticulo = SSCC.IdArticulo, 
			   IdUbicacion = SSCC.IdUbicacion, 
			   Cantidad = SSCC.Cantidad,
			   Lote = SSCC.Lote,
			   FechaVencimiento = SSCC.FechaVencimiento
		FROM RELSSCCTRA SSCC
		inner join [dbo].[ADMConsecutivosSSCC] CSSCC
		on  SSCC.idConsecutivoSSCC = CSSCC.idConsecutivoSSCC
		where Trasladado = 0
		group by SSCC.IdArticulo, SSCC.IdUbicacion, SSCC.Cantidad,
			   SSCC.Lote, SSCC.FechaVencimiento
		
	)
	
	SELECT *
	FROM SSCCNoTrasladado
GO


