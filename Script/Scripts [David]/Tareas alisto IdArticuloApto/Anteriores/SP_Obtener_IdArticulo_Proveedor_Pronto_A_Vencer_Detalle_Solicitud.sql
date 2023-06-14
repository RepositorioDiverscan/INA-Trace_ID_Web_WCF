ALTER PROCEDURE SP_Obtener_IdArticulo_Proveedor_Pronto_A_Vencer_Detalle_Solicitud
	 @PIdInternoArticulo	VARCHAR(1000)
	,@PCantidadKilosSolicitada	DECIMAL(18,2)
	,@PIdArticuloOUT		BIGINT OUTPUT
	,@PGranelOUT            BIT	OUTPUT
	,@PIsFoundOUT			BIT OUTPUT

AS
BEGIN

------------------[CURSOR]-----------------------------
DECLARE 
	--Variables para recorrido de cursor		
	 @CIdArticulo	BIGINT
	,@CIdInterno	VARCHAR(1000)
	,@CGranel		BIT
	,@CFV			DATETIME
	,@CCantidadDisponible	DECIMAL(18,2)

	--Variables para compararción de elementos en el cursor
	,@AUXIdArticulo	BIGINT
	,@AUXIdInterno	VARCHAR(1000)
	,@AUXGranel		BIT
	,@AUXFV			DATETIME
	,@AUXCantidadUI	DECIMAL(18,2)


	--BANDERAS DE CURSOR
	,@AUXCantidadEstaDisponible	BIT,
	 @AUXArticuloDisponible BIT



	SET @CIdArticulo = -1
	SET @AUXCantidadEstaDisponible = 0
	SET @AUXArticuloDisponible = 0


	IF EXISTS(SELECT VDA.idInterno FROM Vista_DisponibilidadArticulos VDA WHERE VDA.idInterno = @PIdInternoArticulo)
	BEGIN
		SET @AUXArticuloDisponible = 1
	END
	ELSE
	BEGIN
		SET @PIdArticuloOUT = -1
		SET @PGranelOUT = 0
		SET @PIsFoundOUT = 0
	END



	--Si hay artículos disponibles de ese IdInterno se ejecuta el cursor para buscar el más apto
	IF (@AUXArticuloDisponible = 1)
	BEGIN
	DECLARE Cursor_ObtenerArticulo CURSOR FOR   
	
		--[Consulta para extraer los datos]
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

		OPEN Cursor_ObtenerArticulo
		FETCH NEXT FROM  Cursor_ObtenerArticulo
		INTO 
			 @CIdArticulo	
			,@CIdInterno	
			,@CGranel		
			,@CFV			
			,@CCantidadDisponible	
					
		--ERROR CURSOR
		IF @@FETCH_STATUS <> 0 
		BEGIN  
			
			SET @PIdArticuloOUT = -1
			SET @PGranelOUT = 0
			SET @PIsFoundOUT = 0
			PRINT '<<ERROR AL EJECUTAR EL CURSOR>>' 
		END


		--[RECORRIDO DEL CURSOR]
		WHILE @@FETCH_STATUS = 0
		BEGIN

			------SHOW CURSOR
			--SELECT 	
			--	 @CIdArticulo	IdArticulo	
			--	,@CIdInterno	IdInterno	 
			--	,@CGranel		Granel	
			--	,@CFV			FV			
			--	,@CCantidadDisponible	CantidadUI
																				
			/*
				CASO 1:Condiciones
				-> La FV es menor o igual a la del primero registro 
				-> La cantidad disponible es menor o igual a la solicitada
			*/						
			IF(@PCantidadKilosSolicitada <= @CCantidadDisponible AND @AUXCantidadEstaDisponible = 0)
			BEGIN
				SET @PIdArticuloOUT = @CIdArticulo
				SET @PGranelOUT = @CGranel
				SET @PIsFoundOUT = 1	
				SET @AUXCantidadEstaDisponible = 1
				SET @AUXCantidadUI = @CCantidadDisponible
			END	
			--[Si la cantidad es infrerior a la disponible trae la máxima posible]
			ELSE IF (@PCantidadKilosSolicitada > @CCantidadDisponible AND @PIsFoundOUT = 0)
			BEGIN
				SELECT TOP 1
					 @PIdArticuloOUT = VDAP.idarticulo					
				    ,@PGranelOUT = VDAP.Granel
					,@PIsFoundOUT = 1
					,@AUXCantidadUI = (SELECT TOP 1
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
								) AS T)			
				FROM 
					Vista_DisponibilidadArticulos VDAP
				WHERE 
					VDAP.idInterno = @PIdInternoArticulo				
				GROUP BY  
					idarticulo,
					idInterno,
					Granel	

				--ORDER BY (SELECT TOP 1 FV FROM Vista_DisponibilidadArticulos VD WHERE VDAP.idarticulo = VD.idarticulo ORDER BY FV ASC)
				ORDER BY --SE ORDENA POR LA CANTIDAD PARA QUE DEVUELVA EL ID CON MAYOR CANTIDAD POSIBLE
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
								) AS T) DESC


			END
																				
			--RECARGA DE VARIABLES AUXILIARES
			SET @AUXIdArticulo = @CIdArticulo
			SET @AUXFV = @CFV
			--SET @AUXCantidadUI = @CCantidadDisponible			


			--[LECURA DENTRO DEL WHILE]
			SELECT 
				 @CIdArticulo = VDAP.idarticulo
				,@CIdInterno = VDAP.idInterno
				,@CGranel = VDAP.Granel
				--Fecha de vencimiento más pronta a vencer IdArticulo 
				,@CFV = (SELECT TOP 1 FV FROM Vista_DisponibilidadArticulos VD WHERE VDAP.idarticulo = VD.idarticulo ORDER BY FV ASC) 
				--Cantidad total de artículo ignorando su fecha vencimiento y agrupado por IdInterno
				,@CCantidadDisponible = (SELECT TOP 1
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
								) AS T) --AS CantidadUI
				FROM 
					Vista_DisponibilidadArticulos VDAP
				WHERE 
					VDAP.idInterno = @PIdInternoArticulo				
				GROUP BY  
					idarticulo,
					idInterno,
					Granel	

				ORDER BY (SELECT TOP 1 FV FROM Vista_DisponibilidadArticulos VD WHERE VDAP.idarticulo = VD.idarticulo ORDER BY FV ASC)


			--[SIGUIENTE REGISTRO]
			FETCH NEXT FROM  Cursor_ObtenerArticulo INTO 		 
					@CIdArticulo	
				,@CIdInterno	
				,@CGranel		
				,@CFV			
				,@CCantidadDisponible				
			END	
			CLOSE Cursor_ObtenerArticulo;  
		DEALLOCATE Cursor_ObtenerArticulo; 
	END

	PRINT '===========[DATOS OBTENIDOS]============'
	PRINT '===========[IMPRESION DE DATOS]============'
	PRINT 'ID ARTICULO: ' + CONVERT(VARCHAR, @PIdArticuloOUT)
	PRINT 'GRANEL: ' + CONVERT(VARCHAR, @PGranelOUT)
	PRINT 'ISFOND: ' + CONVERT(VARCHAR, @PIsFoundOUT)
	PRINT 'CANTIDADDISPONIBLE: '  + CONVERT(VARCHAR, @AUXCantidadUI)
