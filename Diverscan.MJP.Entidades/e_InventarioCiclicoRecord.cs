using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Diverscan.MJP.Entidades
{
    [Serializable]
    [DataContract]
    public class e_InventarioCiclicoRecord
    {
        long _idInventario;
        int _idCategoriaArticulo;
        string _nombreCategoria;
        DateTime _fechaPorAplicar;

        public e_InventarioCiclicoRecord(long idInventario,
        int idCategoriaArticulo, string nombreCategoria, DateTime fechaPorAplicar)
        {
            _idInventario = idInventario;
            _idCategoriaArticulo = idCategoriaArticulo;
            _nombreCategoria = nombreCategoria;
            _fechaPorAplicar = fechaPorAplicar;
        }

        public e_InventarioCiclicoRecord(int idCategoriaArticulo, DateTime fechaPorAplicar)
        {
            _idCategoriaArticulo = idCategoriaArticulo;
            _fechaPorAplicar = fechaPorAplicar;
        }

        public e_InventarioCiclicoRecord(int idCategoriaArticulo, string nombreCategoria, DateTime fechaPorAplicar)
        {
            _idCategoriaArticulo = idCategoriaArticulo;
            _nombreCategoria = nombreCategoria;
            _fechaPorAplicar = fechaPorAplicar;
        }
        [DataMember]
        public long IdInventario
        {
            get { return _idInventario; }
            set { _idInventario = value; }
        }
        [DataMember]
        public int IdCategoriaArticulo
        {
            get { return _idCategoriaArticulo; }
            set { _idCategoriaArticulo = value; }
        }
        [DataMember]
        public string NombreCategoria
        {
            get { return _nombreCategoria; }
            set { _nombreCategoria = value; }
        }
        [DataMember]
        public DateTime FechaPorAplicar
        {
            get { return _fechaPorAplicar; }
            set { _fechaPorAplicar = value; }
        }
    }
}
