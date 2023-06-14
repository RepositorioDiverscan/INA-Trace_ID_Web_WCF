using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    [Serializable]
    public class EDetalleReporteOC
    {
        private string idInterno;
        private string gtin;
        private string nombre;
        private double cantidadxRecibir;
        private double cantidadRecibidos;
        private double cantidadTransito;
        private int cantidadBodega;

        public string IdInterno { get => idInterno; set => idInterno = value; }
        public string Gtin { get => gtin; set => gtin = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public double CantidadxRecibir { get => cantidadxRecibir; set => cantidadxRecibir = value; }
        public double CantidadRecibidos { get => cantidadRecibidos; set => cantidadRecibidos = value; }
        public double CantidadTransito { get => cantidadTransito; set => cantidadTransito = value; }
        public int CantidadBodega { get => cantidadBodega; set => cantidadBodega = value; }

        public EDetalleReporteOC(IDataReader reader)
        {
            IdInterno = Convert.ToString(reader["idInterno"]);
            Gtin = Convert.ToString(reader["GTIN"]);
            Nombre = Convert.ToString(reader["Nombre"]);
            CantidadxRecibir = Convert.ToDouble(reader["CantidadxRecibir"]);
            CantidadRecibidos = Convert.ToDouble(reader["CantidadRecibidos"]);
            CantidadTransito = Convert.ToDouble(reader["CantidadTransito"]);
            CantidadBodega = Convert.ToInt32(reader["CantidadBodega"]);
        }
    }
}
