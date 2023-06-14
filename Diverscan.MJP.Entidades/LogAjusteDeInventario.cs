using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class LogAjusteDeInventario
    {
        long _idRecord;
        long _idTRAIngresoSalidaArticulos;
        long _idAjusteInventario;
        DateTime _fechaRegistro;
        int _estado;

        public LogAjusteDeInventario(long idRecord, long idTRAIngresoSalidaArticulos,
            long idAjusteInventario, DateTime fechaRegistro, int estado)
        {
            _idRecord = idRecord;
            _idTRAIngresoSalidaArticulos = idTRAIngresoSalidaArticulos;
            _idAjusteInventario = idAjusteInventario;
            _fechaRegistro = fechaRegistro;
            _estado = estado;
        }
               
        public LogAjusteDeInventario(long idTRAIngresoSalidaArticulos,
            long idAjusteInventario, DateTime fechaRegistro, int estado)
        {            
            _idTRAIngresoSalidaArticulos = idTRAIngresoSalidaArticulos;
            _idAjusteInventario = idAjusteInventario;
            _fechaRegistro = fechaRegistro;
            _estado = estado;
        }

        public long IdRecord
        {
            get { return _idRecord; }
        }
        public long IdTRAIngresoSalidaArticulos
        {
            get { return _idTRAIngresoSalidaArticulos; }
        }
        public long IdAjusteInventario
        {
            get { return _idAjusteInventario; }
        }
        public DateTime FechaRegistro
        {
            get { return _fechaRegistro; }            
        }
        public int Estado
        {
            get { return _estado; }
        }
    }
}
