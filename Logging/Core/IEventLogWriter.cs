using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Core
{
    public interface IEventLogWriter
    {
        void WriteToEventLog(LogEntity entity, Exception ex);
    }
}
