/****** Object:  Trigger [dbo].[TRI_Trazabilidad]    Script Date: 05/11/2017 14:21:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER TRIGGER [dbo].[TRI_Trazabilidad]
ON [dbo].[Trazabilidad]
AFTER INSERT
AS

DECLARE @IdTrazabilidad bigint, @IdEstado int, @Cantidad int ,@IdMetodoAccion int
SET @IdTrazabilidad = (SELECT IdTrazabilidad FROM inserted)
SET @IdEstado = (SELECT IdEstado FROM inserted)
SET @Cantidad = (SELECT Cantidad FROM inserted)
SET @IdMetodoAccion = (SELECT IdMetodoAccion FROM inserted)

IF (@IdEstado = 12)
BEGIN
	insert into [dbo].[TrazabilidadAuxEntrada]([IdTrazabilidad],[Cantidad])
	values(@IdTrazabilidad,@Cantidad);
END


IF (@IdEstado = 14 and @IdMetodoAccion != 8)
BEGIN
	insert into [dbo].[TrazabilidadAuxSalida]([IdTrazabilidad],[Cantidad])
	values(@IdTrazabilidad,@Cantidad);
END