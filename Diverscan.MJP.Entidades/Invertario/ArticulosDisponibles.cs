using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    [Serializable]
    public class ArticulosDisponibles : ArticuloInventario, IArticuloInventario
    {
        long _idRegistro;


        public ArticulosDisponibles(long idRegistro, long idArticulo, DateTime fechaVencimiento,
            string lote, long idUbicacion, string etiqueta, int cantidad, string unidadMedida, bool esGranel, string nombreArticulo, double unidadInventario)
            : base(idUbicacion, etiqueta, idArticulo, cantidad, fechaVencimiento, lote, unidadMedida, esGranel, nombreArticulo, unidadInventario)
        {
            _idRegistro = idRegistro;
        }

        public ArticulosDisponibles(IDataReader reader)
        {
            //_idRegistro = long.Parse(reader["IdRegistro"].ToString());           
            IdArticulo = long.Parse(reader["IdArticulo"].ToString());
            IdInterno = reader["idInterno"].ToString();
            FechaVencimiento = DateTime.Parse(reader["FechaVencimiento"].ToString());
            Lote = reader["Lote"].ToString();
            IdUbicacion = long.Parse(reader["IdUbicacion"].ToString());
            Etiqueta = reader["ETIQUETA"].ToString();
            Cantidad = int.Parse(Single.Parse(reader["Cantidad"].ToString()).ToString());
            UnidadMedida = reader["Unidad_Medida"].ToString();
            EsGranel = bool.Parse(reader["Granel"].ToString());
            NombreArticulo = reader["Nombre"].ToString();
            UnidadInventario = Convert.ToDouble(reader["UI"]);
        }

        public long IdRegistro
        {
            set { _idRegistro = value; }
            get { return _idRegistro; }
        }
    }
}
