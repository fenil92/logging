using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Core
{
    public class LogEntity
    {
        public string ActivityId { get; set; }

        public string ClassName { get; set; }

        public string Message { get; set; }

        public DateTime DateLogged { get; set; }

        public string TraceLevel { get; set; }
        public string StackTrace { get; set; }

        public string ApplicationErrorCode { get; set; }

        public string UserName { get; set; }
    }
}
