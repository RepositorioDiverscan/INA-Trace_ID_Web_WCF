using System;
using System.Data;
using System.Runtime.Serialization;

namespace Diverscan.MJP.Entidades.SyncActiceID
{
    [DataContract]
    public class eUbicacionActivo
    {
        #region VARIABLES PRIVADAS

        private string _companySysId, _buildingSysId, _floorSysId, _officeSysId, _buildingName, _floorName, _officeName;

        #endregion


        #region CONSTRUCTORES

        public eUbicacionActivo(IDataReader reader)
        {
            BuildingName = Convert.ToString(reader["Edificio"]);
            FloorName = Convert.ToString(reader["Piso"]);
            OfficeName = Convert.ToString(reader["Oficina"]);
        }

        public eUbicacionActivo(string companySysId, string buildingSysId, string floorSysId, string officeSysId)
        {
            _companySysId = companySysId;
            _buildingSysId = buildingSysId;
            _floorSysId = floorSysId;
            _officeSysId = officeSysId;
        }


        #endregion


        #region GETERS SETTERS
        [DataMember]
        public string CompanySysId { get => _companySysId; set => _companySysId = value; }
        [DataMember]
        public string BuildingSysId { get => _buildingSysId; set => _buildingSysId = value; }
        [DataMember]
        public string FloorSysId { get => _floorSysId; set => _floorSysId = value; }
        [DataMember]
        public string OfficeSysId { get => _officeSysId; set => _officeSysId = value; }
        [DataMember]
        public string BuildingName { get => _buildingName; set => _buildingName = value; }
        [DataMember]
        public string FloorName { get => _floorName; set => _floorName = value; }
        [DataMember]
        public string OfficeName { get => _officeName; set => _officeName = value; }

        #endregion
    }
}
