using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ISqlDatabase:IDisposable
    {
        IDataReader ExecuteReader(IDbCommand command);

        void Open(string connectionString);

        IDbCommand CreateCommand();

        IDbCommand CreateCommand(string commandText, params IDbDataParameter[] parameters);

        IDbDataParameter CreateParameter(string name);

        IDbDataParameter CreateParameter(string name,object value);

        DbParameter CreateParameter(string name, SqlDbType type, int size, object value);

        DbParameter CreateParameter(string name, SqlDbType type, string typeName, object value);

        int ExecuteSPNonQuery(IDbCommand command);

        int ExecuteNonQuery(IDbCommand command);

        IDbConnection GetConnection();

        DataSet ExecuteDataSet(IDbCommand command);

        object ExecuteScalar(IDbCommand command);
        void PrepareCommand(IDbCommand command, IDbConnection connection);
        void PrepareCommand(IDbCommand command, IDbConnection connection,IDbDataParameter[] parameters);

        void PrepareSPCommand(IDbCommand command, IDbConnection connection, IDbDataParameter[] parameters);

        void SetCommandParameter(IDbCommand command, IDbDataParameter[] parameters);

        IDataReader ExecuteSPReader(IDbCommand command);
    }
}
