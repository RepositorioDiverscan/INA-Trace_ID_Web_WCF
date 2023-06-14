CREATE TABLE Trazabilidad
(
	 IdTrazabilidad BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY
	,IdArticulo INT
	,Lote VARCHAR(200)
	,FechaVencimiento DATE
	,IdUbicacion INT
	,Cantidad INT
	,IdSecuencia BIGINT--NumeroDocumentoAccion INT
	,IdEstado INT 
	,IdUsuario INT
	,FechaRegistro DATETIME DEFAULT  GETDATE() ,
	IdMetodoAccion int
)

alter table Trazabilidad add IdMetodoAccion int null

-- DROP TABLE Trazabilidad