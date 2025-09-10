using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiGameLibrary.Exceptions
{
    public class GameApiFailedException : Exception
    {
   
        public GameApiFailedException(string message)
            : base(message)
        {
        }
    }
}
