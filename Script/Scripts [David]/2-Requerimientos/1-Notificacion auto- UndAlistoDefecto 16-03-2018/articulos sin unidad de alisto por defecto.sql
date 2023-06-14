




	  select ma.idInterno as IdERP,
	        ma.idArticulo,
			ma.Nombre     as nombre_maestro,
			ma.GTIN13,
			ma.GTIN13_Activo,
			ma.Empaque_Maestro,
			ma.GTIN14,
			ma.GTIN14_Activo,
			ma.Empaque
		from ADMUnidadAlistoDefecto AS UAD 
		  full outer join Vista_RelacionGTIN13_GTIN14 AS MA on (MA.idArticulo = UAD.idArticulo)
		where isnull(UAD.idArticulo,0) = 0
		      AND ma.GTIN13_Activo = 1
			  --and ma.idInterno = 2003
		--select top 10 * from Vista_RelacionGTIN13_GTIN14 where idInterno = 2003
		--select * from ADMUnidadAlistoDefecto where idArticulo in (select idArticulo from ADMMaestroArticulo where idInterno = 2003)

	 --select * from ADMMaestroArticulo where nombre like '%palitos%'      --idInterno = 9520
	 --select * from Vistas..v_gtin13 where ItemCode = 9520