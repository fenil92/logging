using Logging.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    class EventLogWriter : IEventLogWriter
    {

        private static IEventLogWriter _instance = new EventLogWriter();

        private EventLogWriter()
        { }

        public static IEventLogWriter Instance {
           get {
                return _instance;
            }
        }
        public void WriteToEventLog(LogEntity entity, Exception ex)
        {
            Log(entity, ex);
        }

        private void Log(LogEntity entity, Exception ex)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "abc";
                eventLog.WriteEntry(entity.Message,EventLogEntryType.Error,9001);
                eventLog.Close();
            }
        }
    }
}
