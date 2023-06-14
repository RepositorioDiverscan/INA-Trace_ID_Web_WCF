ALTER PROCEDURE SP_Obtener_Id_Articulo_Disponible_Pronto_A_Vencer_Detalle_Solicitud
	 @PIdInternoArticulo	VARCHAR(1000)
	,@PCantidadKilos		DECIMAL(18,2)
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
	,@CCantidadUI	DECIMAL(18,2)

	--Variables para compararción de elementos en el cursor
	,@AUXIdArticulo	BIGINT
	,@AUXIdInterno	VARCHAR(1000)
	,@AUXGranel		BIT
	,@AUXFV			DATETIME
	,@AUXCantidadUI	DECIMAL(18,2)
	,@AUXCantidadEncontrada	DECIMAL(18,2)



	SET @CIdArticulo = -1
	SET @AUXCantidadEncontrada  = -1
	DECLARE Cursor_ObtenerArticulo CURSOR FOR   

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
			,@CCantidadUI	
					
		--ERROR CURSOR
		IF @@FETCH_STATUS <> 0 
		BEGIN  
			
			SET @PIdArticuloOUT = -1
			SET @PGranelOUT = 0
			SET @PIsFoundOUT = 0
			PRINT '         <<ERROR AL EJECUTAR EL CURSOR>>' 
		END--CASO DE ENCONTRAR EL ARTÍCULO DISPONIBLE
		ELSE IF @PIdArticuloOUT <> -1
		BEGIN
			SET @PIdArticuloOUT = @CIdArticulo
			SET @PGranelOUT = @CGranel
			SET @PIsFoundOUT = 1

			--Se cargan las variables con el primer registro encontrado
			SET @AUXIdArticulo = @CIdArticulo
			SET @AUXFV = @CFV
			SET @AUXCantidadUI = @CCantidadUI	

			--PRINT '!No hay disponibilidad del artículo!'
		END

		

		--[RECORRIDO DEL CURSOR]
		WHILE @@FETCH_STATUS = 0
		BEGIN

			----SHOW CURSOR
			--SELECT 	
			--	 @CIdArticulo	IdArticulo	
			--	,@CIdInterno	IdInterno	 
			--	,@CGranel		Granel	
			--	,@CFV			FV			
			--	,@CCantidadUI	CantidadUI
			
			
			PRINT '==========[RECORRIDO DE REGISTROS]=============='
			PRINT 'IdArticulo ' +  CONVERT(VARCHAR, @CIdArticulo)
			PRINT 'IdInterno ' +  CONVERT(VARCHAR, @CIdInterno)
			PRINT 'Granel ' +  CONVERT(VARCHAR, @CGranel)
			PRINT 'FV' +  CONVERT(VARCHAR, @CFV)
			PRINT 'Cantidad ' +  CONVERT(VARCHAR, @CCantidadUI)			
								
										
			--[FV menor a la actual y cantidad disponible es mayor a la solicitada]
			PRINT 'AUX: |' + CONVERT(VARCHAR, @AUXFV) + ' CFV: |' +   CONVERT(VARCHAR, @CFV) 
			PRINT 'CANT.DISPONIBLE: |' + CONVERT(VARCHAR, @CCantidadUI) + ' CANT.SOLI |' + CONVERT(VARCHAR, @PCantidadKilos)
			IF(CONVERT(DATE, @CFV) <= CONVERT(DATE, @AUXFV) AND @PCantidadKilos <= @CCantidadUI)
			BEGIN
				SET @PIdArticuloOUT = @CIdArticulo
				SET @PGranelOUT = @CGranel
				SET @PIsFoundOUT = 1	
				SET @AUXCantidadEncontrada = @CCantidadUI
				
				PRINT '>>CANTIDAD MENOR IGUAL A LA DISPONIBLE<<<'						
						
			END 

			--RECARGA DE VARIABLES AUXILIARES
			SET @AUXIdArticulo = @CIdArticulo
			SET @AUXFV = @CFV
			SET @AUXCantidadUI = @CCantidadUI			

			SELECT 
				 @CIdArticulo = VDAP.idarticulo
				,@CIdInterno = VDAP.idInterno
				,@CGranel = VDAP.Granel
				--Fecha de vencimiento más pronta a vencer IdArticulo 
				,@CFV = (SELECT TOP 1 FV FROM Vista_DisponibilidadArticulos VD WHERE VDAP.idarticulo = VD.idarticulo ORDER BY FV ASC) 
				--Cantidad total de artículo ignorando su fecha vencimiento y agrupado por IdInterno
				,@CCantidadUI = (SELECT TOP 1
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
			--SIG-REGISTRO
			FETCH NEXT FROM  Cursor_ObtenerArticulo INTO 		 
				 @CIdArticulo	
				,@CIdInterno	
				,@CGranel		
				,@CFV			
				,@CCantidadUI				
		END	
		CLOSE Cursor_ObtenerArticulo;  
	DEALLOCATE Cursor_ObtenerArticulo; 
		
		PRINT '===========[IMPRESION DE DATOS]============'
		PRINT CONVERT(VARCHAR, @PIdArticuloOUT)
		PRINT CONVERT(VARCHAR, @PGranelOUT)
		PRINT CONVERT(VARCHAR, @PIsFoundOUT)
		PRINT CONVERT(VARCHAR, @AUXCantidadEncontrada)
END

GO
--EXEC SP_Obtener_Id_Articulo_Disponible_Pronto_A_Vencer_Detalle_Solicitud '4001',0, 0,0,0
--EXEC SP_Obtener_Id_Articulo_Disponible_Pronto_A_Vencer_Detalle_Solicitud '2003',1800, 0,0,0--HONGOS
EXEC SP_Obtener_Id_Articulo_Disponible_Pronto_A_Vencer_Detalle_Solicitud '1023',1801, 0,0,0--BASE PAN PIZZA
--EXEC SP_Obtener_Id_Articulo_Disponible_Pronto_A_Vencer_Detalle_Solicitud '19002',0, 0,0,0---
--EXEC SP_Obtener_Id_Articulo_Disponible_Pronto_A_Vencer_Detalle_Solicitud '10013',0, 0,0,0---CEBOLLA MORADA P/PIZZA








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
	 

