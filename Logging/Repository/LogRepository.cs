using Configuration;
using Configuration.Core;
using Logging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data;

namespace Logging.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly IConfigAppSettings _appSettings;

        private readonly IDatabaseFactory _databaseFactory;
        public LogRepository():this(new ConfigAppSettings(),new DatabaseFactory())
        {

        }

        internal LogRepository(IConfigAppSettings appSettings,IDatabaseFactory databaseFactory)
        {
            _appSettings = appSettings;
            _databaseFactory = databaseFactory;
        }
        public void Log(LogEntity entity)
        {
            //DB call
            using (var database = _databaseFactory.GetDatabase())
            {
                var activityId = database.CreateParameter("@activityId", SqlDbType.VarChar, 60, entity.ActivityId);
                IDbCommand command = database.CreateCommand();
                command.Parameters.Add(activityId);
                command.CommandType = CommandType.StoredProcedure;
                database.Open(_appSettings.ConfigDataBaseConnection);
                command.Connection = database.GetConnection();
                var affectedRows = command.ExecuteNonQuery();
            }
                
                    
        }
      
        public void Log(List<LogEntity> exceptionLogList, string errorCode, string errorDescription)
        {
            throw new NotImplementedException();
        }
    }
}
