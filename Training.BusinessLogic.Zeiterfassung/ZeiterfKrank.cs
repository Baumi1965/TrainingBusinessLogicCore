using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
	public class ZeiterfKrank
	{
		public int ID { get; set; }
		public string MitarbeiterId { get; set; }
		public DateTime? Datum { get; set; }

		public async static Task<List<ZeiterfKrank>> GetByMitarbeiterAndDate(string mitarbeiterId, DateTime start, DateTime end)
		{
			try
			{
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
				var krank = await UOW.UOW.uow.Query<zeiterfkrank>()
					.Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date >= start.Date && x.Datum.Value.Date <= end.Date)
					.OrderBy(x => x.Datum)
					.ToListAsync();

				List<ZeiterfKrank> lst = new List<ZeiterfKrank>();
				foreach (var item in krank)
				{
					lst.Add(new ZeiterfKrank
					{
						ID = item.ID,
						Datum = item.Datum,
						MitarbeiterId = item.MitarbeiterId,
					});
				}

				return lst;
			}
			catch (Exception)
			{
				throw;
			}
		}

        public async static Task<int> CountByMitarbeiterAndDateAsync(string mitarbeiterId, DateTime start, DateTime end)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
                var krank = await UOW.UOW.uow.Query<zeiterfkrank>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date >= start.Date && x.Datum.Value.Date <= end.Date)
                    .CountAsync();

                return krank;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task DeleteAsync(string mitarbeiterId, DateTime datum)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
                var krank = await UOW.UOW.uow
                    .Query<zeiterfkrank>()
                    .Where(
                        x => x.MitarbeiterId == mitarbeiterId &&
                            x.Datum.Value.Date == datum.Date)
                    .ToListAsync();

                if (krank == null)
                {
                    return;
                }

                await UOW.UOW.uow.DeleteAsync(krank);
                await UOW.UOW.SaveAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task InsertAsync(string mitarbeiterId, DateTime date)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var cnt = await UOW.UOW.uow.Query<zeiterfkrank>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date == date).CountAsync();

                if (cnt == 0)
                {
                    zeiterfkrank krank = new zeiterfkrank(UOW.UOW.uow);
                    krank.MitarbeiterId = mitarbeiterId;
                    krank.Datum = date;
                }
                await UOW.UOW.SaveAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
