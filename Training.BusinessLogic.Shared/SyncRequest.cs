using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Shared
{
    public class SyncRequest
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

        public static async Task AddAsync(List<int> spielstaetten, string aktion, string tableName, string guid, string daten, bool saveImmediatly = false)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                foreach (var location in spielstaetten)
                {
                    sync sync = new sync(UOW.Uow._uow)
                    {
                        SpielstaetteId = location,
                        Aktion = aktion,
                        TableName = tableName,
                        Guid = guid,
                        Daten = daten,
                        TSCreated = DateTime.Now,
                        Status = 0
                    };
                }

                if (saveImmediatly)
                {
                    await UOW.Uow.SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}