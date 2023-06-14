ALTER VIEW Vista_ArticulosIdInterno_Granel_NoGranel
AS 
/*			[NOTAS SOBRE VISTA]
	->Muestra los art�culos por su idInterno
	->Nombre
	->Si son de tipo granel
	->Unidad Medida
	->�SOLO ART�CULOS ACTIVOS�
*/
SELECT DISTINCT
	 ADMMA.idInterno	AS IdInternoArticulo
	,ADMMA.Nombre		AS NombreArticulo
	,ADMMA.Granel		AS EsGranel
	,SUBSTRING(ADMMA.Unidad_Medida, CHARINDEX('-', ADMMA.Unidad_Medida) + 1, LEN(ADMMA.Unidad_Medida))	
						AS UnidadMedida
	--,* 
FROM	ADMMaestroArticulo ADMMA
WHERE	ADMMA.Activo = 1
	
--ORDER BY ADMMA.idInterno ASC

