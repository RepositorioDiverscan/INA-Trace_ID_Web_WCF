--SELECT TOP 5 * FROM SolicitudAjusteInventario 
--GO
--SELECT TOP 5 * FROM ADMAjusteInventario
--GO
--SELECT TOP 100 * FROM Trazabilidad WHERE IdMetodoAccion IS NULL OR IdSecuencia in(-1,-2)


-----Reporte de AJUSTES DE INVENTARIO
--SELECT * FROM ADMMaestroUbicacion WHERE idUbicacion IN(772, 783)
--SELECT * FROM Trazabilidad T WHERE T.IdSecuencia = -1
--GO



--REPORTE DE AJUSTE DE INVENTARIO MEJORADO
SELECT	
	 MA.IdArticulo						AS IdArticulo
	,ISNULL(MA.IdInterno, '')			AS IdInterno
	,ISNULL(MA.Nombre, '')				AS NombreArticulo
	,CASE MA.Granel
		WHEN 0 THEN CONVERT(INT,(ISNULL(T.Cantidad, 0)))					
		WHEN 1 THEN CONVERT(INT,(ISNULL(T.Cantidad, 0)/1000))					
	 END								AS Cantidad
	,MA.Unidad_Medida					AS UnidadMedida	
	,CASE 
		WHEN T.IdEstado = 12 AND T.IdMetodoAccion IS NULL	THEN 'Ajuste Inventario Entrada'
		WHEN T.IdEstado = 14 AND T.IdMetodoAccion IS NULL	THEN 'Ajuste Inventario Salida'
		WHEN T.IdEstado = 12 AND T.IdMetodoAccion = 8		THEN 'Ajuste Inventario Entrada'
		WHEN T.IdEstado = 14 AND T.IdMetodoAccion = 8		THEN 'Ajuste Inventario Salida'
		WHEN T.IdEstado = 12 AND T.IdMetodoAccion = 55		THEN 'Recepción'
		WHEN T.IdEstado = 12 AND T.IdMetodoAccion = 63		THEN 'Alisto'
	END									AS AjusteInventarioDescripcion
	,T.FechaRegistro					AS FechaRegistroTrazabilidad
	,T.IdUbicacion						AS IdUbicacion--IdDestino 
	,MU.Descripcion						AS Ubicacion --DescripcionDestino
	,T.IdUsuario						AS IdUsuario
	,U.Usuario							AS Usuario
	,U.NOMBRE_PILA 
			 + ' ' + U.APELLIDOS_PILA	
										AS UsuarioNombreCompleto
	,T.IdEstado							AS IdEstado
	,ISNULL(T.IdMetodoAccion, -3)		AS IdMetodoAccion
	,CASE 
		WHEN T.IdEstado = 12 AND T.IdMetodoAccion IS NULL	THEN 'Ajuste Inventario Entrada'
		WHEN T.IdEstado = 14 AND T.IdMetodoAccion IS NULL	THEN 'Ajuste Inventario Salida'
		WHEN T.IdEstado = 12 AND T.IdMetodoAccion = 8		THEN 'Ajuste Inventario Entrada'
		WHEN T.IdEstado = 14 AND T.IdMetodoAccion = 8		THEN 'Ajuste Inventario Salida'
		WHEN T.IdEstado = 12 AND T.IdMetodoAccion = 55		THEN 'Recepción'
		WHEN T.IdEstado = 12 AND T.IdMetodoAccion = 63		THEN 'Alisto'
	END									AS MetodoAccionDetalle
	,T.Lote								AS Lote	 
FROM 
	Trazabilidad T
	
	INNER JOIN ADMMaestroArticulo MA
	ON T.IdArticulo = MA.idArticulo

	INNER JOIN Usuarios U
	ON T.IdUsuario = U.IdUsuario

	INNER JOIN ADMMaestroUbicacion MU
	ON T.IdUbicacion = MU.IdUbicacion
	 
WHERE

	T.IdArticulo = 49 	
	AND 
	T.IdSecuencia IN(
		-1,--Proviene de un Ajuste de Inventario
		-2 --Proviene de una OC
	)	
	AND (
		T.IdMetodoAccion IN (
			55, --Recepción Entrada
			63  --Entrada Zona Tránsito
			)
		OR
		T.IdMetodoAccion IS NULL
		)
ORDER BY
	--T.IdArticulo
	T.IdSecuencia ASC
GO

--SELECT * FROM ADMMaestroUbicacion MU WHERE MU.IdUbicacion = 772
SELECT * FROM Trazabilidad  T WHERE T.IdMetodoAccion = 63
SELECT * FROM TipoMetodoAccion
SELECT * FROM ADMMaestroUbicacion