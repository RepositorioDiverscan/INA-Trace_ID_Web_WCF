/****** Object:  Trigger [dbo].[TRI_Trazabilidad]    Script Date: 05/11/2017 14:21:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create TRIGGER [dbo].[TRI_DetalleSolicitud]
ON [dbo].[OPESALPreDetalleSolicitud]
AFTER INSERT
AS

DECLARE @Nombre varchar(200), @idMaestroSolicitud bigint, @idDestino bigint ,
	@idInternoArticulo varchar(200),@CantidadKilos decimal,@Descripcion varchar(1000),@IdCompania varchar(10),
	@IdUsuario int,  @numLinea int

SET @Nombre = (SELECT Nombre FROM inserted)
SET @idMaestroSolicitud = (SELECT idMaestroSolicitud FROM inserted)
SET @idDestino = (SELECT idDestino FROM inserted)
SET @idInternoArticulo = (SELECT idInternoArticulo FROM inserted)
SET @CantidadKilos = (SELECT Cantidad FROM inserted)
SET @Descripcion = (SELECT Descripcion FROM inserted)
SET @IdCompania = (SELECT idCompania FROM inserted)
SET @IdUsuario= (SELECT idUsuario FROM inserted)
SET @numLinea = (SELECT numLinea FROM inserted)

Declare @CantidadUnidadAlistoKilos decimal(18,2) , @CantidadUnidadAlisto decimal(18,2), @CantidadUnidadAlistoPreparar decimal(18,2), @CantidadUnidadAlistoInventario decimal(18,2)
Declare @IdArticulo bigint
set @CantidadUnidadAlisto = 1
Declare @EsGTIN13 bit

		select @EsGTIN13 = UAD.esGTIN13, @IdArticulo=MA.idArticulo,  @CantidadUnidadAlistoKilos = MA.Contenido
		from ADMUnidadAlistoDefecto UAD
		inner join ADMMaestroArticulo MA
		on  MA.idArticulo =UAD.idArticulo
		where MA.idInterno = @idInternoArticulo

		if(@IdArticulo>0)
			begin
				if(@EsGTIN13=0)
				begin
					select @CantidadUnidadAlisto= G14D.Cantidad, @CantidadUnidadAlistoKilos=G14D.Contenido
					from ADMGTIN14VariableLogistica G14D
					inner join ADMUnidadAlistoDefecto UAD
					on UAD.idGTIN14VariableLogistica = G14D.idGTIN14VariableLogistica
					inner join ADMMaestroArticulo MA
					on  MA.idArticulo =UAD.idArticulo
					where MA.idInterno = @idInternoArticulo
				end	
																	
				SET @CantidadUnidadAlistoPreparar= (SELECT CEILING(@CantidadKilos/@CantidadUnidadAlistoKilos) AS total)
				SET @CantidadUnidadAlistoInventario = @CantidadUnidadAlistoPreparar* @CantidadUnidadAlisto


				INSERT INTO OPESALDetalleSolicitud
						  (Nombre,
						   idMaestroSolicitud,
						   idDestino,
						   idArticulo,
						   Cantidad,
						   Descripcion,		
						   IdCompania,
						   idUsuario,
						   Procesado,
						   numLinea,
						   LineaOriginal,
						   CantidadUnidadAlisto,
						   CantidadInventario) 
						   values(
						   @Nombre,
						   @idMaestroSolicitud,
						   @idDestino,
						   @IdArticulo,
						   @CantidadKilos,
						   @Descripcion,
						   @IdCompania,
						   @IdUsuario,
						   0,
						   @NumLinea,
						   1,
						   @CantidadUnidadAlistoPreparar,
						   @CantidadUnidadAlistoInventario
						   )
			end	