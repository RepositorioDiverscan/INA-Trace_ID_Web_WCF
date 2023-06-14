using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Traslado.Entidad
{
    [Serializable]
    public class EListObtenerTrasladoMercaderia
    {
        string _sku,
             _nombreArticulo,
             _usuario;
        int _unidades;

        public EListObtenerTrasladoMercaderia(string sku, string nombreArticulo, string usuario, int unidades)
        {
            _sku = sku;
            _nombreArticulo = nombreArticulo;
            _usuario = usuario;
            _unidades = unidades;
        }

        public string Sku { get => _sku; set => _sku = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public string Usuario { get => _usuario; set => _usuario = value; }
        public int Unidades { get => _unidades; set => _unidades = value; }
    }
}
