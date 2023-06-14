		
		DECLARE @PIdInternoArticulo	VARCHAR(1000)
		SET @PIdInternoArticulo = '1023'--HONGOS
--SET @PIdInternoArticulo = 13015 -- BOLSA BASURA		
		SELECT 
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

		ORDER BY (SELECT TOP 1 FV FROM Vista_DisponibilidadArticulos VD WHERE VDAP.idarticulo = VD.idarticulo ORDER BY FV ASC)
	go

	SELECT * FROM Vista_DisponibilidadArticulos VD 
	WHERE VD.idInterno = 1023-- 13015
	ORDER BY VD.idInterno