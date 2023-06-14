using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes
{
    public class e_ReporteSolicitudDevolucion
    {
        private long _idInterno;
        private int _cantidad;
        private string _nombreArticulo;

        public e_ReporteSolicitudDevolucion(IDataReader reader)
        {
            this._idInterno = Convert.ToInt64(reader["idInterno"].ToString());
            this._cantidad = (int)Convert.ToDouble(reader["cantidad"].ToString());
            this._nombreArticulo = reader["nombreArticulo"].ToString();
        }

        public long IdInterno { get => _idInterno; set => _idInterno = value; }
        public int Cantidad { get => _cantidad; set => _cantidad = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
    }
}
