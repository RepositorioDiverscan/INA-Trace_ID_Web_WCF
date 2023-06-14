using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.AccesoDatos.Recaudo
{
    [Serializable]
    public class EJornadaRecaudo
    {
        private long _idJornada;
        private string _correoRecaudador;
        private DateTime _fechaInicio;
        private DateTime _fechaFin;
        private int _estado;

        public EJornadaRecaudo()
        {
        }

        public EJornadaRecaudo(IDataReader reader)
        {
            _idJornada = Convert.ToInt64(reader["IdJornada"].ToString());
            _correoRecaudador = reader["CorreoRecaudador"].ToString();
            _fechaInicio = Convert.ToDateTime(reader["FechaInicio"].ToString());
            if(!string.IsNullOrEmpty(reader["FechaFin"].ToString()))
                _fechaFin = Convert.ToDateTime(reader["FechaFin"].ToString());
            _estado = Convert.ToInt32(reader["Estado"].ToString());
        }

        public EJornadaRecaudo(long idJornada,string correoRecaudador, DateTime fechaInicio, DateTime fechaFin, int estado)
        {
            _idJornada = idJornada;
            _correoRecaudador = correoRecaudador;
            _fechaInicio = fechaInicio;
            _fechaFin = fechaFin;
            _estado = estado;
        }

        public long IdJornada { get => _idJornada; set => _idJornada = value; }
        public string CorreoRecaudador { get => _correoRecaudador; set => _correoRecaudador = value; }
        public DateTime FechaInicio { get => _fechaInicio; set => _fechaInicio = value; }
        public DateTime FechaFin { get => _fechaFin; set => _fechaFin = value; }
        public int Estado { get => _estado; set => _estado = value; }

        public string EstadoToShow
        {
            get
            {
                if (Estado == 0)
                    return "Abierto";
                return "Cerrado";
            }
        }
    }
}
