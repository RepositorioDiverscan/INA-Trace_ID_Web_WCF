using System;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.GestionPedido.PedidoOriginal
{
    public class EPedidoOriginalEncabezado
    {
        private int _id;
        private string _idInterno;
        private string _nombreDestino;
        private string _descripcionDestino;
        private DateTime _fechaCreacion;
        private DateTime _fechaEntrega;
        private string _tipo;
        private string _profesor;
        private string _estado;

        public EPedidoOriginalEncabezado(IDataReader reader)
        {
            _id = Convert.ToInt32(reader["ID"]);
            _idInterno = Convert.ToString(reader["IdInterno"]);
            _nombreDestino = Convert.ToString(reader["NombreDestino"]);
            _descripcionDestino = Convert.ToString(reader["DescripcionDestino"]);
            _fechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
            _fechaEntrega = Convert.ToDateTime(reader["FechaEntrega"]);
            _tipo = Convert.ToString(reader["Tipo"]);
            _profesor = Convert.ToString(reader["Profesor"]);
            _estado = Convert.ToString(reader["Estado"]);
        }

        public int Id { get => _id; set => _id = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        public string NombreDestino { get => _nombreDestino; set => _nombreDestino = value; }
        public string DescripcionDestino { get => _descripcionDestino; set => _descripcionDestino = value; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public DateTime FechaEntrega { get => _fechaEntrega; set => _fechaEntrega = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        public string Profesor { get => _profesor; set => _profesor = value; }
        public string Estado { get => _estado; set => _estado = value; }

    }
}
