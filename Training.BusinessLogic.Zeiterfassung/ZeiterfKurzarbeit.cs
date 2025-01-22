using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
	public class ZeiterfKurzarbeit
	{
		public int ID { get; set; }
		public string MitarbeiterId { get; set; }
		public DateTime? Datum { get; set; }
		public string Soll { get; set; }

		public async static Task<List<ZeiterfKurzarbeit>> GetByMitarbeiterAndDate(string mitarbeiterId, DateTime start, DateTime end)
		{
			try
			{
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
				var kurzarbeit = await UOW.UOW.uow.Query<zeiterfkurzarbeit>()
					.Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date >= start.Date && x.Datum.Value.Date <= end.Date)
					.OrderBy(x => x.Datum)
					.ToListAsync();

				List<ZeiterfKurzarbeit> lst = new List<ZeiterfKurzarbeit>();
				foreach (var item in kurzarbeit)
				{
					lst.Add(new ZeiterfKurzarbeit
					{
						ID = item.ID,
						Datum = item.Datum,
						MitarbeiterId = item.MitarbeiterId,
						Soll = item.Soll,
					});
				}

				return lst;
			}
			catch (Exception)
			{
				throw;
			}
		}

        public async static Task InsertOrUpdateAsync(string mitarbeiterId, DateTime date, string soll)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var ka = await UOW.UOW.uow.Query<zeiterfkurzarbeit>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date == date.Date)
                    .FirstOrDefaultAsync();

                if (ka == null)
                {
                    zeiterfkurzarbeit zeitka = new zeiterfkurzarbeit(UOW.UOW.uow)
                    {
                        MitarbeiterId = mitarbeiterId,
                        Datum = date,
                        Soll = soll,
                    };
                }
                else
                {
                    ka.Soll = soll;
                }

                await UOW.UOW.SaveAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class ZeitKurzarbeit
    {
        public DateTime Datum
        {
            get;
            set;
        }

        public string Soll
        {
            get;
            set;
        }

        public ZeitKurzarbeit(DateTime dtDatum, String stSoll)
        {
            Datum = dtDatum;
            Soll = stSoll;
        }
    }
}
