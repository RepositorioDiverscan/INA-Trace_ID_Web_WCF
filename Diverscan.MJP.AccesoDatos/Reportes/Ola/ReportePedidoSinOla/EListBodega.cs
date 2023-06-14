using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.ReportePedidoSinOla
{
    [Serializable]
    public class EListBodega
    {
        int _idBodega;
        string _nombre,
            _idinterno;

        public EListBodega(int idBodega, string nombre, string idinterno)
        {
            IdBodega = idBodega;
            Nombre = nombre;
            Idinterno = idinterno;
        }

        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Idinterno { get => _idinterno; set => _idinterno = value; }
    }
}
