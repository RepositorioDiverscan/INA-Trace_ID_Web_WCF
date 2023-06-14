using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.Kardex
{
    [Serializable]
    public class e_KardexMacroArticulo
    {
        private long _idInterno_Articulo;
        private string _nombre_Articulo;
        private decimal _cantidad_Unidades_Inventario;
        private string _unidad_medida;
        private string _detalle_Movimiento;
        private string _num_documento;
        private DateTime _fecha_Registro;
        private string _fechaExport;

        public e_KardexMacroArticulo() { }

        public e_KardexMacroArticulo(
            long idInterno_Articulo,
            string nombre_Articulo,
            decimal cantidad_Unidades_Inventario,
            string unidad_medida,
            string detalle_Movimiento,
            string num_documento,
            DateTime fecha_Registro)
        {
            _idInterno_Articulo = idInterno_Articulo;
            _nombre_Articulo = nombre_Articulo;
            _cantidad_Unidades_Inventario = cantidad_Unidades_Inventario;
            _unidad_medida = unidad_medida;
            _detalle_Movimiento = detalle_Movimiento;
            _num_documento = num_documento;
            _fecha_Registro = fecha_Registro;
        }

        public long IdInterno_Articulo
        {
            get { return _idInterno_Articulo; }
            set { _idInterno_Articulo = value; }
        }
        public string Nombre_Articulo
        {
            get { return _nombre_Articulo; }
            set { _nombre_Articulo = value; }
        }
        public decimal Cantidad_Unidades_Inventario
        {
            get { return _cantidad_Unidades_Inventario; }
            set { _cantidad_Unidades_Inventario = value; }
        }
        public string Unidad_medida
        {
            get { return _unidad_medida; }
            set { _unidad_medida = value; }
        }
        public string Detalle_Movimiento
        {
            get { return _detalle_Movimiento; }
            set { _detalle_Movimiento = value; }
        }
        public string Num_documento
        {
            get { return _num_documento; }
            set { _num_documento = value; }
        }
        public DateTime Fecha_Registro
        {
            get { return _fecha_Registro; }
            set { _fecha_Registro = value; }
        }
        public string FechaExport
        {
            get { return _fecha_Registro.ToShortDateString(); }            
        }
    }
}
