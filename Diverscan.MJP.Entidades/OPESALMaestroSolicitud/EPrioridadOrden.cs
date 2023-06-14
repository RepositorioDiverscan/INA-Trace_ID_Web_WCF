using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.OPESALMaestroSolicitud
{
    [Serializable]

    public class EPrioridadOrden
    {
        int _idPrioridad;
        string _descripcion;

        public EPrioridadOrden(int idPrioridad, string descripcion)
        {
            _idPrioridad = idPrioridad;
            _descripcion = descripcion;
        }

        public EPrioridadOrden(IDataReader reader)
        {
            _idPrioridad= Convert.ToInt32(reader["IdPrioridad"].ToString());
            _descripcion = reader["Descripcion"].ToString();
        }

        public int IdPrioridad { get => _idPrioridad; set => _idPrioridad = value; }

        public string Descripcion { get => _descripcion; set => _descripcion = value; }
    }
}