END

GO

EXEC SP_Obtener_IdArticulo_Proveedor_Pronto_A_Vencer_Detalle_Solicitud '10013',0, 0,0,0--BASE PAN PIZZA
EXEC SP_Obtener_IdArticulo_Proveedor_Pronto_A_Vencer_Detalle_Solicitud '1001DD3',11230, 0,0,0--BASE PAN PIZZA
--EXEC SP_Obtener_IdArticulo_Proveedor_Pronto_A_Vencer_Detalle_Solicitud '4001',0, 0,0,0
--EXEC SP_Obtener_IdArticulo_Proveedor_Pronto_A_Vencer_Detalle_Solicitud '2003',1800, 0,0,0--HONGOS

--EXEC SP_Obtener_IdArticulo_Proveedor_Pronto_A_Vencer_Detalle_Solicitud '19002',0, 0,0,0---
--EXEC SP_Obtener_IdArticulo_Proveedor_Pronto_A_Vencer_Detalle_Solicitud '10013',0, 0,0,0---CEBOLLA MORADA P/PIZZA








--FECHA MÁS PRONTA
--SELECT TOP 3 FV FROM Vista_DisponibilidadArticulos WHERE idInterno = 1023 ORDER BY FV ASC
--SELECT * FROM Vista_DisponibilidadArticulos ORDER BY idarticulo,idInterno ASC
--EXEC SP_Obtener_Id_Articulo_Disponible_Pronto_A_Vencer_Detalle_Solicitud '1023',0, 0,0,0
------CANTIDAD DEL ARTÍCULO DESPRECIANDO LA FECHA
--SELECT
--	 T.idInterno
--	,T.idarticulo
--	,T.FV
--	,T.CantidadUI	
--FROM 
	
--	(SELECT
--		idInterno,
--		idarticulo,
--		(SELECT TOP 1 FV FROM Vista_DisponibilidadArticulos WHERE idInterno = 1023 ORDER BY FV ASC) FV,
--		SUM(SUMACantidadEstado)	AS CantidadUI
--	FROM 
--		Vista_DisponibilidadArticulos 
--	WHERE 
--		idInterno = 1023
--	GROUP BY 
--		idInterno,
--		idarticulo
--				) AS T
--GO

--SELECT idInterno,idarticulo, COUNT(idarticulo) FROM Vista_DisponibilidadArticulos  GROUP BY idInterno,idarticulo HAVING COUNT(IdArticulo) > 1
--select * from Vista_DisponibilidadArticulos where idInterno = 4001
	 

