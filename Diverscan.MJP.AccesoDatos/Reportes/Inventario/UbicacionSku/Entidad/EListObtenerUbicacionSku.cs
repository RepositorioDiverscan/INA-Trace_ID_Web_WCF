using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Inventario.UbicacionSku.Entidad
{
    [Serializable]
    public class EListObtenerUbicacionSku
    {
        string _idInterno,
            _nombre,
            _descripcion;
        int _cantidadDisponible;

        public EListObtenerUbicacionSku(string idInterno, string nombre, string descripcion, int cantidadDisponible)
        {
            _idInterno = idInterno;
            _nombre = nombre;
            _descripcion = descripcion;
            _cantidadDisponible = cantidadDisponible;
        }

        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public int CantidadDisponible { get => _cantidadDisponible; set => _cantidadDisponible = value; }
    }
}
