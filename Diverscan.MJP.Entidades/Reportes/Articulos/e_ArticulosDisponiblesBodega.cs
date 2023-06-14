using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.Articulos
{
 
    public class e_ArticulosDisponiblesBodega
    {
        private int _idTID;
        private string _idERP;
        private string _articulo;
        private decimal _unidadesInventario;
        private string _lote;
        private string _fechaVencimiento;
        private string _ubicacion;
        private string _unidadMedida;

        public e_ArticulosDisponiblesBodega(IDataReader reader)
        {
            _idTID = Convert.ToInt32(reader["IdTID"]);
            _idERP = Convert.ToString(reader["IdERP"]);
            _articulo = Convert.ToString(reader["Articulo"]);
            _unidadesInventario = Convert.ToDecimal(reader["UnidadesInventario"]);
            _lote = Convert.ToString(reader["Lote"]);
            _fechaVencimiento = Convert.ToString(reader["Fecha"]);
            _ubicacion = Convert.ToString(reader["Ubicacion"]);
            _unidadMedida = Convert.ToString(reader["UnidadMedida"]);
        }

        public int IdTID { get => _idTID; set => _idTID = value; }
        public string IdERP { get => _idERP; set => _idERP = value; }
        public string Articulo { get => _articulo; set => _articulo = value; }
        public decimal UnidadesInventario { get => _unidadesInventario; set => _unidadesInventario = value; }
        public string Lote { get => _lote; set => _lote = value; }
        public string FechaVencimiento { get => _fechaVencimiento; set => _fechaVencimiento = value; }
        public string Ubicacion { get => _ubicacion; set => _ubicacion = value; }
        
        public string UnidadMedida { get => _unidadMedida; set => _unidadMedida = value; }
    }
}
