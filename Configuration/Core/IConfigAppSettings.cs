using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Core
{
    public interface IConfigAppSettings
    {
        string ConfigDataBaseConnection { get; }

        int TraceLevel { get; }

        string isTracingEnabled { get; }
    }
}
