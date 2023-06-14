USE [TEST_TRACEID_V2]
--SELECT TOP 1 * FROM [dbo].[OPESALMaestroSolicitud]
--SELECT TOP 1 * FROM [dbo].[OPEINGMaestroOrdenCompra]
--SELECT TOP 1000 * FROM OPEArticulosRecibidosOC AD WHERE AD.idArticulo = 49
--SELECT TOP 1 * FROM OPEArticulosDespachados 
--SELECT TOP 1 * FROM Trazabilidad
--SELECT * FROM ADMMaestroArticulo  MA WHERE MA.Nombre LIKE '%HONGOS%' OR ma.idArticulo = 350
-----Cantidad de despachos y cantidad en trazabilidad
--SELECT SUM(AROC.Cantidad) AS CantidadArticulo, COUNT(AROC.Cantidad) AS CantidadRegistros FROM OPEArticulosRecibidosOC AROC WHERE AROC.idArticulo = 49
--SELECT SUM(T.Cantidad) AS CantidadArticulo, COUNT(T.Cantidad) AS CantidadRegistros FROM Trazabilidad T WHERE T.IdArticulo = 49 AND T.IdMetodoAccion = 55
--GO




---Muestra los artículos recibidos por idInterno
DECLARE
	@PIdInterno		VARCHAR(1000),	 
	@PIdCompania	VARCHAR(10),
	@VARArticulosActivos BIT
	

--HARINA
	SET @PIdCompania = 'AMCO'
	SET @PIdInterno = '1002'
	SET @VARArticulosActivos = 1
