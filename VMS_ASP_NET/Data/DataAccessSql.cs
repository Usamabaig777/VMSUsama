using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace VMS_ASP_NET
{
    public class DataAccessSql
    {
        private static bool isInitialized = false;
        private static string dbConnectionString = null;
        private static int connCount = 0;
        private const int commandTimeout = 60000;
        private Random random = new Random(DateTime.Now.Millisecond);

        public static void Initialize(string connectionString)
        {
            dbConnectionString = connectionString;
            isInitialized = true;
        }

        /// Constructor of DataAccessSql
        internal DataAccessSql()
        {
            if (dbConnectionString != null)
                isInitialized = true;
            else
                throw new Exception("Connection String not Specified");
        }

        /// This method serves to check that database is connected with the provided connectionString on not.
        public static bool IsDatabaseConnected()
        {
            try
            {
                if (!isInitialized)
                    throw new Exception("DataAccessSql is not Initialized!");

                using (var loConnection = new SqlConnection(dbConnectionString))
                {
                    loConnection.Open();
                    loConnection.Close();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Create Connection

        /// Creates a new SqlConnection,Provides connection to DB command wrapper.
        internal SqlConnection CreateConnection()
        {
            if (!isInitialized)
                throw new Exception("DataAccessSql is not Initialized");

            return new SqlConnection(dbConnectionString);
        }

        internal async Task<SqlConnection> CreateConnectionAsync()
        {
            if (!isInitialized)
                throw new Exception("Data.DataAccessSql is not Initialized");

            var loConnection = await Task.Run(() => new SqlConnection(dbConnectionString));
            return loConnection;
        }

        #endregion

        #region Create Command

        internal DbCommand CreateCommand(string commandText)
        {

            SqlCommand command = new SqlCommand(commandText, CreateConnection());
            command.CommandTimeout = commandTimeout;
            command.CommandType = CommandType.StoredProcedure;

            return command;
        }

        internal async Task<DbCommand> CreateCommandAsync(string commandText)
        {

            SqlCommand command = new SqlCommand(commandText, await CreateConnectionAsync());
            command.CommandTimeout = commandTimeout;
            command.CommandType = CommandType.StoredProcedure;

            return command;
        }

        internal DbCommand CreateInlineCommand(string commandText)
        {

            SqlCommand command = new SqlCommand(commandText, CreateConnection());
            command.CommandTimeout = commandTimeout;
            command.CommandType = CommandType.Text;

            return command;
        }

        #endregion

        #region Create Parameter

        internal DbParameter CreateParameter(string name, object value)
        {

            SqlParameter param = new SqlParameter();

            param.IsNullable = true;
            param.ParameterName = name;

            if (string.IsNullOrEmpty(Convert.ToString(value)) || Convert.ToString(value).Trim().Length == 0)
                param.Value = DBNull.Value;
            else
                param.Value = value;

            if (value is DataTable)
            {
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = ((DataTable)value).TableName;
                param.Value = value;
            }

            return param;
        }

        internal DbParameter CreateParameter(string name, object value, bool isNullable)
        {

            SqlParameter param = new SqlParameter();
            param.IsNullable = true;
            param.ParameterName = name;
            param.Value = value;
            param.IsNullable = isNullable;

            return param;
        }

        internal DbParameter CreateParameter(string name, int size, DbType type, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter();
            param.IsNullable = true;
            param.ParameterName = name;
            param.DbType = type;
            param.Size = size;
            param.Direction = direction;

            return param;
        }

        internal DbParameter CreateParameter(string name, int size, SqlDbType type, ParameterDirection direction)
        {
            SqlParameter param = new SqlParameter();

            param.IsNullable = true;
            param.ParameterName = name;
            param.SqlDbType = type;
            param.Direction = direction;
            param.Size = size;

            return param;
        }

        #endregion

        #region Execute Procedure

        internal DataTable ExecuteProcedure(DbCommand command, DataTable table)
        {
            SqlDataAdapter sqlAdopter = new SqlDataAdapter((SqlCommand)command);
            try
            {
                var currentTime = DateTime.Now;
                sqlAdopter.Fill(table);
                var timetaken = DateTime.Now - currentTime;

                if (table != null)
                    return table;
                else
                    return new DataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlAdopter.Dispose();
            }
        }

        internal async Task<DataTable> ExecuteProcedureAsync(DbCommand command, DataTable table)
        {
            SqlDataAdapter sqlAdopter = new SqlDataAdapter((SqlCommand)command);
            try
            {
                var currentTime = DateTime.Now;
                await Task.Run(() => sqlAdopter.Fill(table));
                var timetaken = DateTime.Now - currentTime;

                if (table != null)
                    return table;
                else
                    return new DataTable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlAdopter.Dispose();
            }
        }

        internal DataSet ExecuteProcedure(DbCommand command, DataSet tables)
        {
            SqlDataAdapter sqlAdopter = new SqlDataAdapter((SqlCommand)command);
            try
            {
                var currentTime = DateTime.Now;
                sqlAdopter.Fill(tables);
                var timetaken = DateTime.Now - currentTime;
                return tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlAdopter.Dispose();
            }

        }

        internal async Task<DataSet> ExecuteProcedureAsync(DbCommand command, DataSet tables)
        {
            SqlDataAdapter sqlAdopter = new SqlDataAdapter((SqlCommand)command);
            try
            {
                var currentTime = DateTime.Now;
                await Task.Run(() => sqlAdopter.Fill(tables));
                var timetaken = DateTime.Now - currentTime;
                return tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlAdopter.Dispose();
            }
        }

        internal object ExecuteProcedure(DbCommand command)
        {
            try
            {
                command.Connection.Open();
                if (command.Connection != null && command.Connection.State == ConnectionState.Open)
                {
                    connCount++;
                }

                var currentTime = DateTime.Now;

                object result = command.ExecuteNonQuery();

                var timetaken = DateTime.Now - currentTime;

                return result;
            }
            catch (Exception ex)
            {
                if (command.Connection != null && command.Connection.State == ConnectionState.Open)
                {
                    command.Connection.Close();
                    connCount--;
                }

                throw ex;
            }
            finally
            {
                if (command.Connection != null && command.Connection.State == ConnectionState.Open)
                {
                    command.Connection.Close();
                    connCount--;
                }
            }
        }

        internal object ExecuteProcedure(SqlCommand command, AsyncCallback callback)
        {
            try
            {
                command.Connection.ConnectionString = (dbConnectionString + ";Asynchronous Processing=true;");
                command.Connection.Open();
                if (command.Connection != null && command.Connection.State == ConnectionState.Open)
                {
                    connCount++;
                }
                object result = command.BeginExecuteNonQuery(callback, random.Next());

                return result;
            }
            catch (Exception ex)
            {
                if (command.Connection != null && command.Connection.State == ConnectionState.Open)
                {
                    command.Connection.Close();
                    connCount--;
                }

                throw ex;
            }
            finally
            {
                if (command.Connection != null && command.Connection.State == ConnectionState.Open)
                {
                    command.Connection.Close();
                    connCount--;
                }

            }
        }

        #endregion

        #region Execute

        internal static DataTable ExecuteSqlQuery(DbCommand command)
        {
            try
            {
                SqlDataAdapter sqlAdopter = new SqlDataAdapter();

                sqlAdopter.SelectCommand = (SqlCommand)command;

                DataTable table = new DataTable();

                sqlAdopter.Fill(table);

                return table;

            }
            catch (SqlException exc)
            {
                throw exc;
            }
        }

        internal object ExecuteSqlFunction(DbCommand command)
        {
            string result = "";
            try
            {
                command.Connection.Open();
                if (command.Connection != null && command.Connection.State == ConnectionState.Open)
                {
                    connCount++;
                }

                SqlDataReader dr = (SqlDataReader)command.ExecuteReader();

                if (dr.Read())
                    result = Convert.ToString(dr[0]);
            }
            catch (Exception ex)
            {
                if (command.Connection != null && command.Connection.State == ConnectionState.Open)
                {
                    command.Connection.Close();
                    connCount--;
                }
                throw ex;
            }
            finally
            {
                if (command.Connection != null && command.Connection.State == ConnectionState.Open)
                {
                    command.Connection.Close();
                    connCount--;
                }

            }
            return result;
        }

        #endregion
    }
}