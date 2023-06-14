using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Diverscan.MJP.Entidades.Devolutions.SolicitudDevolucion
{
    [DataContract]
    public class e_Transportista
    {
        string _nombre;
        int _idTransportista;

        public e_Transportista(string nombre, int idTransportista)
        {
            _nombre = nombre;
            _idTransportista = idTransportista;
        }

        [DataMember]
        public string Nombre { get => _nombre; set => _nombre = value; }
        [DataMember]
        public int IdTransportista { get => _idTransportista; set => _idTransportista = value; }

    }
}
