using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    public class EBodegas
    {
        //Atributos
        private int _idBodega;
        private string _nombre;

        //Constructor
        public EBodegas(IDataReader reader)
        {
            _idBodega = Convert.ToInt32(reader["idBodega"].ToString());
            _nombre = reader["nombre"].ToString();
        }

        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
    }
}
