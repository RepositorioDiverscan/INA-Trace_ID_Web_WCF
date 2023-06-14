ALTER PROCEDURE SP_Obtener_SSCC_Zona_Transito_Por_Destino_Solicitud
	@PNumSolicitudTID BIGINT
AS
BEGIN
	SELECT DISTINCT
		  RSSCC.idConsecutivoSSCC	AS IdConsecutivoSSCC
		 ,ADMSSCC.SSCCGenerado
		 ,RSSCC.IdMaestroSolicitud
		-- ,* 	
	FROM 
		RELSSCCTRA RSSCC
		
		INNER JOIN [dbo].[Vista_Ubicaciones_Transito_Id_Etiqueta] VUTIE
		ON RSSCC.idUbicacion = VUTIE.IdUbicacion

		INNER JOIN ADMConsecutivosSSCC ADMSSCC
		ON RSSCC.idConsecutivoSSCC = ADMSSCC.idConsecutivoSSCC
	WHERE
		RSSCC.IdMaestroSolicitud = @PNumSolicitudTID
	ORDER BY
		ADMSSCC.SSCCGenerado ASC				
END
GO
EXEC SP_Obtener_SSCC_Zona_Transito_Por_Destino_Solicitud 34131
EXEC SP_Obtener_SSCC_Zona_Transito_Por_Destino_Solicitud 34087
EXEC SP_Obtener_SSCC_Zona_Transito_Por_Destino_Solicitud 34125



--SELECT TOP 1 * FROM OPESALMaestroSolicitud MS
--SELECT TOP 100 * FROM RELSSCCTRA ORDER BY FechaRegistro DESC
--SELECT * FROM [dbo].[Vista_Ubicaciones_Transito_Id_Etiqueta]






