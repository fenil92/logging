using Logging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Security.Principal;
using Configuration.Core;
using Configuration;

namespace Logging
{
    public class Logger : ILogger
    {
        private static ILogger _instance = new Logger();

        public static ILogger Instance
        {
            get { return _instance; }
        }
        private readonly IConfigAppSettings _configAppSettings;

        private readonly ILogWriter _logWriter;

        private readonly IEventLogWriter _eventWriter;

        private readonly IEnterpriseLibraryFileWriter _entLibWriter;


        private int ConfigTraceLevel
        {
            get;set;
        }
        public Logger(): this(new ConfigAppSettings(),(ILogWriter)LogWriterfactory.GetLogWriter(LogWriterType.DataBaseLogWriter), (IEventLogWriter)LogWriterfactory.GetLogWriter(LogWriterType.EventLogWriter), (IEnterpriseLibraryFileWriter)LogWriterfactory.GetLogWriter(LogWriterType.EnterpriseLibraryFileWriter))
        {

        }

        internal Logger(IConfigAppSettings configAppSettings, ILogWriter logWriter, IEventLogWriter eventWriter, IEnterpriseLibraryFileWriter entLibWriter)
        {
            _configAppSettings = configAppSettings;
            _logWriter = logWriter;
            _eventWriter = eventWriter;
            _entLibWriter = entLibWriter;

        }
        public void LogAlert(BaseException exception)
        {
            throw new NotImplementedException();
        }

        public void LogError(BaseException exception)
        {
            if (_configAppSettings.TraceLevel != 0 && _configAppSettings.TraceLevel <= 3)
            {
                Write(exception.Message, "3", exception);
            }
        }

        public void LogError(List<BaseException> exception, string errorCode, string errorDescription)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(Func<string> func)
        {
            if (_configAppSettings.TraceLevel != 0 && _configAppSettings.TraceLevel <= 1)
            {
                if (func != null)
                {
                    Write(func());
                }
            }
        }

        public void LogInfo(string message)
        {
            throw new NotImplementedException();
        }

        public void LogInfo(Func<string, string> action, Func<string> func)
        {
            throw new NotImplementedException();
        }

        public void LogWarning(BaseException exception)
        {
            throw new NotImplementedException();
        }

        private void Write(string message, string configTraceLevel, BaseException exception = null)
        {
            var contxt = System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("appContext");
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Suppress, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                LogEntity entity = new LogEntity();
                try
                {
                   
                    entity.ActivityId = "abc";
                    entity.Message = message;
                    entity.StackTrace = exception.StackTrace;
                    entity.TraceLevel = configTraceLevel;
                    entity.UserName = WindowsIdentity.GetCurrent().Name;
                    if (_logWriter != null)
                    {
                        _logWriter.Log(entity);
                    }
                }
                catch (Exception ex){
                    _eventWriter.WriteToEventLog(entity, ex);
                }
            }

           
        }

        private void Write(string message)
        {
            LogEntity entity = new LogEntity();
            try
            {

                entity.ActivityId = "abc";
                entity.Message = message;
                entity.TraceLevel = "1";
                entity.UserName = WindowsIdentity.GetCurrent().Name;
                if (_entLibWriter != null)
                {
                    _entLibWriter.LogToFile(entity);
                }
            }
            catch (Exception ex)
            {
                _eventWriter.WriteToEventLog(entity, ex);
            }
        }
    }
}
