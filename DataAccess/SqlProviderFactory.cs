using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SqlProviderFactory:DbProviderFactory
    {
        public override DbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public override DbConnection CreateConnection()
        {
            return new SqlConnection();
        }

        public override DbParameter CreateParameter()
        {
            return new SqlParameter();
        }
    }
}
