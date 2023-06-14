using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Cliente
{
    [Serializable]
    public class EListObtenerCliente
    {
        string _idCliente,
            _nombreCliente;

        public EListObtenerCliente(string idCliente, string nombreCliente)
        {
            _idCliente = idCliente;
            _nombreCliente = nombreCliente;
        }

        public string IdCliente { get => _idCliente; set => _idCliente = value; }
        public string NombreCliente { get => _nombreCliente; set => _nombreCliente = value; }
    }
}
