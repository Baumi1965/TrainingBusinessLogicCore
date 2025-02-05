using Training.BusinessLogic.Shared;
using Training.BusinessLogic.UOW;

namespace Training.BusinessLogic.Lokal.Spielstaetten;

public class Spielstaetten
{
    public int ID { get; set; }
    public string Bezeichnung { get; set; }
    public string Adresse1 { get; set; }
    public string Adresse2 { get; set; }
    public string PLZ { get; set; }
    public string Ort { get; set; }
    public string Color { get; set; }
    public int? BtsTicketId { get; set; }
    public Guid Guid { get; set; }

    public static async Task<SyncResult> DoSyncAsync(string aktion, string daten)
    {
        var result = new SyncResult();
        try
        {
            if (Uow._uowLokal == null || !Uow._uowLokal.IsConnected)
            {
                Uow.ConnectLokal();
            }



            await Uow.SaveLokalAsync();
            return result;
        }
        catch (Exception ex)
        {
            result.ErrorMessage = ex.Message;
            result.Error = true;
            return result;
        }
    }
}