using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    [Serializable]
    public class e_CategoriaArticulo
    {
        int _idCategoriaArticulo;
        string _nombre;

        public e_CategoriaArticulo(int idCategoriaArticulo, string nombre)
        {
            _idCategoriaArticulo = idCategoriaArticulo;
            _nombre = nombre;
        }

        public e_CategoriaArticulo(string nombre)
        {
            _nombre = nombre;
        }

        public e_CategoriaArticulo(IDataReader reader)
        {
            _idCategoriaArticulo = int.Parse(reader["IdCategoriaArticulo"].ToString());
            _nombre = reader["Nombre"].ToString();
        }

        public int IdCategoriaArticulo
        {
            get { return _idCategoriaArticulo; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }       
    }
}
