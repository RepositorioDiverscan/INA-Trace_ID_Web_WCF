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
        string _idFamilia;
        string _familia;
        short _idTipoInventario;
        string _tipoInventario;
        DateTime _fechaPorAplicar;
        bool _estado;
        string _fechaPorAplicarAndroid;
        string _bodega;
        bool _trazableBodega;
        int _idUsuario;
        string _Usuario;

        public InventarioBasicoRecord()
        {
        }

        public InventarioBasicoRecord(string idFamilia, short idTipoInventario, int idUsuario, DateTime fechaPorAplicar)
        {
            _idFamilia = idFamilia;
            _idTipoInventario = idTipoInventario;
            _fechaPorAplicar = fechaPorAplicar;
            _idUsuario = idUsuario;
        }

        public InventarioBasicoRecord(IDataReader reader)
        {
            _idInventarioBasico = long.Parse(reader["IdInventarioBasico"].ToString());
            _idFamilia = reader["idInterno"].ToString();
            _tipoInventario = reader["TipoInventario"].ToString();
            _fechaPorAplicar = DateTime.Parse(reader["FechaPorAplicar"].ToString());
            _estado = Convert.ToBoolean(reader["Estado"]);
            _Usuario = reader["Usuario"].ToString();
        }

        [DataMember]
        public long IdInventarioBasico
        {
            get { return _idInventarioBasico; }
            set { _idInventarioBasico = value; }
        }

        public short IdTipoInventario
        {
            get { return _idTipoInventario; }
            set { _idTipoInventario = value; }
        }

        [DataMember]
        public string IdFamilia
        {
            get { return _idFamilia; }
            set { _idFamilia = value; }
        }
        [DataMember]
        public string TipoInventario
        {
            get { return _tipoInventario; }
            set { _tipoInventario = value; }
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
        public string Bodega { get => _bodega; set => _bodega = value; }

        [DataMember]
        public bool TrazableBodega { get => _trazableBodega; set => _trazableBodega = value; }
       
        [DataMember]
        public string Usuario { get => _Usuario; set => _Usuario = value; }

        public int IdUsuario { get => _idUsuario; set => _idUsuario = value; }

        [DataMember]
        public string Familia { get => _familia; set => _familia = value; }
    }
}
