DECLARE @PIdInternoArticulo VARCHAR(1000),
		@PCantidadKGSolicitada DECIMAL(18,2)
--SET @PIdInternoArticulo = '1023'

--SET @PIdInternoArticulo = '2001'

SET @PIdInternoArticulo = '10013'

SET @PCantidadKGSolicitada = 1
		
		SELECT --TOP 1
			VDAP.idarticulo,
			VDAP.idInterno,
			VDAP.Granel,
			--Fecha de vencimiento más pronta a vencer IdArticulo 
			(SELECT TOP 1 FV FROM Vista_DisponibilidadArticulos VD WHERE VDAP.idarticulo = VD.idarticulo ORDER BY FV ASC) FV, 
			--Cantidad total de artículo ignorando su fecha vencimiento y agrupado por IdInterno
			(SELECT TOP 1
				T.CantidadUI	
			FROM 	
			(SELECT--SE OBTIENE LA CANTIDAD por artículo que pertenece al mismo idinterno
				VDAS.idInterno,
				VDAS.idarticulo,
				(SELECT TOP 1 FV FROM Vista_DisponibilidadArticulos VDI WHERE VDI.idarticulo = VDAS.idarticulo ORDER BY FV ASC)  FV,
				SUM(VDAS.SUMACantidadEstado) AS CantidadUI
			FROM 
				Vista_DisponibilidadArticulos VDAS
			WHERE 
				VDAS.idarticulo = VDAP.idarticulo
			GROUP BY 
				VDAS.idInterno,
				VDAS.idarticulo
						) AS T) AS CantidadUI
		FROM 
			Vista_DisponibilidadArticulos VDAP
		WHERE 
			VDAP.idInterno = @PIdInternoArticulo				

		GROUP BY  
			idarticulo,
			idInterno,
			Granel	
		
		--HAVING 

		--	@PCantidadKGSolicitada <= (SELECT TOP 1
		--		T.CantidadUI	
		--	FROM 	
		--	(SELECT--SE OBTIENE LA CANTIDAD por artículo que pertenece al mismo idinterno
		--		VDAS.idInterno,
		--		VDAS.idarticulo,
		--		(SELECT TOP 1 FV FROM Vista_DisponibilidadArticulos VDI WHERE VDI.idarticulo = VDAS.idarticulo ORDER BY FV ASC)  FV,
		--		SUM(VDAS.SUMACantidadEstado) AS CantidadUI
		--	FROM 
		--		Vista_DisponibilidadArticulos VDAS
		--	WHERE 
		--		VDAS.idarticulo = VDAP.idarticulo
		--	GROUP BY 
		--		VDAS.idInterno,
		--		VDAS.idarticulo
		--				) AS T)			

		ORDER BY (SELECT TOP 1 FV FROM Vista_DisponibilidadArticulos VD WHERE VDAP.idarticulo = VD.idarticulo ORDER BY FV ASC)


