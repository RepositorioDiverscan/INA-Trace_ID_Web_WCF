using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Ola.DisponibleFcturacion
{
    [Serializable]
    public class EListObtenerOlasDisponiblesFacturacion
    {
        string _fecha;
        long _idOla;
        string _nombreCliente
            ,_diasFinalizados
            ,_avance;

        public EListObtenerOlasDisponiblesFacturacion(string fecha, long idOla, string nombreCliente, string diasFinalizados, string avance)
        {
            _fecha = fecha;
            _idOla = idOla;
            _nombreCliente = nombreCliente;
            _diasFinalizados = diasFinalizados;
            _avance = avance;
        }

        public string Fecha { get => _fecha; set => _fecha = value; }
        public long IdOla { get => _idOla; set => _idOla = value; }
        public string NombreCliente { get => _nombreCliente; set => _nombreCliente = value; }
        public string DiasFinalizados { get => _diasFinalizados; set => _diasFinalizados = value; }
        public string Avance { get => _avance; set => _avance = value; }
    }
}
