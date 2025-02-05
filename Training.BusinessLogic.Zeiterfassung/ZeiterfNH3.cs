using DevExpress.DirectX.NativeInterop.CCW;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfNH3
	{
		public int ID { get; set; }
		public string MitarbeiterId { get; set; }
		public DateTime? Datum { get; set; }
		public int? SP { get; set; }
		public DateTime? DBTSKOMMT { get; set; }
		public DateTime? DBTSGEHT { get; set; }
		public bool? NH3 { get; set; }
		public string NH3Zeit { get; set; }
		public int? NH3Prozent { get; set; }
		public string NH3Kommt { get; set; }
		public string NH3Geht { get; set; }
		public string EdituserID { get; set; }
		public DateTime? TSEdit { get; set; }
		public string Bemerkung { get; set; }


		public async static Task<List<ZeiterfNH3>> GetByMitarbeiterAndDate(string mitarbeiterId, DateTime start, DateTime end)
		{
			try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

				var nh3 = await UOW.Uow._uow.Query<zeiterfnh3>()
					.Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date >= start.Date && x.Datum.Value.Date <= end.Date)
					.OrderBy(x => x.Datum)
					.ToListAsync();

				List<ZeiterfNH3> lstNH3 = new List<ZeiterfNH3>();
				foreach (var item in nh3)
				{
					lstNH3.Add(new ZeiterfNH3
					{
						ID = item.ID,
						Datum = item.Datum,
						Bemerkung = item.Bemerkung,
						DBTSGEHT = item.DBTSGEHT,
						DBTSKOMMT = item.DBTSKOMMT,
						EdituserID = item.EdituserID,
						MitarbeiterId = item.MitarbeiterId,
						NH3 = item.NH3,
						NH3Geht = item.NH3Geht,
						NH3Kommt = item.NH3Kommt,
						NH3Prozent = item.NH3Prozent,
						NH3Zeit = item.NH3Zeit,
						SP = item.SP,
						TSEdit = item.TSEdit,
					});
				}

				return lstNH3;
			}
			catch (Exception)
			{
				throw;
			}
		}

        public static List<clsZeiterfassungUebersicht> GetByMitarbeiterAndDate(string mitarbeiterId, DateTime datum)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var nh3 = UOW.Uow._uow.Query<zeiterfnh3>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date == datum.Date)
                    .OrderBy(x => x.ID)
                    .ToList();

                List<clsZeiterfassungUebersicht> lstNH3 = new List<clsZeiterfassungUebersicht>();
                foreach (var item in nh3)
                {
                    lstNH3.Add(new clsZeiterfassungUebersicht
                    {
                        ID = item.ID,
                        Datum = item.Datum.Value,
                        NH3Geht = item.NH3Geht,
                        NH3Kommt = item.NH3Kommt,
                        Spielstaette = item.SP.ToString(),
                    });
                }

                return lstNH3;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool CheckNH3Ende(string mitarbeiterId, DateTime date)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var nh3 = UOW.Uow._uow
                    .Query<zeiterfnh3>()
                    .Where(
                        x => x.MitarbeiterId == mitarbeiterId &&
                            x.Datum.Value.Date == date.Date &&
                            !string.IsNullOrEmpty(x.NH3Kommt) &&
                            string.IsNullOrEmpty(x.NH3Geht))
                    .Count();

				if (nh3 == 0)
				{
                    return true;
				}
				else
				{
                    return false;
				}
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> CheckNH3EndeAsync(string mitarbeiterId, DateTime date)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var nh3 = await UOW.Uow._uow
                    .Query<zeiterfnh3>()
                    .Where(
                        x => x.MitarbeiterId == mitarbeiterId &&
                            x.Datum.Value.Date == date.Date &&
                            !string.IsNullOrEmpty(x.NH3Kommt) &&
                            string.IsNullOrEmpty(x.NH3Geht))
                    .CountAsync();

                if (nh3 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task UpdateAsync(int id, string beginn, string ende, int spielstaette, string user,
            string bemerkung)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var nh3 = await UOW.Uow._uow
                    .Query<zeiterfnh3>()
                    .Where(
                        x => x.ID == id)
                    .FirstOrDefaultAsync();

				if (nh3 == null)
				{
                    return;
				}

                nh3.NH3Geht = ende;
                nh3.NH3Kommt = beginn;
                nh3.SP = spielstaette;
                nh3.EdituserID = user;
                nh3.TSEdit = DateTime.Now;
                nh3.Bemerkung = bemerkung;
                nh3.NH3 = true;

                await UOW.Uow.SaveAsync(); 

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task DeleteAsync(int id)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var nh3 = await UOW.Uow._uow
                    .Query<zeiterfnh3>()
                    .Where(
                        x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (nh3 == null)
                {
                    return;
                }

                await UOW.Uow._uow.DeleteAsync(nh3);
                await UOW.Uow.SaveAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
        }


    }

    public class clsZeitErfNH3
    {
        public DateTime Datum { get; set; }
        public string NH3Start { get; set; }
        public string NH3Ende { get; set; }
        public string NH3Zeit { get; set; }
    }
}
