using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.Invertario
{
    [DataContract]
    public class UbicacionesInventarioCiclicoRecord
    {
        long _idUbicacionesInventario;
        long _idArticulo;
        long _idUbicacion;
        string _etiqueta;
        int _secuencia;
        int _idBodega;
        string _nombreBodega;

        public UbicacionesInventarioCiclicoRecord(long idUbicacionesInventario, long idArticulo, long idUbicacion, 
            string etiqueta, int secuencia, int idBodega, string nombreBodega)
        {
            _idUbicacionesInventario = idUbicacionesInventario;
            _idArticulo = idArticulo;
            _idUbicacion = idUbicacion;
            _etiqueta = etiqueta;
            _secuencia = secuencia;
            _idBodega = idBodega;
            _nombreBodega = nombreBodega;
        }

        public UbicacionesInventarioCiclicoRecord(IDataReader reader)
        {
            _idUbicacionesInventario = long.Parse(reader["IdUbicacionesInventario"].ToString());
            _idArticulo = Convert.ToInt64(reader["IdArticulo"]);
            _idUbicacion = long.Parse(reader["IdUbicacion"].ToString());
            _etiqueta = reader["ETIQUETA"].ToString();
            _secuencia = Convert.ToInt32(reader["Secuencia"]);
            _idBodega = Convert.ToInt32(reader["idBodega"]);
            _nombreBodega = reader["NombreBodega"].ToString();
        }

        [DataMember]
        public long IdUbicacionesInventario
        {
            get { return _idUbicacionesInventario; }
            set { _idUbicacionesInventario = value; }
        }

        [DataMember]
        public long IdArticulo
        {
            get { return _idArticulo; }
            set { _idArticulo = value; }
        }

        [DataMember]
        public long IdUbicacion
        {
            get { return _idUbicacion; }
            set { _idUbicacion = value; }
        }

        [DataMember]
        public string Etiqueta
        {
            get { return _etiqueta; }
            set { _etiqueta = value; }
        }

        [DataMember]
        public int Secuencia
        {
            get { return _secuencia; }
            set { _secuencia = value; }
        }

        [DataMember]
        public int IdBodega
        {
            get { return _idBodega; }
            set { _idBodega = value; }
        }

        [DataMember]
        public string NombreBodega
        {
            get { return _nombreBodega; }
            set { _nombreBodega = value; }
        }
    }
}
