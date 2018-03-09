using Logging.Core;
using Logging.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    class DatabaseLogWriter : ILogWriter
    {
        private ILogRepository _logRepository;

        public DatabaseLogWriter():this(new LogRepository())
        {

        }
        internal DatabaseLogWriter(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        public void Log(LogEntity entity)
        {
            _logRepository.Log(entity);
        }

        public void Log(List<LogEntity> exceptionLogList, string errorCode, string errorDescription)
        {
            _logRepository.Log(exceptionLogList, errorCode, errorDescription);
        }
    }
}
