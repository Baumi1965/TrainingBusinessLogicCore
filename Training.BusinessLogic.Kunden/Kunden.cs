using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Kunden
{
    public class Kunden
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
        public bool? KaestchenEisring { get; set; }
        public bool? KaestchenStadthalle { get; set; }
        public string KaestchenEisringNr { get; set; }
        public string KaestchenStadthalleNr { get; set; }
        public decimal? WertAbend { get; set; }
        public bool? DSGVO { get; set; }
        public DateTime? TS_DSGV { get; set; }
        public string User_DSGV { get; set; }
        public bool? Karte { get; set; }
        public string Geschlecht { get; set; }
        public bool? COVIDOK { get; set; }
        public bool? ParkplatzEisring { get; set; }
        public bool? Mahnungsmail { get; set; }
        public DateTime? DatumMahnungsmail { get; set; }
        public string TrainerLizenz { get; set; }
        public bool? EiszeitenESHERS { get; set; }
        public Guid Guid { get; set; }

        public static async Task<List<Kunden>> GetAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                List<Kunden> LstKunde = new List<Kunden>();
                var kunden = await UOW.Uow._uow.Query<kunden>().ToListAsync();
                foreach (var item in kunden)
                {
                    Kunden k = new Kunden();
                    k.Adresse = item.Adresse;
                    k.Aktiv = item.Aktiv;
                    k.Arbeitsbewilligung = item.Arbeitsbewilligung;
                    k.Begruendung = item.Begruendung;
                    k.Block = item.Block;
                    k.COVIDOK = item.COVIDOK;
                    k.DatumFreigabe = item.DatumFreigabe;
                    k.DatumMahnungsmail = item.DatumMahnungsmail;
                    k.DSGVO = item.DSGVO;
                    k.EiszeitenESHERS = item.EiszeitenESHERS;
                    k.EKLNr = item.EKLNr;
                    k.EMail = item.EMail;
                    k.FreigabeInfo1 = item.FreigabeInfo1;
                    k.FreigabeInfo2 = item.FreigabeInfo2;
                    k.FreigabeInfo3 = item.FreigabeInfo3;
                    k.FreigabeVerein = item.FreigabeVerein;
                    k.Geburtsdatum = item.Geburtsdatum;
                    k.Geschlecht = item.Geschlecht;
                    k.Guthaben = item.Guthaben;
                    k.IBAN = item.IBAN;
                    k.ID = item.ID;
                    k.Info1 = item.Info1;
                    k.Info2 = item.Info2;
                    k.Info3 = item.Info3;
                    k.Info4 = item.Info4;
                    k.Info5 = item.Info5;
                    k.KaestchenEisring = item.KaestchenEisring;
                    k.KaestchenEisringNr = item.KaestchenEisringNr;
                    k.KaestchenStadthalle = item.KaestchenStadthalle;
                    k.KaestchenStadthalleNr = item.KaestchenStadthalleNr;
                    k.Karte = item.Karte;
                    k.KdNr = item.KdNr;
                    k.Kuerklasse = item.Kuerklasse;
                    k.LizenzNr = item.LizenzNr;
                    k.Mahnungsmail = item.Mahnungsmail;
                    k.NName = item.NName;
                    k.Ort = item.Ort;
                    k.ParkplatzEisring = item.ParkplatzEisring;
                    k.Passwort = item.Passwort;
                    k.PLZ = item.PLZ;
                    k.PreiseID = item.PreiseID;
                    k.Sperre = item.Sperre;
                    k.SperreVerein = item.SperreVerein;
                    k.Steuernummer = item.Steuernummer;
                    k.SVNummer = item.SVNummer;
                    k.Tel = item.Tel;
                    k.Trainer = item.Trainer;
                    k.TrainerLizenz = item.TrainerLizenz;
                    k.TS_DSGV = item.TS_DSGV;
                    k.Typ = item.Typ;
                    k.User_DSGV = item.User_DSGV;
                    k.Verband = item.Verband;
                    k.VerbandGueltigBis = item.VerbandGueltigBis;
                    k.VerbandLizenz = item.VerbandLizenz;
                    k.VerbandNation = item.VerbandNation;
                    k.VerbandVerein = item.VerbandVerein;
                    k.VName = item.VName;
                    k.Wert = item.Wert;
                    k.WertAbend = item.WertAbend;
                    k.Guid = new Guid(item.Guid);
                    LstKunde.Add(k);
                }

                return LstKunde;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Kunden>> GetWithKaestchenAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                List<Kunden> LstKunde = new List<Kunden>();
                var kunden = await UOW.Uow._uow.Query<kunden>().Where(x => x.KaestchenEisring == true || x.KaestchenStadthalle == true).ToListAsync();
                foreach (var item in kunden)
                {
                    Kunden k = new Kunden();
                    k.Adresse = item.Adresse;
                    k.Aktiv = item.Aktiv;
                    k.Arbeitsbewilligung = item.Arbeitsbewilligung;
                    k.Begruendung = item.Begruendung;
                    k.Block = item.Block;
                    k.COVIDOK = item.COVIDOK;
                    k.DatumFreigabe = item.DatumFreigabe;
                    k.DatumMahnungsmail = item.DatumMahnungsmail;
                    k.DSGVO = item.DSGVO;
                    k.EiszeitenESHERS = item.EiszeitenESHERS;
                    k.EKLNr = item.EKLNr;
                    k.EMail = item.EMail;
                    k.FreigabeInfo1 = item.FreigabeInfo1;
                    k.FreigabeInfo2 = item.FreigabeInfo2;
                    k.FreigabeInfo3 = item.FreigabeInfo3;
                    k.FreigabeVerein = item.FreigabeVerein;
                    k.Geburtsdatum = item.Geburtsdatum;
                    k.Geschlecht = item.Geschlecht;
                    k.Guthaben = item.Guthaben;
                    k.IBAN = item.IBAN;
                    k.ID = item.ID;
                    k.Info1 = item.Info1;
                    k.Info2 = item.Info2;
                    k.Info3 = item.Info3;
                    k.Info4 = item.Info4;
                    k.Info5 = item.Info5;
                    k.KaestchenEisring = item.KaestchenEisring;
                    k.KaestchenEisringNr = item.KaestchenEisringNr;
                    k.KaestchenStadthalle = item.KaestchenStadthalle;
                    k.KaestchenStadthalleNr = item.KaestchenStadthalleNr;
                    k.Karte = item.Karte;
                    k.KdNr = item.KdNr;
                    k.Kuerklasse = item.Kuerklasse;
                    k.LizenzNr = item.LizenzNr;
                    k.Mahnungsmail = item.Mahnungsmail;
                    k.NName = item.NName;
                    k.Ort = item.Ort;
                    k.ParkplatzEisring = item.ParkplatzEisring;
                    k.Passwort = item.Passwort;
                    k.PLZ = item.PLZ;
                    k.PreiseID = item.PreiseID;
                    k.Sperre = item.Sperre;
                    k.SperreVerein = item.SperreVerein;
                    k.Steuernummer = item.Steuernummer;
                    k.SVNummer = item.SVNummer;
                    k.Tel = item.Tel;
                    k.Trainer = item.Trainer;
                    k.TrainerLizenz = item.TrainerLizenz;
                    k.TS_DSGV = item.TS_DSGV;
                    k.Typ = item.Typ;
                    k.User_DSGV = item.User_DSGV;
                    k.Verband = item.Verband;
                    k.VerbandGueltigBis = item.VerbandGueltigBis;
                    k.VerbandLizenz = item.VerbandLizenz;
                    k.VerbandNation = item.VerbandNation;
                    k.VerbandVerein = item.VerbandVerein;
                    k.VName = item.VName;
                    k.Wert = item.Wert;
                    k.WertAbend = item.WertAbend;
                    k.Guid = new Guid(item.Guid);
                    LstKunde.Add(k);
                }

                return LstKunde;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateGuthabenAsync(string kdnr)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.ExecuteSprocAsync(new System.Threading.CancellationToken(), "GUTHABEN_UPDATE", kdnr);
                var Guthaben = Convert.ToDecimal(result.ResultSet.FirstOrDefault().Rows.FirstOrDefault().Values.FirstOrDefault());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateGuthabenAsync(List<Kunden> kunden)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                foreach (var item in kunden)
                {
                    var result = await UOW.Uow._uow.ExecuteSprocAsync(new System.Threading.CancellationToken(), "GUTHABEN_UPDATE", item.KdNr);
                    var Guthaben = Convert.ToDecimal(result.ResultSet.FirstOrDefault().Rows.FirstOrDefault().Values.FirstOrDefault());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Kunden>> GetForVerbandAsync(string info1, string info2, string info3, string info4, string info5, string info6, string info7, string info8, string info9, string info10)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                List<Kunden> LstKunde = new List<Kunden>();
                var kunden = await UOW.Uow._uow.Query<kunden>().
                    Where(x => (x.Verband == info1
                        || x.Verband == info2
                        || x.Verband == info3
                        || x.Verband == info4
                        || x.Verband == info5
                        || x.Verband == info6
                        || x.Verband == info7
                        || x.Verband == info8
                        || x.Verband == info9
                        || x.Verband == info10) &&
                        (x.EKLNr == info1
                        || x.EKLNr == info2
                        || x.EKLNr == info3
                        || x.EKLNr == info4
                        || x.EKLNr == info5
                        || x.EKLNr == info6
                        || x.EKLNr == info7
                        || x.EKLNr == info8
                        || x.EKLNr == info9
                        || x.EKLNr == info10)).ToListAsync();

                foreach (var item in kunden)
                {
                    Kunden k = new Kunden();
                    k.Adresse = item.Adresse;
                    k.Aktiv = item.Aktiv;
                    k.Arbeitsbewilligung = item.Arbeitsbewilligung;
                    k.Begruendung = item.Begruendung;
                    k.Block = item.Block;
                    k.COVIDOK = item.COVIDOK;
                    k.DatumFreigabe = item.DatumFreigabe;
                    k.DatumMahnungsmail = item.DatumMahnungsmail;
                    k.DSGVO = item.DSGVO;
                    k.EiszeitenESHERS = item.EiszeitenESHERS;
                    k.EKLNr = item.EKLNr;
                    k.EMail = item.EMail;
                    k.FreigabeInfo1 = item.FreigabeInfo1;
                    k.FreigabeInfo2 = item.FreigabeInfo2;
                    k.FreigabeInfo3 = item.FreigabeInfo3;
                    k.FreigabeVerein = item.FreigabeVerein;
                    k.Geburtsdatum = item.Geburtsdatum;
                    k.Geschlecht = item.Geschlecht;
                    k.Guthaben = item.Guthaben;
                    k.IBAN = item.IBAN;
                    k.ID = item.ID;
                    k.Info1 = item.Info1;
                    k.Info2 = item.Info2;
                    k.Info3 = item.Info3;
                    k.Info4 = item.Info4;
                    k.Info5 = item.Info5;
                    k.KaestchenEisring = item.KaestchenEisring;
                    k.KaestchenEisringNr = item.KaestchenEisringNr;
                    k.KaestchenStadthalle = item.KaestchenStadthalle;
                    k.KaestchenStadthalleNr = item.KaestchenStadthalleNr;
                    k.Karte = item.Karte;
                    k.KdNr = item.KdNr;
                    k.Kuerklasse = item.Kuerklasse;
                    k.LizenzNr = item.LizenzNr;
                    k.Mahnungsmail = item.Mahnungsmail;
                    k.NName = item.NName;
                    k.Ort = item.Ort;
                    k.ParkplatzEisring = item.ParkplatzEisring;
                    k.Passwort = item.Passwort;
                    k.PLZ = item.PLZ;
                    k.PreiseID = item.PreiseID;
                    k.Sperre = item.Sperre;
                    k.SperreVerein = item.SperreVerein;
                    k.Steuernummer = item.Steuernummer;
                    k.SVNummer = item.SVNummer;
                    k.Tel = item.Tel;
                    k.Trainer = item.Trainer;
                    k.TrainerLizenz = item.TrainerLizenz;
                    k.TS_DSGV = item.TS_DSGV;
                    k.Typ = item.Typ;
                    k.User_DSGV = item.User_DSGV;
                    k.Verband = item.Verband;
                    k.VerbandGueltigBis = item.VerbandGueltigBis;
                    k.VerbandLizenz = item.VerbandLizenz;
                    k.VerbandNation = item.VerbandNation;
                    k.VerbandVerein = item.VerbandVerein;
                    k.VName = item.VName;
                    k.Wert = item.Wert;
                    k.WertAbend = item.WertAbend;
                    k.Guid = new Guid(item.Guid);
                    LstKunde.Add(k);
                }

                return LstKunde;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> CountAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var cnt = await UOW.Uow._uow.Query<kunden>().CountAsync();
                return cnt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> CountKaestchenAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var cnt = await UOW.Uow._uow.Query<kunden>().Where(x => x.KaestchenEisring == true || x.KaestchenStadthalle == true).CountAsync();
                return cnt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<short> MaxKdNrAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var max = await UOW.Uow._uow.Query<kunden>().MaxAsync(x => x.KdNr);
                if (max == null)
                    return 0;
                else
                    return Convert.ToInt16(max);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Kunden> GetByIdAsync(int id)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var item = await UOW.Uow._uow.Query<kunden>().Where(x => x.ID == id).FirstOrDefaultAsync();

                Kunden k = new Kunden();
                k.Adresse = item.Adresse;
                k.Aktiv = item.Aktiv;
                k.Arbeitsbewilligung = item.Arbeitsbewilligung;
                k.Begruendung = item.Begruendung;
                k.Block = item.Block;
                k.COVIDOK = item.COVIDOK;
                k.DatumFreigabe = item.DatumFreigabe;
                k.DatumMahnungsmail = item.DatumMahnungsmail;
                k.DSGVO = item.DSGVO;
                k.EiszeitenESHERS = item.EiszeitenESHERS;
                k.EKLNr = item.EKLNr;
                k.EMail = item.EMail;
                k.FreigabeInfo1 = item.FreigabeInfo1;
                k.FreigabeInfo2 = item.FreigabeInfo2;
                k.FreigabeInfo3 = item.FreigabeInfo3;
                k.FreigabeVerein = item.FreigabeVerein;
                k.Geburtsdatum = item.Geburtsdatum;
                k.Geschlecht = item.Geschlecht;
                k.Guthaben = item.Guthaben;
                k.IBAN = item.IBAN;
                k.ID = item.ID;
                k.Info1 = item.Info1;
                k.Info2 = item.Info2;
                k.Info3 = item.Info3;
                k.Info4 = item.Info4;
                k.Info5 = item.Info5;
                k.KaestchenEisring = item.KaestchenEisring;
                k.KaestchenEisringNr = item.KaestchenEisringNr;
                k.KaestchenStadthalle = item.KaestchenStadthalle;
                k.KaestchenStadthalleNr = item.KaestchenStadthalleNr;
                k.Karte = item.Karte;
                k.KdNr = item.KdNr;
                k.Kuerklasse = item.Kuerklasse;
                k.LizenzNr = item.LizenzNr;
                k.Mahnungsmail = item.Mahnungsmail;
                k.NName = item.NName;
                k.Ort = item.Ort;
                k.ParkplatzEisring = item.ParkplatzEisring;
                k.Passwort = item.Passwort;
                k.PLZ = item.PLZ;
                k.PreiseID = item.PreiseID;
                k.Sperre = item.Sperre;
                k.SperreVerein = item.SperreVerein;
                k.Steuernummer = item.Steuernummer;
                k.SVNummer = item.SVNummer;
                k.Tel = item.Tel;
                k.Trainer = item.Trainer;
                k.TrainerLizenz = item.TrainerLizenz;
                k.TS_DSGV = item.TS_DSGV;
                k.Typ = item.Typ;
                k.User_DSGV = item.User_DSGV;
                k.Verband = item.Verband;
                k.VerbandGueltigBis = item.VerbandGueltigBis;
                k.VerbandLizenz = item.VerbandLizenz;
                k.VerbandNation = item.VerbandNation;
                k.VerbandVerein = item.VerbandVerein;
                k.VName = item.VName;
                k.Wert = item.Wert;
                k.WertAbend = item.WertAbend;
                k.Guid = new Guid(item.Guid);

                return k;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Kunden> GetByKdNrAsync(string kdnr)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
                var item = await UOW.Uow._uow.Query<kunden>().Where(x => x.KdNr == kdnr).FirstOrDefaultAsync();
                if (item == null)
                    return null;

                Kunden k = new Kunden();
                k.Adresse = item.Adresse;
                k.Aktiv = item.Aktiv;
                k.Arbeitsbewilligung = item.Arbeitsbewilligung;
                k.Begruendung = item.Begruendung;
                k.Block = item.Block;
                k.COVIDOK = item.COVIDOK;
                k.DatumFreigabe = item.DatumFreigabe;
                k.DatumMahnungsmail = item.DatumMahnungsmail;
                k.DSGVO = item.DSGVO;
                k.EiszeitenESHERS = item.EiszeitenESHERS;
                k.EKLNr = item.EKLNr;
                k.EMail = item.EMail;
                k.FreigabeInfo1 = item.FreigabeInfo1;
                k.FreigabeInfo2 = item.FreigabeInfo2;
                k.FreigabeInfo3 = item.FreigabeInfo3;
                k.FreigabeVerein = item.FreigabeVerein;
                k.Geburtsdatum = item.Geburtsdatum;
                k.Geschlecht = item.Geschlecht;
                k.Guthaben = item.Guthaben;
                k.IBAN = item.IBAN;
                k.ID = item.ID;
                k.Info1 = item.Info1;
                k.Info2 = item.Info2;
                k.Info3 = item.Info3;
                k.Info4 = item.Info4;
                k.Info5 = item.Info5;
                k.KaestchenEisring = item.KaestchenEisring;
                k.KaestchenEisringNr = item.KaestchenEisringNr;
                k.KaestchenStadthalle = item.KaestchenStadthalle;
                k.KaestchenStadthalleNr = item.KaestchenStadthalleNr;
                k.Karte = item.Karte;
                k.KdNr = item.KdNr;
                k.Kuerklasse = item.Kuerklasse;
                k.LizenzNr = item.LizenzNr;
                k.Mahnungsmail = item.Mahnungsmail;
                k.NName = item.NName;
                k.Ort = item.Ort;
                k.ParkplatzEisring = item.ParkplatzEisring;
                k.Passwort = item.Passwort;
                k.PLZ = item.PLZ;
                k.PreiseID = item.PreiseID;
                k.Sperre = item.Sperre;
                k.SperreVerein = item.SperreVerein;
                k.Steuernummer = item.Steuernummer;
                k.SVNummer = item.SVNummer;
                k.Tel = item.Tel;
                k.Trainer = item.Trainer;
                k.TrainerLizenz = item.TrainerLizenz;
                k.TS_DSGV = item.TS_DSGV;
                k.Typ = item.Typ;
                k.User_DSGV = item.User_DSGV;
                k.Verband = item.Verband;
                k.VerbandGueltigBis = item.VerbandGueltigBis;
                k.VerbandLizenz = item.VerbandLizenz;
                k.VerbandNation = item.VerbandNation;
                k.VerbandVerein = item.VerbandVerein;
                k.VName = item.VName;
                k.Wert = item.Wert;
                k.WertAbend = item.WertAbend;
                k.Guid = new Guid(item.Guid);

                return k;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Kunden> GetForBankAsync(string kdnr, string zg, string zr, string zz, string ag, string iban)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
                var item = await UOW.Uow._uow.Query<kunden>()
                    .Where(x => x.KdNr == kdnr || x.KdNr == zg || x.KdNr == zr || x.KdNr == zz || x.KdNr == ag)
                    .FirstOrDefaultAsync();

                if (item == null)
                {
                    if (!string.IsNullOrEmpty(iban))
                    {
                        item = await UOW.Uow._uow.Query<kunden>()
                            .Where(x => x.IBAN == iban).FirstOrDefaultAsync();

                        if (item == null)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;

                    }
                }

                Kunden k = new Kunden();
                k.Adresse = item.Adresse;
                k.Aktiv = item.Aktiv;
                k.Arbeitsbewilligung = item.Arbeitsbewilligung;
                k.Begruendung = item.Begruendung;
                k.Block = item.Block;
                k.COVIDOK = item.COVIDOK;
                k.DatumFreigabe = item.DatumFreigabe;
                k.DatumMahnungsmail = item.DatumMahnungsmail;
                k.DSGVO = item.DSGVO;
                k.EiszeitenESHERS = item.EiszeitenESHERS;
                k.EKLNr = item.EKLNr;
                k.EMail = item.EMail;
                k.FreigabeInfo1 = item.FreigabeInfo1;
                k.FreigabeInfo2 = item.FreigabeInfo2;
                k.FreigabeInfo3 = item.FreigabeInfo3;
                k.FreigabeVerein = item.FreigabeVerein;
                k.Geburtsdatum = item.Geburtsdatum;
                k.Geschlecht = item.Geschlecht;
                k.Guthaben = item.Guthaben;
                k.IBAN = item.IBAN;
                k.ID = item.ID;
                k.Info1 = item.Info1;
                k.Info2 = item.Info2;
                k.Info3 = item.Info3;
                k.Info4 = item.Info4;
                k.Info5 = item.Info5;
                k.KaestchenEisring = item.KaestchenEisring;
                k.KaestchenEisringNr = item.KaestchenEisringNr;
                k.KaestchenStadthalle = item.KaestchenStadthalle;
                k.KaestchenStadthalleNr = item.KaestchenStadthalleNr;
                k.Karte = item.Karte;
                k.KdNr = item.KdNr;
                k.Kuerklasse = item.Kuerklasse;
                k.LizenzNr = item.LizenzNr;
                k.Mahnungsmail = item.Mahnungsmail;
                k.NName = item.NName;
                k.Ort = item.Ort;
                k.ParkplatzEisring = item.ParkplatzEisring;
                k.Passwort = item.Passwort;
                k.PLZ = item.PLZ;
                k.PreiseID = item.PreiseID;
                k.Sperre = item.Sperre;
                k.SperreVerein = item.SperreVerein;
                k.Steuernummer = item.Steuernummer;
                k.SVNummer = item.SVNummer;
                k.Tel = item.Tel;
                k.Trainer = item.Trainer;
                k.TrainerLizenz = item.TrainerLizenz;
                k.TS_DSGV = item.TS_DSGV;
                k.Typ = item.Typ;
                k.User_DSGV = item.User_DSGV;
                k.Verband = item.Verband;
                k.VerbandGueltigBis = item.VerbandGueltigBis;
                k.VerbandLizenz = item.VerbandLizenz;
                k.VerbandNation = item.VerbandNation;
                k.VerbandVerein = item.VerbandVerein;
                k.VName = item.VName;
                k.Wert = item.Wert;
                k.WertAbend = item.WertAbend;
                k.Guid = new Guid(item.Guid);

                return k;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Kunden>> GetParkplatzEinfahrtAktivAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var lstKunden = new List<Kunden>();
                var kunden = await UOW.Uow._uow.Query<kunden>().Where(x => x.ParkplatzEisring == true).ToListAsync();
                foreach (var item in kunden)
                {
                    Kunden k = new Kunden();
                    k.Adresse = item.Adresse;
                    k.Aktiv = item.Aktiv;
                    k.Arbeitsbewilligung = item.Arbeitsbewilligung;
                    k.Begruendung = item.Begruendung;
                    k.Block = item.Block;
                    k.COVIDOK = item.COVIDOK;
                    k.DatumFreigabe = item.DatumFreigabe;
                    k.DatumMahnungsmail = item.DatumMahnungsmail;
                    k.DSGVO = item.DSGVO;
                    k.EiszeitenESHERS = item.EiszeitenESHERS;
                    k.EKLNr = item.EKLNr;
                    k.EMail = item.EMail;
                    k.FreigabeInfo1 = item.FreigabeInfo1;
                    k.FreigabeInfo2 = item.FreigabeInfo2;
                    k.FreigabeInfo3 = item.FreigabeInfo3;
                    k.FreigabeVerein = item.FreigabeVerein;
                    k.Geburtsdatum = item.Geburtsdatum;
                    k.Geschlecht = item.Geschlecht;
                    k.Guthaben = item.Guthaben;
                    k.IBAN = item.IBAN;
                    k.ID = item.ID;
                    k.Info1 = item.Info1;
                    k.Info2 = item.Info2;
                    k.Info3 = item.Info3;
                    k.Info4 = item.Info4;
                    k.Info5 = item.Info5;
                    k.KaestchenEisring = item.KaestchenEisring;
                    k.KaestchenEisringNr = item.KaestchenEisringNr;
                    k.KaestchenStadthalle = item.KaestchenStadthalle;
                    k.KaestchenStadthalleNr = item.KaestchenStadthalleNr;
                    k.Karte = item.Karte;
                    k.KdNr = item.KdNr;
                    k.Kuerklasse = item.Kuerklasse;
                    k.LizenzNr = item.LizenzNr;
                    k.Mahnungsmail = item.Mahnungsmail;
                    k.NName = item.NName;
                    k.Ort = item.Ort;
                    k.ParkplatzEisring = item.ParkplatzEisring;
                    k.Passwort = item.Passwort;
                    k.PLZ = item.PLZ;
                    k.PreiseID = item.PreiseID;
                    k.Sperre = item.Sperre;
                    k.SperreVerein = item.SperreVerein;
                    k.Steuernummer = item.Steuernummer;
                    k.SVNummer = item.SVNummer;
                    k.Tel = item.Tel;
                    k.Trainer = item.Trainer;
                    k.TrainerLizenz = item.TrainerLizenz;
                    k.TS_DSGV = item.TS_DSGV;
                    k.Typ = item.Typ;
                    k.User_DSGV = item.User_DSGV;
                    k.Verband = item.Verband;
                    k.VerbandGueltigBis = item.VerbandGueltigBis;
                    k.VerbandLizenz = item.VerbandLizenz;
                    k.VerbandNation = item.VerbandNation;
                    k.VerbandVerein = item.VerbandVerein;
                    k.VName = item.VName;
                    k.Wert = item.Wert;
                    k.WertAbend = item.WertAbend;
                    k.Guid = new Guid(item.Guid);
                    lstKunden.Add(k);
                }

                return lstKunden;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Kunden>> GetWithNegativeBalanceAsync(bool onlyactive)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var lstKunden = new List<Kunden>();
                var kunden = new List<kunden>();

                if (onlyactive)
                {
                    kunden = await UOW.Uow._uow.Query<kunden>().Where(x => x.Guthaben < 0 && x.Verband.ToUpper() != "AUSGESCHIEDEN").OrderBy(x => x.KdNr).ToListAsync();
                }
                else
                {
                    kunden = await UOW.Uow._uow.Query<kunden>().Where(x => x.Guthaben < 0).OrderBy(x => x.KdNr).ToListAsync();
                }

                foreach (var item in kunden)
                {
                    Kunden k = new Kunden();
                    k.Adresse = item.Adresse;
                    k.Aktiv = item.Aktiv;
                    k.Arbeitsbewilligung = item.Arbeitsbewilligung;
                    k.Begruendung = item.Begruendung;
                    k.Block = item.Block;
                    k.COVIDOK = item.COVIDOK;
                    k.DatumFreigabe = item.DatumFreigabe;
                    k.DatumMahnungsmail = item.DatumMahnungsmail;
                    k.DSGVO = item.DSGVO;
                    k.EiszeitenESHERS = item.EiszeitenESHERS;
                    k.EKLNr = item.EKLNr;
                    k.EMail = item.EMail;
                    k.FreigabeInfo1 = item.FreigabeInfo1;
                    k.FreigabeInfo2 = item.FreigabeInfo2;
                    k.FreigabeInfo3 = item.FreigabeInfo3;
                    k.FreigabeVerein = item.FreigabeVerein;
                    k.Geburtsdatum = item.Geburtsdatum;
                    k.Geschlecht = item.Geschlecht;
                    k.Guthaben = item.Guthaben;
                    k.IBAN = item.IBAN;
                    k.ID = item.ID;
                    k.Info1 = item.Info1;
                    k.Info2 = item.Info2;
                    k.Info3 = item.Info3;
                    k.Info4 = item.Info4;
                    k.Info5 = item.Info5;
                    k.KaestchenEisring = item.KaestchenEisring;
                    k.KaestchenEisringNr = item.KaestchenEisringNr;
                    k.KaestchenStadthalle = item.KaestchenStadthalle;
                    k.KaestchenStadthalleNr = item.KaestchenStadthalleNr;
                    k.Karte = item.Karte;
                    k.KdNr = item.KdNr;
                    k.Kuerklasse = item.Kuerklasse;
                    k.LizenzNr = item.LizenzNr;
                    k.Mahnungsmail = item.Mahnungsmail;
                    k.NName = item.NName;
                    k.Ort = item.Ort;
                    k.ParkplatzEisring = item.ParkplatzEisring;
                    k.Passwort = item.Passwort;
                    k.PLZ = item.PLZ;
                    k.PreiseID = item.PreiseID;
                    k.Sperre = item.Sperre;
                    k.SperreVerein = item.SperreVerein;
                    k.Steuernummer = item.Steuernummer;
                    k.SVNummer = item.SVNummer;
                    k.Tel = item.Tel;
                    k.Trainer = item.Trainer;
                    k.TrainerLizenz = item.TrainerLizenz;
                    k.TS_DSGV = item.TS_DSGV;
                    k.Typ = item.Typ;
                    k.User_DSGV = item.User_DSGV;
                    k.Verband = item.Verband;
                    k.VerbandGueltigBis = item.VerbandGueltigBis;
                    k.VerbandLizenz = item.VerbandLizenz;
                    k.VerbandNation = item.VerbandNation;
                    k.VerbandVerein = item.VerbandVerein;
                    k.VName = item.VName;
                    k.Wert = item.Wert;
                    k.WertAbend = item.WertAbend;
                    k.Guid = new Guid(item.Guid);
                    lstKunden.Add(k);
                }

                return lstKunden;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Kunden>> GetForMahnungAsync(int wert)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                List<Kunden> lstKunden = new List<Kunden>();

                var kunden = await UOW.Uow._uow.Query<kunden>().Where(x => x.Guthaben < wert && x.Verband.ToUpper() != "AUSGESCHIEDEN").OrderBy(x => x.KdNr).ToListAsync();

                foreach (var item in kunden)
                {
                    Kunden k = new Kunden();
                    k.Adresse = item.Adresse;
                    k.Aktiv = item.Aktiv;
                    k.Arbeitsbewilligung = item.Arbeitsbewilligung;
                    k.Begruendung = item.Begruendung;
                    k.Block = item.Block;
                    k.COVIDOK = item.COVIDOK;
                    k.DatumFreigabe = item.DatumFreigabe;
                    k.DatumMahnungsmail = item.DatumMahnungsmail;
                    k.DSGVO = item.DSGVO;
                    k.EiszeitenESHERS = item.EiszeitenESHERS;
                    k.EKLNr = item.EKLNr;
                    k.EMail = item.EMail;
                    k.FreigabeInfo1 = item.FreigabeInfo1;
                    k.FreigabeInfo2 = item.FreigabeInfo2;
                    k.FreigabeInfo3 = item.FreigabeInfo3;
                    k.FreigabeVerein = item.FreigabeVerein;
                    k.Geburtsdatum = item.Geburtsdatum;
                    k.Geschlecht = item.Geschlecht;
                    k.Guthaben = item.Guthaben;
                    k.IBAN = item.IBAN;
                    k.ID = item.ID;
                    k.Info1 = item.Info1;
                    k.Info2 = item.Info2;
                    k.Info3 = item.Info3;
                    k.Info4 = item.Info4;
                    k.Info5 = item.Info5;
                    k.KaestchenEisring = item.KaestchenEisring;
                    k.KaestchenEisringNr = item.KaestchenEisringNr;
                    k.KaestchenStadthalle = item.KaestchenStadthalle;
                    k.KaestchenStadthalleNr = item.KaestchenStadthalleNr;
                    k.Karte = item.Karte;
                    k.KdNr = item.KdNr;
                    k.Kuerklasse = item.Kuerklasse;
                    k.LizenzNr = item.LizenzNr;
                    k.Mahnungsmail = item.Mahnungsmail;
                    k.NName = item.NName;
                    k.Ort = item.Ort;
                    k.ParkplatzEisring = item.ParkplatzEisring;
                    k.Passwort = item.Passwort;
                    k.PLZ = item.PLZ;
                    k.PreiseID = item.PreiseID;
                    k.Sperre = item.Sperre;
                    k.SperreVerein = item.SperreVerein;
                    k.Steuernummer = item.Steuernummer;
                    k.SVNummer = item.SVNummer;
                    k.Tel = item.Tel;
                    k.Trainer = item.Trainer;
                    k.TrainerLizenz = item.TrainerLizenz;
                    k.TS_DSGV = item.TS_DSGV;
                    k.Typ = item.Typ;
                    k.User_DSGV = item.User_DSGV;
                    k.Verband = item.Verband;
                    k.VerbandGueltigBis = item.VerbandGueltigBis;
                    k.VerbandLizenz = item.VerbandLizenz;
                    k.VerbandNation = item.VerbandNation;
                    k.VerbandVerein = item.VerbandVerein;
                    k.VName = item.VName;
                    k.Wert = item.Wert;
                    k.WertAbend = item.WertAbend;
                    k.Guid = new Guid(item.Guid);
                    lstKunden.Add(k);
                }

                return lstKunden;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Kunden> UpdateGuthabenAsync(string kdnr, decimal guthaben)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var ppe = await UOW.Uow._uow.Query<kunden>().Where(x => x.KdNr == kdnr).FirstOrDefaultAsync();
                if (ppe != null)
                {
                    ppe.Guthaben -= guthaben;
                    await UOW.Uow.SaveAsync();
                }

                return await Kunden.GetByIdAsync(ppe.ID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateFromBankImportAsync(int id, string begruendung, bool sperre, decimal wert, decimal preisAbend, decimal preis, string iban)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var kunde = await UOW.Uow._uow.Query<kunden>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (kunde != null)
                {
                    kunde.Begruendung = begruendung;
                    kunde.Sperre = sperre;
                    kunde.Wert = preis;
                    kunde.WertAbend = preisAbend;
                    kunde.Guthaben += wert;

                    if (!string.IsNullOrEmpty(iban))
                    {
                        kunde.IBAN = iban;
                    }

                    await UOW.Uow.SaveAsync();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        public static async Task UpdateMahnungMailAsync(string kdnr)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<kunden>().Where(x => x.KdNr == kdnr).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.Mahnungsmail = true;
                    result.DatumMahnungsmail = DateTime.Now;
                    await UOW.Uow.SaveAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdatePreiseAsync(decimal wert, decimal wertAbend, int preiseId)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<kunden>().Where(x => x.PreiseID == preiseId).ToListAsync();
                foreach (var item in result)
                {
                    item.Wert = wert;
                    item.WertAbend = wertAbend;
                }
                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAutosperreAsync(string kdnr, bool sperre, string begruendung, decimal? wert = null, decimal? wertAbend = null)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var resultkunde = await UOW.Uow._uow.Query<kunden>().Where(x => x.KdNr == kdnr).FirstOrDefaultAsync();

                resultkunde.Sperre = sperre;
                resultkunde.Begruendung = begruendung;
                if (wert != null)
                    resultkunde.Wert = wert;
                if (wertAbend != null)
                    resultkunde.WertAbend = wertAbend;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddAsync(string kdnr, string vname, string nname, string adresse, string plz, string ort, string tel, string email, string verband, string eklnr,
            string geburtsdatum, string typ, decimal? wert, bool block, bool sperre, string begruendung, string info1, string info2, string info3, string info4,
            string info5, string kuerklasse, bool freigabeVerein, DateTime? datumFreigabe, string freigabeInfo1, string freigabeInfo2, string freigabeInfo3,
            bool aktiv, string passwort, string iban, string trainer, int? preiseId, string lizenzNr, bool sperreVerein, string verbandNation, string verbandLizenz,
            string verbandVerein, DateTime? verbandGueltigBis, string svNummer, string steuernummer, string arbeitsbewilligung, bool kaestchenERS, string kaestchenERSNr,
            bool kaestchenESH, string kaestchenESHNr, decimal wertAbend, bool dsgvo, DateTime? tsDsgvo, string userDsgvo, bool karte, string geschlecht, bool covidOk,
            bool parkplatzERS, string trainerlizenz, bool eiszeitenESHERS, bool saveImmediatly)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var kunde = new kunden(UOW.Uow._uow);
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
                kunde.KaestchenEisring = kaestchenERS;
                kunde.KaestchenEisringNr = kaestchenERSNr;
                kunde.KaestchenStadthalle = kaestchenESH;
                kunde.KaestchenStadthalleNr = kaestchenESHNr;
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
                kunde.Guthaben = 0;
                kunde.Guid = System.Guid.NewGuid().ToString();
                if (saveImmediatly)
                {
                    await UOW.Uow.SaveAsync();
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
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var kunde = new kunden(UOW.Uow._uow);
                kunde.KdNr = kdnr;
                kunde.VName = vname;
                kunde.NName = nname;
                kunde.Adresse = adresse;
                kunde.PLZ = plz;
                kunde.Ort = ort;
                kunde.Tel = tel;
                kunde.EMail = email;
                kunde.Geburtsdatum = geburtsdatum;
                kunde.Guid = Guid.NewGuid().ToString();
                if (saveImmediatly)
                {
                    await UOW.Uow.SaveAsync();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAsync(int id, string kdnr, string vname, string nname, string adresse, string plz, string ort, string tel, string email, string verband, string eklnr,
            string geburtsdatum, string typ, decimal? wert, bool block, bool sperre, string begruendung, string info1, string info2, string info3, string info4,
            string info5, string kuerklasse, bool freigabeVerein, DateTime? datumFreigabe, string freigabeInfo1, string freigabeInfo2, string freigabeInfo3,
            bool aktiv, string passwort, string iban, string trainer, int? preiseId, string lizenzNr, bool sperreVerein, string verbandNation, string verbandLizenz,
            string verbandVerein, DateTime? verbandGueltigBis, string svNummer, string steuernummer, string arbeitsbewilligung, bool kaestchenERS, string kaestchenERSNr,
            bool kaestchenESH, string kaestchenESHNr, decimal wertAbend, bool dsgvo, DateTime? tsDsgvo, string userDsgvo, bool karte, string geschlecht, bool covidOk,
            bool parkplatzERS, string trainerlizenz, bool eiszeitenESHERS, bool saveImmediatly)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var kunde = await UOW.Uow._uow.Query<kunden>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (kunde != null)
                {
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
                    kunde.KaestchenEisring = kaestchenERS;
                    kunde.KaestchenEisringNr = kaestchenERSNr;
                    kunde.KaestchenStadthalle = kaestchenESH;
                    kunde.KaestchenStadthalleNr = kaestchenESHNr;
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

                    if (saveImmediatly)
                    {
                        await UOW.Uow.SaveAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAsync(int id, Kunden kundeeshers, bool saveImmediatly)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var kunde = await UOW.Uow._uow.Query<kunden>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (kunde != null)
                {
                    kunde.KdNr = kundeeshers.KdNr;
                    kunde.VName = kundeeshers.VName;
                    kunde.NName = kundeeshers.NName;
                    kunde.Adresse = kundeeshers.Adresse;
                    kunde.PLZ = kundeeshers.PLZ;
                    kunde.Ort = kundeeshers.Ort;
                    kunde.Tel = kundeeshers.Tel;
                    kunde.EMail = kundeeshers.EMail;
                    kunde.Verband = kundeeshers.Verband;
                    kunde.EKLNr = kundeeshers.EKLNr;
                    kunde.Geburtsdatum = kundeeshers.Geburtsdatum;
                    kunde.Typ = kundeeshers.Typ;
                    kunde.Wert = kundeeshers.Wert;
                    kunde.Block = kundeeshers.Block;
                    kunde.Sperre = kundeeshers.Sperre;
                    kunde.Begruendung = kundeeshers.Begruendung;
                    kunde.Info1 = kundeeshers.Info1;
                    kunde.Info2 = kundeeshers.Info2;
                    kunde.Info3 = kundeeshers.Info3;
                    kunde.Info4 = kundeeshers.Info4;
                    kunde.Info5 = kundeeshers.Info5;
                    kunde.Kuerklasse = kundeeshers.Kuerklasse;
                    kunde.FreigabeVerein = kundeeshers.FreigabeVerein;
                    kunde.DatumFreigabe = kundeeshers.DatumFreigabe;
                    kunde.FreigabeInfo1 = kundeeshers.FreigabeInfo1;
                    kunde.FreigabeInfo2 = kundeeshers.FreigabeInfo2;
                    kunde.FreigabeInfo3 = kundeeshers.FreigabeInfo3;
                    kunde.Aktiv = kundeeshers.Aktiv;
                    kunde.Passwort = kundeeshers.Passwort;
                    kunde.IBAN = kundeeshers.IBAN;
                    kunde.Trainer = kundeeshers.Trainer;
                    kunde.PreiseID = kundeeshers.PreiseID;
                    kunde.LizenzNr = kundeeshers.LizenzNr;
                    kunde.SperreVerein = kundeeshers.SperreVerein;
                    kunde.VerbandNation = kundeeshers.VerbandNation;
                    kunde.VerbandLizenz = kundeeshers.VerbandLizenz;
                    kunde.VerbandVerein = kundeeshers.VerbandVerein;
                    kunde.VerbandGueltigBis = kundeeshers.VerbandGueltigBis;
                    kunde.SVNummer = kundeeshers.SVNummer;
                    kunde.Steuernummer = kundeeshers.Steuernummer;
                    kunde.Arbeitsbewilligung = kundeeshers.Arbeitsbewilligung;
                    kunde.KaestchenEisring = kundeeshers.KaestchenEisring;
                    kunde.KaestchenEisringNr = kundeeshers.KaestchenEisringNr;
                    kunde.KaestchenStadthalle = kundeeshers.KaestchenStadthalle;
                    kunde.KaestchenStadthalleNr = kundeeshers.KaestchenStadthalleNr;
                    kunde.WertAbend = kundeeshers.WertAbend;
                    kunde.DSGVO = kundeeshers.DSGVO;
                    kunde.TS_DSGV = kundeeshers.TS_DSGV;
                    kunde.User_DSGV = kundeeshers.User_DSGV;
                    kunde.Karte = kundeeshers.Karte;
                    kunde.Geschlecht = kundeeshers.Geschlecht;
                    kunde.COVIDOK = kundeeshers.COVIDOK;
                    kunde.ParkplatzEisring = kundeeshers.ParkplatzEisring;
                    kunde.TrainerLizenz = kundeeshers.TrainerLizenz;
                    kunde.EiszeitenESHERS = kundeeshers.EiszeitenESHERS;

                    if (saveImmediatly)
                    {
                        await UOW.Uow.SaveAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAsync(int id, decimal guthaben, bool block, bool saveImmediatly)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var kunde = await UOW.Uow._uow.Query<kunden>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (kunde != null)
                {
                    kunde.Guthaben += guthaben;
                    kunde.Block = block;

                    if (saveImmediatly)
                    {
                        await UOW.Uow.SaveAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAsync(int id, string begruendung, bool sperre, bool saveImmediatly)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var kunde = await UOW.Uow._uow.Query<kunden>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (kunde != null)
                {
                    kunde.Begruendung = begruendung;
                    kunde.Sperre = sperre;

                    if (saveImmediatly)
                    {
                        await UOW.Uow.SaveAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task DeleteAsync(int id, bool saveImmediatly)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var kunde = await UOW.Uow._uow.Query<kunden>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (kunde != null)
                {
                    await UOW.Uow._uow.DeleteAsync(kunde);

                    if (saveImmediatly)
                    {
                        await UOW.Uow.SaveAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Kunden> GetByNameAsync(string vorname, string nachname)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
                var item = await UOW.Uow._uow.Query<kunden>().Where(x => x.VName == vorname && x.NName == nachname).FirstOrDefaultAsync();
                if (item == null)
                    return null;

                Kunden k = new Kunden();
                k.Adresse = item.Adresse;
                k.Aktiv = item.Aktiv;
                k.Arbeitsbewilligung = item.Arbeitsbewilligung;
                k.Begruendung = item.Begruendung;
                k.Block = item.Block;
                k.COVIDOK = item.COVIDOK;
                k.DatumFreigabe = item.DatumFreigabe;
                k.DatumMahnungsmail = item.DatumMahnungsmail;
                k.DSGVO = item.DSGVO;
                k.EiszeitenESHERS = item.EiszeitenESHERS;
                k.EKLNr = item.EKLNr;
                k.EMail = item.EMail;
                k.FreigabeInfo1 = item.FreigabeInfo1;
                k.FreigabeInfo2 = item.FreigabeInfo2;
                k.FreigabeInfo3 = item.FreigabeInfo3;
                k.FreigabeVerein = item.FreigabeVerein;
                k.Geburtsdatum = item.Geburtsdatum;
                k.Geschlecht = item.Geschlecht;
                k.Guthaben = item.Guthaben;
                k.IBAN = item.IBAN;
                k.ID = item.ID;
                k.Info1 = item.Info1;
                k.Info2 = item.Info2;
                k.Info3 = item.Info3;
                k.Info4 = item.Info4;
                k.Info5 = item.Info5;
                k.KaestchenEisring = item.KaestchenEisring;
                k.KaestchenEisringNr = item.KaestchenEisringNr;
                k.KaestchenStadthalle = item.KaestchenStadthalle;
                k.KaestchenStadthalleNr = item.KaestchenStadthalleNr;
                k.Karte = item.Karte;
                k.KdNr = item.KdNr;
                k.Kuerklasse = item.Kuerklasse;
                k.LizenzNr = item.LizenzNr;
                k.Mahnungsmail = item.Mahnungsmail;
                k.NName = item.NName;
                k.Ort = item.Ort;
                k.ParkplatzEisring = item.ParkplatzEisring;
                k.Passwort = item.Passwort;
                k.PLZ = item.PLZ;
                k.PreiseID = item.PreiseID;
                k.Sperre = item.Sperre;
                k.SperreVerein = item.SperreVerein;
                k.Steuernummer = item.Steuernummer;
                k.SVNummer = item.SVNummer;
                k.Tel = item.Tel;
                k.Trainer = item.Trainer;
                k.TrainerLizenz = item.TrainerLizenz;
                k.TS_DSGV = item.TS_DSGV;
                k.Typ = item.Typ;
                k.User_DSGV = item.User_DSGV;
                k.Verband = item.Verband;
                k.VerbandGueltigBis = item.VerbandGueltigBis;
                k.VerbandLizenz = item.VerbandLizenz;
                k.VerbandNation = item.VerbandNation;
                k.VerbandVerein = item.VerbandVerein;
                k.VName = item.VName;
                k.Wert = item.Wert;
                k.WertAbend = item.WertAbend;
                k.Guid = new Guid(item.Guid);

                return k;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
