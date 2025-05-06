using System.Reflection;
using DevExpress.Xpo;
using Serilog.Events;
using Training.BusinessLogic.UOW;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Sync;

public class SyncStatus
{
    public int Id { get; set; }
    public int Location { get; set; }
    public bool FirstImport { get; set; }
    public DateTime TS_FirstImport { get; set; }
    public DateTime? TS_Import { get; set; }

    public static readonly string? AssemblyName = typeof(Sync).GetTypeInfo().Assembly.GetName().Name;
    public static readonly string? AssemblyVersion = typeof(Sync).GetTypeInfo().Assembly.GetName().Version?.ToString();
    
    public static async Task<bool> CheckForFirstImportDoneAsync(int location, Guid guid)
    {
        try
        {
            Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check if first import is done",
                string.Empty, "SyncStatus", "CheckForFirstImportDoneAsync", location.ToString(), AssemblyVersion, guid);

            if (Uow._uow == null || !Uow._uow.IsConnected)
            {
                Uow.Connect();
            }

            var result = await Uow._uow.Query<sync_status>()
                .Where(x => x.Location == location).FirstOrDefaultAsync();

            if (result == null)
            {
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check if first import is done",
                    string.Empty, "SyncStatus", "CheckForFirstIMportDoneAsync", location.ToString(), AssemblyVersion, guid,
                    $"First import not done yet");
                return false;
            }

            if (result.FirstImport)
            {
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check if first import is done",
                    string.Empty, "SyncStatus", "CheckForFirstIMportDoneAsync", location.ToString(), AssemblyVersion, guid,
                    $"First import done at {result.TS_FirstImport}");
                return true;                
            }

            Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check if first import is done",
                string.Empty, "SyncStatus", "CheckForFirstIMportDoneAsync", location.ToString(), AssemblyVersion, guid,
                $"First import not done yet");
            return false;
        }
        catch (Exception ex)
        {
            Logging.Logging.LogMessage(LogEventLevel.Fatal, AssemblyName, ex.Message, ex.StackTrace, "SyncStatus",
                "CheckForFirstIMportDoneAsync", location.ToString(), AssemblyVersion, guid, "", "", "", "", "", "", ex);
            return false;
        }
    }

    
    public static async Task AddOrUpdateImportDoneAsync(int location, Guid guid)
    {
        try
        {
            Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Add or update import timestamp",
                string.Empty, "SyncStatus", "AddOrUpdateImportDoneAsync", location.ToString(), AssemblyVersion, guid);

            if (Uow._uow == null || !Uow._uow.IsConnected)
            {
                Uow.Connect();
            }

            var result = await Uow._uow.Query<sync_status>()
                .Where(x => x.Location == location).FirstOrDefaultAsync();

            if (result == null)
            {
                var syncstatus = new sync_status(Uow._uow);
                syncstatus.FirstImport = true;
                syncstatus.TS_FirstImport = DateTime.Now;
                syncstatus.TS_Import = DateTime.Now;
                syncstatus.Location = location;
            }
            else
            {
                result.TS_Import = DateTime.Now;
            }

            await Uow.SaveAsync();
            Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Add or update import timestamp finished",
                string.Empty, "SyncStatus", "AddOrUpdateImportDoneAsync", location.ToString(), AssemblyVersion, guid);
        }
        catch (Exception ex)
        {
            Logging.Logging.LogMessage(LogEventLevel.Fatal, AssemblyName, ex.Message, ex.StackTrace, "SyncStatus",
                "AddOrUpdateImportDoneAsync", location.ToString(), AssemblyVersion, guid, "", "", "", "", "", "", ex);        
        }
    }
}