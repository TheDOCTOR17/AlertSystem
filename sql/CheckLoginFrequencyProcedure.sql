CREATE PROCEDURE CheckLoginFrequency
AS
BEGIN
    DECLARE @threshold INT = 4;
    DECLARE @timeWindow INT = 30; -- In minutes (Needs to be modified according to our Co-Pilot data)

    -- Aggregate=ing login events within the last hour
    INSERT INTO UserAlerts (Username, IPAddress, AlertTime, AlertMessage)
    SELECT 
        Username, 
        IPAddress, 
        GETDATE(),
        CONCAT('User ', Username, ' has logged in more than ', @threshold, ' times in the last ', @timeWindow, ' minutes.')
    FROM UserLoginEvents
    WHERE LoginTime >= DATEADD(MINUTE, -@timeWindow, GETDATE())
    GROUP BY Username, IPAddress
    HAVING COUNT(*) > @threshold;
END;
