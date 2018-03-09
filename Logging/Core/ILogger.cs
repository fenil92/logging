using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Core
{
    public interface ILogger
    {

        void LogInfo(string message);

        void LogInfo(Func<string> func);

        void LogInfo(Func<string,string> action, Func<string> func);

        void LogError(BaseException exception);

        void LogError(List<BaseException> exception,string errorCode, string errorDescription);

        void LogWarning(BaseException exception);

        void LogAlert(BaseException exception);
    }
}
