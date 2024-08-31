using System;

namespace UserLoginAlertSystem
{
    public interface ILoggingService
    {
        void LogInformation(string message);
        void LogError(string message, Exception ex);
    }

    public class LoggingService : ILoggingService
    {
        public void LogInformation(string message)
        {
            Console.WriteLine($"INFO: {message}");
        }

        public void LogError(string message, Exception ex)
        {
            Console.WriteLine($"ERROR: {message} - Exception: {ex.Message}");
        }
    }
}
