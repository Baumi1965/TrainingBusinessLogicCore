using DevExpress.Utils.About;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfUrlaubZA
	{
		public int ID { get; set; }
		public string MitarbeiterId { get; set; }
		public string Saldo { get; set; }
		public string Ausbezahlt { get; set; }
		public string Bemerkung { get; set; }
		public int? Monat { get; set; }
		public int? Jahr { get; set; }
		public int? Urlaub { get; set; }
		public bool? Abschluss { get; set; }

		public async static Task<ZeiterfUrlaubZA> GetByMitarbeiterAndDateAsync(string mitarbeiterId, DateTime datum)
		{
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<zeiterfurlaubza>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Monat == datum.Month && x.Jahr == datum.Year).FirstOrDefaultAsync();

				if (result == null)
				{
					return null;
				}

                return new ZeiterfUrlaubZA
                {
                    ID = result.ID,
                    Saldo = result.Saldo,
                    MitarbeiterId = mitarbeiterId,
                    Ausbezahlt = result.Ausbezahlt,
                    Bemerkung = result.Bemerkung,
                    Monat = result.Monat,
                    Jahr = result.Jahr,
                    Urlaub = result.Urlaub,
                    Abschluss = result.Abschluss,
                };

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<List<ZeiterfUrlaubZA>> GetByMitarbeiterAndYearAsync(string mitarbeiterId, DateTime date)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                DateTime von = new DateTime(date.Year,9,1);
                DateTime bis = new DateTime(date.Year + 1, 8, 31);

                var result = await UOW.UOW.uow.Query<zeiterfurlaubza>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && 
                            ((x.Jahr == von.Year && x.Monat >= von.Month) || (x.Jahr == bis.Year && x.Monat <= bis.Month)))
                    .ToListAsync();

                if (result == null)
                {
                    return null;
                }

                List<ZeiterfUrlaubZA> liste = new List<ZeiterfUrlaubZA>();
                foreach (var item in result)
                {
                    liste.Add(
                        new ZeiterfUrlaubZA
                        {
                            ID = item.ID,
                            Saldo = item.Saldo,
                            MitarbeiterId = mitarbeiterId,
                            Ausbezahlt = item.Ausbezahlt,
                            Bemerkung = string.IsNullOrEmpty(item.Bemerkung) ? "" : item.Bemerkung,
                            Monat = item.Monat,
                            Jahr = item.Jahr,
                            Urlaub = item.Urlaub,
                            Abschluss = item.Abschluss,
                        });
                }

                return liste;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<ZeiterfUrlaubZA> GetLastByMitarbeiterAsync(string mitarbeiterId)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<zeiterfurlaubza>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId)
                    .OrderByDescending(x => x.Jahr)
                    .ThenByDescending(x => x.Monat)
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return null;
                }

                return new ZeiterfUrlaubZA
                {
                    ID = result.ID,
                    Saldo = result.Saldo,
                    MitarbeiterId = mitarbeiterId,
                    Ausbezahlt = result.Ausbezahlt,
                    Bemerkung = result.Bemerkung,
                    Monat = result.Monat,
                    Jahr = result.Jahr,
                    Urlaub = result.Urlaub,
                    Abschluss = result.Abschluss,
                };

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<bool> CheckAbschlussAsync(string mitarbeiterId,DateTime date)
		{
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<zeiterfurlaubza>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId 
					&& (x.Abschluss == null || x.Abschluss == false)
					&& (x.Jahr <= date.Year || (x.Jahr == date.Year && x.Monat <= date.Month)))
					.CountAsync();
					
				if (result > 0)
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

        public async static Task<bool> CheckAbschlussAsync(string mitarbeiterId, int month, int year)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<zeiterfurlaubza>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId
                    && x.Abschluss == true
                    && x.Jahr == year && x.Monat == month)
                    .CountAsync();

                if (result > 0)
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

        public async static Task InsertOrAdd(string mitarbeiterId, int year, int month, int urlaub, string saldo, string ausbezahlt, string bemerkung, bool? abschluss = null)
		{
			try
			{
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

				var result = await UOW.UOW.uow.Query<zeiterfurlaubza>()
					.Where(x => x.MitarbeiterId == mitarbeiterId && x.Monat == month && x.Jahr == year).FirstOrDefaultAsync();

				if (result == null)
				{
					zeiterfurlaubza urlaubza = new zeiterfurlaubza(UOW.UOW.uow)
					{
						Abschluss = abschluss,
						Ausbezahlt = ausbezahlt,
						Bemerkung = bemerkung,
						Jahr = year,
						MitarbeiterId = mitarbeiterId,
						Monat = month,
						Saldo = saldo,
						Urlaub = urlaub
					};
				}
				else
				{
					result.Abschluss = abschluss;
					result.Ausbezahlt = ausbezahlt;
					result.Bemerkung = bemerkung;
					result.Saldo = saldo;
					result.Urlaub = urlaub;
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
