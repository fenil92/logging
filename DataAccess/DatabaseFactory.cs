using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccess
{
    public class DatabaseFactory : IDatabaseFactory
    {
        public DatabaseFactory()
        {
        }
        public ISqlDatabase GetDatabase()
        {
            ISqlDatabase database = new SqlDatabase();
            return database;
        }
    }
}
