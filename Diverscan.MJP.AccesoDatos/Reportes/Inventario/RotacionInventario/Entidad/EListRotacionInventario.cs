using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.RotacionInventario.Entidad
{
    [Serializable]
    public class EListRotacionInventario
    {
        int _UnidadesFrecuencia,
            _Unidades,
            _Promedio;
        string _Nombre,
            _SKU;

        public EListRotacionInventario(int unidadesFrecuencia, int unidades, string nombre, string sKU, int promedio)
        {
            _UnidadesFrecuencia = unidadesFrecuencia;
            _Unidades = unidades;
            _Nombre = nombre;
            _SKU = sKU;
            _Promedio = promedio;
        }

        public int UnidadesFrecuencia { get => _UnidadesFrecuencia; set => _UnidadesFrecuencia = value; }
        public int Unidades { get => _Unidades; set => _Unidades = value; }
        public string Nombre { get => _Nombre; set => _Nombre = value; }
        public string SKU { get => _SKU; set => _SKU = value; }
        public int Promedio { get => _Promedio; set => _Promedio = value; }
    }
}
