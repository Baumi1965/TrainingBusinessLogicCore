using System.Diagnostics;
using System.Security.Authentication.ExtendedProtection;
using Loupe.Serilog;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MariaDB;
using Serilog.Sinks.MariaDB.Extensions;

namespace Training.BusinessLogic.Logging;

public static class Logging
{
    private static readonly int _logLevelMariaDb = GetLogLevelMariaDb();
    private static readonly string _logTableMariaDb = GetLogTableMariaDb();
    static Logging()
    {
        Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
        
 
        
        var propertiesToColumn1 = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            ["Exception"] = "Exception",
            ["Level"] = "LogLevel",
            ["Properties"] = "Properties",
            ["Action"] = "Message",
            ["Timestamp"] = "Timestamp",
            ["ExtendedInfo"] = "ExtendedInfo",
            ["MachineName"] = "Hostname",
            ["EnvironmentUserName"] = "User",
            ["Application"] = "Application",
            ["Class"] = "Class",
            ["Method"] = "Method",
            ["Location"] = "Location",
            ["Value1"] = "Value1",
            ["Value2"] = "Value2",
            ["Value3"] = "Value3",
            ["Value4"] = "Value4",
            ["Value5"] = "Value5",
            ["ReturnValue"] = "ReturnValue",
            ["ProgramVersion"] = "ProgramVersion",
            ["Guid"] = "Guid",
            ["TSCreated"] = "TSCreated",
        };
       
        var sinkOptions = new MariaDBSinkOptions()
        {
            PropertiesToColumnsMapping = propertiesToColumn1,
            ExcludePropertiesWithDedicatedColumn = false,
            TimestampInUtc = true,
        };

 
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.Loupe()
            .WriteTo.MariaDB(
                tableName: _logTableMariaDb,
                restrictedToMinimumLevel: (LogEventLevel)_logLevelMariaDb,
                autoCreateTable:true,
                useBulkInsert:false,
                connectionString: GetConnectionStringLogging())
                //options: sinkOptions)
            .CreateLogger();
    }
    
    public static void LogMessage(LogEventLevel level, string? assemblyName, string action, string? extendedInfo, string program, string method, string location, string? assemblyVersion, Guid guid, string value1 = "", string value2 = "", string value3 = "", string value4 = "", string value5 = "", string returnValue = "", Exception ex = null)
    {
        switch (level)
        {
            case LogEventLevel.Verbose:
                Log.Verbose(
                    "{@Hostname}, {@Application}, {@User}, {@Action}, {@ExtendedInfo}, {@Class}, {@Method}, {@Location}, {@Value1}, {@Value2}, {@Value3}, {@Value4}, {@Value5}, {@ReturnValue}, {@ProgramVersion}, {@Guid}, {@TSCreated}",
                    Environment.MachineName, assemblyName, Environment.UserName, action, extendedInfo, program, method, location, value1, value2, value3, value4, value5, returnValue, assemblyVersion, guid, DateTime.Now);
                break;
            case LogEventLevel.Debug:
                Log.Debug(
                    "{@Hostname}, {@Application}, {@User}, {@Action}, {@ExtendedInfo}, {@Class}, {@Method}, {@Location}, {@Value1}, {@Value2}, {@Value3}, {@Value4}, {@Value5}, {@ReturnValue}, {@ProgramVersion}, {@Guid}, {@TSCreated}",
                    Environment.MachineName, assemblyName, Environment.UserName, action, extendedInfo, program, method, location, value1, value2, value3, value4, value5, returnValue, assemblyVersion, guid, DateTime.Now);
                break;
            case LogEventLevel.Information:
                Log.Information(
                    "{@Application}, {@Action}, {@ExtendedInfo}, {@Class}, {@Method}, {@Location}, {@Value1}, {@Value2}, {@Value3}, {@Value4}, {@Value5}, {@ReturnValue}, {@ProgramVersion}, {@Guid}, {@TSCreated}",
                    assemblyName, action, extendedInfo, program, method, location, value1, value2, value3, value4, value5, returnValue, assemblyVersion, guid, DateTime.Now);
                break;
            case LogEventLevel.Warning:
                Log.Warning(
                    "{@Hostname}, {@Application}, {@User}, {@Action}, {@ExtendedInfo}, {@Class}, {@Method}, {@Location}, {@Value1}, {@Value2}, {@Value3}, {@Value4}, {@Value5}, {@ReturnValue}, {@ProgramVersion}, {@Guid}, {@TSCreated} ",
                    Environment.MachineName, assemblyName, Environment.UserName, action, extendedInfo, program, method, location, value1, value2, value3, value4, value5, returnValue, assemblyVersion, guid, DateTime.Now);
                break;
            case LogEventLevel.Error:
                Log.Error(ex,
                    "{@Hostname}, {@Application}, {@User}, {@Action}, {@ExtendedInfo}, {@Class}, {@Method}, {@Location}, {@Value1}, {@Value2}, {@Value3}, {@Value4}, {@Value5}, {@ReturnValue}, {@ProgramVersion}, {@Guid}, {@TSCreated}",
                    Environment.MachineName, assemblyName, Environment.UserName, action, extendedInfo, program, method, location, value1, value2, value3, value4, value5, returnValue, assemblyVersion, guid, DateTime.Now);
                break;
            case LogEventLevel.Fatal:
                Log.Fatal(ex,
                    "{@Hostname}, {@Application}, {@User}, {@Action}, {@ExtendedInfo}, {@Class}, {@Method}, {@Location}, {@Value1}, {@Value2}, {@Value3}, {@Value4}, {@Value5}, {@ReturnValue}, {@ProgramVersion}, {@Guid}, {@TSCreated} ",
                    Environment.MachineName, assemblyName, Environment.UserName, action, extendedInfo, program, method, location, value1, value2, value3, value4, value5, returnValue, assemblyVersion, guid, DateTime.Now);
                break;
        }
    }

    public static void CloseAndFlush()
    {
        Log.CloseAndFlush();
    }
    
    private static IConfiguration GetConfig()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(System.AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        return builder.Build();
    }
    
    private static string GetConnectionStringLogging()
    {
        var settings = GetConfig();
        var connectionString = settings?.GetConnectionString("training");
        return connectionString.Replace("XpoProvider=MySql;","");
    } 
    
    public static int GetLogLevelMariaDb()
    {
        var settings = GetConfig();
        var logLevel = Convert.ToInt32(settings["LogLevelMariaDb"]);
        return logLevel;
    }
    
    public static string GetLogTableMariaDb()
    {
        var settings = GetConfig();
        var logTable = settings["LogTableMariaDb"];
        return logTable;
    }
}