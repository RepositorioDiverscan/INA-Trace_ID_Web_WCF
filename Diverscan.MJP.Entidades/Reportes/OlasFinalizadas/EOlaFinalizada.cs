using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Reportes.OlasFinalizadas
{
   public  class EOlaFinalizada
    {
        //Atributos
        private string _fecha;
        private int _idOla;
        private string _diasFinalizados;
        private string _avance;
        public EOlaFinalizada(IDataReader reader)
        {

            Fecha = Convert.ToString(reader["Fecha"]);
            IdOla = Convert.ToInt32(reader["idOla"]);
            DiasFinalizados = Convert.ToString(reader["DiasFinalizados"]);
            Avance = Convert.ToString(reader["Avance"]);
           
        }

        public string Fecha { get => _fecha; set => _fecha = value; }
        public int IdOla { get => _idOla; set => _idOla = value; }
        public string DiasFinalizados { get => _diasFinalizados; set => _diasFinalizados = value; }
        public string Avance { get => _avance; set => _avance = value; }
    }
}

