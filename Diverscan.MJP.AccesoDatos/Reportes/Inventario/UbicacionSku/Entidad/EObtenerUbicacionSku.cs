using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Inventario.UbicacionSku.Entidad
{
    [Serializable]
    public class EObtenerUbicacionSku
    {
        int _idUbicacion;
        string _idInterno;
        public int IdUbicacion { get => _idUbicacion; set => _idUbicacion = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
    }
}
