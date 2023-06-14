using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Inventario.UnidadDisponibleTIDSAP.Entidad
{
    [Serializable]
    public class EListUnidadDisponibleTIDSAP
    {
        string _nombre,
            _idInterno;
        int _unidadTraceID,
            _unidadSAP,
            _diferencia;

        public EListUnidadDisponibleTIDSAP(string nombre, string idInterno, int unidadTraceID, int unidadSAP, int diferencia)
        {
            _nombre = nombre;
            _idInterno = idInterno;
            _unidadTraceID = unidadTraceID;
            _unidadSAP = unidadSAP;
            _diferencia = diferencia;
        }

        public string Nombre { get => _nombre; set => _nombre = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        public int UnidadTraceID { get => _unidadTraceID; set => _unidadTraceID = value; }
        public int UnidadSAP { get => _unidadSAP; set => _unidadSAP = value; }
        public int Diferencia { get => _diferencia; set => _diferencia = value; }
    }
}
