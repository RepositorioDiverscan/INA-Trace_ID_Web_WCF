using System;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudTraslado
{
    public class EDetalleSolicitudTraslado
    {
        //Atributos
        private int _idDetalle;
        private int _idArticulo;
        private string _nombreArticulo;
        private int _cantidad;
        private string _idInternoArticulo;

        //Constructor
        public EDetalleSolicitudTraslado(IDataReader reader)
        {
            _idDetalle = Convert.ToInt32(reader["Detalle"]);
            _idArticulo = Convert.ToInt32(reader["IDArticulo"]);
            _nombreArticulo = Convert.ToString(reader["Nombre"]);
            _cantidad = Convert.ToInt32(reader["Cantidad"]);
            _idInternoArticulo = Convert.ToString(reader["IDInternoArticulo"]);

        }

        //Metodos get y set
        public int IdDetalle { get => _idDetalle; set => _idDetalle = value; }
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }

        public string IdInternoArticulo { get => _idInternoArticulo; set => _idInternoArticulo = value; }




    }
}
