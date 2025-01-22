using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
	public class ZeiterfFrei
	{
		public int ID { get; set; }
		public string MitarbeiterId { get; set; }
		public DateTime? Datum { get; set; }
		public string Bemerkung { get; set; }

		public async static Task<List<ZeiterfFrei>> GetByMitarbeiterAndDate(string mitarbeiterId, DateTime start, DateTime end)
		{
			try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

				var frei = await UOW.UOW.uow.Query<zeiterffrei>()
					.Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date >= start.Date && x.Datum.Value.Date <= end.Date)
					.OrderBy(x => x.Datum)
					.ToListAsync();

				List<ZeiterfFrei> lst = new List<ZeiterfFrei>();
				foreach (var item in frei)
				{
					lst.Add(new ZeiterfFrei
					{
						ID = item.ID,
						Datum = item.Datum,
						MitarbeiterId = item.MitarbeiterId,
						Bemerkung = item.Bemerkung,
					});
				}

				return lst;
			}
			catch (Exception)
			{
				throw;
			}
		}

        public async static Task InsertOrUpdateAsync(string mitarbeiterId, DateTime date, string bemerkung)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var frei = await UOW.UOW.uow.Query<zeiterffrei>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date == date.Date)
                    .FirstOrDefaultAsync();

                if (frei == null)
                {
                    zeiterffrei zeitfrei = new zeiterffrei(UOW.UOW.uow)
                    {
                        MitarbeiterId = mitarbeiterId,
                        Datum = date,
                        Bemerkung = bemerkung,
                    };
                }
                else
                {
                    frei.Bemerkung = bemerkung;
                }

                await UOW.UOW.SaveAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class ZeitFrei
    {
        public DateTime Datum
        {
            get;
            set;
        }

        public String Bemerkung
        {
            get;
            set;
        }

        public ZeitFrei(DateTime dtDatum, String stBemerkung)
        {
            Datum = dtDatum;
            Bemerkung = stBemerkung;
        }
    }
}
