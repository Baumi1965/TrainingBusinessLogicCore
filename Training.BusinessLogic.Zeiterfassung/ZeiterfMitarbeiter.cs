using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfMitarbeiter
	{
		public int ID { get; set; }
		public string MitarbeiterId { get; set; }
		public string Name { get; set; }
		public string Finger { get; set; }
		public DateTime? Eintrittsdatum { get; set; }
		public DateTime? Austrittsdatum { get; set; }
		public string ZAAktuell { get; set; }
		public decimal? UrlaubAktuell { get; set; }
		public DateTime? DatumUrlaubNeu { get; set; }
		public int? Jahresurlaub { get; set; }
		public bool? Gesperrt { get; set; }
		public int? Zeitmodell { get; set; }
		public string SecretKey { get; set; }
		public int? SpielstaetteID { get; set; }
		public string SVNr { get; set; }
		public DateTime? GebDatum { get; set; }
		public string Geschlecht { get; set; }
		public string Taetigkeit { get; set; }
		public bool? Angestellter { get; set; }
		public bool? Geringfuegig { get; set; }
		public int? Wochenarbeitstage { get; set; }
		public double? Wochenstunden { get; set; }
		public string UeberwiegendeTaetigkeit { get; set; }
		public int? Dienstplanfarbe { get; set; }
		public bool? Auszahlungsliste { get; set; }
		public bool? NH3 { get; set; }
		public bool? NH3Plus { get; set; }

        public async static Task<List<ZeiterfMitarbeiter>> Get()
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
                var ma = await UOW.UOW.uow.Query<zeiterfmitarbeiter>()
                    .ToListAsync();

                if (ma == null)
                    return null;
                else
                {
                    List<ZeiterfMitarbeiter> lst = new List<ZeiterfMitarbeiter>();
                    foreach (var item in ma)
                    {
                        lst.Add(new ZeiterfMitarbeiter
                        {
                            ID = item.ID,
                            MitarbeiterId = item.MitarbeiterId,
                            Angestellter = item.Angestellter,
                            Austrittsdatum = item.Austrittsdatum,
                            Auszahlungsliste = item.Auszahlungsliste,
                            DatumUrlaubNeu = item.DatumUrlaubNeu,
                            Dienstplanfarbe = item.Dienstplanfarbe,
                            Eintrittsdatum = item.Eintrittsdatum,
                            Finger = item.Finger,
                            GebDatum = item.GebDatum,
                            Geringfuegig = item.Geringfuegig,
                            Geschlecht = item.Geschlecht,
                            Gesperrt = item.Gesperrt,
                            Jahresurlaub = item.Jahresurlaub,
                            Name = item.Name,
                            NH3 = item.NH3,
                            NH3Plus = item.NH3Plus,
                            SecretKey = item.SecretKey,
                            SpielstaetteID = item.SpielstaetteID,
                            SVNr = item.SVNr,
                            Taetigkeit = item.Taetigkeit,
                            UeberwiegendeTaetigkeit = item.UeberwiegendeTaetigkeit,
                            UrlaubAktuell = item.UrlaubAktuell,
                            Wochenarbeitstage = item.Wochenarbeitstage,
                            Wochenstunden = item.Wochenstunden,
                            ZAAktuell = item.ZAAktuell,
                            Zeitmodell = item.Zeitmodell
                        });
                    }
                    return lst;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<List<ZeiterfMitarbeiter>> GetForArbeitsleistungAsync(int location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
                var ma = await UOW.UOW.uow.Query<zeiterfmitarbeiter>()
                    .Where(x => x.Gesperrt == false && x.MitarbeiterId != "1000" && x.SpielstaetteID == location)
                    .OrderBy(x => x.Name)
                    .ToListAsync();

                if (ma == null)
                    return null;
                else
                {
                    List<ZeiterfMitarbeiter> lst = new List<ZeiterfMitarbeiter>();
                    foreach (var item in ma)
                    {
                        lst.Add(new ZeiterfMitarbeiter
                        {
                            ID = item.ID,
                            MitarbeiterId = item.MitarbeiterId,
                            Angestellter = item.Angestellter,
                            Austrittsdatum = item.Austrittsdatum,
                            Auszahlungsliste = item.Auszahlungsliste,
                            DatumUrlaubNeu = item.DatumUrlaubNeu,
                            Dienstplanfarbe = item.Dienstplanfarbe,
                            Eintrittsdatum = item.Eintrittsdatum,
                            Finger = item.Finger,
                            GebDatum = item.GebDatum,
                            Geringfuegig = item.Geringfuegig,
                            Geschlecht = item.Geschlecht,
                            Gesperrt = item.Gesperrt,
                            Jahresurlaub = item.Jahresurlaub,
                            Name = item.Name,
                            NH3 = item.NH3,
                            NH3Plus = item.NH3Plus,
                            SecretKey = item.SecretKey,
                            SpielstaetteID = item.SpielstaetteID,
                            SVNr = item.SVNr,
                            Taetigkeit = item.Taetigkeit,
                            UeberwiegendeTaetigkeit = item.UeberwiegendeTaetigkeit,
                            UrlaubAktuell = item.UrlaubAktuell,
                            Wochenarbeitstage = item.Wochenarbeitstage,
                            Wochenstunden = item.Wochenstunden,
                            ZAAktuell = item.ZAAktuell,
                            Zeitmodell = item.Zeitmodell
                        });
                    }
                    return lst;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<List<ZeiterfMitarbeiter>> GetForAuszahlungsliste(int location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
                var ma = await UOW.UOW.uow.Query<zeiterfmitarbeiter>()
                    .Where(x => x.Gesperrt == false && x.Auszahlungsliste == true && x.MitarbeiterId != "1000" && x.SpielstaetteID == location)
                    .OrderBy(x => x.Name)
                    .ToListAsync();

                if (ma == null)
                    return null;
                else
                {
                    List<ZeiterfMitarbeiter> lst = new List<ZeiterfMitarbeiter>();
                    foreach (var item in ma)
                    {
                        lst.Add(new ZeiterfMitarbeiter
                        {
                            ID = item.ID,
                            MitarbeiterId = item.MitarbeiterId,
                            Angestellter = item.Angestellter,
                            Austrittsdatum = item.Austrittsdatum,
                            Auszahlungsliste = item.Auszahlungsliste,
                            DatumUrlaubNeu = item.DatumUrlaubNeu,
                            Dienstplanfarbe = item.Dienstplanfarbe,
                            Eintrittsdatum = item.Eintrittsdatum,
                            Finger = item.Finger,
                            GebDatum = item.GebDatum,
                            Geringfuegig = item.Geringfuegig,
                            Geschlecht = item.Geschlecht,
                            Gesperrt = item.Gesperrt,
                            Jahresurlaub = item.Jahresurlaub,
                            Name = item.Name,
                            NH3 = item.NH3,
                            NH3Plus = item.NH3Plus,
                            SecretKey = item.SecretKey,
                            SpielstaetteID = item.SpielstaetteID,
                            SVNr = item.SVNr,
                            Taetigkeit = item.Taetigkeit,
                            UeberwiegendeTaetigkeit = item.UeberwiegendeTaetigkeit,
                            UrlaubAktuell = item.UrlaubAktuell,
                            Wochenarbeitstage = item.Wochenarbeitstage,
                            Wochenstunden = item.Wochenstunden,
                            ZAAktuell = item.ZAAktuell,
                            Zeitmodell = item.Zeitmodell
                        });
                    }
                    return lst;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async static Task<List<ZeiterfMitarbeiter>> GetNotBlocked()
		{
			try
			{
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
				var ma = await UOW.UOW.uow.Query<zeiterfmitarbeiter>()
					.Where(x => x.Gesperrt == false)
					.ToListAsync();

				if (ma == null)
					return null;
				else
				{
					List<ZeiterfMitarbeiter> lst = new List<ZeiterfMitarbeiter>();
					foreach (var item in ma)
					{
						lst.Add(new ZeiterfMitarbeiter
						{
							ID = item.ID,
							MitarbeiterId = item.MitarbeiterId,
							Angestellter = item.Angestellter,
							Austrittsdatum = item.Austrittsdatum,
							Auszahlungsliste = item.Auszahlungsliste,
							DatumUrlaubNeu = item.DatumUrlaubNeu,
							Dienstplanfarbe = item.Dienstplanfarbe,
							Eintrittsdatum = item.Eintrittsdatum,
							Finger = item.Finger,
							GebDatum = item.GebDatum,
							Geringfuegig = item.Geringfuegig,
							Geschlecht = item.Geschlecht,
							Gesperrt = item.Gesperrt,
							Jahresurlaub = item.Jahresurlaub,
							Name = item.Name,
							NH3 = item.NH3,
							NH3Plus = item.NH3Plus,
							SecretKey = item.SecretKey,
							SpielstaetteID = item.SpielstaetteID,
							SVNr = item.SVNr,
							Taetigkeit = item.Taetigkeit,
							UeberwiegendeTaetigkeit = item.UeberwiegendeTaetigkeit,
							UrlaubAktuell = item.UrlaubAktuell,
							Wochenarbeitstage = item.Wochenarbeitstage,
							Wochenstunden = item.Wochenstunden,
							ZAAktuell = item.ZAAktuell,
							Zeitmodell = item.Zeitmodell
						});
					}
					return lst;
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async static Task<ZeiterfMitarbeiter> GetById(string mitarbeiterId)
		{
			try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

				var ma = await UOW.UOW.uow.Query<zeiterfmitarbeiter>()
					.Where(x => x.MitarbeiterId == mitarbeiterId)
					.FirstOrDefaultAsync();

				if (ma == null)
					return null;
				else
				{
					return new ZeiterfMitarbeiter
					{
						ID = ma.ID,
						MitarbeiterId = ma.MitarbeiterId,
						Angestellter = ma.Angestellter,
						Austrittsdatum = ma.Austrittsdatum,
						Auszahlungsliste = ma.Auszahlungsliste,
						DatumUrlaubNeu = ma.DatumUrlaubNeu,
						Dienstplanfarbe = ma.Dienstplanfarbe,
						Eintrittsdatum = ma.Eintrittsdatum,
						Finger = ma.Finger,
						GebDatum = ma.GebDatum,
						Geringfuegig = ma.Geringfuegig,
						Geschlecht = ma.Geschlecht,
						Gesperrt = ma.Gesperrt,
						Jahresurlaub = ma.Jahresurlaub,
						Name = ma.Name,
						NH3 = ma.NH3,
						NH3Plus = ma.NH3Plus,
						SecretKey = ma.SecretKey,
						SpielstaetteID = ma.SpielstaetteID,
						SVNr = ma.SVNr,
						Taetigkeit = ma.Taetigkeit,
						UeberwiegendeTaetigkeit = ma.UeberwiegendeTaetigkeit,
						UrlaubAktuell = ma.UrlaubAktuell,
						Wochenarbeitstage = ma.Wochenarbeitstage,
						Wochenstunden = ma.Wochenstunden,
						ZAAktuell = ma.ZAAktuell,
						Zeitmodell = ma.Zeitmodell
					};
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async static Task UpdateUrlaub(string mitarbeiterId, int urlaub)
		{
			try
			{
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
				var ma = await UOW.UOW.uow.Query<zeiterfmitarbeiter>()
					.Where(x => x.MitarbeiterId == mitarbeiterId)
					.FirstOrDefaultAsync();

				if (ma != null)
				{
					ma.UrlaubAktuell = urlaub;
					await UOW.UOW.SaveAsync(); 
				}
			}
			catch (Exception)
			{
				throw;
			}
		}

        public async static Task Delete(int id)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
                var ma = await UOW.UOW.uow.Query<zeiterfmitarbeiter>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (ma != null)
                {
                    await UOW.UOW.uow.DeleteAsync(ma);
                    await UOW.UOW.SaveAsync(); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task Add(ZeiterfMitarbeiter ma)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                zeiterfmitarbeiter mitarbeiter = new zeiterfmitarbeiter(UOW.UOW.uow)
                {
					MitarbeiterId = ma.MitarbeiterId,
					Name = ma.Name,
					Finger = ma.Finger,
					Eintrittsdatum = ma.Eintrittsdatum,
					Austrittsdatum = ma.Austrittsdatum,
					ZAAktuell = ma.ZAAktuell,
					UrlaubAktuell = ma.UrlaubAktuell,
					DatumUrlaubNeu = ma.DatumUrlaubNeu,
					Jahresurlaub = ma.Jahresurlaub,
					Gesperrt = ma.Gesperrt,
					SpielstaetteID = ma.SpielstaetteID,
					SecretKey = ma.SecretKey,
					Zeitmodell = ma.Zeitmodell,
					SVNr = ma.SVNr,
					Taetigkeit = ma.Taetigkeit,
					Angestellter = ma.Angestellter,
					Geringfuegig = ma.Geringfuegig,
					Geschlecht = ma.Geschlecht,
					GebDatum = ma.GebDatum,
					Wochenarbeitstage = ma.Wochenarbeitstage,
					Wochenstunden = ma.Wochenstunden,
					Auszahlungsliste = ma.Auszahlungsliste,
					NH3 = ma.NH3,
					NH3Plus = ma.NH3Plus,
                };

                await UOW.UOW.SaveAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task Update(int id,ZeiterfMitarbeiter ma)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }


                var result = await UOW.UOW.uow
                    .Query<zeiterfmitarbeiter>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

				if (result == null)
				{
                    return;
				}

                result.MitarbeiterId = ma.MitarbeiterId;
                result.Name = ma.Name;
                result.Finger = ma.Finger;
                result.Eintrittsdatum = ma.Eintrittsdatum;
                result.Austrittsdatum = ma.Austrittsdatum;
                result.ZAAktuell = ma.ZAAktuell;
                result.UrlaubAktuell = ma.UrlaubAktuell;
                result.DatumUrlaubNeu = ma.DatumUrlaubNeu;
                result.Jahresurlaub = ma.Jahresurlaub;
                result.Gesperrt = ma.Gesperrt;
                result.SpielstaetteID = ma.SpielstaetteID;
                result.SecretKey = ma.SecretKey;
                result.Zeitmodell = ma.Zeitmodell;
                result.SVNr = ma.SVNr;
                result.Taetigkeit = ma.Taetigkeit;
                result.Angestellter = ma.Angestellter;
                result.Geringfuegig = ma.Geringfuegig;
                result.Geschlecht = ma.Geschlecht;
                result.GebDatum = ma.GebDatum;
                result.Wochenarbeitstage = ma.Wochenarbeitstage;
                result.Wochenstunden = ma.Wochenstunden;
                result.Auszahlungsliste = ma.Auszahlungsliste;
                result.NH3 = ma.NH3;
                result.NH3Plus = ma.NH3Plus;

                await UOW.UOW.SaveAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
