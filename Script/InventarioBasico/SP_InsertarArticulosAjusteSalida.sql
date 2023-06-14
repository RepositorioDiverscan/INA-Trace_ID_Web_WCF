USE [TEST_TRACEID_V2]
GO
/****** Object:  StoredProcedure [dbo].[SP_InsertarArticulosAjusteEntrada]    Script Date: 05/11/2017 15:05:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SP_InsertarArticulosAjusteSalida]
	@ArticulosData TRAISArticulo READONLY	
	as
	
	DECLARE @IdRegistro bigint
    DECLARE @SumUno_RestaCero bit 
    DECLARE @IdArticulo bigint
    DECLARE @FechaVencimiento datetime
    DECLARE @Lote  nvarchar(500)
	DECLARE @IdUsuario int 
	DECLARE @IdMetodoAccion bigint
	DECLARE @IdTablaCampoDocumentoAccion nvarchar(200)
	DECLARE @IdCampoDocumentoAccion nvarchar(200)
	DECLARE @NumDocumentoAccion nvarchar(200)
	DECLARE @IdUbicacion bigint
	DECLARE @Cantidad decimal 
	DECLARE @Procesado bit
	DECLARE @FechaRegistro datetime
	DECLARE @IdEstado int
	
	
	Declare cursor_Articulos cursor for
	select IdRegistro,SumUno_RestaCero,IdArticulo,FechaVencimiento,
	Lote,IdUsuario,IdMetodoAccion,IdTablaCampoDocumentoAccion,
	IdCampoDocumentoAccion,NumDocumentoAccion,IdUbicacion,Cantidad,
	Procesado,FechaRegistro,IdEstado
	from @ArticulosData
	OPEN cursor_Articulos 
	FETCH NEXT FROM cursor_Articulos INTO @IdRegistro ,
     @SumUno_RestaCero,@IdArticulo,@FechaVencimiento,@Lote,
	 @IdUsuario,@IdMetodoAccion,@IdTablaCampoDocumentoAccion,
	 @IdCampoDocumentoAccion,@NumDocumentoAccion,@IdUbicacion,
	 @Cantidad ,@Procesado,@FechaRegistro,@IdEstado
	WHILE @@FETCH_STATUS = 0  
		BEGIN	
		
		
		--insert into dbo.TRAIngresoSalidaArticulos(SumUno_RestaCero,idArticulo,FechaVencimiento,Lote,idUsuario,
		--idMetodoAccion,idTablaCampoDocumentoAccion,idCampoDocumentoAccion,NumDocumentoAccion,idUbicacion,Cantidad,
		--Procesado,FechaRegistro,idEstado)
		--Values(@SumUno_RestaCero,@idArticulo,@FechaVencimiento,@Lote,@idUsuario,@idMetodoAccion,@idTablaCampoDocumentoAccion,
		--	@idCampoDocumentoAccion,@NumDocumentoAccion,@idUbicacion,@Cantidad,@Procesado,GETDATE(),@idEstado)
			

		   exec [dbo].[SP_SalidaTrazabilidad] @idArticulo, @Lote,@FechaVencimiento,@idUbicacion,@Cantidad,@idUsuario,8

		
		FETCH NEXT FROM cursor_Articulos INTO @IdRegistro ,
     @SumUno_RestaCero,@IdArticulo,@FechaVencimiento,@Lote,
	 @IdUsuario,@IdMetodoAccion,@IdTablaCampoDocumentoAccion,
	 @IdCampoDocumentoAccion,@NumDocumentoAccion,@IdUbicacion,
	 @Cantidad ,@Procesado,@FechaRegistro,@IdEstado
		END
	CLOSE cursor_Articulos;  
	DEALLOCATE cursor_Articulos;