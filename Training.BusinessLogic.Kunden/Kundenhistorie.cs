using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;


namespace Training.BusinessLogic.Kunden
{
    public class Kundenhistorie
    {
        public int ID { get; set; }
        public string KdNr { get; set; }
        public string Verband { get; set; }
        public string EKLNr { get; set; }
        public string VName { get; set; }
        public string NName { get; set; }
        public string Adresse { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Tel { get; set; }
        public string EMail { get; set; }
        public string Geburtsdatum { get; set; }
        public string Typ { get; set; }
        public decimal? Guthaben { get; set; }
        public bool? Block { get; set; }
        public decimal? Wert { get; set; }
        public bool? Sperre { get; set; }
        public string Begruendung { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public string Info4 { get; set; }
        public string Info5 { get; set; }
        public string Kuerklasse { get; set; }
        public bool? FreigabeVerein { get; set; }
        public DateTime? DatumFreigabe { get; set; }
        public string FreigabeInfo1 { get; set; }
        public string FreigabeInfo2 { get; set; }
        public string FreigabeInfo3 { get; set; }
        public bool? Aktiv { get; set; }
        public string Passwort { get; set; }
        public string Trainer { get; set; }
        public string IBAN { get; set; }
        public int? PreiseID { get; set; }
        public string LizenzNr { get; set; }
        public bool? SperreVerein { get; set; }
        public string VerbandNation { get; set; }
        public string VerbandLizenz { get; set; }
        public DateTime? VerbandGueltigBis { get; set; }
        public string VerbandVerein { get; set; }
        public string SVNummer { get; set; }
        public string Steuernummer { get; set; }
        public string Arbeitsbewilligung { get; set; }
        public decimal? WertAbend { get; set; }
        public bool? DSGVO { get; set; }
        public DateTime? TS_DSGV { get; set; }
        public string User_DSGV { get; set; }
        public bool? Karte { get; set; }
        public string Geschlecht { get; set; }
        public bool? AKader { get; set; }
        public bool? ParkplatzEisring { get; set; }
        //public bool? Mahnungsmail { get; set; }
        public DateTime? DatumMahnungsmail { get; set; }
        public string TrainerLizenz { get; set; }
        public bool? EiszeitenESHERS { get; set; }
        public string Benutzer { get; set; }
        public bool? Geloescht { get; set; }
        public DateTime? DatumAenderung { get; set; }

        public static async Task AddAsync(string kdnr, string vname, string nname, string adresse, string plz, string ort, string tel, string email, string verband, string eklnr,
             string geburtsdatum, string typ, decimal? wert, bool block, bool sperre, string begruendung, string info1, string info2, string info3, string info4,
             string info5, string kuerklasse, bool freigabeVerein, DateTime? datumFreigabe, string freigabeInfo1, string freigabeInfo2, string freigabeInfo3,
             bool aktiv, string passwort, string iban, string trainer, int? preiseId, string lizenzNr, bool sperreVerein, string verbandNation, string verbandLizenz,
             string verbandVerein, DateTime? verbandGueltigBis, string svNummer, string steuernummer, string arbeitsbewilligung,
             decimal? wertAbend, bool dsgvo, DateTime? tsDsgvo, string userDsgvo, bool karte, string geschlecht, bool covidOk,
             bool parkplatzERS, string trainerlizenz, bool eiszeitenESHERS, string benutzer, bool geloescht, DateTime datumAenderung, bool saveImmediatly)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var kunde = new kundenhistorie(UOW.UOW.uow);
                kunde.KdNr = kdnr;
                kunde.VName = vname;
                kunde.NName = nname;
                kunde.Adresse = adresse;
                kunde.PLZ = plz;
                kunde.Ort = ort;
                kunde.Tel = tel;
                kunde.EMail = email;
                kunde.Verband = verband;
                kunde.EKLNr = eklnr;
                kunde.Geburtsdatum = geburtsdatum;
                kunde.Typ = typ;
                kunde.Wert = wert;
                kunde.Block = block;
                kunde.Sperre = sperre;
                kunde.Begruendung = begruendung;
                kunde.Info1 = info1;
                kunde.Info2 = info2;
                kunde.Info3 = info3;
                kunde.Info4 = info4;
                kunde.Info5 = info5;
                kunde.Kuerklasse = kuerklasse;
                kunde.FreigabeVerein = freigabeVerein;
                kunde.DatumFreigabe = datumFreigabe;
                kunde.FreigabeInfo1 = freigabeInfo1;
                kunde.FreigabeInfo2 = freigabeInfo2;
                kunde.FreigabeInfo3 = freigabeInfo3;
                kunde.Aktiv = aktiv;
                kunde.Passwort = passwort;
                kunde.IBAN = iban;
                kunde.Trainer = trainer;
                kunde.PreiseID = preiseId;
                kunde.LizenzNr = lizenzNr;
                kunde.SperreVerein = sperreVerein;
                kunde.VerbandNation = verbandNation;
                kunde.VerbandLizenz = verbandLizenz;
                kunde.VerbandVerein = verbandVerein;
                kunde.VerbandGueltigBis = verbandGueltigBis;
                kunde.SVNummer = svNummer;
                kunde.Steuernummer = steuernummer;
                kunde.Arbeitsbewilligung = arbeitsbewilligung;
                kunde.WertAbend = wertAbend;
                kunde.DSGVO = dsgvo;
                kunde.TS_DSGV = tsDsgvo;
                kunde.User_DSGV = userDsgvo;
                kunde.Karte = karte;
                kunde.Geschlecht = geschlecht;
                kunde.COVIDOK = covidOk;
                kunde.ParkplatzEisring = parkplatzERS;
                kunde.TrainerLizenz = trainerlizenz;
                kunde.EiszeitenESHERS = eiszeitenESHERS;
                kunde.Benutzer = benutzer;
                kunde.Geloescht = geloescht;
                kunde.DatumAenderung = datumAenderung;

                if (saveImmediatly)
                {
                    await UOW.UOW.SaveAsync();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddAsync(Kunden esherskunde, string benutzer, bool geloescht, DateTime datumAenderung, bool saveImmediatly)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var kunde = new kundenhistorie(UOW.UOW.uow);
                kunde.KdNr = esherskunde.KdNr;
                kunde.VName = esherskunde.VName;
                kunde.NName = esherskunde.NName;
                kunde.Adresse = esherskunde.Adresse;
                kunde.PLZ = esherskunde.PLZ;
                kunde.Ort = esherskunde.Ort;
                kunde.Tel = esherskunde.Tel;
                kunde.EMail = esherskunde.EMail;
                kunde.Verband = esherskunde.Verband;
                kunde.EKLNr = esherskunde.EKLNr;
                kunde.Geburtsdatum = esherskunde.Geburtsdatum;
                kunde.Typ = esherskunde.Typ;
                kunde.Wert = esherskunde.Wert;
                kunde.Block = esherskunde.Block;
                kunde.Sperre = esherskunde.Sperre;
                kunde.Begruendung = esherskunde.Begruendung;
                kunde.Info1 = esherskunde.Info1;
                kunde.Info2 = esherskunde.Info2;
                kunde.Info3 = esherskunde.Info3;
                kunde.Info4 = esherskunde.Info4;
                kunde.Info5 = esherskunde.Info5;
                kunde.Kuerklasse = esherskunde.Kuerklasse;
                kunde.FreigabeVerein = esherskunde.FreigabeVerein;
                kunde.DatumFreigabe = esherskunde.DatumFreigabe;
                kunde.FreigabeInfo1 = esherskunde.FreigabeInfo1;
                kunde.FreigabeInfo2 = esherskunde.FreigabeInfo2;
                kunde.FreigabeInfo3 = esherskunde.FreigabeInfo3;
                kunde.Aktiv = esherskunde.Aktiv;
                kunde.Passwort = esherskunde.Passwort;
                kunde.IBAN = esherskunde.IBAN;
                kunde.Trainer = esherskunde.Trainer;
                kunde.PreiseID = esherskunde.PreiseID;
                kunde.LizenzNr = esherskunde.LizenzNr;
                kunde.SperreVerein = esherskunde.SperreVerein;
                kunde.VerbandNation = esherskunde.VerbandNation;
                kunde.VerbandLizenz = esherskunde.VerbandLizenz;
                kunde.VerbandVerein = esherskunde.VerbandVerein;
                kunde.VerbandGueltigBis = esherskunde.VerbandGueltigBis;
                kunde.SVNummer = esherskunde.SVNummer;
                kunde.Steuernummer = esherskunde.Steuernummer;
                kunde.Arbeitsbewilligung = esherskunde.Arbeitsbewilligung;
                kunde.WertAbend = esherskunde.WertAbend;
                kunde.DSGVO = esherskunde.DSGVO;
                kunde.TS_DSGV = esherskunde.TS_DSGV;
                kunde.User_DSGV = esherskunde.User_DSGV;
                kunde.Karte = esherskunde.Karte;
                kunde.Geschlecht = esherskunde.Geschlecht;
                kunde.COVIDOK = esherskunde.COVIDOK;
                kunde.ParkplatzEisring = esherskunde.ParkplatzEisring;
                kunde.TrainerLizenz = esherskunde.TrainerLizenz;
                kunde.EiszeitenESHERS = esherskunde.EiszeitenESHERS;
                kunde.Benutzer = benutzer;
                kunde.Geloescht = geloescht;
                kunde.DatumAenderung = datumAenderung;

                if (saveImmediatly)
                {
                    await UOW.UOW.SaveAsync();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddAsync(string kdnr, string vname, string nname, string adresse, string plz, string ort, string tel, string email, string geburtsdatum, bool saveImmediatly)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var kunde = new kundenhistorie(UOW.UOW.uow);
                kunde.KdNr = kdnr;
                kunde.VName = vname;
                kunde.NName = nname;
                kunde.Adresse = adresse;
                kunde.PLZ = plz;
                kunde.Ort = ort;
                kunde.Tel = tel;
                kunde.EMail = email;
                kunde.Geburtsdatum = geburtsdatum;

                if (saveImmediatly)
                {
                    await UOW.UOW.SaveAsync();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Kundenhistorie>> GetAsync(string kdnr, DateTime? von, DateTime? bis)
        {
            try
            {
				if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
				{
					UOW.UOW.Connect();
				}

				var result = new List<kundenhistorie>();

                if (string.IsNullOrEmpty(kdnr))
                {
                    if (von == null && bis == null)
                    {
						result = await UOW.UOW.uow.Query<kundenhistorie>()
                            .OrderBy(x => x.KdNr)
                            .ThenBy(x => x.DatumAenderung)
                            .ToListAsync();
					}
					else if (von != null && bis == null)
                    {
						result = await UOW.UOW.uow.Query<kundenhistorie>()
                            .Where(x => x.DatumAenderung.Value.Date >= von.Value.Date)
							.OrderBy(x => x.KdNr)
							.ThenBy(x => x.DatumAenderung)
							.ToListAsync();

					}
					else if (von == null && bis != null)
                    {
						result = await UOW.UOW.uow.Query<kundenhistorie>()
							.Where(x => x.DatumAenderung.Value.Date <= bis.Value.Date)
							.OrderBy(x => x.KdNr)
							.ThenBy(x => x.DatumAenderung)
							.ToListAsync();
					}
                    else if (von != null && bis != null)
                    {
						result = await UOW.UOW.uow.Query<kundenhistorie>()
							.Where(x => x.DatumAenderung.Value.Date >= von.Value.Date && x.DatumAenderung.Value.Date <= bis.Value.Date)
							.OrderBy(x => x.KdNr)
							.ThenBy(x => x.DatumAenderung)
							.ToListAsync();
					}
				}
				else
                {
					if (von == null && bis == null)
					{
						result = await UOW.UOW.uow.Query<kundenhistorie>()
                            .Where(x => x.KdNr == kdnr)
							.OrderBy(x => x.DatumAenderung)
							.ToListAsync();
					}
					else if (von != null && bis == null)
					{
						result = await UOW.UOW.uow.Query<kundenhistorie>()
							.Where(x => x.KdNr == kdnr && x.DatumAenderung.Value.Date >= von.Value.Date)
							.OrderBy(x => x.DatumAenderung)
							.ToListAsync();

					}
					else if (von == null && bis != null)
					{
						result = await UOW.UOW.uow.Query<kundenhistorie>()
							.Where(x => x.KdNr == kdnr && x.DatumAenderung.Value.Date <= bis.Value.Date)
							.OrderBy(x => x.DatumAenderung)
							.ToListAsync();
					}
					else if (von != null && bis != null)
					{
						result = await UOW.UOW.uow.Query<kundenhistorie>()
							.Where(x => x.KdNr == kdnr && x.DatumAenderung.Value.Date >= von.Value.Date && x.DatumAenderung.Value.Date <= bis.Value.Date)
							.OrderBy(x => x.DatumAenderung)
							.ToListAsync();
					}
				}

                if (result == null)
                {
					return null;
                }

				List<Kundenhistorie> kh = new List<Kundenhistorie>();
                foreach (var item in result)
                {
					kh.Add(
						new Kundenhistorie
						{
							Adresse = item.Adresse,
							Aktiv = item.Aktiv,
							Arbeitsbewilligung = item.Arbeitsbewilligung,
							Begruendung = item.Begruendung,
							Benutzer = item.Benutzer,
							Block = item.Block,
							AKader = item.COVIDOK,
							DatumAenderung = item.DatumAenderung,
							DatumFreigabe = item.DatumFreigabe,
							DatumMahnungsmail = null,
							DSGVO = item.DSGVO,
							EiszeitenESHERS = item.EiszeitenESHERS,
							EKLNr = item.EKLNr,
							EMail = item.EMail,
							FreigabeInfo1 = item.FreigabeInfo1,
							FreigabeInfo2 = item.FreigabeInfo2,
							FreigabeInfo3 = item.FreigabeInfo3,
							FreigabeVerein = item.FreigabeVerein,
							Geburtsdatum = item.Geburtsdatum,
							Geloescht = item.Geloescht,
							Geschlecht = item.Geschlecht,
							Guthaben = item.Guthaben,
							IBAN = item.IBAN,
							ID = item.ID,
							Info1 = item.Info1,
							Info2 = item.Info2,
							Info3 = item.Info3,
							Info4 = item.Info4,
							Info5 = item.Info5,
							//KaestchenEisring = null,
							//KaestchenEisringNr = string.Empty,
							//KaestchenStadthalle = null,
							//KaestchenStadthalleNr = string.Empty,
							Karte = item.Karte,
							KdNr = item.KdNr,
							Kuerklasse = item.Kuerklasse,
							LizenzNr = item.LizenzNr,
							//Mahnungsmail = null,
							NName = item.NName,
							Ort = item.Ort,
							ParkplatzEisring = item.ParkplatzEisring,
							Passwort = item.Passwort,
							PLZ = item.PLZ,
							PreiseID = item.PreiseID,
							Sperre = item.Sperre,
							SperreVerein = item.SperreVerein,
							Steuernummer = item.Steuernummer,
							SVNummer = item.SVNummer,
							Tel = item.Tel,
							Trainer = item.Trainer,
							TrainerLizenz = item.TrainerLizenz,
							TS_DSGV = item.TS_DSGV,
							Typ = item.Typ,
							User_DSGV = item.User_DSGV,
							Verband = item.Verband,
							VerbandGueltigBis = item.VerbandGueltigBis,
							VerbandLizenz = item.VerbandLizenz,
							VerbandNation = item.VerbandNation,
							VerbandVerein = item.VerbandVerein,
							VName = item.VName,
							Wert = item.Wert,
							WertAbend = item.WertAbend,
						});
                }

                return kh;

			}
            catch (Exception)
            {

                throw;
            }
        }
    }
}
