using System;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;
using System.Linq;
using System.Threading;
using Training.BusinessLogic.UOW;

namespace Training.BusinessLogic.ZeitenKalender
{
    public class ZeitenKalenderSyncDate
    {
        public int Id { get; set; }
        public int SpielstaetteId { get; set; }
        public DateTime SyncTimestamp { get; set; }
    
        public static async Task<ZeitenKalenderSyncDate> GetAsync(int spielstaetteId, CancellationToken cancellationToken = default )
        {   
            try
            {
                if (Uow._uow == null || !Uow._uow.IsConnected)
                {
                    Uow.Connect();
                }
                
                Uow._uow.DropIdentityMap();
                
                var zk = await Uow._uow.Query<zeitenkalender_syncdate>()
                    .Where(x => x.SpielstaetteId == spielstaetteId)
                    .OrderByDescending(x => x.SyncTimestamp)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (zk == null)
                {
                    return null;
                }

                return new ZeitenKalenderSyncDate()
                {
                    Id = zk.ID,
                    SyncTimestamp = zk.SyncTimestamp,
                    SpielstaetteId = spielstaetteId,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddOrUpdateAsync(int spielstaetteId, CancellationToken cancellationToken = default)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }


                var lastsync = await Uow._uow.Query<zeitenkalender_syncdate>()
                    .Where(x => x.SpielstaetteId == spielstaetteId)
                    .OrderByDescending(x => x.SyncTimestamp)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (lastsync != null)
                {
                    lastsync.SyncTimestamp = DateTime.Now;
                }
                else
                {
                    var sync = new zeitenkalender_syncdate(Uow._uow)
                    {
                        SyncTimestamp = DateTime.Now,
                        SpielstaetteId = spielstaetteId,
                    };
                }
                await Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }            
        }
        
        public static async Task AddAsync(int spielstaetteId, CancellationToken cancellationToken = default )
        {   
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
                
                
                var lastsync = new zeitenkalender_syncdate(Uow._uow)
                {
                    SyncTimestamp = DateTime.Now,
                    SpielstaetteId = spielstaetteId,
                };

                await Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static async Task UpdateAsync(int spielstaetteId, CancellationToken cancellationToken = default )
        {   
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }


                var lastsync = await Uow._uow.Query<zeitenkalender_syncdate>()
                    .Where(x => x.SpielstaetteId == spielstaetteId)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (lastsync != null)
                {
                    lastsync.SyncTimestamp = DateTime.Now;
                }
                
                await Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }    
}
