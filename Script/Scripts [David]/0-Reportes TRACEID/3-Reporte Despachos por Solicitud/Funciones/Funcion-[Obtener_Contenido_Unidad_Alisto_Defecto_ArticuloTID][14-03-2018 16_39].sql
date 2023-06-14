ALTER FUNCTION Function_Obtener_Contenido_Unidad_Alisto_Defecto_ArticuloTID(@IdArticuloTID BIGINT= null)
RETURNS DECIMAL(18,6)
WITH RETURNS NULL ON NULL INPUT AS
BEGIN
    DECLARE @CantidadUnidadAlisto DECIMAL(18,6);
    SELECT TOP 1 	 
	--CASE UAD_SUB.esGTIN13
	--	WHEN 0 THEN 'GTIN14'
	--	WHEN 1 THEN 'GTIN13'
	--END	
	@CantidadUnidadAlisto = CASE UAD_SUB.esGTIN13
		WHEN 0 THEN (SELECT TOP 1 GVL.Contenido FROM [dbo].[ADMGTIN14VariableLogistica] GVL WHERE GVL.IdGTIN14VariableLogistica = UAD_SUB.IdGTIN14VariableLogistica)
		WHEN 1 THEN (SELECT TOP 1 ADMMA_SUB.Contenido FROM ADMMaestroArticulo ADMMA_SUB WHERE ADMMA_SUB.IdArticulo = UAD_SUB.idArticulo)
	END						--AS UnidadAlistoBase 
	--,* 
FROM
	[dbo].[ADMUnidadAlistoDefecto] UAD_SUB
WHERE
	UAD_SUB.idArticulo IN(@IdArticuloTID)	
    RETURN @CantidadUnidadAlisto;
END;
GO
SELECT dbo.Function_Obtener_Contenido_Unidad_Alisto_Defecto_ArticuloTID (49)

--SELECT * FROM ADMMaestroArticulo MA WHERE MA.idArticulo = 49
--SELECT * FROM [ADMGTIN14VariableLogistica] VL WHERE VL.idInterno = 1002

select * from ADMMaestroArticulo m where m.Nombre like '%HONGOS REBANADOS%'