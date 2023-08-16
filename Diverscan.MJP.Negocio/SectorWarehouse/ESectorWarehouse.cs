using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Negocio.SectorWarehouse
{
    [Serializable]
    public class ESectorWarehouse
    {
        private readonly int _idSectorWarehouse;
        private int _idBodega;
        private string _idInternoBodega, _name, _description, _activo;
        private bool _active;

        public ESectorWarehouse(int idSectorWarehouse, int idBodega, string name, string description, bool active)
        {
            _idSectorWarehouse = idSectorWarehouse;
            _idBodega = idBodega;
            _name = name;
            _description = description;
            _active = active;
        }

        public ESectorWarehouse( int idBodega, string name, string description, bool active)
        {
            _idBodega = idBodega;
            _name = name;
            _description = description;
            _active = active;
        }

        public ESectorWarehouse(IDataReader reader)
        {
            _idSectorWarehouse = Convert.ToInt32(reader["idSectorBodega"].ToString());
            _idBodega = Convert.ToInt32(reader["idBodega"].ToString());
            _idInternoBodega = reader["idInternoBodega"].ToString();
            _name = reader["nombre"].ToString();
            _description = reader["descripcion"].ToString();
            _activo = reader["activo"].ToString();
        }

        public int IdSectorWarehouse => _idSectorWarehouse;
        public int IdBodega { get => _idBodega; set => _idBodega = value; }
        public string Name { get => _name; set => _name = value; }        
        public string Description { get => _description; set => _description = value; }
        public bool Active { get => _active; set => _active = value; }
        public string Activo { get => _activo; set => _activo = value; }
        public string IdInternoBodega { get => _idInternoBodega; set => _idInternoBodega = value; }
    }
}
