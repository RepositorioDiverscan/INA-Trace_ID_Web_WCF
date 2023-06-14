using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.VencimientoProducto.Entidades
{
    [Serializable]
    public class EListVencimientoProducto
    {
        int _diasVencimiento,
            _cantidad;
        string _sku,
            _nombreProducto,
            _descripcion;

        public EListVencimientoProducto(int diasVencimiento, int cantidad, string sku, string nombreProducto, string descripcion)
        {
            _diasVencimiento = diasVencimiento;
            _cantidad = cantidad;
            _sku = sku;
            _nombreProducto = nombreProducto;
            _descripcion = descripcion;
        }

        public int DiasVencimiento { get => _diasVencimiento; set => _diasVencimiento = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public string Sku { get => _sku; set => _sku = value; }
        public string NombreProducto { get => _nombreProducto; set => _nombreProducto = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
    }
}
