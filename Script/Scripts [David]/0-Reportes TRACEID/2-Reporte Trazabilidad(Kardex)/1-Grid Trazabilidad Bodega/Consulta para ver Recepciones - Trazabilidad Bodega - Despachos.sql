--===========================================
--  VER ARTÍCULOS RECIBIDOS POR FECHA
--===========================================
SELECT DISTINCT 
--*
AROC.idArticulo,
CONVERT(DATE, AROC.FechaRegistro),
MA.Nombre
FROM OPEArticulosRecibidosOC AROC 
	 INNER JOIN ADMMaestroArticulo MA
	 ON AROC.idArticulo = MA.idArticulo
ORDER BY CONVERT(DATE, AROC.FechaRegistro)  DESC
GO

--===========================================
--  VER TRAZABILIDAD BODEGA ARTÍCULO
--===========================================

SELECT * FROM TipoMetodoAccion


SELECT DISTINCT 
--*
T.idArticulo,
CONVERT(DATE, T.FechaRegistro),
MA.Nombre,
TMA.Nombre
FROM Trazabilidad T
	 INNER JOIN ADMMaestroArticulo MA
	 ON T.idArticulo = MA.idArticulo
	 
	 INNER JOIN TipoMetodoAccion TMA
	 ON TMA.IdMetodoAccion = T.IdMetodoAccion
WHERE
	T.IdMetodoAccion IN(58, 59, 75,76)
ORDER BY CONVERT(DATE, T.FechaRegistro)  DESC
GO

--===========================================
--  VER ARTÍCULOS DESPACHADOS POR FECHA
--===========================================
SELECT DISTINCT 
--*
AD.idArticulo,
CONVERT(DATE, AD.FechaRegistro),
MA.Nombre
FROM OPEArticulosDespachados AD
	 INNER JOIN ADMMaestroArticulo MA
	 ON AD.idArticulo = MA.idArticulo
ORDER BY CONVERT(DATE, AD.FechaRegistro)  DESC
GO