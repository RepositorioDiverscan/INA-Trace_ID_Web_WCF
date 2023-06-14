using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.SSCC
{
    [Serializable]
    public class ESSCC
    {
        private long _idSSCC;
        private string _descripcionSSCC;
        private string _consecutivoSSCC;
        private string _fechaProcesadoSSCC;
        private string _idUbicacionSSCC;
        private string _ubicacionSSCC;
        private string _nombreUsuario;
        private long _usuarioCertificador;       
        private long _idMaestroSolicitud;
        private bool _traslado;
        private bool _despacho;
        private bool _alistado;
        private int _total;
        private int _avance;
        private string _cantidadCertificada;
        public ESSCC()
        {
        }

        public ESSCC(IDataReader reader)
        {            
            _idSSCC = long.Parse(reader["idConsecutivoSSCC"].ToString());
            _descripcionSSCC = reader["Descripcion"].ToString();
            _consecutivoSSCC = reader["SSCCGenerado"].ToString();
            _traslado = Convert.ToBoolean(reader["Trasladado"].ToString());
            _despacho = Convert.ToBoolean(reader["Despacho"].ToString());
            _idUbicacionSSCC = reader["IdUbicacionSSCC"].ToString();
            _ubicacionSSCC = reader["Ubicacion"].ToString();
            _alistado = Convert.ToBoolean(reader["Alistado"].ToString());
            _idMaestroSolicitud = long.Parse(reader["IdMaestroSolicitud"].ToString());
            string usuario = reader["idUsuarioCertificacion"].ToString();
            _usuarioCertificador = long.Parse(usuario);
        }
        public long IdSSCC { get => _idSSCC; set => _idSSCC = value; }
        public string DescripcionSSCC { get => _descripcionSSCC; set => _descripcionSSCC = value; }
        public string ConsecutivoSSCC { get => _consecutivoSSCC; set => _consecutivoSSCC = value; }        
        public string UbicacionSSCC { get => _ubicacionSSCC; set => _ubicacionSSCC = value; }
        public long UsuarioCertificador { get => _usuarioCertificador; set => _usuarioCertificador = value; }
        public string FechaProcesadoSSCC
        {
            get {
                if (_fechaProcesadoSSCC.Contains("1900-01-01"))
                    _fechaProcesadoSSCC = "";
                return _fechaProcesadoSSCC;
            }
            set => _fechaProcesadoSSCC = value; }
        public bool Traslado { get => _traslado; set => _traslado = value; }
        public bool Despacho { get => _despacho; set => _despacho = value; }
        public long IdMaestroSolicitud { get => _idMaestroSolicitud; set => _idMaestroSolicitud = value; }
        public string NombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public string IdUbicacionSSCC { get => _idUbicacionSSCC; set => _idUbicacionSSCC = value; }
        public int Total { get => _total; set => _total = value; }
        public int Avance { get => _avance; set => _avance = value; }
        public string CantidadCertificada
        {
            get
            {
                _cantidadCertificada = Avance + " de " + Total;
                return _cantidadCertificada;
            }
            set => _cantidadCertificada = value;
        }
    }
}
