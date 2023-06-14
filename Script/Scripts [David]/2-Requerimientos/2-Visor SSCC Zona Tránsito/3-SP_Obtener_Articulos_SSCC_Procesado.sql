ALTER PROCEDURE SP_Obtener_Articulos_SSCC_Procesado
	@PIdConsecutivoSSCC BIGINT
AS
BEGIN
	SELECT
		 ADMMA.Nombre						AS NombreArticulo
		,CASE ADMMA.Granel
		 WHEN 1 
			THEN SUM(ADMMA.Contenido / 1000)  		
		 ELSE 
			SUM(ADMMA.Contenido * RSSCC.Cantidad)		 		
		 END								AS CantidadUI
		,SUBSTRING(ADMMA.Unidad_Medida, CHARINDEX('-', ADMMA.Unidad_Medida) + 1, LEN(ADMMA.Unidad_Medida))
											AS UnidadMedida		
		--,* 
	FROM 
		RELSSCCTRA RSSCC
		
		INNER JOIN ADMMaestroArticulo ADMMA 
		ON RSSCC.idArticulo = ADMMA.idArticulo
	WHERE
		RSSCC.idConsecutivoSSCC = @PIdConsecutivoSSCC

	GROUP BY 
		 ADMMA.Nombre
		,ADMMA.Granel			
		,ADMMA.Unidad_Medida
END
GO
--EXEC SP_Obtener_Articulos_SSCC_Procesado 779
EXEC SP_Obtener_Articulos_SSCC_Procesado 796


--select * from RELSSCCTRA where idConsecutivoSSCC = 796
--SELECT * FROM ADMMaestroArticulo MA WHERE MA.idArticulo IN(19, 20)
--select * from RELSSCCTRA where idConsecutivoSSCC = 779
--select * from [dbo].[Vista_Ubicaciones_Transito_Id_Etiqueta]
--select * from  
--	RELSSCCTRA RSSCC
--	inner join [dbo].[Vista_Ubicaciones_Transito_Id_Etiqueta] VE
--	ON RSSCC.idUbicacion = VE.IdUbicacion
--ORDER BY RSSCC.idConsecutivoSSCC


	
