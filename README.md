# **Alert System**

## **Table of Contents**
- [Introduction](#introduction)
- [Features](#features)
- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Alerting Scenarios](#alerting-scenarios)



## **Introduction**

The **Alert System** is a C# .NET application designed to monitor user login activities in real-time using MS SQL Server and Dapper. The system is built to detect and alert on unusual or suspicious login behavior, such as multiple logins in a short time frame, logins from different IP addresses, or logins outside of normal hours.

This system is optimized to minimize database load while ensuring timely alerts, making it suitable for production environments where performance and reliability are critical.

## **Features**

- **Real-time Monitoring**: Tracks user logins in real-time and processes events as they happen.
- **Configurable Thresholds**: Allows configuration of login thresholds to prevent false positives.
- **Anomaly Detection**: Identifies and alerts on unusual login patterns, such as logins from different IPs or at unusual times.
- **Debouncing**: Groups multiple login attempts within a short window into a single alert to avoid spamming.
- **Database-Driven**: Utilizes MS SQL Server for storing and querying user activity logs.
- **Thread-Safe and Concurrency-Handled**: Designed with thread safety and concurrency in mind to handle multiple login events efficiently.
- **Custom Logging**: Integrates a logging service for error and information tracking.

## **Architecture**

The application consists of several key components:

1. **LoginEventPublisher**: This class is responsible for publishing login events. It monitors the login activity and triggers alerts based on predefined conditions.

2. **ILoggingService**: An interface for logging errors and information. This can be implemented to log to the console, files, or any logging framework.

3. **LoggingService**: A basic implementation of `ILoggingService` that logs messages to the console.

4. **Alerting Logic**: Embedded within the `LoginEventPublisher`, the logic checks login events against thresholds and conditions to determine if an alert should be generated.

5. **Database Interaction**: Utilizes Dapper for efficient and lightweight interaction with the MS SQL Server database.

## **Prerequisites**

Before running this application, ensure you have the following:

- **.NET 6 SDK** or later
- **MS SQL Server** (any version compatible with your requirements)
- **Dapper** (installed via NuGet)
- **A database** with the necessary tables for logging user activities

## **Installation**

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/UserLoginAlertSystem.git
   cd UserLoginAlertSystem

2. **Restore NuGet packages**:
   ```bash
   dotnet restore
   
3. **Build The Solution**:
   ```bash
   dotnet build
4. **Set up the dabatase**:
   - Create a database in MS SQL Server.
   - Run the SQL script to create the necessary tables for logging user activities.

## **Configuration**

1. **Configuration**
   - Update the connection string in Program.cs with your MS SQL Server connection details.
     ```csharp
     var connectionString = "your_connection_string_here";
2. **Logging Service**:
   - You can replace the LoggingService with your implementation if you want to log to a file or external logging service.

## **Usage**

1. **Run the application**
   ```bash
   dotnet run

2. **Monitor and Test**:
   - You can simulate user login activities by inserting records into the login activity table.
   - The application will process these records and generate alerts based on the configured logic.

3. **Alerts**:
   - Alerts are currently logged to the console. You can modify the ILoggingService implementation to send emails, SMS, or other notifications.

# **Alerting Scenarios**

## **Frequent Logins Within a Short Time Frame**

- If a user logs in multiple times within a short period (e.g., 5 logins in 3 minutes), the system will group these into a single alert to avoid spamming.

## **Threshold-Based Alerts**

- If a user exceeds a certain number of logins (e.g., more than 4 times in 1 hour), the system triggers an alert.

## **Unusual Login Patterns**

- Logins during odd hours or outside the user’s normal behavior patterns are flagged as suspicious.

