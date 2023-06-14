using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.Rol
{
    [Serializable]
    public class e_RolHH
    {
        int _IdRol;
        string _FormHH;

        public e_RolHH(int idRol, string formHH)
        {
            _IdRol = idRol;
            _FormHH = formHH;
        }

        public int IdRol
        {
            get { return _IdRol; }
            set { _IdRol = value; }
        }

        public string FormHH
        {
            get { return _FormHH; }
            set { _FormHH = value; }
        }
    }
}
