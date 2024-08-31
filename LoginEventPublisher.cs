using System;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging; 

namespace UserLoginAlertSystem
{

    public class LoginEventPublisher
    {
        private readonly string _connectionString;
        private readonly ILoggingService _loggingService;

        public LoginEventPublisher(string connectionString, ILoggingService loggingService)
        {
            _connectionString = connectionString;
            _loggingService = loggingService;
        }

        public async Task PublishLoginEventAsync(string username, string ipAddress)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = "INSERT INTO UserLoginEvents (Username, IPAddress, LoginTime) VALUES (@Username, @IPAddress, @LoginTime)";
                    var parameters = new { Username = username, IPAddress = ipAddress, LoginTime = DateTime.UtcNow };
                    await connection.ExecuteAsync(query, parameters);
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error publishing login event", ex);
            }
        }
    }
}
