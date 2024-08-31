using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace UserLoginAlertSystem
{
    public class NotificationService
    {
        private readonly string _connectionString;
        private readonly ILoggingService _loggingService;

        public NotificationService(string connectionString, ILoggingService loggingService)
        {
            _connectionString = connectionString;
            _loggingService = loggingService;
        }

        public void ProcessAlerts()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    var query = @"
                        SELECT * FROM UserAlerts 
                        WHERE AlertTime >= @Since;
                    ";
                    var parameters = new { Since = DateTime.UtcNow.AddMinutes(-5) };
                    var alerts = connection.Query(query, parameters);

                    foreach (var alert in alerts)
                    {
                        SendAlert(alert.Username, alert.AlertMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error processing alerts", ex);
            }
        }

        private void SendAlert(string username, string message)
        {
            // Implement email/SMS sending logic here
            Console.WriteLine($"Alert for {username}: {message}");
        }
    }
}
