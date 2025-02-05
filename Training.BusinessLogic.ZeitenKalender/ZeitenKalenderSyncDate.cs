using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.ZeitenKalender;

public class ZeitenKalenderSyncDate
{
    public int Id { get; set; }
    public int SpielstaetteId { get; set; }
    public DateTime SyncTimestamp { get; set; }
    
     public static async Task<DateTime> GetAsync(int spielstaetteId)
        {   
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var zk = await UOW.Uow._uow.Query<zeitenkalender_syncdate>()
                    .Where(x => x.SpielstaetteId == spielstaetteId).FirstOrDefaultAsync();

                if (zk == null)
                {
                    return DateTime.Now;
                }
                
                return zk.SyncTimestamp.Date.AddDays(1);
            }
            catch (Exception)
            {
                throw;
            }
        }

    
}