using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Diverscan.MJP.AccesoDatos.Operaciones
{
    [Serializable]

    public class eReporteSOC
    {
        private int _idMaestroSinOrdenCompra;
        private string _idInterno;
        private string _descCorta;
        private DateTime _fechaCreacion;
        private string _numTransaccion;
        private string _idCompania;
        private string _numCert;
        private int _procesada;
        private DateTime _fechaProcesamiento;
        private string _numeroFactura;
        private string _nombreProveedor;
        private int _idBodega;
        private string _Usuario;
        private double _porcentajeRecepcion;


        public eReporteSOC(IDataReader reader)
        {
            IdMaestroSinOrdenCompra = Convert.ToInt32(reader["idMaestroOrdenCompraSinDoc"]);
            IdInterno = Convert.ToString(reader["IdInterno"]);
            DescCorta = Convert.ToString(reader["DescripcionCorta"]);
            FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
            NumTransaccion = Convert.ToString(reader["NumeroTransaccion"]);
            IdCompania = Convert.ToString(reader["IdCompania"]);
            NumCert = Convert.ToString(reader["NumeroCertificado"]);
            NumeroFactura = Convert.ToString(reader["NumeroFactura"]);
            Procesada = Convert.ToInt32(reader["Procesada"]);
            FechaProcesamiento = Convert.ToDateTime(reader["FechaProcesamiento"]);
            NombreProveedor = Convert.ToString(reader["NombreProveedor"]);
            IdBodega = Convert.ToInt32(reader["IdBodega"]);
            Usuario = Convert.ToString(reader["Usuario"]);
            PorcentajeRecepcion = Convert.ToDouble(reader["PorcentajeRecepcion"]);
        }

        public int IdMaestroSinOrdenCompra { get => _idMaestroSinOrdenCompra; set => _idMaestroSinOrdenCompra = value; }
        public string IdInterno { get => _idInterno; set => _idInterno = value; }
        public string DescCorta { get => _descCorta; set => _descCorta = value; }
        public DateTime FechaCreacion { get => _fechaCreacion; set => _fechaCreacion = value; }
        public string NumTransaccion { get => _numTransaccion; set => _numTransaccion = value; }
        public string IdCompania { get => _idCompania; set => _idCompania = value; }
        public string NumCert { get => _numCert; set => _numCert = value; }
        public int Procesada { get => _procesada; set => _procesada = value; }
        public DateTime FechaProcesamiento { get => _fechaProcesamiento; set => _fechaProcesamiento = value; }
        public string NumeroFactura { get => _numeroFactura; set => _numeroFactura = value; }
        public string NombreProveedor { get => _nombreProveedor; set => _nombreProveedor = value; }
        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public string Usuario { get => _Usuario; set => _Usuario = value; }
        public double PorcentajeRecepcion { get => _porcentajeRecepcion; set => _porcentajeRecepcion = value; }
    }
}
