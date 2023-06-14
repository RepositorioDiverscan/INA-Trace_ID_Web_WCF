CREATE PROCEDURE SP_Activar_InActivar_Unidad_Alisto_Defecto_JOB
AS
BEGIN
	--°°°°°°°°°°°°°°°°°°°INACTIVACIÓN DE UNIDAD ALISTO DEFECTO°°°°°°°°°°°°°°°°°°°
	/*=====================================================>
		[InActivar UnidadesAlistoDefecto es GTIN13]

		1.1-Artículos inactivos en el maestro.
		1.2-Artículos con UAD como GTIN13
	--=====================================================>*/
	BEGIN
		--SELECT
			-- ADMMA.idArticulo 
			--,ADMMA.GTIN
			--,ADMMA.idInterno
			--,ADMMA.Nombre
		UPDATE ADMUAD
		SET
			ADMUAD.Activo = 0		
				
		FROM
			ADMMaestroArticulo ADMMA

			INNER JOIN ADMUnidadAlistoDefecto ADMUAD
			ON	ADMMA.idArticulo	= ADMUAD.idArticulo
			AND	ADMMA.idInterno		= ADMUAD.IdInternoArticulo
			AND ADMMA.GTIN			= ADMUAD.GTIN	
		WHERE
			ADMMA.Activo = 0
			AND
			ADMUAD.esGTIN13 = 1 
	END
	/*=====================================================>
		[InActivar UnidadesAlistoDefecto es GTIN14]

		1.1-Artículos inactivos en ADMGTIN14VariableLogistica.
		1.2-Artículos con UAD no GTIN13
	--=====================================================>*/
	BEGIN
		--SELECT
		--	 ADMG14VL.idGTIN14VariableLogistica 
		--	,ADMG14VL.ConsecutivoGTIN14
		--	,ADMG14VL.idInterno
		--	,ADMG14VL.Descripcion
		
		UPDATE ADMUAD
		SET
			ADMUAD.Activo = 0	

		FROM
			ADMGTIN14VariableLogistica ADMG14VL

			INNER JOIN ADMUnidadAlistoDefecto ADMUAD
			ON	ADMG14VL.idGTIN14VariableLogistica	= ADMUAD.idGTIN14VariableLogistica
			AND ADMG14VL.ConsecutivoGTIN14			= ADMUAD.GTIN	
			AND ADMG14VL.idInterno					= ADMUAD.IdInternoArticulo

		WHERE
			ADMG14VL.Activo = 0
			AND
			ADMUAD.esGTIN13 = 0 
	END

	PRINT 'Inactivación de Unidades Alisto Defecto'


	--°°°°°°°°°°°°°°°°°°°ACTIVACIÓN DE UNIDAD ALISTO DEFECTO°°°°°°°°°°°°°°°°°°°
	/*=====================================================>
		[InActivar UnidadesAlistoDefecto es GTIN13]

		1.1-Artículos inactivos en el maestro.
		1.2-Artículos con UAD como GTIN13
	--=====================================================>*/
	BEGIN
		--SELECT
		--	 ADMMA.idArticulo 
		--	,ADMMA.GTIN
		--	,ADMMA.idInterno
		--	,ADMMA.Nombre

		UPDATE ADMUAD
		SET
			ADMUAD.Activo = 1

		FROM
			ADMMaestroArticulo ADMMA

			INNER JOIN ADMUnidadAlistoDefecto ADMUAD
			ON	ADMMA.idArticulo	= ADMUAD.idArticulo
			AND	ADMMA.idInterno		= ADMUAD.IdInternoArticulo
			AND ADMMA.GTIN			= ADMUAD.GTIN	
		WHERE
			ADMMA.Activo = 1
			AND
			ADMUAD.esGTIN13 = 1 
	END
	/*=====================================================>
		[InActivar UnidadesAlistoDefecto es GTIN14]

		1.1-Artículos inactivos en ADMGTIN14VariableLogistica.
		1.2-Artículos con UAD no GTIN13
	--=====================================================>*/
	BEGIN
		--SELECT
		--	 ADMG14VL.idGTIN14VariableLogistica 
		--	,ADMG14VL.ConsecutivoGTIN14
		--	,ADMG14VL.idInterno
		--	,ADMG14VL.Descripcion

		UPDATE ADMUAD
		SET
			ADMUAD.Activo = 1

		FROM
			ADMGTIN14VariableLogistica ADMG14VL

			INNER JOIN ADMUnidadAlistoDefecto ADMUAD
			ON	ADMG14VL.idGTIN14VariableLogistica	= ADMUAD.idGTIN14VariableLogistica
			AND ADMG14VL.ConsecutivoGTIN14			= ADMUAD.GTIN	
			AND ADMG14VL.idInterno					= ADMUAD.IdInternoArticulo

		WHERE
			ADMG14VL.Activo = 1
			AND
			ADMUAD.esGTIN13 = 0 
	END
	PRINT 'Activación de Unidades Alisto Defecto'
END
GO
--EXEC SP_Activar_InActivar_Unidad_Alisto_Defecto_JOB
SELECT * FROM ADMUnidadAlistoDefecto ORDER BY FechaModificacion DESC
EXEC SP_Activar_InActivar_Unidad_Alisto_Defecto_JOB
SELECT * FROM ADMUnidadAlistoDefecto ORDER BY FechaModificacion DESC