--(
SELECT
	 ADMMA.idInterno				AS IdERPArticulo
	,ADMMA.Nombre					AS NombreArticulo
	,AROC.Cantidad					AS CantidadUnidadesInventario
	,(
		SELECT 		 
		 CASE UAD.esGTIN13
			WHEN 0 THEN (SELECT TOP 1 SUBSTRING(VL.Nombre, CHARINDEX('-',VL.Nombre)+1,LEN(VL.Nombre)) FROM ADMGTIN14VariableLogistica VL WHERE VL.idInterno = @PIdInterno AND VL.Activo = @VARArticulosActivos ORDER BY  VL.ConsecutivoGTIN14 ASC)
			WHEN 1 THEN SUBSTRING(ADMMA.Empaque, CHARINDEX('-',ADMMA.Empaque)+1,LEN(ADMMA.Empaque))		
		 END					AS UnidadInventario		
		--*
		FROM ADMUnidadAlistoDefecto UAD 
			INNER JOIN ADMMaestroArticulo ADMMA
			ON UAD.idArticulo = ADMMA.idArticulo
		WHERE 
			ADMMA.Activo = @VARArticulosActivos	
			AND ADMMA.idInterno = @PIdInterno
	)								AS UnidadInventario 
	,CONVERT(DECIMAL(18,2), 
	 CASE ADMMA.Granel
		WHEN 0 THEN SUM((AROC.Cantidad * ADMMA.Contenido))
		WHEN 1 THEN SUM((AROC.Cantidad/1000))
	 END)							AS CantidadUnidadMedida

	,SUBSTRING(ADMMA.Unidad_Medida, CHARINDEX('-',ADMMA.Unidad_Medida)+1,LEN(ADMMA.Unidad_Medida))				
									AS UnidadMedida
	,CONVERT(DATE, AROC.FechaRegistro)
									AS FechaRegistro --Agrupa por día
	--,'[Datos sin agrupar]'			AS Separador
	--,* 
FROM 
	OPEArticulosRecibidosOC AROC 
	
	INNER JOIN OPEINGMaestroOrdenCompra MOC
	ON AROC.idMaestroOrdenCompra = MOC.idMaestroOrdenCompra

	INNER JOIN ADMMaestroArticulo ADMMA
	ON AROC.IdArticulo = ADMMA.IdArticulo

WHERE
	ADMMA.idInterno = @PIdInterno
	AND	ADMMA.idCompania = @PIdCompania
	AND ADMMA.Activo = @VARArticulosActivos
GROUP BY 
	 ADMMA.idInterno				
	,ADMMA.Nombre					
	,AROC.Cantidad						
	,ADMMA.Granel
	,ADMMA.Unidad_Medida
	,CONVERT(DATE, AROC.FechaRegistro)
GO



---Muestra todos  los artículos recibidos por idInterno
DECLARE
	@PIdInterno		VARCHAR(1000),	 
	@PIdCompania	VARCHAR(10),
	@VARArticulosActivos BIT
	

--HARINA
	SET @PIdCompania = 'AMCO'
	SET @PIdInterno = '1002'
	SET @VARArticulosActivos = 1
--(
SELECT
	 ADMMA.idInterno				AS IdERPArticulo
	,ADMMA.Nombre					AS NombreArticulo
	,AROC.Cantidad					AS CantidadUnidadesInventario
	,(
		SELECT TOP 1		 
		 CASE UAD.esGTIN13
			WHEN 0 THEN (SELECT TOP 1 SUBSTRING(VL.Nombre, CHARINDEX('-',VL.Nombre)+1,LEN(VL.Nombre)) FROM ADMGTIN14VariableLogistica VL WHERE VL.idInterno = ADMMA.idInterno AND VL.Activo = @VARArticulosActivos ORDER BY  VL.ConsecutivoGTIN14 ASC)
			WHEN 1 THEN SUBSTRING(ADMMA.Empaque, CHARINDEX('-',ADMMA.Empaque)+1,LEN(ADMMA.Empaque))		
		 END					AS UnidadInventario		
		--*
		FROM ADMUnidadAlistoDefecto UAD 
			INNER JOIN ADMMaestroArticulo ADMMA
			ON UAD.idArticulo = ADMMA.idArticulo
		WHERE 
			ADMMA.Activo = @VARArticulosActivos	
			--AND ADMMA.idInterno = @PIdInterno
			
	)								AS UnidadInventario 
	,CONVERT(DECIMAL(18,2), 
	 CASE ADMMA.Granel
		WHEN 0 THEN SUM((AROC.Cantidad * ADMMA.Contenido))
		WHEN 1 THEN SUM((AROC.Cantidad/1000))
	 END)							AS CantidadUnidadMedida

	,SUBSTRING(ADMMA.Unidad_Medida, CHARINDEX('-',ADMMA.Unidad_Medida)+1,LEN(ADMMA.Unidad_Medida))				
									AS UnidadMedida
	,CONVERT(DATE, AROC.FechaRegistro)
									AS FechaRegistro --Agrupa por día
	--,'[Datos sin agrupar]'			AS Separador
	--,* 
FROM 
	OPEArticulosRecibidosOC AROC 
	
	INNER JOIN OPEINGMaestroOrdenCompra MOC
	ON AROC.idMaestroOrdenCompra = MOC.idMaestroOrdenCompra

	INNER JOIN ADMMaestroArticulo ADMMA
	ON AROC.IdArticulo = ADMMA.IdArticulo

WHERE
	--ADMMA.idInterno = @PIdInterno
	--AND
		ADMMA.idCompania = @PIdCompania
	AND ADMMA.Activo = @VARArticulosActivos
GROUP BY 
	 ADMMA.idInterno				
	,ADMMA.Nombre					
	,AROC.Cantidad						
	,ADMMA.Granel
	,ADMMA.Unidad_Medida
	,CONVERT(DATE, AROC.FechaRegistro)
GO








----) AS ArticulosRecibidosOC

-----Obtener el nombre de la unidad de inventario y su contenido
--SELECT * FROM ADMUnidadAlistoDefecto where esGTIN13 = 1
--SELECT TOP 1 * FROM ADMGTIN14VariableLogistica VL WHERE VL.idInterno = 1002 AND VL.Activo = 1 ORDER BY  VL.ConsecutivoGTIN14 ASC
----

-----OBTIENE el nombre de las unidades de inventario un ID INTERNO
--SELECT 
--	 UAD.idArticulo
--	,ADMMA.idInterno
--	,CASE UAD.esGTIN13
--		WHEN 0 THEN (SELECT TOP 1 SUBSTRING(VL.Nombre, CHARINDEX('-',VL.Nombre)+1,LEN(VL.Nombre)) FROM ADMGTIN14VariableLogistica VL WHERE VL.idInterno = 1002 AND VL.Activo = 1 ORDER BY  VL.ConsecutivoGTIN14 ASC)
--		WHEN 1 THEN SUBSTRING(ADMMA.Empaque, CHARINDEX('-',ADMMA.Empaque)+1,LEN(ADMMA.Empaque))		
--	 END					AS UnidadInventario
--    ,ADMMA.Nombre			AS NombreArticulo
--	--*
--FROM ADMUnidadAlistoDefecto UAD 
--	INNER JOIN ADMMaestroArticulo ADMMA
--	ON UAD.idArticulo = ADMMA.idArticulo
--WHERE 
--	ADMMA.Activo = 1	
--	AND ADMMA.idInterno = 1002 

	
	


/*






SELECT TOP 1 * FROM OPEArticulosRecibidosOC
SELECT TOP 1 * FROM Trazabilidad
SELECT TOP 1 * FROM OPEArticulosDespachados
GO
--RECEPCION ARTÍCULOS - TRAZABILIDAD
SELECT 
	*
FROM OPEArticulosRecibidosOC AROC
GO
SELECT
	SUM(AROC.Cantidad )
	--*
FROM OPEArticulosRecibidosOC  AROC
	
WHERE 
	AROC.idArticulo = 49
	
GO
SELECT 
	SUM(T.Cantidad) 
FROM 
	Trazabilidad T 

WHERE 
	T.IdMetodoAccion = 55
	AND 
	T.IdArticulo = 49

GO






*/

SELECT * FROM ADMAjusteInventario
SELECT * FROM BitacoraAjustesAplicados

SELECT * FROM Trazabilidad T WHERE 
T.IdMetodoAccion = 8 AND T.IdEstado = 12
