SELECT * FROM ADMUnidadAlistoDefecto UAD WHERE UAD.idArticulo = 6
SELECT * FROM ADMGTIN14VariableLogistica VL WHERE VL.idGTIN14VariableLogistica =447

SELECT TOP 1 * FROM ADMUnidadAlistoDefecto UAD WHERE UAD.esGTIN13 = 1
SELECT TOP 1 * FROM ADMMaestroArticulo ADMMA WHERE ADMMA.idArticulo = 26

--======================================================>
--				ACTUALIZACIÓN GTIN13
--======================================================> 
SELECT 
	* 
FROM ADMUnidadAlistoDefecto UAD

	INNER JOIN ADMMaestroArticulo ADMMA
	ON UAD.idArticulo = ADMMA.idArticulo

GO
--ACTUALIZA LOS CAMPOS agregados a ADMUnidadAlistoDefecto
SELECT * FROM ADMUnidadAlistoDefecto
UPDATE UAD 
SET  UAD.Activo				= 1
	,UAD.GTIN				= ADMMA.GTIN
	,UAD.IdInternoArticulo	= ADMMA.idInterno
FROM ADMUnidadAlistoDefecto UAD
INNER JOIN ADMMaestroArticulo ADMMA
	ON UAD.idArticulo = ADMMA.idArticulo
WHERE UAD.esGTIN13 = 1
SELECT * FROM ADMUnidadAlistoDefecto


--======================================================>
--				ACTUALIZACIÓN GTIN14
--======================================================> 
SELECT 
	* 
FROM ADMUnidadAlistoDefecto UAD

	INNER JOIN ADMMaestroArticulo ADMMA
	ON UAD.idArticulo = ADMMA.idArticulo

GO
--ACTUALIZA LOS CAMPOS agregados a ADMUnidadAlistoDefecto
SELECT * FROM ADMUnidadAlistoDefecto
UPDATE UAD 
SET  UAD.Activo				= 1
	,UAD.GTIN				= VL.ConsecutivoGTIN14
	,UAD.IdInternoArticulo	= VL.idInterno
FROM ADMUnidadAlistoDefecto UAD
INNER JOIN ADMGTIN14VariableLogistica VL
	ON UAD.idGTIN14VariableLogistica = VL.idGTIN14VariableLogistica
WHERE UAD.esGTIN13 = 0
SELECT * FROM ADMUnidadAlistoDefecto