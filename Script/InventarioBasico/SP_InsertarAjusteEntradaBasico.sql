
Alter PROCEDURE [dbo].[SP_InsertarAjusteEntradaBasico]
	@ArticulosData TRAISArticulo READONLY
	,@IdInventarioBasico bigint
	,@SAIdUsuario AS int
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
	
	DECLARE @Identity bigint
	DECLARE @IdCopiaSistemaArticulo bigint
	
	DECLARE @IdSolicitudAjusteInventario bigint
	
	DECLARE @IdLogAjustesTRA bigint
		
	insert into dbo.SolicitudAjusteInventario(IdUsuario,IdAjusteInventario,FechaSolicitud,Estado,FechaAprobado,idDestino)
	--Toma fisica Entrada 1 IdAjuste =100 idERP
	Values(@SAIdUsuario,1,GETDATE(),1,GETDATE(),11)
	SET @IdSolicitudAjusteInventario = SCOPE_IDENTITY()
	
	insert into dbo.LogAjustesTRA(IdSolicitudAjusteInventario,FechaRegistro)
	Values(@IdSolicitudAjusteInventario, GETDATE())
	SET @IdLogAjustesTRA = SCOPE_IDENTITY()
	
	
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
		
		insert into dbo.RELArticuloXSolicitudAjuste(IdSolicitudAjusteInventario,IdArticulo,Lote,
		FechaVencimiento,IdUbicacionActual,IdUbicacionMover,Cantidad)
		Values(@IdSolicitudAjusteInventario,@IdArticulo,@Lote,@FechaVencimiento,@idUbicacion,
			@idUbicacion,@Cantidad)
		
		
			---------------------------------------------------------------------------------
			INSERT INTO [dbo].[Trazabilidad](IdArticulo, Lote,FechaVencimiento,
								   IdUbicacion, Cantidad, 
								   IdSecuencia, IdEstado,IdUsuario)
	VALUES(@idArticulo, @Lote,@FechaVencimiento,
		   @idUbicacion, @Cantidad, 
		   -1, 
		   12,
		   @idUsuario) 

			-------------------------------------------------------------------------
				
		
		set @IdCopiaSistemaArticulo =(select IdCopiaSistemaArticulo 
		from dbo.CopiaSistemaArticulo
		where IdInventarioBasico = @IdInventarioBasico and IdArticulo=@IdArticulo)

		if(@IdCopiaSistemaArticulo>0)
			begin
				insert into dbo.CopiaSistemaArticuloDetalle(IdCopiaSistemaArticulo,FechaVencimiento,Lote,IdUbicacion,Cantidad)
				values(@IdCopiaSistemaArticulo,@FechaVencimiento,@Lote,@IdUbicacion,@Cantidad)
			end
		
		
		
		
		FETCH NEXT FROM cursor_Articulos INTO @IdRegistro ,
     @SumUno_RestaCero,@IdArticulo,@FechaVencimiento,@Lote,
	 @IdUsuario,@IdMetodoAccion,@IdTablaCampoDocumentoAccion,
	 @IdCampoDocumentoAccion,@NumDocumentoAccion,@IdUbicacion,
	 @Cantidad ,@Procesado,@FechaRegistro,@IdEstado
		END
	CLOSE cursor_Articulos;  
	DEALLOCATE cursor_Articulos;

	update dbo.SolicitudAjusteInventario
	set Estado=2
	where IdSolicitudAjusteInventario =@IdSolicitudAjusteInventario 
	