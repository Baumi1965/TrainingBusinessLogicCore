using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfUrlaub
	{
		public int ID { get; set; }
		public string MitarbeiterId { get; set; }
		public DateTime? Datum { get; set; }

		public async static Task<List<ZeiterfUrlaub>> GetByMitarbeiterAndDate(string mitarbeiterId, DateTime start, DateTime end)
		{
			try
			{
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

				var urlaub = await UOW.UOW.uow.Query<zeiterfurlaub>()
					.Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date >= start.Date && x.Datum.Value.Date <= end.Date)
					.OrderBy(x => x.Datum)
					.ToListAsync();

				List<ZeiterfUrlaub> lst = new List<ZeiterfUrlaub>();
				foreach (var item in urlaub)
				{
					lst.Add(new ZeiterfUrlaub
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

                var urlaub = await UOW.UOW.uow
                    .Query<zeiterfurlaub>()
                    .Where(
                        x => x.MitarbeiterId == mitarbeiterId &&
                            x.Datum.Value.Date >= start.Date &&
                            x.Datum.Value.Date <= end.Date)
                    .CountAsync();

                return urlaub;
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
                var urlaub = await UOW.UOW.uow
                    .Query<zeiterfurlaub>()
                    .Where(
                        x => x.MitarbeiterId == mitarbeiterId &&
                            x.Datum.Value.Date == datum.Date)
                    .ToListAsync();

                if (urlaub == null)
                {
                    return;
                }

                await UOW.UOW.uow.DeleteAsync(urlaub);
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

                var cnt = await UOW.UOW.uow.Query<zeiterfurlaub>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date == date).CountAsync();

                if (cnt == 0)
                {
                    zeiterfurlaub urlaub = new zeiterfurlaub(UOW.UOW.uow);
                    urlaub.MitarbeiterId = mitarbeiterId;
                    urlaub.Datum = date;
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
