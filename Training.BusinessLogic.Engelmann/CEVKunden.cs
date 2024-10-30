using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;


namespace Training.BusinessLogic.Engelmann
{
    public class CEVKunden
    {
        public int ID { get; set; }
        public string Mitgliedsnummer { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public string GebDatum { get; set; }
        public string Adresse { get; set; }
        public string Land { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Tel { get; set; }
        public string Tel2 { get; set; }
        public string EMail { get; set; }
        public string Kaestchen { get; set; }
        public string Zusatzinfo { get; set; }
        public bool SaisonkarteEngelmann { get; set; }
        public bool Ehrenkarte { get; set; }
        public string Zusatzinfo2 { get; set; }
        public string Zusatzinfo3 { get; set; }
        public string Zusatzinfo4 { get; set; }
        public string Zusatzinfo5 { get; set; }
        public byte[] Foto { get; set; }
        public string MitgliedsnummerAlt { get; set; }
        public string KundennummerEisring { get; set; }
        public bool KundeEisring { get; set; }
        public string TrainerNr { get; set; }
        public string Trainer { get; set; }
        public string MitgliedSeit { get; set; }
        public bool Abmeldung { get; set; }
        public bool Anmeldung { get; set; }
        public string AbmeldungMit { get; set; }
        public string Zugehoerigkeit { get; set; }
        public string Staatsangehoerigkeit { get; set; }
        public string Lizenznummer { get; set; }
        public string IBAN { get; set; }
        public string VerbandNation { get; set; }
        public string VerbandLizenz { get; set; }
        public string VerbandGueltigBis { get; set; }
        public string VerbandVerein { get; set; }
        public string Typ { get; set; }
        public bool? MBVerrechnung { get; set; }
        public int? MBGebucht { get; set; }
        public bool? DSGV { get; set; }
        public DateTime? TS_DSGV { get; set; }
        public string User_DSGV { get; set; }
        public bool? NurKaestchen { get; set; }
        public bool HasFoto { get; set; }
        public bool Auswahl { get; set; }

        public static async Task<int> GetIdByNameAsync(string nachname, string vorname)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevkunden>().Where(x => x.Nachname == nachname && x.Vorname == vorname).FirstOrDefaultAsync();
                if (result != null)
                {
                    return result.ID;
                }
                else
                    return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<List<CEVKunden>> GetAsync()
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow
                    .ExecuteSprocAsync(new System.Threading.CancellationToken(), "GET_CEV_KUNDEN");

                if (result == null)
                {
                    return null;
                }

                var list = new List<CEVKunden>();

                var resultcev = result.ResultSet.FirstOrDefault();
                if(resultcev != null)
                {
                    foreach(var item in resultcev.Rows)
                    {
                        int? mbGebucht = null;
                        if(item.Values[40] != null)
                        {
                            mbGebucht = Convert.ToInt32(item.Values[40]);
                        }

                        bool? dsvg = null;
                        if (item.Values[41] != null)
                        {
                            dsvg = Convert.ToBoolean(item.Values[41]);
                        }

                        bool? nurKaestchen = null;
                        if (item.Values[42] != null)
                        {
                            nurKaestchen = Convert.ToBoolean(item.Values[42]);
                        }

                        var cev = new CEVKunden();

                        cev.Abmeldung = Convert.ToBoolean(item.Values[26]);
                        cev.ID = Convert.ToInt32(item.Values[0]);
                        cev.AbmeldungMit = item.Values[28] == null ? "" : item.Values[28].ToString();
                        cev.Adresse = item.Values[5] == null ? "" : item.Values[5].ToString();
                        cev.Anmeldung = Convert.ToBoolean(item.Values[27]);
                        cev.DSGV = dsvg;
                        cev.Ehrenkarte = Convert.ToBoolean(item.Values[15]);
                        cev.EMail = item.Values[11] == null ? "" : item.Values[11].ToString();
                        cev.GebDatum = item.Values[4] == null ? "" : item.Values[4].ToString();
                        cev.IBAN = item.Values[32] == null ? "" : item.Values[32].ToString();
                        cev.Kaestchen = item.Values[12] == null ? "" : item.Values[12].ToString();
                        cev.KundeEisring = Convert.ToBoolean(item.Values[22]);
                        cev.KundennummerEisring = item.Values[21] == null ? "" : item.Values[21].ToString();
                        cev.Land = item.Values[6] == null ? "" : item.Values[6].ToString();
                        cev.Lizenznummer = item.Values[31] == null ? "" : item.Values[31].ToString();
                        cev.MBGebucht = mbGebucht;
                        cev.MBVerrechnung = Convert.ToBoolean(item.Values[38]);
                        cev.MitgliedSeit = item.Values[25] == null ? "" : item.Values[25].ToString();
                        cev.Mitgliedsnummer = item.Values[1] == null ? "" : item.Values[1].ToString();
                        cev.MitgliedsnummerAlt = item.Values[20] == null ? "" : item.Values[20].ToString();
                        cev.Nachname = item.Values[2] == null ? "" : item.Values[2].ToString();
                        cev.NurKaestchen = nurKaestchen;
                        cev.Ort = item.Values[8] == null ? "" : item.Values[8].ToString();
                        cev.PLZ = item.Values[7] == null ? "" : item.Values[7].ToString();
                        cev.SaisonkarteEngelmann = Convert.ToBoolean(item.Values[14]);
                        cev.Staatsangehoerigkeit = item.Values[30] == null ? "" : item.Values[30].ToString();
                        cev.Tel = item.Values[9] == null ? "" : item.Values[9].ToString();
                        cev.Tel2 = item.Values[10] == null ? "" : item.Values[10].ToString();
                        cev.Trainer = item.Values[24] == null ? "" : item.Values[24].ToString();
                        cev.TrainerNr = item.Values[23] == null ? "" : item.Values[23].ToString();
                        cev.Typ = item.Values[37] == null ? "" : item.Values[37].ToString();
                        cev.VerbandGueltigBis = item.Values[36] == null ? "" : item.Values[36].ToString();
                        cev.VerbandLizenz = item.Values[34] == null ? "" : item.Values[34].ToString();
                        cev.VerbandNation = item.Values[33] == null ? "" : item.Values[33].ToString();
                        cev.VerbandVerein = item.Values[35] == null ? "" : item.Values[35].ToString();
                        cev.Vorname = item.Values[3] == null ? "" : item.Values[3].ToString();
                        cev.Zugehoerigkeit = item.Values[29] == null ? "" : item.Values[29].ToString();
                        cev.Zusatzinfo = item.Values[13] == null ? "" : item.Values[13].ToString();
                        cev.Zusatzinfo2 = item.Values[16] == null ? "" : item.Values[16].ToString();
                        cev.Zusatzinfo3 = item.Values[17] == null ? "" : item.Values[17].ToString();
                        cev.Zusatzinfo4 = item.Values[18] == null ? "" : item.Values[18].ToString();
                        cev.Zusatzinfo5 = item.Values[19] == null ? "" : item.Values[19].ToString();
                        cev.HasFoto = Convert.ToBoolean(item.Values[39]);

                        list.Add(cev);
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<byte[]> GetFotoAsync(string mitgliedsnummer)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevkunden>().Where(x => x.Mitgliedsnummer == mitgliedsnummer).FirstOrDefaultAsync();

                if (result == null)
                {
                    return null;
                }

                return result.Foto;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<string> GetNameByMitgliedsNummerAsync(string mitgliedsnummer)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevkunden>().Where(x => x.Mitgliedsnummer == mitgliedsnummer).FirstOrDefaultAsync();
                if (result != null)
                {
                    return $"{result.Vorname} {result.Nachname}";
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<CEVKunden> GetByMitgliedsNummerAsync(string mitgliedsnummer)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevkunden>().Where(x => x.Mitgliedsnummer == mitgliedsnummer).FirstOrDefaultAsync();
                if (result != null)
                {
                    return new CEVKunden
                    {
                        Abmeldung = result.Abmeldung,
                        ID = result.ID,
                        AbmeldungMit = result.AbmeldungMit,
                        Adresse = result.Adresse,
                        Anmeldung = result.Anmeldung,
                        DSGV = result.DSGV,
                        Ehrenkarte = result.Ehrenkarte,
                        EMail = result.EMail,
                        Foto = result.Foto,
                        GebDatum = result.GebDatum,
                        IBAN = result.IBAN,
                        Kaestchen = result.Kaestchen,
                        KundeEisring = result.KundeEisring,
                        KundennummerEisring = result.KundennummerEisring,
                        Land = result.Land,
                        Lizenznummer = result.Lizenznummer,
                        MBGebucht = result.MBGebucht,
                        MBVerrechnung = result.MBVerrechnung,
                        MitgliedSeit = result.MitgliedSeit,
                        Mitgliedsnummer = result.Mitgliedsnummer,
                        MitgliedsnummerAlt = result.MitgliedsnummerAlt,
                        Nachname = result.Nachname,
                        NurKaestchen = result.NurKaestchen,
                        Ort = result.Ort,
                        PLZ = result.PLZ,
                        SaisonkarteEngelmann = result.SaisonkarteEngelmann,
                        Staatsangehoerigkeit = result.Staatsangehoerigkeit,
                        Tel = result.Tel,
                        Tel2 = result.Tel2,
                        Trainer = result.Trainer,
                        TrainerNr = result.TrainerNr,
                        TS_DSGV = result.TS_DSGV,
                        Typ = result.Typ,
                        User_DSGV = result.User_DSGV,
                        VerbandGueltigBis = result.VerbandGueltigBis,
                        VerbandLizenz = result.VerbandLizenz,
                        VerbandNation = result.VerbandNation,
                        VerbandVerein = result.VerbandVerein,
                        Vorname = result.Vorname,
                        Zugehoerigkeit = result.Zugehoerigkeit,
                        Zusatzinfo = result.Zusatzinfo,
                        Zusatzinfo2 = result.Zusatzinfo2,
                        Zusatzinfo3 = result.Zusatzinfo3,
                        Zusatzinfo4 = result.Zusatzinfo4,
                        Zusatzinfo5 = result.Zusatzinfo5,
                    };
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<CEVKunden> GetForBankAsync(string n, string o, string p, string q, string iban)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevkunden>()
                    .Where(x => x.Mitgliedsnummer == n 
                                    || x.Mitgliedsnummer == o
                                    || x.Mitgliedsnummer == p
                                    || x.Mitgliedsnummer == q)
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    if (!string.IsNullOrEmpty(iban))
                    {
                        result = await UOW.UOW.uow.Query<cevkunden>().Where(x => x.IBAN == iban).FirstOrDefaultAsync();
                        if (result == null)
                        {
                            return null;
                        }
                    }
                    else 
                    {
                        return null;
                    }
                }

                return new CEVKunden
                {
                    Abmeldung = result.Abmeldung,
                    ID = result.ID,
                    AbmeldungMit = result.AbmeldungMit,
                    Adresse = result.Adresse,
                    Anmeldung = result.Anmeldung,
                    DSGV = result.DSGV,
                    Ehrenkarte = result.Ehrenkarte,
                    EMail = result.EMail,
                    Foto = result.Foto,
                    GebDatum = result.GebDatum,
                    IBAN = result.IBAN,
                    Kaestchen = result.Kaestchen,
                    KundeEisring = result.KundeEisring,
                    KundennummerEisring = result.KundennummerEisring,
                    Land = result.Land,
                    Lizenznummer = result.Lizenznummer,
                    MBGebucht = result.MBGebucht,
                    MBVerrechnung = result.MBVerrechnung,
                    MitgliedSeit = result.MitgliedSeit,
                    Mitgliedsnummer = result.Mitgliedsnummer,
                    MitgliedsnummerAlt = result.MitgliedsnummerAlt,
                    Nachname = result.Nachname,
                    NurKaestchen = result.NurKaestchen,
                    Ort = result.Ort,
                    PLZ = result.PLZ,
                    SaisonkarteEngelmann = result.SaisonkarteEngelmann,
                    Staatsangehoerigkeit = result.Staatsangehoerigkeit,
                    Tel = result.Tel,
                    Tel2 = result.Tel2,
                    Trainer = result.Trainer,
                    TrainerNr = result.TrainerNr,
                    TS_DSGV = result.TS_DSGV,
                    Typ = result.Typ,
                    User_DSGV = result.User_DSGV,
                    VerbandGueltigBis = result.VerbandGueltigBis,
                    VerbandLizenz = result.VerbandLizenz,
                    VerbandNation = result.VerbandNation,
                    VerbandVerein = result.VerbandVerein,
                    Vorname = result.Vorname,
                    Zugehoerigkeit = result.Zugehoerigkeit,
                    Zusatzinfo = result.Zusatzinfo,
                    Zusatzinfo2 = result.Zusatzinfo2,
                    Zusatzinfo3 = result.Zusatzinfo3,
                    Zusatzinfo4 = result.Zusatzinfo4,
                    Zusatzinfo5 = result.Zusatzinfo5,
                };

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<CEVKunden> GetForBankAsync(string mitgliedsnummer, string zg, string zr, string zz, string ag, string iban)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevkunden>()
                    .Where(x => x.Mitgliedsnummer == mitgliedsnummer
                                    || x.Mitgliedsnummer == zg
                                    || x.Mitgliedsnummer == zr
                                    || x.Mitgliedsnummer == zz
                                    || x.Mitgliedsnummer == ag)
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    if (!string.IsNullOrEmpty(iban))
                    {
                        result = await UOW.UOW.uow.Query<cevkunden>().Where(x => x.IBAN == iban).FirstOrDefaultAsync();
                        if (result == null)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }

                return new CEVKunden
                {
                    Abmeldung = result.Abmeldung,
                    ID = result.ID,
                    AbmeldungMit = result.AbmeldungMit,
                    Adresse = result.Adresse,
                    Anmeldung = result.Anmeldung,
                    DSGV = result.DSGV,
                    Ehrenkarte = result.Ehrenkarte,
                    EMail = result.EMail,
                    Foto = result.Foto,
                    GebDatum = result.GebDatum,
                    IBAN = result.IBAN,
                    Kaestchen = result.Kaestchen,
                    KundeEisring = result.KundeEisring,
                    KundennummerEisring = result.KundennummerEisring,
                    Land = result.Land,
                    Lizenznummer = result.Lizenznummer,
                    MBGebucht = result.MBGebucht,
                    MBVerrechnung = result.MBVerrechnung,
                    MitgliedSeit = result.MitgliedSeit,
                    Mitgliedsnummer = result.Mitgliedsnummer,
                    MitgliedsnummerAlt = result.MitgliedsnummerAlt,
                    Nachname = result.Nachname,
                    NurKaestchen = result.NurKaestchen,
                    Ort = result.Ort,
                    PLZ = result.PLZ,
                    SaisonkarteEngelmann = result.SaisonkarteEngelmann,
                    Staatsangehoerigkeit = result.Staatsangehoerigkeit,
                    Tel = result.Tel,
                    Tel2 = result.Tel2,
                    Trainer = result.Trainer,
                    TrainerNr = result.TrainerNr,
                    TS_DSGV = result.TS_DSGV,
                    Typ = result.Typ,
                    User_DSGV = result.User_DSGV,
                    VerbandGueltigBis = result.VerbandGueltigBis,
                    VerbandLizenz = result.VerbandLizenz,
                    VerbandNation = result.VerbandNation,
                    VerbandVerein = result.VerbandVerein,
                    Vorname = result.Vorname,
                    Zugehoerigkeit = result.Zugehoerigkeit,
                    Zusatzinfo = result.Zusatzinfo,
                    Zusatzinfo2 = result.Zusatzinfo2,
                    Zusatzinfo3 = result.Zusatzinfo3,
                    Zusatzinfo4 = result.Zusatzinfo4,
                    Zusatzinfo5 = result.Zusatzinfo5,
                };

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task UpdateFromBankImportAsync(int id, string zusatzinfo, string iban, bool saveImmediatly)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevkunden>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.Zusatzinfo5 = zusatzinfo;
                    if (!string.IsNullOrEmpty(iban))
                    {
                        result.IBAN = iban;
                    }

                    if (saveImmediatly)
                    {
                        await UOW.UOW.SaveAsync();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task UpdateFromESHERSAsync(int id, string adresse, string plz, string ort, string tel, string email, string gebdatum, string trainer, string kundennummerESHERS, bool kundeESHERS,
            string iban, string lizenznummer, string verbandnation, string verbandlizenz, string verbandverein, string verbandgueltigbis, bool saveImmediatly)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevkunden>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.Adresse = adresse;
                    result.PLZ = plz;
                    result.Ort = ort;
                    result.Tel = tel;
                    result.EMail = email;
                    result.GebDatum = gebdatum;
                    result.Trainer = trainer;
                    result.KundennummerEisring = kundennummerESHERS;
                    result.KundeEisring = kundeESHERS;
                    result.IBAN = iban;
                    result.Lizenznummer = lizenznummer;
                    result.VerbandNation = verbandnation;
                    result.VerbandLizenz = verbandlizenz;
                    result.VerbandVerein = verbandverein;
                    result.VerbandGueltigBis = verbandgueltigbis;

                    if (saveImmediatly)
                    {
                        await UOW.UOW.SaveAsync();
                    }
                }
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
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevkunden>().CountAsync();
                return result;
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
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                short KdNr = 10000;

                var count = await CountAsync();
                if (count > 0)
                {
                    var result = await UOW.UOW.uow.Query<cevkunden>().Select(x => x.Mitgliedsnummer).MaxAsync();
                    if (result != null)
                    {
                        KdNr = Convert.ToInt16(result);
                    }
                }
                KdNr += 1;
                return KdNr;
            }
            catch
            {
                throw;
            }
        }

        public static async Task ImportAsync(string mitgliedsnummer, string nachname, string vorname, string adresse, string plz, string ort, string land, string gebDatum,
            bool ehrenkarte, string tel, string tel2, string trainerNr, string trainer, string mitgliedSeit, bool anmeldung, bool abmeldung, string abmeldungmit,
            string lizenzNr, string zusatzInfo, string zugehoerigkeit, string email, string staatszugehoerigkeit, bool saveImmediatly)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var old = await UOW.UOW.uow.Query<cevkunden>().Where(x => x.Mitgliedsnummer == mitgliedsnummer).FirstOrDefaultAsync();
                if (old == null)
                {
                    bool KundeESHERS = false;
                    string KdNrESHERS = string.Empty;

                    var kunde = await Kunden.Kunden.GetByName(vorname, nachname);
                    if (kunde != null)
                    {
                        KundeESHERS = true;
                        KdNrESHERS = kunde.KdNr;
                    }
                    cevkunden cev = new cevkunden(UOW.UOW.uow);
                    cev.Mitgliedsnummer = mitgliedsnummer;
                    cev.KundennummerEisring = KdNrESHERS;
                    cev.Nachname = nachname;
                    cev.Vorname = vorname;
                    cev.Adresse = adresse;
                    cev.Land = land;
                    cev.PLZ = plz;
                    cev.Ort = ort;
                    cev.GebDatum = gebDatum;
                    cev.Ehrenkarte = ehrenkarte;
                    cev.Tel = tel;
                    cev.Tel2 = tel2;
                    cev.TrainerNr = trainerNr;
                    cev.Trainer = trainer;
                    cev.MitgliedSeit = mitgliedSeit;
                    cev.Anmeldung = anmeldung;
                    cev.Abmeldung = abmeldung;
                    cev.AbmeldungMit = abmeldungmit;
                    cev.Lizenznummer = lizenzNr;
                    cev.Zusatzinfo = zusatzInfo;
                    cev.Zugehoerigkeit = zugehoerigkeit;
                    cev.EMail = email;
                    cev.KundeEisring = KundeESHERS;
                    cev.Staatsangehoerigkeit = staatszugehoerigkeit;

                    if (saveImmediatly)
                    {
                        await UOW.UOW.SaveAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateMBGebuchtAsync(string mitgliedsnummer)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevkunden>().Where(x => x.Mitgliedsnummer == mitgliedsnummer).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.MBGebucht = null;
                }

                await UOW.UOW.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateMBGebuchtAsync(string mitgliedsnummer, int saison)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevkunden>().Where(x => x.Mitgliedsnummer == mitgliedsnummer).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.MBGebucht = saison;
                    result.MBVerrechnung = true;
                }

                await UOW.UOW.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddAsync(CEVKunden cevKunde)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                cevkunden cevkunden = new cevkunden(UOW.UOW.uow)
                {
                    Abmeldung = cevKunde.Abmeldung,
                    AbmeldungMit = cevKunde.AbmeldungMit,
                    Adresse = cevKunde.Adresse,
                    Anmeldung = cevKunde.Anmeldung,
                    DSGV = cevKunde.DSGV,
                    Ehrenkarte = cevKunde.Ehrenkarte,
                    EMail = cevKunde.EMail,
                    GebDatum = cevKunde.GebDatum,
                    IBAN = cevKunde.IBAN,
                    Kaestchen = cevKunde.Kaestchen,
                    KundeEisring = cevKunde.KundeEisring,
                    KundennummerEisring = cevKunde.KundennummerEisring,
                    Land = cevKunde.Land,
                    Lizenznummer = cevKunde.Lizenznummer,
                    MBGebucht = cevKunde.MBGebucht,
                    MBVerrechnung = cevKunde.MBVerrechnung,
                    MitgliedSeit = cevKunde.MitgliedSeit,
                    Mitgliedsnummer = cevKunde.Mitgliedsnummer,
                    MitgliedsnummerAlt = cevKunde.MitgliedsnummerAlt,
                    Nachname = cevKunde.Nachname,
                    NurKaestchen = cevKunde.NurKaestchen,
                    Ort = cevKunde.Ort,
                    PLZ = cevKunde.PLZ,
                    SaisonkarteEngelmann = cevKunde.SaisonkarteEngelmann,
                    Staatsangehoerigkeit = cevKunde.Staatsangehoerigkeit,
                    Tel = cevKunde.Tel,
                    Tel2 = cevKunde.Tel2,
                    Trainer = cevKunde.Trainer,
                    TrainerNr = cevKunde.TrainerNr,
                    TS_DSGV = cevKunde.TS_DSGV,
                    Typ = cevKunde.Typ,
                    User_DSGV = cevKunde.User_DSGV,
                    VerbandGueltigBis = cevKunde.VerbandGueltigBis,
                    VerbandLizenz = cevKunde.VerbandLizenz,
                    VerbandNation = cevKunde.VerbandNation,
                    VerbandVerein = cevKunde.VerbandVerein,
                    Vorname = cevKunde.Vorname,
                    Zugehoerigkeit = cevKunde.Zugehoerigkeit,
                    Zusatzinfo = cevKunde.Zusatzinfo,
                    Zusatzinfo2 = cevKunde.Zusatzinfo2,
                    Zusatzinfo3 = cevKunde.Zusatzinfo3,
                    Zusatzinfo4 = cevKunde.Zusatzinfo4,
                    Zusatzinfo5 = cevKunde.Zusatzinfo5,
                    Foto = cevKunde.Foto,
                };

                await UOW.UOW.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAsync(int id, CEVKunden cevKunde)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow
                    .Query<cevkunden>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return;
                }

                result.Abmeldung = cevKunde.Abmeldung;
                result.AbmeldungMit = cevKunde.AbmeldungMit;
                result.Adresse = cevKunde.Adresse;
                result.Anmeldung = cevKunde.Anmeldung;
                result.DSGV = cevKunde.DSGV;
                result.Ehrenkarte = cevKunde.Ehrenkarte;
                result.EMail = cevKunde.EMail;
                result.GebDatum = cevKunde.GebDatum;
                result.IBAN = cevKunde.IBAN;
                result.Kaestchen = cevKunde.Kaestchen;
                result.KundeEisring = cevKunde.KundeEisring;
                result.KundennummerEisring = cevKunde.KundennummerEisring;
                result.Land = cevKunde.Land;
                result.Lizenznummer = cevKunde.Lizenznummer;
                result.MBGebucht = cevKunde.MBGebucht;
                result.MBVerrechnung = cevKunde.MBVerrechnung;
                result.MitgliedSeit = cevKunde.MitgliedSeit;
                result.Mitgliedsnummer = cevKunde.Mitgliedsnummer;
                result.MitgliedsnummerAlt = cevKunde.MitgliedsnummerAlt;
                result.Nachname = cevKunde.Nachname;
                result.NurKaestchen = cevKunde.NurKaestchen;
                result.Ort = cevKunde.Ort;
                result.PLZ = cevKunde.PLZ;
                result.SaisonkarteEngelmann = cevKunde.SaisonkarteEngelmann;
                result.Staatsangehoerigkeit = cevKunde.Staatsangehoerigkeit;
                result.Tel = cevKunde.Tel;
                result.Tel2 = cevKunde.Tel2;
                result.Trainer = cevKunde.Trainer;
                result.TrainerNr = cevKunde.TrainerNr;
                result.TS_DSGV = cevKunde.TS_DSGV;
                result.Typ = cevKunde.Typ;
                result.User_DSGV = cevKunde.User_DSGV;
                result.VerbandGueltigBis = cevKunde.VerbandGueltigBis;
                result.VerbandLizenz = cevKunde.VerbandLizenz;
                result.VerbandNation = cevKunde.VerbandNation;
                result.VerbandVerein = cevKunde.VerbandVerein;
                result.Vorname = cevKunde.Vorname;
                result.Zugehoerigkeit = cevKunde.Zugehoerigkeit;
                result.Zusatzinfo = cevKunde.Zusatzinfo;
                result.Zusatzinfo2 = cevKunde.Zusatzinfo2;
                result.Zusatzinfo3 = cevKunde.Zusatzinfo3;
                result.Zusatzinfo4 = cevKunde.Zusatzinfo4;
                result.Zusatzinfo5 = cevKunde.Zusatzinfo5;
                result.Foto = cevKunde.Foto;

                await UOW.UOW.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow
                    .Query<cevkunden>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return;
                }

                await UOW.UOW.DeleteAsync(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
