	
	
	DECLARE @ExisteDisponibilidad AS bit
	
	set @ExisteDisponibilidad=dbo.VerificarDisponibilidadTRA(249,'PRB002','2020-05-18',5,1)
	IF (@ExisteDisponibilidad = 1)
	begin
			SP_SalidaTrazabilidad 249,'PRB002','2020-05-18',5,1,1

			SP_EntradaTrazabilidad 249,'PRB002','2020-05-18',5,13,2,1

		exec SP_MovimientoTrazabilidad 249,'PRB002','2020-05-18',13,5,7,1
		select 'Exito'
	end
	else
		select 'No existe Disponibilidad de la cantidad articulo'