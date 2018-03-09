using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Core
{
    public interface ILogRepository
    {

        void Log(LogEntity entity);

        void Log(List<LogEntity> exceptionLogList, string errorCode, string errorDescription);
    }
}
