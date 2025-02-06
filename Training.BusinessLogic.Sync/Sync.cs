using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Training.BusinessLogic.Einstellungen;
using Training.BusinessLogic.Artikel;
using Training.BusinessLogic.Belegungsart;
using Training.BusinessLogic.Kunden;
using Training.BusinessLogic.Kunden.ModelsLokal;
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
        
      
        public static async Task GetNewAsync(int spielstaetteId)
        {
            try
            {
                if (Uow._uow == null || !Uow._uow.IsConnected)
                {
                    Uow.Connect();
                }

                var result = await Uow._uow.Query<sync>().Where(x => (x.Status == 0 || x.Status == 9) && x.SpielstaetteId == spielstaetteId)
                    .OrderBy(x => x.TSStarted).ToListAsync();
                if (result == null || result.Count == 0)
                {
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
                    Console.WriteLine($"Result: {sync.Status}, Message: {sync.ErrorMessage}, Property: {sync.Property}");

                    sync.TSFinished = DateTime.Now;
                    await Uow.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public static async Task RunForFirstImportAsync(int spielstaetteId)
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

                await CheckEinstellungenForFirstImport(2);
                await CheckArtikelForFirstImport();
                await CheckSpielstaettenForFirstImport();
                await CheckBelegungsartForFirstImport();
                await CheckKundenForFirstImport();
                await GetCheckEklBegleitkarteForFirstImport();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private static async Task CheckEinstellungenForFirstImport(int spielstaetteId)
        {
            var count = await EinstellungenLokal.CountAsync();
            if (count == 0)
            {
                var einst = await Einstellungen.Einstellungen.GetAsync();
                einst = einst.Where(x => x.Spielstaette == 0 || x.Spielstaette == spielstaetteId).ToList();

                if (einst.Any())
                {
                    foreach (var item in einst)
                    {
                        var daten = JsonConvert.SerializeObject(item, Formatting.None);
                        var result = await EinstellungenLokal.DoSyncAsync("I", daten);
                        Console.WriteLine($"Table Einstellungen, Error: {result.Error}, Message: {result.ErrorMessage}, Property: {result.Property}");
                    }
                }
            }
        }
        
        private static async Task CheckArtikelForFirstImport()
        {
            var count = await ArtikelLokal.CountAsync();
            if (count == 0)
            {
                var art = await Artikel.Artikel.GetAsync();

                if (art.Any())
                {
                    foreach (var item in art)
                    {
                        var daten = JsonConvert.SerializeObject(item, Formatting.None);
                        var result = await ArtikelLokal.DoSyncAsync("I", daten);
                        Console.WriteLine($"Table: Artikel, Error: {result.Error}, Message: {result.ErrorMessage}, Property: {result.Property}");
                    }
                }
            }
        }
        
        private static async Task CheckSpielstaettenForFirstImport()
        {
            var count = await SpielstaettenLokal.CountAsync();
            if (count == 0)
            {
                var sp = await Spielstaetten.Spielstaetten.GetAsync();

                if (sp.Any())
                {
                    foreach (var item in sp)
                    {
                        var daten = JsonConvert.SerializeObject(item, Formatting.None);
                        var result = await SpielstaettenLokal.DoSyncAsync("I", daten);
                        Console.WriteLine($"Table: Spielstaetten, Error: {result.Error}, Message: {result.ErrorMessage}, Property: {result.Property}");
                    }
                }
            }
        }    
        
        private static async Task CheckBelegungsartForFirstImport()
        {
            var count = await BelegungsartLokal.CountAsync();
            if (count == 0)
            {
               var bel = await Belegungsart.Belegungsarten.GetAsync();

               if (bel.Any())
               {
                   foreach (var item in bel)
                   {
                       var daten = JsonConvert.SerializeObject(item, Formatting.None);
                       var result = await BelegungsartLokal.DoSyncAsync("I", daten);
                       Console.WriteLine($"Table: Belegungsarten, Error: {result.Error}, Message: {result.ErrorMessage}, Property: {result.Property}");
                   }
               }
            }
        }   
        
        private static async Task CheckKundenForFirstImport()
        {
            var count = await KundenLokal.CountAsync();
            if (count == 0)
            {
                var kd = await Kunden.Kunden.GetAsync();

                if (kd.Any())
                {
                    foreach (var item in kd)
                    {
                        var daten = JsonConvert.SerializeObject(item, Formatting.None);
                        var result = await KundenLokal.DoSyncAsync("I", daten);
                        Console.WriteLine($"Table: Kunden, Error: {result.Error}, Message: {result.ErrorMessage}, Property: {result.Property}");
                    }
                }
            }
        } 
        
        private static async Task GetCheckEklBegleitkarteForFirstImport()
        {
            var count = await EKLBegleitkarteLokal.CountAsync();
            if (count == 0)
            {
                var ekl = await EKLBegleitkarte.GetAsync();

                if (ekl.Any())
                {
                    foreach (var item in ekl)
                    {
                        var daten = JsonConvert.SerializeObject(item, Formatting.None);
                        var result = await EKLBegleitkarteLokal.DoSyncAsync("I", daten);
                        Console.WriteLine($"Table: EKLBegleitkarte, Error: {result.Error}, Message: {result.ErrorMessage}, Property: {result.Property}");
                    }
                }
            }
        }
    }    
}
