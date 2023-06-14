using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Trazabilidad
{
    public class TRARecordTrazabilidad
    {
        public TRARecordTrazabilidad(long idRegistro, bool sumUno_RestaCero, long idArticulo , DateTime fechaVencimiento ,
          string lote, int idUsuario, long idMetodoAccion, string idTablaCampoDocumentoAccion , string idCampoDocumentoAccion ,
          string numDocumentoAccion, long idUbicacion, decimal cantidad, bool procesado, DateTime fecharegistro, int idEstado)
        {
            IdRegistro = idRegistro;
            SumUno_RestaCero = sumUno_RestaCero;
            IdArticulo = idArticulo;
            FechaVencimiento = fechaVencimiento;
            Lote = lote;
            IdUsuario = idUsuario;
            IdMetodoAccion = idMetodoAccion;
            IdTablaCampoDocumentoAccion = idTablaCampoDocumentoAccion;
            IdCampoDocumentoAccion = idCampoDocumentoAccion;
            NumDocumentoAccion = numDocumentoAccion;
            IdUbicacion = idUbicacion;
            Cantidad = cantidad;
            Procesado = procesado;            
            FechaRegistro = fecharegistro;
            IdEstado = idEstado;
        }

        public TRARecordTrazabilidad(IDataReader reader)
        {
            IdRegistro = long.Parse(reader["IdRegistro"].ToString());
            SumUno_RestaCero = (bool)reader["SumUnoRestaCero"];
            IdArticulo = long.Parse(reader["IdArticulo"].ToString());
            FechaVencimiento = DateTime.Parse(reader["FechaVencimiento"].ToString());
            Lote = reader["Lote"].ToString();
            IdUsuario = int.Parse(reader["IdUsuario"].ToString());
            IdMetodoAccion = long.Parse(reader["IdMetodoAccion"].ToString());
            IdTablaCampoDocumentoAccion = reader["IdTablaCampoDocumentoAccion"].ToString();
            IdCampoDocumentoAccion = reader["IdCampoDocumentoAccion"].ToString();
            NumDocumentoAccion = reader["NumDocumentoAccion"].ToString();
            IdUbicacion = long.Parse(reader["IdUbicacion"].ToString());
            Cantidad = decimal.Parse(reader["Cantidad"].ToString());
            Procesado = (bool)reader["Procesado"];
            FechaRegistro = DateTime.Parse(reader["FechaRegistro"].ToString());
            IdEstado = int.Parse(reader["IdEstado"].ToString());
            Etiqueta = reader["ETIQUETA"].ToString();
            OC_Destino = reader["OC_Destino"].ToString();
            Unidad_Medida = reader["Unidad_Medida"].ToString();
        }

        public TRARecordTrazabilidad(bool sumUno_RestaCero, long idArticulo, DateTime fechaVencimiento,
          string lote, int idUsuario, long idMetodoAccion, string idTablaCampoDocumentoAccion, string idCampoDocumentoAccion,
          string numDocumentoAccion, long idUbicacion, decimal cantidad, bool procesado, DateTime fecharegistro, int idEstado)
        {         
            SumUno_RestaCero = sumUno_RestaCero;
            IdArticulo = idArticulo;
            FechaVencimiento = fechaVencimiento;
            Lote = lote;
            IdUsuario = idUsuario;
            IdMetodoAccion = idMetodoAccion;
            IdTablaCampoDocumentoAccion = idTablaCampoDocumentoAccion;
            IdCampoDocumentoAccion = idCampoDocumentoAccion;
            NumDocumentoAccion = numDocumentoAccion;
            IdUbicacion = idUbicacion;
            Cantidad = cantidad;
            Procesado = procesado;
            FechaRegistro = fecharegistro;
            IdEstado = idEstado;
        }

        public long IdRegistro { set; get; }
        public  bool SumUno_RestaCero { set; get; }
        public  long IdArticulo { set; get; }
        public  DateTime FechaVencimiento { set; get; }
        public  string Lote { set; get; }
        public  int IdUsuario { set; get; }
        public  long IdMetodoAccion { set; get; }
        public  string IdTablaCampoDocumentoAccion { set; get; } 
        public  string IdCampoDocumentoAccion { set; get; }
        public  string NumDocumentoAccion { set; get; }
        public  long IdUbicacion { set; get; }
        public  decimal Cantidad { set; get; }
        public  bool Procesado { set; get; }
        public  DateTime FechaRegistro { set; get; }
        public int IdEstado { set; get; }
        public string Etiqueta { set; get; }
        public string OC_Destino { set; get; }
        public string Unidad_Medida { set; get; }        
    }
}
