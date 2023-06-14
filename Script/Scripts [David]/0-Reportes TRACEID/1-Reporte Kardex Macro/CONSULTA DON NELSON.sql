SELECT D.idInterno,
        D.Nombre as articulo,
        E.Nombre,
  FORMAT(B.FechaRegistro, 'dd-MMM-yyyy hh:mm tt', 'en-US' ) AS 'DateTime Result',
  sum(A.Cantidad) - isNull(C.Cantidad,0) AS Cantidad,
  (
  SELECT TOP 1  
   CASE UAD.esGTIN13
   WHEN 0 THEN (SELECT TOP 1 SUBSTRING(VL.Nombre, CHARINDEX('-',VL.Nombre)+1,LEN(VL.Nombre)) FROM ADMGTIN14VariableLogistica VL WHERE VL.idInterno = D.idInterno AND VL.Activo = 1 ORDER BY  VL.ConsecutivoGTIN14 ASC)
   WHEN 1 THEN SUBSTRING(ADMMA.Empaque, CHARINDEX('-',ADMMA.Empaque)+1,LEN(ADMMA.Empaque))  
   END     AS UnidadInventario  
  --*
  FROM ADMUnidadAlistoDefecto UAD 
   INNER JOIN ADMMaestroArticulo ADMMA
   ON UAD.idArticulo = ADMMA.idArticulo
  WHERE 
   ADMMA.Activo = 1 
   AND ADMMA.idInterno = D.idInterno
 )        AS UnidadInventario 
FROM [dbo].[TrazabilidadAuxEntrada] AS A
 INNER JOIN [dbo].[Trazabilidad]   AS B ON (A.IdTrazabilidad = B.IdTrazabilidad)
 Left Join Vista_SSCCNoTrasladado  AS C ON (B.IdUbicacion = C.IdUbicacion)
 INNER JOIN ADMMaestroArticulo     AS D ON (B.idarticulo = D.idArticulo)
 INNER JOIN TipoMetodoAccion       AS E ON (ISNULL(B.idmetodoaccion,8) = E.IdMetodoAccion)
WHERE D.idCompania = 'AMCO'
   GROUP BY D.idInterno, D.Nombre, E.Nombre, FORMAT(B.FechaRegistro, 'dd-MMM-yyyy hh:mm tt', 'en-US' ), C.Cantidad
   ORDER BY FORMAT(B.FechaRegistro, 'dd-MMM-yyyy hh:mm tt', 'en-US' ) DESC, E.Nombre