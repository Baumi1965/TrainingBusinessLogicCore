using DevExpress.Xpo;
using Training.BusinessLogic.Einstellungen;
using Training.BusinessLogic.Artikel;
using Training.BusinessLogic.Belegungsart;
using Training.BusinessLogic.Kunden;
using Training.BusinessLogic.ParkplatzERS;
using Training.BusinessLogic.Shared;
using Training.BusinessLogic.Spielstaetten;
using Training.BusinessLogic.UOW;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Sync;

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
    
}