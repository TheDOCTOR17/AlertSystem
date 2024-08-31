using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace UserLoginAlertSystem
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "your_connection_string_here";
            var loggingService = new LoggingService(); 


            var serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .AddSingleton(new LoginEventPublisher(connectionString, loggingService))
                .AddSingleton(new AlertProcessorService(connectionString, loggingService))
                .AddSingleton(new NotificationService(connectionString, loggingService))
                .AddSingleton(new DatabaseHelper(connectionString, loggingService))
                .AddSingleton<ILoggingService, LoggingService>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation("Starting the User Login Alert System...");

            var eventPublisher = serviceProvider.GetService<LoginEventPublisher>();
            var alertProcessor = serviceProvider.GetService<AlertProcessorService>();
            var notificationService = serviceProvider.GetService<NotificationService>();

            // Simulate login events
            eventPublisher.PublishLoginEvent("user1", "192.168.1.1");

            // Start processing alerts in the background
            var alertTask = Task.Run(() => alertProcessor.StartProcessing());

            // Start processing notifications in the background
            var notificationTask = Task.Run(() => 
            {
                while (true)
                {
                    notificationService.ProcessAlerts();
                    Task.Delay(TimeSpan.FromMinutes(5)).Wait(); // Run every 5 minutes
                }
            });

            await Task.WhenAll(alertTask, notificationTask);
        }
    }
}
