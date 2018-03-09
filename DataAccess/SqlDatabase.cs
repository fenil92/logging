using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess
{
    public class SqlDatabase : ISqlDatabase
    {
        private readonly DbProviderFactory _dbProviderFactory;
        private DbConnection _databaseConnection;
        private bool _disposed;
        public string ConnectionString
        { get; private set; }
        public SqlDatabase():this(new SqlProviderFactory())
        {

        }
        public SqlDatabase(DbProviderFactory dbProviderFactory)
        {
            _dbProviderFactory = dbProviderFactory;
        }
        public IDbCommand CreateCommand()
        {
            return _dbProviderFactory.CreateCommand();
        }

        public IDbCommand CreateCommand(string commandText, params IDbDataParameter[] parameters)
        {
            IDbCommand command = _dbProviderFactory.CreateCommand();
            command.CommandText = commandText;
            SetCommandParameter(command, parameters);
            return command;
        }

        public IDbDataParameter CreateParameter(string name)
        {
            DbParameter parameter = _dbProviderFactory.CreateParameter();
            parameter.ParameterName = BuildParameterName(name);
            return parameter;
        }

        private string BuildParameterName(string name)
        {
            return name;
        }

        public virtual IDbDataParameter CreateParameter(string name, object value)
        {
            IDbDataParameter param = CreateParameter(name);
            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }
            return param;
        }

        public DbParameter CreateParameter(string name, SqlDbType type, string typeName, object value)
        {
            var param = (SqlParameter)CreateParameter(name);
            param.TypeName = typeName;
            param.SqlDbType = type;
            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }
            return param;
        }

        public DbParameter CreateParameter(string name, SqlDbType type, int size, object value)
        {
            var param = (SqlParameter)CreateParameter(name);
            param.Size = size;
            param.SqlDbType = type;
            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }
            return param;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        private void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    if (_databaseConnection != null)
                    {
                        _databaseConnection.Close();
                        _databaseConnection = null;
                    }
                }
            }
            finally {
                _disposed = true;
            }
        }

        public DataSet ExecuteDataSet(IDbCommand command)
        {
            IDbConnection connection = GetConnection();
            PrepareCommand(command, connection);
            DataSet ds = new DataSet();
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter((SqlCommand)command))
            {
                sqlDataAdapter.Fill(ds);
            }
            return ds;
        }

        public int ExecuteNonQuery(IDbCommand command)
        {
            IDbConnection connection = GetConnection();
            PrepareCommand(command, connection);
            int result = command.ExecuteNonQuery();
            return result;
        }

        public IDataReader ExecuteReader(IDbCommand command)
        {
            IDbConnection connection = GetConnection();
            PrepareCommand(command, connection);
            IDataReader result = command.ExecuteReader();
            return result;
        }

        public object ExecuteScalar(IDbCommand command)
        {
            IDbConnection connection = GetConnection();
            PrepareCommand(command, connection);
            object result = command.ExecuteScalar();
            return result;
        }

        public int ExecuteSPNonQuery(IDbCommand command)
        {
            IDbConnection connection = GetConnection();
            PrepareCommand(command, connection);
            int result = command.ExecuteNonQuery();
            return result;
        }

        public IDataReader ExecuteSPReader(IDbCommand command)
        {
            IDbConnection connection = GetConnection();
            PrepareCommand(command, connection);

            IDataReader result = command.ExecuteReader();
            return result;
        }

        public IDbConnection GetConnection()
        {
            if (_databaseConnection.State != ConnectionState.Open)
            {
                _databaseConnection.Open();
            }
            return _databaseConnection;
        }

        public void Open(string connectionString)
        {
            if (_databaseConnection == null)
            {
                ConnectionString = connectionString;
                _databaseConnection = _dbProviderFactory.CreateConnection();
                _databaseConnection.ConnectionString = connectionString;
            }
        }

        public void PrepareCommand(IDbCommand command, IDbConnection connection)
        {
            command.Connection = connection;
            SetCommandTimeOut(command);
        }

        private void SetCommandTimeOut(IDbCommand command)
        {
            string commandTimeOut = string.Empty;
            int commandTimeOutValue = 0;
            if (command.Connection.ConnectionString.Contains("ConfigDB"))
            {
                commandTimeOut = ConfigurationSettings.AppSettings["ConfigDBcommandTimeOut"] as string;
            }

            if (!int.TryParse(commandTimeOut, out commandTimeOutValue))
            {
                commandTimeOutValue = 60;
            }
            command.CommandTimeout = commandTimeOutValue;
        }

        public void PrepareCommand(IDbCommand command, IDbConnection connection, IDbDataParameter[] parameters)
        {
            command.Connection = connection;
            SetCommandParameter(command, parameters);
            SetCommandTimeOut(command);
        }

        public void PrepareSPCommand(IDbCommand command, IDbConnection connection, IDbDataParameter[] parameters)
        {
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            SetCommandParameter(command, parameters);
            SetCommandTimeOut(command);
        }

        public void SetCommandParameter(IDbCommand command, IDbDataParameter[] parameters)
        {
            if (parameters != null)
            {
                command.Parameters.Add(parameters);
            }
        }
    }
}