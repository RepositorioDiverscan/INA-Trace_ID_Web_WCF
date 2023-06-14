using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Trazabilidad
{
    public class LotesRecord
    {
        string _lote;
        DateTime _fechaVencimiento;
        public LotesRecord(string lote,DateTime fechaVencimiento)
        {
            _lote = lote;
            _fechaVencimiento = fechaVencimiento;
        }
        public string Lote
        {
            get { return _lote;}
        }

        public DateTime FechaVencimiento
        {
            get { return _fechaVencimiento; }
        }

        public string DataToShow
        {
            get { return _lote + " - " +_fechaVencimiento.ToShortDateString();}
        }

    }
}
