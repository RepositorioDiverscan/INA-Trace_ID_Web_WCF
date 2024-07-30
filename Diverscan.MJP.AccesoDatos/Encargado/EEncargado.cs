using System;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Encargado
{
    [Serializable]
    public class EEncargado
    {
        private long _idEncargado, _idBodega;
        private string _nombre, _mail, _comentarios;
        private bool _activo, _pedidoEntregado;

        public EEncargado()
        {
        }

        public EEncargado(long idEncargado, long idBodega, string nombre, string mail, string comentarios, bool activo, bool pedidoEntregado)
        {
            _idEncargado = idEncargado;
            _idBodega = idBodega;
            _nombre = nombre;
            _mail = mail;
            _comentarios = comentarios;
            _activo = activo;
            _pedidoEntregado = pedidoEntregado;
        }

        public EEncargado(long idBodega, string nombre, string mail, string comentarios, bool activo, bool pedidoEntregado)
        {
            _idBodega = idBodega;
            _nombre = nombre;
            _mail = mail;
            _comentarios = comentarios;
            _activo = activo;
            _pedidoEntregado = pedidoEntregado;
        }

        public EEncargado(IDataReader reader)
        {
            _idEncargado = Convert.ToInt64(reader["idEncargado"]);
            _idBodega = Convert.ToInt64(reader["idBodega"]);
            _nombre = reader["Nombre"].ToString();
            _mail = reader["Correo"].ToString();
            _comentarios = reader["Comentarios"].ToString();
            _activo = Convert.ToBoolean(reader["activo"]);
            _pedidoEntregado = Convert.ToBoolean(reader["Entregado"]);
        }

        public long IdEncargado { get => _idEncargado; set => _idEncargado = value; }
        public long IdBodega { get => _idBodega; set => _idBodega = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Mail { get => _mail; set => _mail = value; }
        public string Comentarios { get => _comentarios; set => _comentarios = value; }
        public bool Activo { get => _activo; set => _activo = value; }
        public bool PedidoEntregado { get => _pedidoEntregado; set => _pedidoEntregado = value; }
    }
}
