using System;
using System.Data.SqlClient;

namespace UserLoginAlertSystem
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;
        private readonly ILoggingService _loggingService;

        public DatabaseHelper(string connectionString, ILoggingService loggingService)
        {
            _connectionString = connectionString;
            _loggingService = loggingService;
        }

        public SqlConnection GetConnection()
        {
            try
            {
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error establishing database connection", ex);
                throw;
            }
        }
    }
}
