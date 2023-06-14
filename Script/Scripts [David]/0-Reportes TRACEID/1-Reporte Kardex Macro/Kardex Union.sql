--===================================================
---			[ARTÍCULOS RECIBIDOS DE OC]
--===================================================
SELECT 
	 ADMMA.idInterno						AS IdInternoArticulo
	,ADMMA.Nombre							AS NombreArticulo
	,SUM(AROC.Cantidad)						AS CantidadUnidadesInventario
	--,AROC.Cantidad							AS CantidadUnidadesInventario
	,SUBSTRING(ADMMA.Empaque, CHARINDEX('-', ADMMA.Empaque, 0) + 1, LEN(ADMMA.Empaque))
											AS DetalleUnidadInventario
	,ADMMA.Contenido						AS ContenidoUnidadInventario
	,SUM(AROC.Cantidad * ADMMA.Contenido)	AS CantidadEnUnidadMedida
	,SUBSTRING(ADMMA.Unidad_Medida, CHARINDEX('-', ADMMA.Unidad_Medida, 0) + 1, LEN(ADMMA.Unidad_Medida))
											AS UnidadMedida						
	,'Recepción de artículo'				AS DetalleMovimiento
	,'+'									AS EfectoEnInventario
	,CONVERT(DATE, AROC.FechaRegistro)		AS FechaRegistro
	--*
FROM 
	OPEArticulosRecibidosOC AROC

	INNER JOIN ADMMaestroArticulo ADMMA
	ON AROC.idArticulo = ADMMA.idArticulo
GROUP BY
	 ADMMA.idInterno
	,ADMMA.Nombre
	,CONVERT(DATE, AROC.FechaRegistro)
	,ADMMA.Empaque
	,ADMMA.Contenido
	,ADMMA.Unidad_Medida

--UNION
GO
--===================================================
--			[AJUSTES DE INVENTARIO] 
--			  [Entrada - Salida]
--===================================================
SELECT 
	 ADMMA.idInterno						AS IdInternoArticulo
	,ADMMA.Nombre							AS NombreArticulo
	,SUM(RSA.Cantidad)						AS CantidadUnidadesInventario
	--,RSA.Cantidad							AS CantidadUnidadesInventario
    ,SUBSTRING(ADMMA.Empaque, CHARINDEX('-', ADMMA.Empaque, 0) + 1, LEN(ADMMA.Empaque))
											AS UnidadMedida
	,SAI.IdSolicitudAjusteInventario        AS IdSolicitudAjuste
	,CASE AI.idAjusteInventario
		WHEN 1 THEN 'Toma física entrada'
		WHEN 2 THEN 'Toma física salida'
		WHEN 3 THEN 'Movimiento de inventario entrada'
		WHEN 4 THEN 'Movimiento de inventario salida'
		WHEN 5 THEN 'Bonificación entrada'
		WHEN 6 THEN 'Reversion de bonificacion salida'
		WHEN 7 THEN 'Reversion merma vida util entrada'
		WHEN 8 THEN 'Merma vida util salida'
		WHEN 9 THEN 'Reversion merma almacenamiento entrada'
		WHEN 10 THEN 'Merma almacenamiento salida'
		WHEN 11 THEN 'Reversion merma transporte entrada'
		WHEN 12 THEN 'Merma transporte salida'
		WHEN 13 THEN 'Reversion producto obsoleto entrada'
		WHEN 14 THEN 'Producto obsoleto salida'
		WHEN 15 THEN 'Reversion merma calidad entrada'
		WHEN 16 THEN 'Merma calidad salida'
		WHEN 17 THEN 'Produccion comisariato entrada'
		WHEN 18 THEN 'Reversion produccion comisariato salida'
		WHEN 19 THEN 'Reversion venta interna al personal entrada'
		WHEN 20 THEN 'Venta interna al personal salida'
		WHEN 21 THEN 'Reversion limpieza entrada'
		WHEN 22 THEN 'Limpieza salida'
		WHEN 23 THEN 'Reversion papelería entrada'
		WHEN 24 THEN 'Papelería salida'
		WHEN 25 THEN 'Reversión viáticos entrada'
		WHEN 26 THEN 'Viáticos salida'
		WHEN 27 THEN 'Reversion articulos menores entrada'
		WHEN 28 THEN 'Articulos Menores salida'
		WHEN 29 THEN 'Reversion producto de prueba entrada'
		WHEN 30 THEN 'Producto de prueba salida'
		WHEN 31 THEN 'Reversion familia gutierrez entrada'
		WHEN 32 THEN 'Familia Gutierrez salida'		
	 END									AS DetalleMovimiento
	,CASE AI.TipoAjuste
		WHEN 0 THEN '-'
		WHEN 1 THEN '+'					
	 END									AS EfectoEnInventario
	,CONVERT(date, (SAI.FechaAprobado)) 
											AS FechaRegistro
	--,'================'
	--,*
											 
