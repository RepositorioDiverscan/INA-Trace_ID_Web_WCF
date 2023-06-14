select a.idArticulo,
         b.Nombre,
   convert(nvarchar(10),a.FechaRegistro,103),
   b.GTIN,
   '01' + REPLICATE ( '0' ,14 - len(b.GTIN) ) + b.GTIN + '17' + convert(nvarchar(8),a.FechaVencimiento,12)  + '37' + REPLICATE ( '0' ,8 - len(cast(a.Cantidad as nvarchar (10)))) + cast(a.Cantidad  as nvarchar (10)) + '10' + a.Lote as GS1,
   '01' + REPLICATE ( '0' ,14 - len(b.GTIN) ) + b.GTIN + '17' + convert(nvarchar(8),a.FechaVencimiento,12)  + '10' + a.Lote as GS1_sin_cantidad,
   Cantidad,
   FechaVencimiento,
   Lote,
   a.FechaRegistro,
   --idRegistro,
   --NumDocumentoAccion,
   --idTablaCampoDocumentoAccion,
   --idCampoDocumentoAccion,
   idmetodoaccion,
   idEstado,
   idUbicacion,
   b.idBodega,
   c.Abreviatura 
  from TRAzabilidad as a 
    inner join ADMMaestroArticulo   as b on (a.idArticulo = b.idArticulo)
    inner join ADMUBIBodega         as c on (b.idBodega = c.idBodega)
  where  --a.idArticulo in (4)  
        --and a.idMetodoAccion in (55,59)
        --and lote = '110917' --Cantidad = 0.5 
        --and idEstado = 14 
        --and len(idUbicacion)> 3  --, 
    b.Nombre LIKE '%HARINA PARA PIZZA%'
  order by a.FechaRegistro desc,a.idArticulo,idMetodoAccion