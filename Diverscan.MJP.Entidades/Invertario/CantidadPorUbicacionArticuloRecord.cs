using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    [Serializable]
    public class CantidadPorUbicacionArticuloRecord : ArticuloInventario, ICantidadPorUbicacionArticuloRecord
    {
        public CantidadPorUbicacionArticuloRecord(long idUbicacion, string etiqueta, long idArticulo, int cantidad,DateTime fechaVencimiento,
            string lote, string unidadMedida, bool esGranel, string nombreArticulo,double unidadInventario)
            :base(idUbicacion,  etiqueta,  idArticulo,  cantidad, fechaVencimiento,lote,  unidadMedida,  esGranel,  nombreArticulo, unidadInventario)
        {
        }

        public CantidadPorUbicacionArticuloRecord(IDataReader reader)
        {
             IdUbicacion = long.Parse(reader["IdUbicacion"].ToString());
             Etiqueta = reader["ETIQUETA"].ToString();
             IdArticulo = long.Parse(reader["IdArticulo"].ToString());
             IdInterno = reader["idInterno"].ToString();
            Cantidad = int.Parse(reader["Cantidad"].ToString());
             FechaVencimiento = DateTime.Parse(reader["FechaVencimiento"].ToString());
             Lote = reader["Lote"].ToString();
             UnidadMedida = reader["Unidad_Medida"].ToString();
             EsGranel = bool.Parse(reader["Granel"].ToString());
             NombreArticulo = reader["Nombre"].ToString();
             var ddddd = reader["UI"].ToString();
             UnidadInventario = Convert.ToDouble(reader["UI"]);
         }

        
    }
}
