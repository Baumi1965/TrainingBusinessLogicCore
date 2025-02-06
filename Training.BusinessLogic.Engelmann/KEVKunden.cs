using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Engelmann
{
    public class KEVKunden
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
        public string EMail { get; set; }
        public string Kaestchen { get; set; }
        public string Zusatzinfo { get; set; }
        public bool? Halbjahreskarte { get; set; }
        public bool? Ehrenkarte { get; set; }
        public bool? Ordner { get; set; }
        public string Zusatzinfo2 { get; set; }
        public string Zusatzinfo3 { get; set; }
        public string Zusatzinfo4 { get; set; }
        public string Zusatzinfo5 { get; set; }
        public byte[] Foto { get; set; }
        public string KundennummerEisring { get; set; }
        public bool? KundeEisring { get; set; }
        public string MitgliedsnummerAlt { get; set; }
        public bool? DSGV { get; set; }
        public DateTime? TS_DSGV { get; set; }
        public string User_DSGV { get; set; }
        public bool? NurKaestchen { get; set; }
        public int? Saison { get; set; }
        public bool Auswahl { get; set; }
        public bool HasFoto { get; set; }

        public static async Task<List<KEVKunden>> GetAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow
                    .ExecuteSprocAsync(new System.Threading.CancellationToken(), "GET_KEV_KUNDEN");

                if (result == null)
                {
                    return null;
                }

                var list = new List<KEVKunden>();
                var resultcev = result.ResultSet.FirstOrDefault();
                if (resultcev != null)
                {
                    foreach (var item in resultcev.Rows)
                    {
                        bool? dsvg = null;
                        if (item.Values[24] != null)
                        {
                            dsvg = Convert.ToBoolean(item.Values[24]);
                        }

                        bool? nurKaestchen = null;
                        if (item.Values[25] != null)
                        {
                            nurKaestchen = Convert.ToBoolean(item.Values[25]);
                        }

                        int? saison = null;
                        if (item.Values[26] != null)
                        {
                            saison = Convert.ToInt32(item.Values[26]);
                        }

                        var kev = new KEVKunden();

                        kev.ID = Convert.ToInt32(item.Values[0]);
                        kev.Mitgliedsnummer = item.Values[1] == null ? "" : item.Values[1].ToString();
                        kev.Nachname = item.Values[2] == null ? "" : item.Values[2].ToString();
                        kev.Vorname = item.Values[3] == null ? "" : item.Values[3].ToString();
                        kev.GebDatum = item.Values[4] == null ? "" : item.Values[4].ToString();
                        kev.Adresse = item.Values[5] == null ? "" : item.Values[5].ToString();
                        kev.Land = item.Values[6] == null ? "" : item.Values[6].ToString();
                        kev.PLZ = item.Values[7] == null ? "" : item.Values[7].ToString();
                        kev.Ort = item.Values[8] == null ? "" : item.Values[8].ToString();
                        kev.Tel = item.Values[9] == null ? "" : item.Values[9].ToString();
                        kev.EMail = item.Values[10] == null ? "" : item.Values[10].ToString();
                        kev.Kaestchen = item.Values[11] == null ? "" : item.Values[11].ToString();
                        kev.Zusatzinfo = item.Values[12] == null ? "" : item.Values[12].ToString();
                        kev.Halbjahreskarte = Convert.ToBoolean(item.Values[13]);
                        kev.Ehrenkarte = Convert.ToBoolean(item.Values[14]);
                        kev.Ordner = Convert.ToBoolean(item.Values[15]);
                        kev.Zusatzinfo2 = item.Values[16] == null ? "" : item.Values[16].ToString();
                        kev.Zusatzinfo3 = item.Values[17] == null ? "" : item.Values[17].ToString();
                        kev.Zusatzinfo4 = item.Values[18] == null ? "" : item.Values[18].ToString();
                        kev.Zusatzinfo5 = item.Values[19] == null ? "" : item.Values[19].ToString();
                        kev.HasFoto = Convert.ToBoolean(item.Values[20]);
                        kev.KundennummerEisring = item.Values[21] == null ? "" : item.Values[21].ToString();
                        kev.MitgliedsnummerAlt = item.Values[22] == null ? "" : item.Values[22].ToString();
                        kev.KundeEisring = Convert.ToBoolean(item.Values[23]);
                        kev.DSGV = dsvg;
                        kev.NurKaestchen = nurKaestchen;
                        kev.Saison = saison;

                        list.Add(kev);
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
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<kevkunden>().Where(x => x.Mitgliedsnummer == mitgliedsnummer).FirstOrDefaultAsync();

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

        public static async Task<int> GetIdByNameAsync(string nachname, string vorname)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<kevkunden>().Where(x => x.Nachname == nachname && x.Vorname == vorname).FirstOrDefaultAsync();
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

        public static async Task<string> GetNameByMitgliedsNummerAsync(string mitgliedsnummer)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<kevkunden>().Where(x => x.Mitgliedsnummer == mitgliedsnummer).FirstOrDefaultAsync();
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

        public static async Task<KEVKunden> GetByMitgliedsNummerAsync(string mitgliedsnummer)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<kevkunden>().Where(x => x.Mitgliedsnummer == mitgliedsnummer).FirstOrDefaultAsync();
                if (result != null)
                {
                    return new KEVKunden
                    {
                        ID = result.ID,
                        Adresse = result.Adresse,
                        DSGV = result.DSGV,
                        Ehrenkarte = result.Ehrenkarte,
                        EMail = result.EMail,
                        Foto = result.Foto,
                        GebDatum = result.GebDatum,
                        Kaestchen = result.Kaestchen,
                        KundeEisring = result.KundeEisring,
                        KundennummerEisring = result.KundennummerEisring,
                        Land = result.Land,
                        Mitgliedsnummer = result.Mitgliedsnummer,
                        MitgliedsnummerAlt = result.MitgliedsnummerAlt,
                        Nachname = result.Nachname,
                        NurKaestchen = result.NurKaestchen,
                        Ort = result.Ort,
                        PLZ = result.PLZ,
                        Tel = result.Tel,
                        TS_DSGV = result.TS_DSGV,
                        User_DSGV = result.User_DSGV,
                        Vorname = result.Vorname,
                        Zusatzinfo = result.Zusatzinfo,
                        Zusatzinfo2 = result.Zusatzinfo2,
                        Zusatzinfo3 = result.Zusatzinfo3,
                        Zusatzinfo4 = result.Zusatzinfo4,
                        Zusatzinfo5 = result.Zusatzinfo5,
                        Halbjahreskarte = result.Halbjahreskarte,
                        Ordner = result.Ordner,
                        Saison = result.Saison,
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


        public static async Task UpdateFromESHERSAsync(int id, string adresse, string plz, string ort, string tel, string email, string gebdatum, string kundennummerESHERS, bool kundeESHERS, bool saveImmediatly)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<kevkunden>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.Adresse = adresse;
                    result.PLZ = plz;
                    result.Ort = ort;
                    result.Tel = tel;
                    result.EMail = email;
                    result.GebDatum = gebdatum;
                    result.KundennummerEisring = kundennummerESHERS;
                    result.KundeEisring = kundeESHERS;

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

        public static async Task<int> CountAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
                
                var result = await UOW.Uow._uow.Query<kevkunden>().CountAsync();
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
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                short KdNr = 20000;

                var count = await CountAsync();
                if (count > 0)
                {
                    var result = await UOW.Uow._uow.Query<kevkunden>().Select(x => x.Mitgliedsnummer).MaxAsync();
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

        public static async Task ImportAsync(string mitgliedsnummer, string mitgliedsnummerAlt, string nachname, string vorname,
            string adresse, string plz, string ort, string land, string gebDatum, bool ehrenkarte, string tel, string kaestchen, bool ordner,
            string zusatzInfo, bool halbjahreskarte,  bool saveImmediatly)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var old = await UOW.Uow._uow.Query<kevkunden>().Where(x => x.MitgliedsnummerAlt ==  mitgliedsnummerAlt).FirstOrDefaultAsync();
                if (old == null)
                {
                    bool KundeESHERS = false;
                    string KdNrESHERS = string.Empty;

                    var kunde = await Kunden.Kunden.GetByNameAsync(vorname, nachname);
                    if (kunde != null)
                    {
                        KundeESHERS = true;
                        KdNrESHERS = kunde.KdNr;
                    }
                    kevkunden kEV = new kevkunden(UOW.Uow._uow);
                    kEV.Mitgliedsnummer = mitgliedsnummer;
                    kEV.MitgliedsnummerAlt = mitgliedsnummerAlt;
                    kEV.KundennummerEisring = KdNrESHERS;
                    kEV.Nachname = nachname;
                    kEV.Vorname = vorname;
                    kEV.Adresse = adresse;
                    kEV.Land = land;
                    kEV.PLZ = plz;
                    kEV.Ort = ort;
                    kEV.GebDatum = gebDatum;
                    kEV.Ehrenkarte = ehrenkarte;
                    kEV.Tel = tel;
                    kEV.Kaestchen = kaestchen;
                    kEV.Ordner = ordner;
                    kEV.Zusatzinfo = zusatzInfo;
                    kEV.Halbjahreskarte = halbjahreskarte;
                    kEV.KundeEisring = KundeESHERS;

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

        public static async Task AddAsync(KEVKunden kevKunde)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                kevkunden kevkunden = new kevkunden(UOW.Uow._uow)
                {
                    Adresse = kevKunde.Adresse,
                    DSGV = kevKunde.DSGV,
                    Ehrenkarte = kevKunde.Ehrenkarte,
                    EMail = kevKunde.EMail,
                    GebDatum = kevKunde.GebDatum,
                    Kaestchen = kevKunde.Kaestchen,
                    KundeEisring = kevKunde.KundeEisring,
                    KundennummerEisring = kevKunde.KundennummerEisring,
                    Land = kevKunde.Land,
                    Mitgliedsnummer = kevKunde.Mitgliedsnummer,
                    MitgliedsnummerAlt = kevKunde.MitgliedsnummerAlt,
                    Nachname = kevKunde.Nachname,
                    NurKaestchen = kevKunde.NurKaestchen,
                    Ort = kevKunde.Ort,
                    PLZ = kevKunde.PLZ,
                    Tel = kevKunde.Tel,
                    TS_DSGV = kevKunde.TS_DSGV,
                    User_DSGV = kevKunde.User_DSGV,
                    Vorname = kevKunde.Vorname,
                    Zusatzinfo = kevKunde.Zusatzinfo,
                    Zusatzinfo2 = kevKunde.Zusatzinfo2,
                    Zusatzinfo3 = kevKunde.Zusatzinfo3,
                    Zusatzinfo4 = kevKunde.Zusatzinfo4,
                    Zusatzinfo5 = kevKunde.Zusatzinfo5,
                    Foto = kevKunde.Foto,
                    Halbjahreskarte = kevKunde.Halbjahreskarte,
                    Ordner = kevKunde.Ordner,
                    Saison = kevKunde.Saison,
                };

                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAsync(int id, KEVKunden kevKunde)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow
                    .Query<kevkunden>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return;
                }

                result.Adresse = kevKunde.Adresse;
                result.DSGV = kevKunde.DSGV;
                result.Ehrenkarte = kevKunde.Ehrenkarte;
                result.EMail = kevKunde.EMail;
                result.GebDatum = kevKunde.GebDatum;
                result.Kaestchen = kevKunde.Kaestchen;
                result.KundeEisring = kevKunde.KundeEisring;
                result.KundennummerEisring = kevKunde.KundennummerEisring;
                result.Land = kevKunde.Land;
                result.Mitgliedsnummer = kevKunde.Mitgliedsnummer;
                result.MitgliedsnummerAlt = kevKunde.MitgliedsnummerAlt;
                result.Nachname = kevKunde.Nachname;
                result.NurKaestchen = kevKunde.NurKaestchen;
                result.Ort = kevKunde.Ort;
                result.PLZ = kevKunde.PLZ;
                result.Tel = kevKunde.Tel;
                result.TS_DSGV = kevKunde.TS_DSGV;
                result.User_DSGV = kevKunde.User_DSGV;
                result.Vorname = kevKunde.Vorname;
                result.Zusatzinfo = kevKunde.Zusatzinfo;
                result.Zusatzinfo2 = kevKunde.Zusatzinfo2;
                result.Zusatzinfo3 = kevKunde.Zusatzinfo3;
                result.Zusatzinfo4 = kevKunde.Zusatzinfo4;
                result.Zusatzinfo5 = kevKunde.Zusatzinfo5;
                result.Foto = kevKunde.Foto;
                result.Halbjahreskarte = kevKunde.Halbjahreskarte;
                result.Ordner = kevKunde.Ordner;
                result.Saison = kevKunde.Saison;

                await UOW.Uow.SaveAsync();
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
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow
                    .Query<kevkunden>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return;
                }

                await UOW.Uow.DeleteAsync(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
