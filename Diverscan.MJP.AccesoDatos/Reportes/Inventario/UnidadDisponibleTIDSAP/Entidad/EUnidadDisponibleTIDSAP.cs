using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Inventario.UnidadDisponibleTIDSAP.Entidad
{
    [Serializable]
    public class EUnidadDisponibleTIDSAP
    {
        int _idBodega;
        string _idInterno;

        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
    }
}
