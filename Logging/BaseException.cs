using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class BaseException:Exception
    {
        public ErrorCode ErrorCode { get; private set; }
        public BaseException(ErrorCode errorCode) : base()
        {
            ErrorCode = errorCode;
        }

        public BaseException(string message,Exception innerException,ErrorCode errorCode) : base(message,innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