FROM
	RELArticuloXSolicitudAjuste RSA

	INNER JOIN SolicitudAjusteInventario SAI
	ON RSA.IdSolicitudAjusteInventario = SAI.IdSolicitudAjusteInventario
	
	INNER JOIN ADMAjusteInventario AI
	ON SAI.IdAjusteInventario = AI.idAjusteInventario

	INNER JOIN ADMMaestroArticulo ADMMA
	ON RSA.IdArticulo = ADMMA.idArticulo
GROUP BY 
     ADMMA.idInterno						
	,ADMMA.Nombre								
	,AI.idAjusteInventario
	,AI.TipoAjuste
	,CONVERT(date, (SAI.FechaAprobado))
	,ADMMA.Empaque
	,SAI.IdSolicitudAjusteInventario
--ORDER BY 
--	 CONVERT(date, (SAI.FechaAprobado)) DESC

GO
--===================================================
--				 [Despachos] 
--			      [Salida]
--===================================================
SELECT 
	 ADMMA.idInterno							AS IdInternoArticulo
	,ADMMA.Nombre								AS NombreArticulo
	,SUM(AD.Cantidad)							AS CantidadUnidadesInventario
	,SUBSTRING(ADMMA.Empaque, CHARINDEX('-', ADMMA.Empaque, 0) + 1, LEN(ADMMA.Empaque))
												AS UnidadMedida
	--,AD.Cantidad								AS CantidadUnidadesInventario
	,'Despacho de artículo'						AS DetalleMovimiento
	,'-'										AS EfectoEnInventario
	,CONVERT(DATE, AD.FechaRegistro)			AS FechaRegistro
	--,''	 	
	--,*
FROM 
	OPEArticulosDespachados AD 

	INNER JOIN ADMMaestroArticulo ADMMA
	ON AD.idArticulo = ADMMA.idArticulo
GROUP BY 
	 ADMMA.idInterno
	,ADMMA.Nombre
	,CONVERT(DATE, AD.FechaRegistro)
	,ADMMA.Empaque

GO


SELECT 
	*
FROM 
	Trazabilidad T
	LEFT JOIN TipoMetodoAccion TMA
	ON T.IdMetodoAccion = TMA.IdMetodoAccion
	
	INNER JOIN TipoEstado TE
	ON TE.IdEstado = T.IdEstado
WHERE
	T.IdMetodoAccion IN(58, 59, 75, 76)

















/*
---
SELECT
	 		 
	CASE UAD.esGTIN13
	--WHEN 0 THEN (SELECT TOP 1 SUBSTRING(VL.Nombre, CHARINDEX('-',VL.Nombre)+1,LEN(VL.Nombre)) FROM ADMGTIN14VariableLogistica VL WHERE VL.idInterno = @PIdInterno AND VL.Activo = @VARArticulosActivos ORDER BY  VL.ConsecutivoGTIN14 ASC)
	WHEN 0 THEN (SELECT TOP 1 SUBSTRING(VL.Nombre, CHARINDEX('-',VL.Nombre)+1,LEN(VL.Nombre)) FROM ADMGTIN14VariableLogistica VL WHERE VL.idInterno = ADMMA.idInterno AND VL.Activo = 1 ORDER BY  VL.ConsecutivoGTIN14 ASC)
	WHEN 1 THEN SUBSTRING(ADMMA.Empaque, CHARINDEX('-',ADMMA.Empaque)+1,LEN(ADMMA.Empaque))		
	END					AS UnidadInventario		
	,ADMMA.Nombre		AS NombreArticulo
	,ADMMA.idInterno
	,ADMMA.idArticulo 
--*
FROM ADMUnidadAlistoDefecto UAD 
	INNER JOIN ADMMaestroArticulo ADMMA
	ON UAD.idArticulo = ADMMA.idArticulo
WHERE 
	ADMMA.idInterno = 3001
--	ADMMA.Activo = @VARArticulosActivos	
--	AND ADMMA.idInterno = @PIdInterno

SELECT * FROM 
	ADMUnidadAlistoDefecto UAD 

	INNER JOIN ADMGTIN14VariableLogistica VL 
	ON UAD.idGTIN14VariableLogistica = VL.idGTIN14VariableLogistica
WHERE UAD.idArticulo IN(17,3)

--SELECT * FROM SolicitudAjusteInventario
--GO
--SELECT * FROM ADMAjusteInventario

*/
GO




