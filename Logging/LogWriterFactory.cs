using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    internal class LogWriterfactory
    {

        private LogWriterfactory()
        { }

        public static object GetLogWriter(LogWriterType loggerType)
        {
            object loggerObject = null;
            switch (loggerType)
            {
                case LogWriterType.DataBaseLogWriter:
                    {
                        loggerObject = new DatabaseLogWriter();
                        break;
                    }
                case LogWriterType.EventLogWriter:
                    {
                        loggerObject = null;
                        break;
                    }
                case LogWriterType.EnterpriseLibraryFileWriter:
                    {
                        loggerObject = new EnterpriseLibraryFileWriter();
                        break;
                    }
                default:
                    {
                        loggerObject = null;
                        break;
                    }
            }
            return loggerObject;
        }
    }
}
