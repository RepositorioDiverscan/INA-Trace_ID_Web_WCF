ALTER PROCEDURE SP_Actualizar_Estado_Unidad_Alisto_Defecto
AS
BEGIN
	SELECT --TOP 100 
		 ADMMA.idArticulo
		,ADMMA.GTIN
		,ADMMA.idInterno
		,'>==========================<'
		,* 
	FROM 
		ADMMaestroArticulo ADMMA 
	WHERE 
		ADMMA.Activo = 0

END
GO
EXEC SP_Actualizar_Estado_Unidad_Alisto_Defecto
--SELECT TOP 100 * FROM ADMUnidadAlistoDefecto UAD