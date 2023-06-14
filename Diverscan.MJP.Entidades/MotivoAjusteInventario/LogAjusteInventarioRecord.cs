using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.MotivoAjusteInventario
{
    [Serializable]
    public class LogAjusteInventarioRecord
    {
        long _idRecord;
        DateTime _fechaRegistro;
        string _etiqueta;
        string _motivo;
        string _nombreArticulo;

        public LogAjusteInventarioRecord(long idRecord,DateTime fechaRegistro,
        string etiqueta, string motivo, string nombreArticulo)
        {
            _idRecord = idRecord;          
            _fechaRegistro = fechaRegistro;
            _etiqueta = etiqueta;
            _motivo = motivo;
            _nombreArticulo = nombreArticulo;
        }

        public LogAjusteInventarioRecord(IDataReader reader)
        {
            _idRecord = long.Parse(reader["IdRecord"].ToString());
            _fechaRegistro = DateTime.Parse(reader["FechaRegistro"].ToString());
            _etiqueta = reader["Etiqueta"].ToString();
            _motivo = reader["Motivo"].ToString();
            _nombreArticulo = reader["NombreArticulo"].ToString();
        }



        public long IdRecord
        {
            get { return _idRecord; }
        }        
        public DateTime FechaRegistro
        {
            get { return _fechaRegistro; }
        }
        public string Etiqueta
        {
            get { return _etiqueta; }
        }
        public string Motivo
        {
            get { return _motivo; }
        }
        public string NombreArticulo
        {
            get { return _nombreArticulo; }
        }
    }
}
