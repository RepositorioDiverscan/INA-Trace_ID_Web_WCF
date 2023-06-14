ALTER PROCEDURE SP_Obtener_Destinos_Solicitud_Rango_Fecha_Con_SSCCAsociado
	 	
	 @PParametroBusqueda	VARCHAR(1000)
	,@PFechaInicio			DATETIME
	,@PFechaFin				DATETIME
AS
BEGIN

	IF (@PParametroBusqueda = '')
	BEGIN
		SELECT DISTINCT
			 MS.idMaestroSolicitud	AS IdMaestroSolicitudTID
			,MS.idInternoSAP		AS IdSolicitudSAP
			,MS.FechaCreacion
			,ADMD.Nombre			AS NombreDestino		
		FROM 
			OPESALMaestroSolicitud MS
					
			INNER JOIN RELSSCCTRA RSSCC
			ON MS.idMaestroSolicitud = RSSCC.IdMaestroSolicitud

			INNER JOIN [dbo].[Vista_Ubicaciones_Transito_Id_Etiqueta] VUTIE
			ON RSSCC.idUbicacion = VUTIE.IdUbicacion

			INNER JOIN ADMDestino ADMD
			ON MS.idDestino = ADMD.idDestino
		WHERE
			CONVERT(DATE , MS.FechaCreacion) BETWEEN CONVERT(DATE, @PFechaInicio) AND CONVERT(DATE, @PFechaFin)	
		ORDER BY
			MS.FechaCreacion ASC	
	END 
	ELSE
	BEGIN
		SELECT DISTINCT
			 MS.idMaestroSolicitud	AS IdMaestroSolicitudTID
			,MS.idInternoSAP		AS IdSolicitudSAP
			,MS.FechaCreacion
			,ADMD.Nombre			AS NombreDestino		
		FROM 
			OPESALMaestroSolicitud MS
		
			INNER JOIN RELSSCCTRA RSSCC
			ON MS.idMaestroSolicitud = RSSCC.IdMaestroSolicitud

			INNER JOIN [dbo].[Vista_Ubicaciones_Transito_Id_Etiqueta] VUTIE
			ON RSSCC.idUbicacion = VUTIE.IdUbicacion


			INNER JOIN ADMDestino ADMD
			ON MS.idDestino = ADMD.idDestino
		WHERE
			CONVERT(DATE , MS.FechaCreacion) BETWEEN CONVERT(DATE, @PFechaInicio) AND CONVERT(DATE, @PFechaFin)
			AND
			(
				CONVERT(VARCHAR, MS.idMaestroSolicitud) LIKE '%' +@PParametroBusqueda+ '%'
				OR
				MS.idInternoSAP LIKE '%' + @PParametroBusqueda + '%'
				OR
				ADMD.Nombre LIKE '%' + @PParametroBusqueda + '%'								
			)
		ORDER BY
			MS.FechaCreacion ASC	
	END

	
END
GO
EXEC SP_Obtener_Destinos_Solicitud_Rango_Fecha_Con_SSCCAsociado '', '02-02-2010', '01-01-2020'