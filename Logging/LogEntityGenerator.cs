using Logging.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class LogEntityGenerator
    {

        public static LogEntity Generate(string activityId, string message)
        {
            StackTrace stackTrace = new StackTrace();
            var callingMethod = stackTrace.GetFrame(1).GetMethod();
            return new LogEntity()
            {
                ActivityId = activityId,
                ClassName=string.Format("{0}-{1}",callingMethod.ReflectedType.Name,callingMethod.Name),
                Message=message,
                DateLogged=DateTime.Now
            };
        }
    }
}
