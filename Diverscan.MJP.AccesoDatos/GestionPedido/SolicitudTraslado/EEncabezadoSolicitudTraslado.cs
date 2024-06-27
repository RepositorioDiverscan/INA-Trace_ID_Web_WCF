using System;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.GestionPedido.SolicitudTraslado
{
    public class EEncabezadoSolicitudTraslado
    {
        //Atributos
        private int _idSolicitudTrasladoBodega;
        private string _numeroTransaccion;
        private int _idUsuarioSolicita;
        private string _nombreSolicitante;
        private int _idUsuarioProcesa;
        private string _nombreProcesa;
        private int _idBodegaOrigen;
        private string _nombreBodegaOrigen;
        private int _idBodegaDestino;
        private string _nombreBodegaDestino;
        private int _idPedidoOriginal;
        private string _estado;
        private DateTime _fechaRegistro;

        //Constructor
        public EEncabezadoSolicitudTraslado(IDataReader reader)
        {
            _idSolicitudTrasladoBodega = Convert.ToInt32(reader["Solicitud"]);
            NumeroTransaccion = (string)reader["NumeroTransaccion"];
            _idUsuarioSolicita = Convert.ToInt32(reader["IDUsuarioSolicita"]);
            _nombreSolicitante = (string) reader["NombreSolicita"];
            _idUsuarioProcesa = Convert.ToInt32(reader["IDUsuarioProcesa"]);
            _nombreProcesa = (string)reader["NombreProcesa"];
            _idBodegaOrigen = Convert.ToInt32(reader["Origen"]);
            _nombreBodegaOrigen = (string)reader["NombreBodegaOrigen"];
            _idBodegaDestino = Convert.ToInt32(reader["Destino"]);
            _nombreBodegaDestino = (string)reader["NombreBodegaDestino"];
            //_idPedidoOriginal = Convert.ToInt32(reader["IdPedidoOriginal"]);
            _estado = (string)reader["Estado"];
            _fechaRegistro = (DateTime)reader["Fecha"];
        }

        //Metodos Get y Set
        public int IdSolicitudTrasladoBodega { get => _idSolicitudTrasladoBodega; set => _idSolicitudTrasladoBodega = value; }
        public string NumeroTransaccion { get => _numeroTransaccion; set => _numeroTransaccion = value; }
        public int IdUsuarioSolicita { get => _idUsuarioSolicita; set => _idUsuarioSolicita = value; }
        public string NombreSolicitante { get => _nombreSolicitante; set => _nombreSolicitante = value; }
        public int IdUsuarioProcesa { get => _idUsuarioProcesa; set => _idUsuarioProcesa = value; }
        public string NombreProcesa { get => _nombreProcesa; set => _nombreProcesa = value; }
        public int IdBodegaOrigen { get => _idBodegaOrigen; set => _idBodegaOrigen = value; }
        public string NombreBodegaOrigen { get => _nombreBodegaOrigen; set => _nombreBodegaOrigen = value; }
        public int IdBodegaDestino { get => _idBodegaDestino; set => _idBodegaDestino = value; }
        public string NombreBodegaDestino { get => _nombreBodegaDestino; set => _nombreBodegaDestino = value; }
        public int IdPedidoOriginal { get => _idPedidoOriginal; set => _idPedidoOriginal = value; }
        public string Estado { get => _estado; set => _estado = value; }
        public DateTime FechaRegistro { get => _fechaRegistro; set => _fechaRegistro = value; }
    }
}
