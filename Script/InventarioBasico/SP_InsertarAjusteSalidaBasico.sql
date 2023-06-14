
alter PROCEDURE [dbo].[SP_InsertarAjusteSalidaBasico]
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
		
	insert into dbo.SolicitudAjusteInventario(IdUsuario,IdAjusteInventario,FechaSolicitud,Estado,FechaAprobado,idDestino)
	--Toma fisica Salida 2 IdAjuste = 101 idERP
	Values(@SAIdUsuario,2,GETDATE(),1,GETDATE(),11)
	SET @IdSolicitudAjusteInventario = SCOPE_IDENTITY()	

	
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
		
		
		--insert into dbo.TRAIngresoSalidaArticulos(SumUno_RestaCero,idArticulo,FechaVencimiento,Lote,idUsuario,
		--idMetodoAccion,idTablaCampoDocumentoAccion,idCampoDocumentoAccion,NumDocumentoAccion,idUbicacion,Cantidad,
		--Procesado,FechaRegistro,idEstado)
		--Values(@SumUno_RestaCero,@idArticulo,@FechaVencimiento,@Lote,@idUsuario,@idMetodoAccion,@idTablaCampoDocumentoAccion,
		--	@idCampoDocumentoAccion,@NumDocumentoAccion,@idUbicacion,@Cantidad,@Procesado,GETDATE(),@idEstado)
		--	SET @Identity = SCOPE_IDENTITY()

		exec [dbo].[SP_SalidaTrazabilidad] @idArticulo, @Lote,@FechaVencimiento,@idUbicacion,@Cantidad,@idUsuario,8
				
		
		set @IdCopiaSistemaArticulo =(select IdCopiaSistemaArticulo 
		from dbo.CopiaSistemaArticulo
		where IdInventarioBasico = @IdInventarioBasico and IdArticulo=@IdArticulo)

		if(@IdCopiaSistemaArticulo>0)
			begin

			update CopiaSistemaArticuloDetalle
			set  Cantidad = Cantidad - @Cantidad
			where	IdUbicacion =@IdUbicacion
					and Lote =@Lote
					and FechaVencimiento = @FechaVencimiento

					delete from CopiaSistemaArticuloDetalle
					where Cantidad = 0

				--delete from CopiaSistemaArticuloDetalle
				--where IdCopiaSistemaArticuloDetalle in 
				--	(SELECT TOP 1 IdCopiaSistemaArticuloDetalle 
				--	from CopiaSistemaArticuloDetalle
				--	where IdCopiaSistemaArticulo = @IdCopiaSistemaArticulo		
				--	and IdUbicacion =@IdUbicacion
				--	and Lote =@Lote
				--	and FechaVencimiento = @FechaVencimiento
				--	and Cantidad =@Cantidad)		
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