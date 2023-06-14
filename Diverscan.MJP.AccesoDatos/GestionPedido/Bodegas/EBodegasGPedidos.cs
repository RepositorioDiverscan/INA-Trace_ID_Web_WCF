using System;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.GestionPedido.Bodegas
{
    public class EBodegasGPedidos
    {
        //Atributos
        private int _idBodega;
        private string _nombreBodega;
        private string _nombreArticulo;
        private int _cantidadDisponible;
        private string _idInternoArticulo;
        private int _idArticulo;

        //Constructor
        public EBodegasGPedidos(IDataReader reader)
        {
            _idBodega = Convert.ToInt32(reader["Id_Bodega"]);
            _nombreBodega = Convert.ToString(reader["Bodega"]);
            _nombreArticulo = Convert.ToString(reader["Articulo"]);
            _cantidadDisponible = Convert.ToInt32(reader["Cantidad"]);
            _idInternoArticulo = Convert.ToString(reader["IdInterno"]);
            _idArticulo = Convert.ToInt32(reader["IdArticulo"]);
        }

        //Metodos Get y Set
        public int IdBodega { get => _idBodega; set => _idBodega = value; }

        public string NombreBodega { get => _nombreBodega; set => _nombreBodega = value; }

        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }

        public int CantidadDisponible { get => _cantidadDisponible; set => _cantidadDisponible = value; }

        public string IdInternoArticulo { get => _idInternoArticulo; set => _idInternoArticulo = value; }


        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
    }
}
