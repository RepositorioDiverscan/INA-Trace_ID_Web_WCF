CREATE VIEW Vista_Ubicaciones_Transito_Id_Etiqueta

AS
SELECT
	 ADMMU.idUbicacion AS IdUbicacion
	,ADMMU.Descripcion AS EtiquetaUbicacion	 	
	--,*
FROM 
	ADMMaestroUbicacion ADMMU
WHERE 
	Descripcion LIKE '%TRA%'
