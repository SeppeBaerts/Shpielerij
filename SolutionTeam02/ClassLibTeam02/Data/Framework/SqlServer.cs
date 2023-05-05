using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibTeam02.Data.Framework
{
    /// <summary>
    /// CL-05
    /// </summary>
    public abstract class SqlServer
    {
        SqlConnection connection;
        SqlDataAdapter adapter;
        public static int newId { get; set; }
        public SqlServer()
        {
            connection = new SqlConnection(LocalSettings.GetConnectionString());
        }

        protected SelectResult Select(SqlCommand selectCommand)
        {
            var result = new SelectResult();
            try
            {
                using (connection)
                {
                    selectCommand.Connection = connection;
                    connection.Open();
                    adapter = new SqlDataAdapter(selectCommand);
                    result.DataTable = new DataTable();
                    adapter.Fill(result.DataTable);
                    connection.Close();
                }
                result.ConnectionString = connection.ConnectionString;
                result.Succeeded = true;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
            }
            return result;
        }

        protected SelectResult Select(string tableName)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = $"SELECT * FROM {tableName}";
            return Select(command);
        }

        protected SelectResult SelectCity(string areaCode ,string tableName = "AreaCodes")
        {
            SqlCommand command = new SqlCommand
            {
                CommandText = $"SELECT City FROM {tableName} WHERE AreaCode = '{areaCode}'",
            };
            return Select(command);
        }

        protected InsertResult InsertRecord(SqlCommand insertCommand)
        {
            InsertResult result = new InsertResult();
            try
            {
                using (connection)
                {
                    insertCommand.Connection = connection;
                    connection.Open();
                    insertCommand.ExecuteNonQuery();
                    connection.Close();
                }
                result.Succeeded = true;
                result.ConnectionString = connection.ConnectionString;
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }
        protected InsertResult InsertCity(SqlCommand insertCommand)
        {
            InsertResult result = new InsertResult();
            try
            {
                using (connection)
                {
                    insertCommand.Connection = connection;
                    connection.Open();
                    insertCommand.ExecuteNonQuery();
                    connection.Close();
                    result.Succeeded = true;
                }
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