----AJUSTES DE INVENTARIO UTILIZADOS EN EL GRID DE AJUSTES 
--SELECT
--	'#A.Inventario:' + CONVERT(VARCHAR, SAI.IdSolicitudAjusteInventario)
--											AS IdSolicitudAjusteInventarioDetalle
--	,ADMMA.IdArticulo						AS IdArticulo	
--	,ADMMA.idInterno						AS IdInternoSAP
--	,ADMMA.Nombre							AS NombreArticulo
--	,CASE ADMMA.Granel 
--		WHEN 0 THEN SUM(RSA.Cantidad)						
--		WHEN 1 THEN SUM(RSA.Cantidad/1000)						
--     END									AS Cantidad
--	,SUBSTRING(ADMMA.Unidad_Medida, CHARINDEX('-', ADMMA.Unidad_Medida, 0) + 1, LEN(ADMMA.Unidad_Medida))					
--											AS UnidadMedida
	
--    --,SUBSTRING(ADMMA.Empaque, CHARINDEX('-', ADMMA.Empaque, 0) + 1, LEN(ADMMA.Empaque))
--				--							AS UnidadMedida	
--	,CASE AI.idAjusteInventario
--		WHEN 1 THEN 'Toma física entrada'
--		WHEN 2 THEN 'Toma física salida'
--		WHEN 3 THEN 'Movimiento de inventario entrada'
--		WHEN 4 THEN 'Movimiento de inventario salida'
--		WHEN 5 THEN 'Bonificación entrada'
--		WHEN 6 THEN 'Reversion de bonificacion salida'
--		WHEN 7 THEN 'Reversion merma vida util entrada'
--		WHEN 8 THEN 'Merma vida util salida'
--		WHEN 9 THEN 'Reversion merma almacenamiento entrada'
--		WHEN 10 THEN 'Merma almacenamiento salida'
--		WHEN 11 THEN 'Reversion merma transporte entrada'
--		WHEN 12 THEN 'Merma transporte salida'
--		WHEN 13 THEN 'Reversion producto obsoleto entrada'
--		WHEN 14 THEN 'Producto obsoleto salida'
--		WHEN 15 THEN 'Reversion merma calidad entrada'
--		WHEN 16 THEN 'Merma calidad salida'
--		WHEN 17 THEN 'Produccion comisariato entrada'
--		WHEN 18 THEN 'Reversion produccion comisariato salida'
--		WHEN 19 THEN 'Reversion venta interna al personal entrada'
--		WHEN 20 THEN 'Venta interna al personal salida'
--		WHEN 21 THEN 'Reversion limpieza entrada'
--		WHEN 22 THEN 'Limpieza salida'
--		WHEN 23 THEN 'Reversion papelería entrada'
--		WHEN 24 THEN 'Papelería salida'
--		WHEN 25 THEN 'Reversión viáticos entrada'
--		WHEN 26 THEN 'Viáticos salida'
--		WHEN 27 THEN 'Reversion articulos menores entrada'
--		WHEN 28 THEN 'Articulos Menores salida'
--		WHEN 29 THEN 'Reversion producto de prueba entrada'
--		WHEN 30 THEN 'Producto de prueba salida'
--		WHEN 31 THEN 'Reversion familia gutierrez entrada'
--		WHEN 32 THEN 'Familia Gutierrez salida'		
--	 END									AS AjusteInventarioDescripcion
--	,CASE AI.TipoAjuste
--		WHEN 0 THEN '-'
--		WHEN 1 THEN '+'					
--	 END									AS EfectoEnInventario
--	,CONVERT(date, (SAI.FechaAprobado)) 
--											AS FechaFechaRegistroTrazabilidad
--	,SAI.idDestino							AS IdUbicacion
--	,ADMD.Nombre							AS DescripcionUbicacion --DescripcionDestino
--	,SAI.IdUsuario							AS IdUsuario
--	,U.Usuario							    AS Usuario
--	,U.NOMBRE_PILA + ' ' + U.APELLIDOS_PILA	
--											AS UsuarioNombreCompleto
--	,CASE AI.TipoAjuste
--		WHEN 0 THEN 14
--		WHEN 1 THEN 12		
--     END									AS IdEstado											
--	,8										AS IdMetodoAccion -- 8 equivale a ajuste de inventario
--	,CASE AI.idAjusteInventario
--		WHEN 1 THEN 'Toma física entrada'
--		WHEN 2 THEN 'Toma física salida'
--		WHEN 3 THEN 'Movimiento de inventario entrada'
--		WHEN 4 THEN 'Movimiento de inventario salida'
--		WHEN 5 THEN 'Bonificación entrada'
--		WHEN 6 THEN 'Reversion de bonificacion salida'
--		WHEN 7 THEN 'Reversion merma vida util entrada'
--		WHEN 8 THEN 'Merma vida util salida'
--		WHEN 9 THEN 'Reversion merma almacenamiento entrada'
--		WHEN 10 THEN 'Merma almacenamiento salida'
--		WHEN 11 THEN 'Reversion merma transporte entrada'
--		WHEN 12 THEN 'Merma transporte salida'
--		WHEN 13 THEN 'Reversion producto obsoleto entrada'
--		WHEN 14 THEN 'Producto obsoleto salida'
--		WHEN 15 THEN 'Reversion merma calidad entrada'
--		WHEN 16 THEN 'Merma calidad salida'
--		WHEN 17 THEN 'Produccion comisariato entrada'
--		WHEN 18 THEN 'Reversion produccion comisariato salida'
--		WHEN 19 THEN 'Reversion venta interna al personal entrada'
--		WHEN 20 THEN 'Venta interna al personal salida'
--		WHEN 21 THEN 'Reversion limpieza entrada'
--		WHEN 22 THEN 'Limpieza salida'
--		WHEN 23 THEN 'Reversion papelería entrada'
--		WHEN 24 THEN 'Papelería salida'
--		WHEN 25 THEN 'Reversión viáticos entrada'
--		WHEN 26 THEN 'Viáticos salida'
--		WHEN 27 THEN 'Reversion articulos menores entrada'
--		WHEN 28 THEN 'Articulos Menores salida'
--		WHEN 29 THEN 'Reversion producto de prueba entrada'
--		WHEN 30 THEN 'Producto de prueba salida'
--		WHEN 31 THEN 'Reversion familia gutierrez entrada'
--		WHEN 32 THEN 'Familia Gutierrez salida'		
--	 END									AS MetodoAccionDetalle
--	,RSA.Lote								AS Lote
--	--,'================'
--	--,*
											 
