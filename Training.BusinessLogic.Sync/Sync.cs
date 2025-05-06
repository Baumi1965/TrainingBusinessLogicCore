using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Serilog.Events;
using Training.BusinessLogic.Einstellungen;
using Training.BusinessLogic.Artikel;
using Training.BusinessLogic.Belegungsart;
using Training.BusinessLogic.Benutzer;
using Training.BusinessLogic.Kunden;
using Training.BusinessLogic.ParkplatzERS;
using Training.BusinessLogic.Preis;
using Training.BusinessLogic.Shared;
using Training.BusinessLogic.Spielstaetten;
using Training.BusinessLogic.UOW;
using Training.BusinessLogic.UOW.Models;
using Training.BusinessLogic.ZeitenKalender;

namespace Training.BusinessLogic.Sync
{
    public class Sync
    {
        public int Id { get; set; }
        public int SpielstaetteId { get; set; }
        public string Aktion { get; set; }
        public string TableName { get; set; }
        public string Daten { get; set; }
        public Guid Guid { get; set; }
        public DateTime TsCreated { get; set; }
        public DateTime? TsStarted { get; set; }
        public DateTime? TsFinished { get; set; }
        public short Status { get; set; }
        public string ErrorMessage { get; set; }
        public string Property { get; set; }
        
      
        public static readonly string? AssemblyName = typeof(Sync).GetTypeInfo().Assembly.GetName().Name;
        public static readonly string? AssemblyVersion = typeof(Sync).GetTypeInfo().Assembly.GetName().Version?.ToString();
        
