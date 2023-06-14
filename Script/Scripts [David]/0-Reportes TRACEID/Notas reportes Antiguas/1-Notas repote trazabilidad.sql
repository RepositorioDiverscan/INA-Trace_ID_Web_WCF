/*
	NOTAS GENERALES

-Agregar el campo GTIN13 de artículo en todos los grids
*/




/*=====================================================================
				[[GRID TRAZABILIDAD]]


--En la lista de los artículos en reporte trazabilidad 
 --CARGAR LOS ARTÍCULOS COMO IdInterno HACIENDO UN distinct

 --Cargar un lista de artículos agrupadas por el IdInterno y los reportes agrupados por dicho IDInterno de artículo
 --*********No aplica actualmente se realiza en kardex macro**************
=====================================================================*/
SELECT * FROM ADMMaestroArticulo MA WHERE MA.Nombre LIKE '%HONGOS REBANADOS%'
GO


/*=====================================================================
				 [[GRID AJUSTES DE INVENTARIO]]
	
	-AGREGAR:
		-IdSolicitudAjusteInventario de la tabla SolicitudAjusteInventario
=====================================================================*/
SELECT * FROM ADMAjusteInventario AI
INNER JOIN  SolicitudAjusteInventario SA
ON AI.idAjusteInventario = SA.IdAjusteInventario
INNER JOIN Usuarios U
ON SA.IdUsuario = U.IDUSUARIO
GO




/*=====================================================================
				[[GRID GRID DESPACHOS]]
	
	-AGREGAR:
		-IdInterno SAP de OPESALMaestroSolicitud --> idInternoSAP
		Listo también se agredo el id de solicitud de traceid
=====================================================================*/
SELECT top 2 * FROM OPEArticulosDespachados AD WHERE AD.idArticulo = 49
SELECT * FROM OPESALMaestroSolicitud MS WHERE MS.idMaestroSolicitud = 2898
GO


