using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diverscan.MJP.Entidades.CustomException
{
    public class MoreThanOneRecordException : Exception
    {
         public MoreThanOneRecordException()
        {
        }
         public MoreThanOneRecordException(string message)
        : base(message)
        {
        }
    }
}
