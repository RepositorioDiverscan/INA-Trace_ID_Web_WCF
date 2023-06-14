using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.CustomException
{
    public class RecordNotFoundException: Exception
    {
        public RecordNotFoundException()
        {
        }
        public RecordNotFoundException(string message)
        : base(message)
        {
        }
    }
}
