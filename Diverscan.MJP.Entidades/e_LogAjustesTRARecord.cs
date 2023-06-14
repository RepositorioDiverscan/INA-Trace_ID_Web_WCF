using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class e_LogAjustesTRARecord
    {
        long _idLogAjustesTRA;
        long _idArticulo;
        int _idUsuario;
        DateTime _fechaRegistro;
        string _lote;
        DateTime _fechaVencimiento;

        public e_LogAjustesTRARecord()
        { 
        
        }

        public e_LogAjustesTRARecord( long idLogAjustesTRA, long idArticulo, int idUsuario,
        DateTime fechaRegistro, string lote, DateTime fechaVencimiento)
        {
            _idLogAjustesTRA = idLogAjustesTRA;
            _idArticulo = idArticulo;
            _idUsuario = idUsuario;
            _fechaRegistro = fechaRegistro;
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
        }
        public e_LogAjustesTRARecord(long idArticulo, int idUsuario,
        DateTime fechaRegistro, string lote, DateTime fechaVencimiento)
        {
            _idArticulo = idArticulo;
            _idUsuario = idUsuario;
            _fechaRegistro = fechaRegistro;
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
        }

        public e_LogAjustesTRARecord(IDataReader reader)
        {
            _idLogAjustesTRA = long.Parse(reader["IdLogAjustesTRA"].ToString());
            _idArticulo = long.Parse(reader["IdArticulo"].ToString());
            _idUsuario = int.Parse(reader["IdUsuario"].ToString());
            _fechaRegistro = DateTime.Parse(reader["FechaRegistro"].ToString());
            _lote = reader["Lote"].ToString();
            _fechaVencimiento = DateTime.Parse(reader["FechaVencimiento"].ToString());                    
        }

        public long IdLogAjustesTRA
        {
            get { return _idLogAjustesTRA; }
        }
        public long IdArticulo
        {
            get { return _idArticulo; }
        }
        public int IdUsuario
        {
            get { return _idUsuario; }
        }
        public DateTime FechaRegistro
        {
            get { return _fechaRegistro; }
        }
        public string Lote
        {
            get { return _lote; }
        }
        public DateTime FechaVencimiento
        {
            get { return _fechaVencimiento; }
        }
    }
}
