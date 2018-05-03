using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlServerConnections
{
    public class SqlServer
    {
        public static SqlConnection AzureSqlConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "lokalafavoriter.database.windows.net";
            builder.InitialCatalog = "lokalafavoriter";
            builder.UserID = "sundsvall2018";
            builder.Password = "Sommar2018!";
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            return connection;
        }

        public DataTable QueryRead(string query)
        {
            DataTable dt;
            SqlDataReader dr;
            SqlConnection connection = AzureSqlConnection();
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            dr = command.ExecuteReader();
            dt = new DataTable();
            dt.Load(dr);
            connection.Close();
            return dt;
        }
    }
}
