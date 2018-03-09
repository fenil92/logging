using Configuration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class ConfigAppSettings : IConfigAppSettings
    {
        public string ConfigDataBaseConnection
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string isTracingEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int TraceLevel
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
