using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Transportista
{
    [Serializable]
    public class ETransportista
    {
        private long _idTransportista;
        private long _idBodega;
        private string _nombre;
        private string _telefono;
        private string _mail;
        private string _comentarios;
        private bool _activo;

        public ETransportista()
        {
        }

        public ETransportista(IDataReader reader)
        {
            _idTransportista = long.Parse(reader["idTransportista"].ToString());
            _idBodega = long.Parse(reader["idBodega"].ToString());
            _nombre = reader["Nombre"].ToString();
            _telefono = reader["Telefono"].ToString();
            _mail = reader["Correo"].ToString();
            _comentarios = reader["Comentarios"].ToString();
        }

        public ETransportista(long idTransportista, long idBodega, string nombre, string telefono, string mail, string comentarios, bool activo)
        {
            _idTransportista = idTransportista;
            _idBodega = idBodega;
            _nombre = nombre;
            _telefono = telefono;
            _mail = mail;
            _comentarios = comentarios;
            _activo = activo;
        }

        public ETransportista(long idBodega, string nombre, string telefono, string mail, string comentarios, bool activo)
        {            
            _idBodega = idBodega;
            _nombre = nombre;
            _telefono = telefono;
            _mail = mail;
            _comentarios = comentarios;
            _activo = activo;
        }

        public long IdTransportista { get => _idTransportista; set => _idTransportista = value; }
        public long IdBodega { get => _idBodega; set => _idBodega = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Telefono { get => _telefono; set => _telefono = value; }
        public string Mail { get => _mail; set => _mail = value; }
        public string Comentarios { get => _comentarios; set => _comentarios = value; }
        public bool Activo { get => _activo; set => _activo = value; }
    }
}
