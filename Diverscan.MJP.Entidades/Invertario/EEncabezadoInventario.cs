using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    public class EEncabezadoInventario
    {
        private int _idInventarioBasico;
        private string _nombre;
        private string _descripcion;
        private string _fechaPorAplicar;
        private int _estado;

        public EEncabezadoInventario(IDataReader reader)
        {
            _idInventarioBasico = Convert.ToInt32(reader["IdInventarioBasico"]);
            _nombre = Convert.ToString(reader["Nombre"]);
            _descripcion = Convert.ToString(reader["Descripcion"]);
            _fechaPorAplicar = Convert.ToString(reader["FechaPorAplicar"]);
            _estado = Convert.ToInt32(reader["Estado"]);
        }

        public int IdInventarioBasico { get => _idInventarioBasico; set => _idInventarioBasico = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }
        public string FechaPorAplicar { get => _fechaPorAplicar; set => _fechaPorAplicar = value; }
        public int Estado { get => _estado; set => _estado = value; }
    }
}