        public static async Task GetNewAsync(int spielstaetteId, Guid guid)
        {
            try
            {
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check for new records to import",string.Empty,"Sync","GetNewAsync",spielstaetteId.ToString(), AssemblyVersion, guid );
                
                if (Uow._uow == null || !Uow._uow.IsConnected)
                {
                    Uow.Connect();
                }

                var result = await Uow._uow.Query<sync>().Where(x => (x.Status == 0 || x.Status == 9) && x.SpielstaetteId == spielstaetteId)
                    .OrderBy(x => x.TSStarted).ToListAsync();
                if (result == null || result.Count == 0)
                {
                    Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check for new records to import",string.Empty,"Sync","GetNewAsync",spielstaetteId.ToString(), AssemblyVersion, guid, $"No records to import" );
                    return;
                }

                foreach (var sync in result)
                {
                    sync.Status = 1;
                    sync.TSStarted = DateTime.Now;
                    await Uow.SaveAsync();

                    var aktion = sync.Aktion;
                    var tableName = sync.TableName;
                    var daten = sync.Daten;
                    var json = JsonConvert.SerializeObject(daten, Formatting.None);
                    var syncResult = new SyncResult();
                    
                    if (tableName.Equals("spielstaetten", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await SpielstaettenLokal.DoSyncAsync(aktion, daten);
                    }
                    else if (tableName.Equals("einstellungen", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await EinstellungenLokal.DoSyncAsync(aktion, daten);
                    } 
                    else if (tableName.Equals("artikel", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await ArtikelLokal.DoSyncAsync(aktion, daten);
                    } 
                    else if (tableName.Equals("belegungsarten", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await BelegungsartLokal.DoSyncAsync(aktion, daten);
                    }                 
                    else if (tableName.Equals("parkplatzeisringberechtigt", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await ParkplatzERSBerechtigtLokal.DoSyncAsync(aktion, daten);
                    }  
                    else if (tableName.Equals("kunden", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await KundenLokal.DoSyncAsync(aktion, daten);
                    }
                    else if (tableName.Equals("preise", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await PreisLokal.DoSyncAsync(aktion, daten);
                    }
                    else if (tableName.Equals("eklbegleitkarte", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await EKLBegleitkarteLokal.DoSyncAsync(aktion, daten);
                    }                
                    else if (tableName.Equals("eklbegleitkarteeintritt", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await EKLBegleitkarteEintrittLokal.DoSyncAsync(aktion, daten);
                    }                 
                    else if (tableName.Equals("eklbegleitkarteparken", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await EKLBegleitkarteParkenLokal.DoSyncAsync(aktion, daten);
                    }  
                    else if (tableName.Equals("zeitenkalender", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await ZeitenKalenderLokal.DoSyncAsync(aktion, daten);
                    }                  
                    else if (tableName.Equals("login", StringComparison.CurrentCultureIgnoreCase))
                    {
                        syncResult = await BenutzerLokal.DoSyncAsync(aktion, daten);
                    }                  
                    
                    if (syncResult.Error)
                    {
                        sync.Status = 9;
                        sync.ErrorMessage = syncResult.ErrorMessage;
                        sync.Property = syncResult.Property;
                    }
                    else
                    {
                        sync.Status = 2;
                    }
                    Logging.Logging.LogMessage(LogEventLevel.Debug, AssemblyName, "Check for new records to import",string.Empty,"Sync","GetNewAsync",spielstaetteId.ToString(), AssemblyVersion, guid, json, $"Result: {sync.Status}",$"Message: {sync.ErrorMessage}",$"Property: {sync.Property}",$"Aktion: {aktion}, Table: {tableName}");
                    sync.TSFinished = DateTime.Now;
                    await Uow.SaveAsync();
                }
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check for new records to import",string.Empty,"Sync","GetNewAsync",spielstaetteId.ToString(), AssemblyVersion, guid, $"Import {result.Count} records" );
            }
            catch (Exception ex)
            {
                Logging.Logging.LogMessage(LogEventLevel.Fatal, AssemblyName, ex.Message,ex.StackTrace,"Sync","GetNewAsync",spielstaetteId.ToString(), AssemblyVersion, guid, "", "", "", "", "", "", ex );
            }
        }

        public static async Task RunForFirstImportAsync(int spielstaetteId, Guid guid)
        {
            try
            {
                if (Uow._uow == null || !Uow._uow.IsConnected)
                {
                    Uow.Connect();
                }
            
                if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
                {
                    Uow.ConnectLokal();
                }

                await CheckEinstellungenForImport(spielstaetteId, guid);
                await CheckArtikelForImport(guid);
                await CheckSpielstaettenForImport(guid);
                await CheckBelegungsartForImport(guid);
                await CheckKundenForImport(guid);
                await CheckEklBegleitkarteForImport(guid);
                await CheckParkplatzERSBerechtigtForImport(guid);
                await CheckLoginForImport(guid);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
        }

        private static async Task CheckEinstellungenForImport(int spielstaetteId, Guid guid)
        {
            try
            {
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Einstellungen for records to import",string.Empty,"Sync","CheckEinstellungenForImport",spielstaetteId.ToString(), AssemblyVersion, guid );
                var count = await EinstellungenLokal.CountAsync();
                if (count == 0)
                {
                    var einst = await Einstellungen.Einstellungen.GetAsync();
                    einst = einst.Where(x => x.Spielstaette == 0 || x.Spielstaette == spielstaetteId).ToList();

                    if (einst.Count != 0)
                    {
                        foreach (var item in einst)
                        {
                            var daten = JsonConvert.SerializeObject(item, Formatting.None);
                            var result = await EinstellungenLokal.DoSyncAsync("I", daten);
                            Logging.Logging.LogMessage(LogEventLevel.Debug, AssemblyName, "Import record in table Einstellungen",string.Empty,"Sync","CheckEinstellungenForImport",spielstaetteId.ToString(), AssemblyVersion, guid, daten, $"Error: {result.Error}",$"Message: {result.ErrorMessage}",$"Property: {result.Property}");
                        }
                    }
                    Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Einstellungen for records to import finished",string.Empty,"Sync","CheckEinstellungenForImport",spielstaetteId.ToString(), AssemblyVersion, guid, $"Import {einst.Count} records" );
                }
            }
            catch (Exception ex)
            {
                Logging.Logging.LogMessage(LogEventLevel.Fatal, AssemblyName, ex.Message,ex.StackTrace,"Sync","CheckEinstellungenForImport",spielstaetteId.ToString(), AssemblyVersion, guid, "", "", "", "", "", "", ex );
            }
        }
        
        private static async Task CheckArtikelForImport(Guid guid)
        {
            try
            {
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Artikel for records to import",string.Empty,"Sync","CheckArtikelForImport","", AssemblyVersion, guid );

                var count = await ArtikelLokal.CountAsync();
                if (count == 0)
                {
                    var art = await Artikel.Artikel.GetAsync();

                    if (art.Count != 0)
                    {
                        foreach (var item in art)
                        {
                            var daten = JsonConvert.SerializeObject(item, Formatting.None);
                            var result = await ArtikelLokal.DoSyncAsync("I", daten);
                            Logging.Logging.LogMessage(LogEventLevel.Debug, AssemblyName, "Import record in table Artikel",string.Empty,"Sync","CheckArtikelForImport","", AssemblyVersion, guid, daten, $"Error: {result.Error}",$"Message: {result.ErrorMessage}",$"Property: {result.Property}" );
                        }
                    }
                    Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Artikel for records to import finished",string.Empty,"Sync","CheckArtikelForImport","", AssemblyVersion, guid, $"Import {art.Count} records" );
                }
            }
            catch (Exception ex)
            {
                Logging.Logging.LogMessage(LogEventLevel.Fatal, AssemblyName, ex.Message,ex.StackTrace,"Sync","CheckArtikelForImport","", AssemblyVersion, guid, "", "", "", "", "", "", ex );
            }
        }
        
        private static async Task CheckSpielstaettenForImport(Guid guid)
        {
            try
            {
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Spielstaetten for records to import",string.Empty,"Sync","CheckSpielstaettenForImport","", AssemblyVersion, guid );
                var count = await SpielstaettenLokal.CountAsync();
                if (count == 0)
                {
                    var sp = await Spielstaetten.Spielstaetten.GetAsync();

                    if (sp.Count != 0)
                    {
                        foreach (var item in sp)
                        {
                            var daten = JsonConvert.SerializeObject(item, Formatting.None);
                            var result = await SpielstaettenLokal.DoSyncAsync("I", daten);
                            Logging.Logging.LogMessage(LogEventLevel.Debug, AssemblyName, "Import record in table Spielstaetten",string.Empty,"Sync","CheckSpielstaettenForImport","", AssemblyVersion, guid, daten, $"Error: {result.Error}",$"Message: {result.ErrorMessage}",$"Property: {result.Property}" );
                        }
                    }
                    Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Spielstaetten for records to import finished",string.Empty,"Sync","CheckSpielstaettenForImport","", AssemblyVersion, guid, $"Import {sp.Count} records" );
                }
            }
            catch (Exception ex)
            {
                Logging.Logging.LogMessage(LogEventLevel.Fatal, AssemblyName, ex.Message,ex.StackTrace,"Sync","CheckSpielstaettenForImport","", AssemblyVersion, guid, "", "", "", "", "", "", ex );
            }
        }    
        
        private static async Task CheckBelegungsartForImport(Guid guid)
        {
            try
            {
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Belegungsart for records to import",string.Empty,"Sync","CheckBelegungsartForImport","", AssemblyVersion, guid );
                var count = await BelegungsartLokal.CountAsync();
                if (count == 0)
                {
                    var bel = await Belegungsarten.GetAsync();

                    if (bel.Count != 0)
                    {
                        foreach (var item in bel)
                        {
                            var daten = JsonConvert.SerializeObject(item, Formatting.None);
                            var result = await BelegungsartLokal.DoSyncAsync("I", daten);
                            Logging.Logging.LogMessage(LogEventLevel.Debug, AssemblyName, "Import record in table Belegungsarten",string.Empty,"Sync","CheckBelegungsartForImport","", AssemblyVersion, guid, daten, $"Error: {result.Error}",$"Message: {result.ErrorMessage}",$"Property: {result.Property}" );
                        }
                    }
                    Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Belegungsart for records to import finished",string.Empty,"Sync","CheckBelegungsartForImport","", AssemblyVersion, guid, $"Import {bel.Count} records");
                }
            }
            catch (Exception ex)
            {
                Logging.Logging.LogMessage(LogEventLevel.Fatal, AssemblyName, ex.Message,ex.StackTrace,"Sync","CheckBelegungsartForImport","", AssemblyVersion, guid, "", "", "", "", "", "", ex );
            }
        }   
        
        private static async Task CheckKundenForImport(Guid guid)
        {
            try
            {
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Kunden for records to import",string.Empty,"Sync","CheckKundenForImport","", AssemblyVersion, guid );
                var count = await KundenLokal.CountAsync();
                if (count == 0)
                {
                    var kd = await Kunden.Kunden.GetAsync();

                    if (kd.Count != 0)
                    {
                        foreach (var item in kd)
                        {
                            var daten = JsonConvert.SerializeObject(item, Formatting.None);
                            var result = await KundenLokal.DoSyncAsync("I", daten);
                            Logging.Logging.LogMessage(LogEventLevel.Debug, AssemblyName, "Import record in table Kunden",string.Empty,"Sync","CheckKundenForImport","", AssemblyVersion, guid, daten, $"Error: {result.Error}",$"Message: {result.ErrorMessage}",$"Property: {result.Property}" );
                        }
                    }
                    Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Kunden for records to import finished",string.Empty,"Sync","CheckKundenForImport","", AssemblyVersion, guid, $"Import {kd.Count} records" );
                }
            }
            catch (Exception ex)
            {
                Logging.Logging.LogMessage(LogEventLevel.Fatal, AssemblyName, ex.Message,ex.StackTrace,"Sync","CheckKundenForImport","", AssemblyVersion, guid, "", "", "", "", "", "", ex );

            }
        } 
        
        private static async Task CheckEklBegleitkarteForImport(Guid guid)
        {
            try
            {
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check EklBegleitkarte for records to import",string.Empty,"Sync","CheckEklBegleitkarteForImport","", AssemblyVersion, guid );
                var count = await EKLBegleitkarteLokal.CountAsync();
                if (count == 0)
                {
                    var ekl = await EKLBegleitkarte.GetAsync();

                    if (ekl.Count != 0)
                    {
                        foreach (var item in ekl)
                        {
                            var daten = JsonConvert.SerializeObject(item, Formatting.None);
                            var result = await EKLBegleitkarteLokal.DoSyncAsync("I", daten);
                            Logging.Logging.LogMessage(LogEventLevel.Debug, AssemblyName, "Import record in EklBegleitkarte Kunden",string.Empty,"Sync","CheckEklBegleitkarteForImport","", AssemblyVersion, guid, daten, $"Error: {result.Error}",$"Message: {result.ErrorMessage}",$"Property: {result.Property}" );
                        }
                    }
                    Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check EklBegleitkarte for records to import finished",string.Empty,"Sync","CheckEklBegleitkarteForImport","", AssemblyVersion, guid, $"Import {ekl.Count} records" );
                }
            }
            catch (Exception ex)
            {
                Logging.Logging.LogMessage(LogEventLevel.Fatal, AssemblyName, ex.Message,ex.StackTrace,"Sync","CheckEklBegleitkarteForImport","", AssemblyVersion, guid, "", "", "", "", "", "", ex );
            }
        }

        private static async Task CheckLoginForImport(Guid guid)
        {
            try
            {
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Login for records to import",string.Empty,"Sync","CheckLoginForImport","", AssemblyVersion, guid );
                var count = await BenutzerLokal.CountAsync();
                if (count == 0)
                {
                    var ben = await Benutzer.Benutzer.GetAsync();
                
                    if (ben.Count != 0)
                    {
                        foreach (var item in ben)
                        {
                            var daten = JsonConvert.SerializeObject(item, Formatting.None);
                            var result = await BenutzerLokal.DoSyncAsync("I", daten);
                            Logging.Logging.LogMessage(LogEventLevel.Debug, AssemblyName, "Import record in table Login",string.Empty,"Sync","CheckLoginForImport","", AssemblyVersion, guid, daten, $"Error: {result.Error}",$"Message: {result.ErrorMessage}",$"Property: {result.Property}" );
                        }
                    }
                    Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check Login for records to import finished",string.Empty,"Sync","CheckPLoginForImport","", AssemblyVersion, guid, $"Import {ben.Count} records");
                }
            }
            catch (Exception ex)
            {
                Logging.Logging.LogMessage(LogEventLevel.Fatal, AssemblyName, ex.Message,ex.StackTrace,"Sync","CheckLoginForImport","", AssemblyVersion, guid, "", "", "", "", "", "", ex );
            }
        }
        
        private static async Task CheckParkplatzERSBerechtigtForImport(Guid guid)
        {
            try
            {
                Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check ParkplatzERSBerechtigt for records to import",string.Empty,"Sync","CheckParkplatzERSBerechtigtForImport","", AssemblyVersion, guid );
                var count = await ParkplatzERSBerechtigtLokal.CountAsync();
                if (count == 0)
                {
                    var pp = await ParkplatzERSBerechtigt.GetAsync();
                
                    if (pp.Count != 0)
                    {
                        foreach (var item in pp)
                        {
                            var daten = JsonConvert.SerializeObject(item, Formatting.None);
                            var result = await ParkplatzERSBerechtigtLokal.DoSyncAsync("I", daten);
                            Logging.Logging.LogMessage(LogEventLevel.Debug, AssemblyName, "Import record in table ParkplatzERSBerechtigt",string.Empty,"Sync","CheckParkplatzERSBerechtigtForImport","", AssemblyVersion, guid, daten, $"Error: {result.Error}",$"Message: {result.ErrorMessage}",$"Property: {result.Property}" );
                        }
                    }
                    Logging.Logging.LogMessage(LogEventLevel.Information, AssemblyName, "Check ParkplatzERSBerechtigt for records to import finished",string.Empty,"Sync","CheckParkplatzERSBerechtigtForImport","", AssemblyVersion, guid, $"Import {pp.Count} records");
                }
            }
            catch (Exception ex)
            {
                Logging.Logging.LogMessage(LogEventLevel.Fatal, AssemblyName, ex.Message,ex.StackTrace,"Sync","CheckParkplatzERSBerechtigtForImport","", AssemblyVersion, guid, "", "", "", "", "", "", ex );
            }
        }
    }    
}
