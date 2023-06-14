using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades.InventarioBasico
{
    [Serializable]
    [DataContract]
    public class InventarioBasicoRecord
    {
        long _idInventarioBasico;
        string _nombre;
        string _descripcion;
        DateTime _fechaPorAplicar;
        bool _estado;
        string _fechaPorAplicarAndroid;
        string _idInternoBodega;
        bool _trazableBodega;

        public InventarioBasicoRecord()
        {
        }

        public InventarioBasicoRecord(string nombre, string descripcion,DateTime fechaPorAplicar)
        {
            _nombre = nombre;
            _descripcion = descripcion;
            _fechaPorAplicar = fechaPorAplicar;
        }

        public InventarioBasicoRecord(IDataReader reader)
        {
            _idInventarioBasico = long.Parse(reader["IdInventarioBasico"].ToString());
            _nombre = reader["Nombre"].ToString();
            _descripcion = reader["Descripcion"].ToString();
            _fechaPorAplicar = DateTime.Parse(reader["FechaPorAplicar"].ToString());
            _estado = Convert.ToBoolean(reader["Estado"]);
        }

        [DataMember]
        public long IdInventarioBasico
        {
            get { return _idInventarioBasico; }
            set { _idInventarioBasico = value; }
        }
        [DataMember]
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        [DataMember]
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        
        public DateTime FechaPorAplicar
        {
            get { return _fechaPorAplicar; }
            set { _fechaPorAplicar = value; }
        }

        [DataMember]
        public bool Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public string EstadoToShow
        {
            get 
            {
                if(_estado)
                    return "Activo"; 
                else
                    return "Cerrado"; 
            }
        }
        [DataMember]
        public string FechaPorAplicarAndroid {
            get
            {
                if (FechaPorAplicar != null)
                  return FechaPorAplicar.ToShortDateString();
                
                return "";
            }
            set => _fechaPorAplicarAndroid = value;
        }
        [DataMember]
        public string IdInternoBodega { get => _idInternoBodega; set => _idInternoBodega = value; }
        [DataMember]
        public bool TrazableBodega { get => _trazableBodega; set => _trazableBodega = value; }
    }
}
