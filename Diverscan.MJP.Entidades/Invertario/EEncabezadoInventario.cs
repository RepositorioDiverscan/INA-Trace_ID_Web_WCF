using System;
using System.Data;

namespace Diverscan.MJP.Entidades.Invertario
{
    public class EEncabezadoInventario
    {
        private int _idInventarioBasico;
        private string _familia, _tipoInventario, _fechaPorAplicar, _estado;

        public EEncabezadoInventario(IDataReader reader)
        {
            _idInventarioBasico = Convert.ToInt32(reader["IdInventarioBasico"]);
            _familia = Convert.ToString(reader["Familia"]);
            _tipoInventario = Convert.ToString(reader["TipoInventario"]);
            _fechaPorAplicar = Convert.ToString(reader["FechaPorAplicar"]);
            _estado = Convert.ToString(reader["Estado"]);
        }

        public int IdInventarioBasico { get => _idInventarioBasico; set => _idInventarioBasico = value; }
        public string Familia { get => _familia; set => _familia = value; }
        public string TipoInventario { get => _tipoInventario; set => _tipoInventario = value; }
        public string FechaPorAplicar { get => _fechaPorAplicar; set => _fechaPorAplicar = value; }
        public string Estado { get => _estado; set => _estado = value; }
    }
}
