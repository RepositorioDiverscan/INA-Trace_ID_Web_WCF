SELECT * FROM Vistas.[dbo].V_INVENTARIO_CEDI_SAP V 
--WHERE V.ItemCode IN ('1002', '9007') 
ORDER BY V.OnHand ASC--WHERE VICS.ItemCode IN('1002', '1003')
GO
SELECT * FROM OPEDiferenciaArticulosERP V WHERE V.idInternoArticulo IN ('1002', '9007') ORDER BY V.Cantidad ASC-- WHERE DERP.idInternoArticulo IN('1002', '1003')
GO
SELECT * FROM Vista_DisponibilidadArticulos V WHERE V.idInterno IN ('1002', '9007')
GO