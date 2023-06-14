using Diverscan.MJP.Entidades.MotivoAjusteInventario;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.AjusteInventario
{
    public class ArticuloXSolicitudAjusteDBA
    {
        public List<ArticuloXSolicitudAjusteDetalle> ObtenerDetalleSoliditudAjusteInventario(long pidSolicitudAjusteInventario)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerDetalleSoliditudAjusteInventario");

            dbTse.AddInParameter(dbCommand, "@IdSolicitudAjusteInventario", DbType.Int64, pidSolicitudAjusteInventario);

            List<ArticuloXSolicitudAjusteDetalle> articuloXSolicitudAjusteDetallelist = new List<ArticuloXSolicitudAjusteDetalle>();
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    var idSolicitudAjusteInventario = long.Parse(reader["IdSolicitudAjusteInventario"].ToString());
                    var idArticulo = long.Parse(reader["IdArticulo"].ToString());
                    var nombreArticulo = reader["NombreArticulo"].ToString();
                    var idInterno = reader["idInterno"].ToString();
                    var unidadMedida = reader["Unidad_Medida"].ToString();
                    var esGranel = bool.Parse(reader["Granel"].ToString());
                    var cantidad = int.Parse(reader["Cantidad"].ToString());
                    var cantidadUI = decimal.Parse(reader["CantidadUI"].ToString());
                    var lote = reader["Lote"].ToString();
                    var fechaVencimiento = DateTime.Parse(reader["FechaVencimiento"].ToString());
                    var idUbicacionActual = int.Parse(reader["IdUbicacionActual"].ToString());
                    var etiqueta = reader["ETIQUETA"].ToString();
                    var precio = string.IsNullOrEmpty(reader["Precio"].ToString()) ? 0 : decimal.Parse(reader["Precio"].ToString()) ;
                    articuloXSolicitudAjusteDetallelist.Add(new ArticuloXSolicitudAjusteDetalle(idSolicitudAjusteInventario,
                        idArticulo, idInterno, nombreArticulo, unidadMedida, esGranel, lote, fechaVencimiento, idUbicacionActual,
                        idUbicacionActual, cantidad, cantidadUI, etiqueta, precio));
             
                }
            }
            return articuloXSolicitudAjusteDetallelist;
        
        }


        public decimal ObtenerPrecioArticulo(long idArticulo)
        {
            var dbTse = DatabaseFactory.CreateDatabase("MJPConnectionString");
            var dbCommand = dbTse.GetStoredProcCommand("SP_ObtenerPrecioArticulo");

            dbTse.AddInParameter(dbCommand, "@IdArticulo", DbType.Int64, idArticulo);
            using (var reader = dbTse.ExecuteReader(dbCommand))
            {
                while (reader.Read())
                {
                    return decimal.Parse(reader["Precio"].ToString());
                }
            }
            return 0;
        }
    }
}
