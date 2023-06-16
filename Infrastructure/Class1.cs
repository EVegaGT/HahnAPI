using Microsoft.Data.SqlClient;
using System.Data;

namespace Infrastructure
{
    public class DatabaseHandler
    {
        private readonly string _connectionString;

        public DatabaseHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataSet Select(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var adapter = new SqlDataAdapter(query, connection);
            var dataSet = new DataSet();
            adapter.Fill(dataSet);

            return dataSet;
        }

        public int RowsAffectedByQuery(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(query, connection);
            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected;
        }

        public int GetRowsAffectedCount(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var command = new SqlCommand(query, connection);
            var result = command.ExecuteScalar();

            int rowCount = Convert.ToInt32(result);

            return rowCount;
        }
    }

}
