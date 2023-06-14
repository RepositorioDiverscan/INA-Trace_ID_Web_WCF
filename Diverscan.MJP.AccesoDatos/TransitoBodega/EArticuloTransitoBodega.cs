using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.TransitoBodega
{
    
    public class EArticuloTransitoBodega
    {
        private string _nombreUsuario;
        private string _apellidosUsuario;
        private string _ubicacionArticulo;
        private string _nombreArticulo;
        private string _idInterno;
        private int _cantidadTransito;
        private string _lote;
        private string _fechaVencimiento;

       

        public EArticuloTransitoBodega(IDataReader reader)
        {
            _nombreUsuario = reader["NombreUsuario"].ToString();
            _apellidosUsuario = reader["APELLIDOS_PILA"].ToString();
            _ubicacionArticulo = reader["Ubicacion"].ToString();
            _nombreArticulo = reader["NombreArticulo"].ToString();
            _idInterno = reader["idInterno"].ToString();
            _cantidadTransito = Convert.ToInt32(reader["cantidadTransito"].ToString());
            _lote = reader["Lote"].ToString();
            _fechaVencimiento = Convert.ToString(reader["FechaVencimiento"]);
        }

        

        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string ApellidosUsuario { get => _apellidosUsuario; set => _apellidosUsuario = value; }
        public string UbicacionArticulo { get => _ubicacionArticulo; set => _ubicacionArticulo = value; }
        public string NombreArticulo { get => _nombreArticulo; set => _nombreArticulo = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        public int CantidadTransito { get => _cantidadTransito; set => _cantidadTransito = value; }
        public string Lote { get => _lote; set => _lote = value; }
        public string FechaVencimiento { get => _fechaVencimiento; set => _fechaVencimiento = value; }

        public string NombreCompletoUsuario { get => _nombreUsuario+" "+ _apellidosUsuario; }
    }
}
