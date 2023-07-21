using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Pedidos.Cursos
{
    public class EDetallePedido
    {
        //Atributos
        private int _detalle, _idArticulo, _cantidad, _estado, _cantidadOriginal, _idBodega, _cantidadDisponibleBodega, _diferencia;
        private string _idInternoArticulo, _nombreArticulo, _nombreBodega;

        //Constructor
        public EDetallePedido(IDataReader reader)
        {
            _detalle = Convert.ToInt32(reader["Detalle"]);
            _idArticulo = Convert.ToInt32(reader["IDArticulo"]);
            _idInternoArticulo = Convert.ToString(reader["IDInternoArticulo"]);
            _nombreArticulo = Convert.ToString(reader["NombreArticulo"]);
            _cantidad = Convert.ToInt32(reader["Cantidad"]);
            _estado = Convert.ToInt32(reader["Estado"]);
            _cantidadOriginal = Convert.ToInt32(reader["CantidadOriginal"]);
            _idBodega = Convert.ToInt32(reader["IDBodega"]);
            _nombreBodega = Convert.ToString(reader["Bodega"]);
            _cantidadDisponibleBodega = Convert.ToInt32(reader["CantidadDisponibleBodega"]);
            _diferencia = Convert.ToInt32(reader["Diferencia"]);
        }

        //Métodos Get and Set
        public int Detalle { get => _detalle; set => _detalle = value; }
        public int IdArticulo { get => _idArticulo; set => _idArticulo = value; }
        public string IdInternoArticulo { get => _idInternoArticulo; set => _idInternoArticulo = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public int Estado { get => _estado; set => _estado = value; }
        public int CantidadOriginal { get => _cantidadOriginal; set => _cantidadOriginal = value; }
        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public string NombreBodega { get => _nombreBodega; set => _nombreBodega = value; }
        public int CantidadDisponibleBodega { get => _cantidadDisponibleBodega; set => _cantidadDisponibleBodega = value; }
        public int Diferencia { get => _diferencia; set => _diferencia = value; }
    }

}
