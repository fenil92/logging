using Configuration;
using Configuration.Core;
using Logging.Core;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;


namespace Logging
{
    class EnterpriseLibraryFileWriter : IEnterpriseLibraryFileWriter
    {

        private readonly IConfigAppSettings _appSettings;

        private LogWriter _logger;

        public LogWriter Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = new LogWriterFactory().Create(); //this factory class belongs to enterpriselibrary dll
                    Microsoft.Practices.EnterpriseLibrary.Logging.Logger.SetLogWriter(_logger, false);
                }
                return _logger;
            }
        }


        public EnterpriseLibraryFileWriter():this(new ConfigAppSettings())
        {

        }
        internal EnterpriseLibraryFileWriter(IConfigAppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public void LogToFile(LogEntity logEntity)
        {
            if (_appSettings.isTracingEnabled.ToUpper() == "TRUE")
            {
                Logger.Write(new LogEntry
                {
                    Message = logEntity.Message
                });
            }
        }

       
    }
}
