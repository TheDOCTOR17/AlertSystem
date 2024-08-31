using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace UserLoginAlertSystem
{
    public class AlertProcessorService
    {
        private readonly string _connectionString;
        private readonly ILoggingService _loggingService;

        public AlertProcessorService(string connectionString, ILoggingService loggingService)
        {
            _connectionString = connectionString;
            _loggingService = loggingService;
        }

        public void StartProcessing()
        {
            while (true)
            {
                try
                {
                    CheckForAlerts().Wait();
                }
                catch (Exception ex)
                {
                    _loggingService.LogError("Error processing alerts", ex);
                }

                Thread.Sleep(TimeSpan.FromMinutes(5)); // Run every 5 minutes
            }
        }

        private async Task CheckForAlerts()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    EXEC CheckLoginFrequency;
                ";
                await connection.ExecuteAsync(query);
            }
        }
    }
}
