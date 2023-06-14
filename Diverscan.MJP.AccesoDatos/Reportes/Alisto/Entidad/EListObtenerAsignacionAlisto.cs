using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Reportes.Alisto.Entidad
{
 
    public class EListObtenerAsignacionAlisto
    {
        private string _consecutivoSSCC;
        private string _sku;
        private string _nombreArticulo;
        private int _unidadesPendiente;
        private float _unidadesAsignadas;
        private string _estadoSSCC;

        public EListObtenerAsignacionAlisto(IDataReader reader )
        {
            _consecutivoSSCC = Convert.ToString(reader["ConsecutivoSSCC"]);
            _sku =Convert.ToString(reader["SKU"]) ;
            _nombreArticulo =Convert.ToString(reader["NombreArticulo"]) ;
            _unidadesAsignadas = (float)Convert.ToDouble(reader["UnidadesAsignadas"]);
            _unidadesPendiente = Convert.ToInt32(reader["UnidadesPendientes"]);
            _estadoSSCC =Convert.ToString(reader["EstadoSSCC"]);
        }

        public string ConsecutivoSSCC { get => _consecutivoSSCC; set => _consecutivoSSCC = value; }
        public string Sku { get => _sku; set => _sku = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public float UnidadesAsignadas { get => _unidadesAsignadas; set => _unidadesAsignadas = value; }
        public int UnidadesPendiente { get => _unidadesPendiente; set => _unidadesPendiente = value; }
        public string EstadoSSCC { get => _estadoSSCC; set => _estadoSSCC = value; }
    }
}
