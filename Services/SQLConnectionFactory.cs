using Microsoft.Data.SqlClient;

namespace FoodDeliveryapi.Services
{
    public class SQLConnectionFactory
    {
        public readonly string _connectionString;
        public SQLConnectionFactory (string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection Create()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
