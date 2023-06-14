using System;
using System.Data;

namespace Diverscan.MJP.Entidades.Operacion.TrasladoBodegas
{
    public class EArticulosBodegas
    {
        //Atributos
        private string _articulo;
        private string _idInterno;
        private string _bodega;
        private int _cantidad;
        private int _idBodega;
        private int _idArticulo;


        //Constructor
        public EArticulosBodegas(IDataReader reader)
        {
            _articulo = Convert.ToString(reader["Articulo"]);
            _idInterno = Convert.ToString(reader["ID_Interno"]);
            _bodega = Convert.ToString(reader["Bodega"]);
            _cantidad = Convert.ToInt32(reader["Cantidad"]);
            _idBodega = Convert.ToInt32(reader["ID_Bodega"]);
            _idArticulo = Convert.ToInt32(reader["ID_Articulo"]);
        }


        //Métodos Get y Set
        public string Articulo { get => _articulo; set => _articulo = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        public string Bodega { get => _bodega; set => _bodega = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
    }
}
