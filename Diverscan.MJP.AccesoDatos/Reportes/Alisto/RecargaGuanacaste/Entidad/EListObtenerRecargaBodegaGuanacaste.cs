using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Alisto.RecargaGuanacaste.Entidad
{
    [Serializable]
    public class EListObtenerRecargaBodegaGuanacaste
    {
        long _numeroOla;
        string _fecha
            , _sku
            , _NombreArticulo;
        int _unidades;

        public EListObtenerRecargaBodegaGuanacaste(long numeroOla, string fecha, string sku, string nombreArticulo, int unidades)
        {
            _numeroOla = numeroOla;
            _fecha = fecha;
            _sku = sku;
            _NombreArticulo = nombreArticulo;
            _unidades = unidades;
        }

        public long NumeroOla { get => _numeroOla; set => _numeroOla = value; }
        public string Fecha { get => _fecha; set => _fecha = value; }
        public string Sku { get => _sku; set => _sku = value; }
        public string NombreArticulo { get => _NombreArticulo; set => _NombreArticulo = value; }
        public int Unidades { get => _unidades; set => _unidades = value; }
    }
}