--FROM
--	RELArticuloXSolicitudAjuste RSA

--	INNER JOIN SolicitudAjusteInventario SAI
--	ON RSA.IdSolicitudAjusteInventario = SAI.IdSolicitudAjusteInventario
	
--	INNER JOIN ADMAjusteInventario AI
--	ON SAI.IdAjusteInventario = AI.idAjusteInventario

--	INNER JOIN ADMMaestroArticulo ADMMA
--	ON RSA.IdArticulo = ADMMA.idArticulo

--	INNER JOIN ADMDestino ADMD
--	ON SAI.idDestino = ADMD.idDestino

--	INNER JOIN Usuarios U
--	ON SAI.IdUsuario = U.IdUsuario
--WHERE 
--	SAI.Estado = 2 -- Estado = 1 pendiente || Estado = 2 aprobado
--GROUP BY
--	 ADMMA.IdArticulo 
--    ,ADMMA.idInterno						
--	,ADMMA.Nombre
--	,ADMMA.Unidad_Medida								
--	,AI.idAjusteInventario
--	,AI.TipoAjuste
--	,CONVERT(date, (SAI.FechaAprobado))
--	,ADMMA.Empaque
--	,SAI.IdSolicitudAjusteInventario
--	,ADMMA.Granel
--	,SAI.idDestino
--	,ADMD.Nombre
--	,SAI.IdUsuario
--	,U.Usuario	
--	,U.NOMBRE_PILA
--	,U.APELLIDOS_PILA	
--	,SAI.Estado
--	,RSA.Lote
----ORDER BY 
----	 CONVERT(date, (SAI.FechaAprobado)) DESC

--GO

--select * from RELArticuloXSolicitudAjuste
--select * from SolicitudAjusteInventario
--select * from ADMAjusteInventario
--select * from ADMDestino