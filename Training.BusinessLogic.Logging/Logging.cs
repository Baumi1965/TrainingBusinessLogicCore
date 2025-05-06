using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using DevExpress.Data.Linq.Helpers;
using DevExpress.Xpo;
using Loupe.Serilog;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MariaDB;
using Serilog.Sinks.MariaDB.Extensions;
using Training.BusinessLogic.UOW;
using Training.BusinessLogic.UOW.Models;
using Formatting = System.Xml.Formatting;

namespace Training.BusinessLogic.Logging;

public class LoggingList
{
    public long Id { get; set; }
    public string Exception { get; set; }
    public string LogLevel { get; set; }
    //public string Properties { get; set; }
    //public DateTime? Timestamp { get; set; }
    public DateTime TSCreated { get; set; }
    public string Class { get; set; }
    public string Method { get; set; }
    public string Action { get; set; }
    public string ExtendedInfo { get; set; }
    public string Location { get; set; }
    public string Value1 { get; set; }
    public string Value2 { get; set; }
    public string Value3 { get; set; }
    public string Value4 { get; set; }
    public string Value5 { get; set; }
    public string ReturnValue { get; set; }
    public string Application { get; set; }
    public string ProgramVersion { get; set; }
    public string MachineName { get; set; }
    public string EnvironmentUserName { get; set; }
    public string Guid { get; set; }
}

public class LoggingDb : DynamicObject
{
    public long Id { get; set; }
    public string Exception { get; set; }
    public string LogLevel { get; set; }
    public string Message { get; set; }
    public string MessageTemplate { get; set; }
    public string Properties { get; set; }
    public DateTime? Timestamp { get; set; }
    //public int Index { get; set; }
    
    
    private readonly ExpandoObject _extraProperties;

    public LoggingDb()
    {
        
    }
    public LoggingDb(ExpandoObject expandoObject)
    {
        _extraProperties = new ExpandoObject();

        var dictionary = (IDictionary<string, object>)_extraProperties;
        if (dictionary.ContainsKey("Id"))
        {
            Id = Convert.ToInt32(dictionary["Id"]);
        }
        if (dictionary.ContainsKey("Exception"))
        {
            Exception = dictionary["Exception"].ToString();
        }
        if (dictionary.ContainsKey("LogLevel"))
        {
            LogLevel = dictionary["LogLevel"].ToString();
        }
        if (dictionary.ContainsKey("Message"))
        {
            Message = dictionary["Message"].ToString();
        }
        if (dictionary.ContainsKey("MessageTemplate"))
        {
            MessageTemplate = dictionary["MessageTemplate"].ToString();
        }
        if (dictionary.ContainsKey("Properties"))
        {
            Properties = dictionary["Properties"].ToString();
        }
        if (dictionary.ContainsKey("Timestamp"))
        {
            Timestamp = Convert.ToDateTime(dictionary["Timestamp"]);
        }
    }    
    
    public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        var dictionary = (IDictionary<string, object>)_extraProperties;
        return dictionary.TryGetValue(binder.Name, out result);
    }

    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        var dictionary = (IDictionary<string, object>)_extraProperties;
        dictionary[binder.Name] = value;
        return true;
    }
    
    private static ExpandoObject ReorderExpando(IDictionary<string,object> expando, List<string> order)
    {
        // Sort the dictionary based on the custom order list
        var sortedDict = expando.OrderBy(kvp => order.IndexOf(kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        // Create a new ExpandoObject and insert the properties in sorted order
        dynamic newExpando = new ExpandoObject();
        var newDict = (IDictionary<string, object>)newExpando;

        foreach (var kvp in sortedDict)
        {
            newDict[kvp.Key] = kvp.Value;
        }

        return newExpando;
    }    

       public static async Task<BindingList<LoggingList>> GetAsync(int records, bool all, DateTime from, DateTime to, string application, CancellationToken cancellationToken)
        {
            try
            {
                if (Uow._uow == null || !Uow._uow.IsConnected)
                {
                    Uow.Connect();
                }

                cancellationToken.ThrowIfCancellationRequested();

                if (application.ToLower() == "training.buchung.ers")
                {
                    List<logging_training_buchungers> result = new List<logging_training_buchungers>();

                    if (!all)
                    {
                        result = await Uow._uow.Query<logging_training_buchungers>()
                            .Where(x => x.Timestamp >= from && x.Timestamp <= to)
                            .OrderByDescending(x => x.Timestamp)
                            .Take(records)
                            .ToListAsync(cancellationToken)
                            .ConfigureAwait(true);
                    }
                    else
                    {
                        result = await Uow._uow.Query<logging_training_buchungers>()
                            .OrderByDescending(x => x.Timestamp)
                            .ToListAsync(cancellationToken)
                            .ConfigureAwait(true);
                    }

                    var lstLogging = new BindingList<object>();

                    if (result.Count > 0)
                    {
                        foreach (dynamic item in result)
                        {
                            var orderList = new List<string> { "Id", "LogLevel", "TSCreated", "Class", "Method", "Action", "ExtendedInfo", "Location", 
                            "Value1", "Value2", "Value3", "Value4", "Value5", "ReturnValue", "Application", "ProgramVersion", "MachineName", "EnvironmentUserName",
                            "Guid", "Exception", "Timestamp", "Properties"};

                            var converter = new ExpandoObjectConverter();
                            var dynamicObject = new ExpandoObject() as IDictionary<string, object>;
                            var dynamicObjectResult = new ExpandoObject() as IDictionary<string, object>;

                            dynamicObject = JsonConvert.DeserializeObject<ExpandoObject>(item.Properties, converter);

                            dynamicObject.Add("Id", item.Id);
                            dynamicObject.Add("LogLevel", item.LogLevel);
                            dynamicObject.Add("Timestamp", item.Timestamp);
                            dynamicObject.Add("Exception", item.Exception);
                            dynamicObject.Add("Properties", item.Properties);

                            dynamicObjectResult = ReorderExpando(dynamicObject, orderList);

                            lstLogging.Add(dynamicObjectResult);
                        }
                    }
                    
                    BindingList<LoggingList> loggingList = new BindingList<LoggingList>();

                    foreach (ExpandoObject item in lstLogging)
                    {
                        var json = JsonConvert.SerializeObject(item);
                        var log = JsonConvert.DeserializeObject<LoggingList>(json);
                        loggingList.Add(log);
                    }
                    
                    return loggingList;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
   
}

public class Logging
{
    
    private static readonly int _logLevelMariaDb = GetLogLevelMariaDb();
    private static readonly string _logTableMariaDb = GetLogTableMariaDb();
    private static readonly bool _logToLocalDb = GetLogToLokalDb();
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
            autoCreateTable: true,
            useBulkInsert: false,
            connectionString: GetConnectionStringLogging(_logToLocalDb))
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
    
    private static string GetConnectionStringLogging(bool lokalDb = false)
    {
        var settings = GetConfig();
        if (lokalDb)
        {
            var connectionString = settings?.GetConnectionString("traininglokal");
            return connectionString.Replace("XpoProvider=MySql;","");
        }
        else
        {
            var connectionString = settings?.GetConnectionString("training");
            return connectionString.Replace("XpoProvider=MySql;","");
        }
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
    
    public static bool GetLogToLokalDb()
    {
        var settings = GetConfig();
        var logLocal = Convert.ToBoolean(settings["LogToLocalDb"]);
        return logLocal;
    }
}