using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAutoGlass.CORE.Notifications
{
    public class ExcecoesPersonalizadas:Exception
    {
        public ExcecoesPersonalizadas(string message)
            :base(message)
        {}
        
        public int StatusCode { get; private set; }

        public ExcecoesPersonalizadas(string message, int statusCode)
            :base(message)
        {
            StatusCode = statusCode;
        }
    }
}