using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioAutoGlass.CORE.Notifications
{
    public class ErrorReponse
    {
        public ErrorReponse(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }

        public int StatusCode { get; private set; }
        public string Message { get; private set; }
    }
}