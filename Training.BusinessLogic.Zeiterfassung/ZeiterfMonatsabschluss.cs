using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfMonatsabschluss
	{
		public int ID { get; set; }
		public DateTime? Datum { get; set; }
		public string Tag { get; set; }
		public string Taetigkeitsbereich { get; set; }
		public string Sollstunden { get; set; }
		public string Beginn { get; set; }
		public string Ende { get; set; }
		public string Pause { get; set; }
		public string IstStunden { get; set; }
		public string Anwesenheit { get; set; }
		public string Saldo { get; set; }
		public string Bemerkung { get; set; }
		public string Dienstgang { get; set; }
		public string Urlaub { get; set; }
		public bool? Krankenstand { get; set; }
		public bool? Frei { get; set; }
		public string Wochensaldo { get; set; }
		public bool? Feiertag { get; set; }
		public bool? Edit { get; set; }
		public string BeginnBuchung { get; set; }
		public string EndeBuchung { get; set; }
		public string GeaendertVon { get; set; }
		public DateTime? GeaendertAm { get; set; }
		public string Engelmann { get; set; }
		public string ERS { get; set; }
		public string ESH { get; set; }
		public string MitarbeiterId { get; set; }
		public DateTime? DBTS { get; set; }
		public bool? NH3 { get; set; }
		public string NH3Prozent { get; set; }
		public string NH3Zeit { get; set; }


		public async static Task<List<ZeiterfMonatsabschluss>> GetByMitarbeiterAndDate(string mitarbeiterId, DateTime start, DateTime end)
		{
			try
			{
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

				var ma = await UOW.UOW.uow.Query<zeiterfmonatabschluss>()
					.Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date >= start.Date && x.Datum.Value.Date <= end.Date)
					.OrderBy(x => x.Datum)
					.ToListAsync();

				List<ZeiterfMonatsabschluss> lst = new List<ZeiterfMonatsabschluss>();
				foreach (var item in ma)
				{
					lst.Add(new ZeiterfMonatsabschluss
					{
						ID = item.ID,
						Datum = item.Datum,
						MitarbeiterId = item.MitarbeiterId,
						Anwesenheit = item.Anwesenheit,
						Frei = item.Frei == 0 ? false : true,
						Beginn = item.Beginn,
						BeginnBuchung = item.BeginnBuchung,
						Bemerkung = item.Bemerkung,
						DBTS = item.DBTS,
						Dienstgang = item.Dienstgang,
						Edit = item.Edit == 0 ? false : true,
						Ende = item.Ende,
						EndeBuchung = item.EndeBuchung,
						Engelmann = item.Engelmann,
						ERS = item.ERS,
						ESH = item.ESH,
						Feiertag = item.Feiertag == 0 ? false : true,
						GeaendertAm = item.GeaendertAm,
						GeaendertVon = item.GeaendertVon,
						IstStunden = item.Iststunden,
						Krankenstand = item.Krankenstand == 0 ? false : true,
						NH3 = item.NH3,
						NH3Prozent = item.NH3Prozent,
						NH3Zeit = item.NH3Zeit,
						Pause = item.Pause,
						Saldo = item.Saldo,
						Sollstunden = item.Sollstunden,
						Taetigkeitsbereich = item.Taetigkeitsbereich,
						Tag = item.Tag,
						Urlaub = item.Urlaub,
						Wochensaldo = item.Wochensaldo
					});
				}

				return lst;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public static async Task MakeMonatsabschluss(List<ZeiterfMonatsabschluss> abschluesse)
		{
			try
			{
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

				foreach (var item in abschluesse)
				{
					zeiterfmonatabschluss ma = new zeiterfmonatabschluss(UOW.UOW.uow);
					ma.Datum = item.Datum;
					ma.MitarbeiterId = item.MitarbeiterId;
					ma.Anwesenheit = item.Anwesenheit;
					ma.Frei = item.Frei == false ? (short)0 : (short)1;
					ma.Beginn = item.Beginn;
					ma.BeginnBuchung = item.BeginnBuchung;
					ma.Bemerkung = item.Bemerkung;
					ma.DBTS = item.DBTS;
					ma.Dienstgang = item.Dienstgang;
					ma.Edit = item.Edit == false ? (short)0 : (short)1;
					ma.Ende = item.Ende;
					ma.EndeBuchung = item.EndeBuchung;
					ma.Engelmann = item.Engelmann;
					ma.ERS = item.ERS;
					ma.ESH = item.ESH;
					ma.Feiertag = item.Feiertag == false ? (short)0 : (short)1;
					ma.GeaendertAm = item.GeaendertAm;
					ma.GeaendertVon = item.GeaendertVon;
					ma.Iststunden = item.IstStunden;
					ma.Krankenstand = item.Krankenstand == false ? (short)0 : (short)1;
					ma.NH3 = item.NH3;
					ma.NH3Prozent = item.NH3Prozent;
					ma.NH3Zeit = item.NH3Zeit;
					ma.Pause = item.Pause;
					ma.Saldo = item.Saldo;
					ma.Sollstunden = item.Sollstunden;
					ma.Taetigkeitsbereich = item.Taetigkeitsbereich;
					ma.Tag = item.Tag;
					ma.Urlaub = item.Urlaub;
					ma.Wochensaldo = item.Wochensaldo;
				}
				await UOW.UOW.SaveAsync(); 			}
			catch (Exception)
			{
				throw;
			}
		}

		public static async Task DeleteMonatsabschluss(string mitarbeiter, DateTime von, DateTime bis)
		{
			try
			{
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
					
				var result = await UOW.UOW.uow.Query<zeiterfmonatabschluss>()
					.Where(x => x.MitarbeiterId == mitarbeiter && x.Datum.Value.Date >= von.Date && x.Datum.Value.Date <= bis.Date)
					.ToListAsync();

				foreach (var item in result)
				{
					await UOW.UOW.uow.DeleteAsync(item);
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
