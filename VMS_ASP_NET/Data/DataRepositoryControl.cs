using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace VMS_ASP_NET
{
    public class DataRepositoryControl
    {
        private static DataAccessSql dataAccess = new DataAccessSql();

        public static bool IsDatabaseConnected()
        {
            return DataAccessSql.IsDatabaseConnected();
        }

        #region ExecuteTable

        public static DataTable ExecuteTable(string procedureName, SortedList<string, object> lst)
        {
            DbCommand procedure = CreateCommand(procedureName, lst);

            return dataAccess.ExecuteProcedure(procedure, new DataTable());
        }

        public static async Task<DataTable> ExecuteTableAsync(string procedureName, SortedList<string, object> lst)
        {
            DbCommand procedure = await CreateCommandAsync(procedureName, lst);

            return await dataAccess.ExecuteProcedureAsync(procedure, new DataTable());
        }

        #endregion

        #region ExecuteDataset

        public static DataSet ExecuteDataset(string procedureName, SortedList<string, object> lst)
        {
            DbCommand procedure = CreateCommand(procedureName, lst);

            return dataAccess.ExecuteProcedure(procedure, new DataSet());
        }

        public static async Task<DataSet> ExecuteDatasetAsync(string procedureName, SortedList<string, object> lst)
        {
            DbCommand procedure = await CreateCommandAsync(procedureName, lst);

            return await dataAccess.ExecuteProcedureAsync(procedure, new DataSet());
        }

        #endregion

        #region Execute

        public static object Execute(string procedureName, string retunrParam, SortedList<string, object> lst)
        {
            DbCommand procedure = CreateCommand(procedureName, lst);
            if (!string.IsNullOrEmpty(retunrParam))
                procedure.Parameters.Add(dataAccess.CreateParameter(retunrParam, 4000, SqlDbType.VarChar, ParameterDirection.Output));
            var result = dataAccess.ExecuteProcedure(procedure);
            if (!string.IsNullOrEmpty(retunrParam))
                return procedure.Parameters[retunrParam].Value;
            else
                return result;

        }

        public static object Execute(string procedureName, string retunrParam, SortedList<string, object> lst, AsyncCallback callback)
        {
            SqlCommand procedure = CreateCommand(procedureName, lst) as SqlCommand;
            if (!string.IsNullOrEmpty(retunrParam))
                procedure.Parameters.Add(dataAccess.CreateParameter(retunrParam, 4000, SqlDbType.VarChar, ParameterDirection.Output));
            var result = dataAccess.ExecuteProcedure(procedure, callback);

            return result;

        }

        #endregion

        #region CreateCommand

        private static DbCommand CreateCommand(string procedureName, SortedList<string, object> lst)
        {
            DbCommand procedure = dataAccess.CreateCommand(procedureName);

            StringBuilder dbParams = new StringBuilder("EXEC " + procedureName + Environment.NewLine);

            foreach (var item in lst)
            {
                dbParams.Append("@" + item.Key + "='" + item.Value + "', ");
                procedure.Parameters.Add(dataAccess.CreateParameter(item.Key, item.Value));
            }

            return procedure;
        }

        private static async Task<DbCommand> CreateCommandAsync(string procedureName, SortedList<string, object> lst)
        {
            DbCommand procedure = await dataAccess.CreateCommandAsync(procedureName);

            StringBuilder dbParams = new StringBuilder("EXEC " + procedureName + Environment.NewLine);

            foreach (var item in lst)
            {
                dbParams.Append("@" + item.Key + "='" + item.Value + "', ");
                procedure.Parameters.Add(dataAccess.CreateParameter(item.Key, item.Value));
            }

            return procedure;
        }

        #endregion
    }
}