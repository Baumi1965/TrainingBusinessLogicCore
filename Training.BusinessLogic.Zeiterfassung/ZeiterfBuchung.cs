using DevExpress.Internal;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
	public class ZeiterfBuchung
	{
		public int ID { get; set; }
		public string MitarbeiterId { get; set; }
		public DateTime? Datum { get; set; }
		public string ZeitKommt { get; set; }
		public string ZeitGeht { get; set; }
		public string Kennung { get; set; }
		public string Grund { get; set; }
		public string KommtBuchung { get; set; }
		public string GehtBuchung { get; set; }
		public string EdituserID { get; set; }
		public string Bemerkung { get; set; }
		public string Taetigkeitsbereich { get; set; }
		public string NH3Zeit { get; set; }
		public string NH3Kommt { get; set; }
		public string NH3Geht { get; set; }
		public int? Status { get; set; }
		public int? ArbeitszeitID { get; set; }
		public int? SP { get; set; }
		public int? NH3Prozent { get; set; }
		public bool? Edit { get; set; }
		public bool? Abschluss { get; set; }
		public bool? NH3 { get; set; }
		public DateTime? TSEdit { get; set; }
		public DateTime? TSGeaendert { get; set; }
		public DateTime? DBTSKOMMT { get; set; }
		public DateTime? DBTSGEHT { get; set; }

		public async static Task<List<ZeiterfBuchung>> GetByMitarbeiterAndDate(string mitarbeiterId, DateTime start, DateTime end)
		{
			try
			{
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
				var buchung = await UOW.Uow._uow.Query<zeiterfbuchung>()
					.Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date >= start.Date && x.Datum.Value.Date <= end.Date)
					.OrderBy(x => x.Datum).ThenBy(x => x.ID)
					.ToListAsync();

				List<ZeiterfBuchung> lst = new List<ZeiterfBuchung>();
				foreach (var item in buchung)
				{
					lst.Add(new ZeiterfBuchung
					{
						ID = item.ID,
						Datum = item.Datum,
						MitarbeiterId = item.MitarbeiterId,
						Abschluss = item.Abschluss,
						ArbeitszeitID = item.ArbeitszeitID,
						Bemerkung = item.Bemerkung,
						DBTSGEHT = item.DBTSGEHT,
						DBTSKOMMT = item.DBTSKOMMT,
						Edit = item.Edit,
						EdituserID = item.EdituserID,
						GehtBuchung = item.GehtBuchung,
						Grund = item.Grund,
						Kennung = item.Kennung,
						KommtBuchung = item.KommtBuchung,
						NH3 = item.NH3,
						NH3Geht = item.NH3Geht,
						NH3Kommt = item.NH3Kommt,
						NH3Prozent = item.NH3Prozent,
						NH3Zeit = item.NH3Zeit,
						SP = item.SP,
						Status = item.Status,
						Taetigkeitsbereich = item.Taetigkeitsbereich,
						TSEdit = item.TSEdit,
						TSGeaendert = item.TSGeaendert,
						ZeitGeht = item.ZeitGeht,
						ZeitKommt = item.ZeitKommt,
					});
				}

				return lst;
			}
			catch (Exception)
			{
				throw;
			}
		}

        public async static Task<bool> CheckByMitarbeiterAndDateAsync(string mitarbeiterId, DateTime start, DateTime end)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
                var buchung = await UOW.Uow._uow.Query<zeiterfbuchung>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date >= start.Date && x.Datum.Value.Date <= end.Date)
                    .CountAsync();
                 
                if (buchung > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async static Task<bool> CheckIfBookingsAreCorrect(string mitarbeiterId, DateTime start, DateTime end)
		{
			try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

				var cnt = await UOW.Uow._uow.Query<zeiterfbuchung>()
					.Where(x => x.MitarbeiterId == mitarbeiterId 
						&& x.Datum.Value.Date >= start.Date 
						&& x.Datum.Value.Date <= end.Date
						&& ((!string.IsNullOrEmpty(x.ZeitKommt) && string.IsNullOrEmpty(x.ZeitGeht))
						|| (string.IsNullOrEmpty(x.ZeitKommt) && !string.IsNullOrEmpty(x.ZeitGeht)))).CountAsync();

				if (cnt > 0)
				{
					return false;
				}
				else
				{
					return true;
				}
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

                var result = await UOW.Uow._uow
                    .Query<zeiterfbuchung>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    await UOW.Uow._uow.DeleteAsync(result);
                    await UOW.Uow.SaveAsync(); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task InsertAsync(string mitarbeiterId, DateTime datum, string zeitGeht, string zeitKommt, 
            int spielstaette, string grund, string kennung, int status, bool edit, string buchungGeht, string buchungKommt, 
            string user, string bemerkung, string taetigkeitsbereich)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                zeiterfbuchung buchung = new zeiterfbuchung(UOW.Uow._uow)
                {
                    MitarbeiterId = mitarbeiterId,
                    Datum = datum,
                    ZeitKommt = zeitKommt,
                    ZeitGeht = zeitGeht,
                    Grund = grund,
                    Kennung = kennung,
                    Status = status,
                    SP = spielstaette,
                    Edit = edit,
                    KommtBuchung = buchungKommt,
                    GehtBuchung = buchungGeht,
                    EdituserID = user,
                    TSEdit = DateTime.Now,
                    Bemerkung = bemerkung,
                    Taetigkeitsbereich = taetigkeitsbereich,
                };

                await UOW.Uow.SaveAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task UpdateAsync(int id, string zeitGeht, string zeitKommt, int spielstaette, string grund, 
			string buchungGeht, string buchungKommt, string user, string bemerkung, string taetigkeitsbereich)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow
                    .Query<zeiterfbuchung>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
					result.ZeitGeht = zeitGeht;
                    result.ZeitKommt = zeitKommt;
					result.SP = spielstaette;
                    result.Grund = grund;
                    result.Kennung = "";
                    result.Edit = true;
                    result.GehtBuchung = buchungGeht;
                    result.KommtBuchung = buchungKommt;
                    result.EdituserID = user;
                    result.TSEdit = DateTime.Now;
                    result.Bemerkung = bemerkung;
                    result.Taetigkeitsbereich = taetigkeitsbereich;

                    await UOW.Uow.SaveAsync(); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task UpdateKommtAsync(int id, string zeitKommt, int spielstaette, string grund,
            string buchungKommt, string user, string bemerkung, string taetigkeitsbereich)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow
                    .Query<zeiterfbuchung>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    result.ZeitKommt = zeitKommt;
                    result.SP = spielstaette;
                    result.Grund = grund;
                    result.Kennung = "";
                    result.Edit = true;
                    result.KommtBuchung = buchungKommt;
                    result.EdituserID = user;
                    result.TSEdit = DateTime.Now;
                    result.Bemerkung = bemerkung;
                    result.Taetigkeitsbereich = taetigkeitsbereich;

                    await UOW.Uow.SaveAsync(); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task UpdateGehtAsync(int id, string zeitGeht, int spielstaette, string grund,
            string buchungGeht, string user, string bemerkung, string taetigkeitsbereich)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow
                    .Query<zeiterfbuchung>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    result.ZeitGeht = zeitGeht;
                    result.SP = spielstaette;
                    result.Grund = grund;
                    result.Kennung = "";
                    result.Edit = true;
                    result.GehtBuchung = buchungGeht;
                    result.EdituserID = user;
                    result.TSEdit = DateTime.Now;
                    result.Bemerkung = bemerkung;
                    result.Taetigkeitsbereich = taetigkeitsbereich;

                    await UOW.Uow.SaveAsync(); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task UpdateNH3Async(string mitarbeiterId, DateTime datum, string zeitNH3)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow
                    .Query<zeiterfbuchung>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date == datum)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    result.NH3 = true;
                    result.NH3Zeit = zeitNH3;

                    await UOW.Uow.SaveAsync(); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<List<ZeiterfBuchung>> GetByMitarbeiterAndDateAsync(string mitarbeiterId, DateTime datum)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var buchung = await UOW.Uow._uow.Query<zeiterfbuchung>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date == datum.Date)
                    .OrderBy(x => x.ID)
                    .ToListAsync();

                if (buchung == null)
                {
                    return null;
                }

                List<ZeiterfBuchung> lstBuchung = new List<ZeiterfBuchung>();
                foreach (var item in buchung)
                {
                    lstBuchung.Add(new ZeiterfBuchung
                    {
                        ID = item.ID,
                        Datum = item.Datum.Value,
                        NH3Geht = item.NH3Geht,
                        NH3Kommt = item.NH3Kommt,
                        Abschluss = item.Abschluss,
                        ArbeitszeitID = item.ArbeitszeitID,
                        Bemerkung = item.Bemerkung,
                        DBTSGEHT = item.DBTSGEHT,
                        DBTSKOMMT = item.DBTSKOMMT,
                        Edit = item.Edit,
                        EdituserID = item.EdituserID,
                        GehtBuchung = item.GehtBuchung,
                        Grund = item.Grund,
                        Kennung = item.Kennung,
                        KommtBuchung = item.KommtBuchung,
                        MitarbeiterId = item.MitarbeiterId,
                        NH3 = item.NH3,
                        NH3Prozent = item.NH3Prozent,
                        NH3Zeit = item.NH3Zeit,
                        SP = item.SP,
                        Status = item.Status,
                        Taetigkeitsbereich = item.Taetigkeitsbereich,
                        TSEdit = item.TSEdit,
                        TSGeaendert = item.TSGeaendert,
                        ZeitGeht = item.ZeitGeht,
                        ZeitKommt = item.ZeitKommt,
                    });
                }

                return lstBuchung;
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

                var buchung = UOW.Uow._uow.Query<zeiterfbuchung>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date == datum.Date)
                    .OrderBy(x => x.Datum).ThenBy(x => x.ID)
                    .ToList();

                List<clsZeiterfassungUebersicht> lstBuchung = new List<clsZeiterfassungUebersicht>();
                foreach (var item in buchung)
                {
                    lstBuchung.Add(new clsZeiterfassungUebersicht
                    {
                        ID = item.ID,
                        Datum = item.Datum.Value,
                        Geht = item.ZeitGeht,
                        Kommt = item.ZeitKommt,
                        Spielstaette = item.SP.ToString(),
                    });
                }

                return lstBuchung;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
