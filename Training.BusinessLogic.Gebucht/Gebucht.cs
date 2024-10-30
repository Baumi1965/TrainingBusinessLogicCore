using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;
using static System.Net.WebRequestMethods;

namespace Training.BusinessLogic.Gebucht
{
    public class Gebucht
    {
        public int ID { get; set; }
        public string KdNr { get; set; }
        public string VName { get; set; }
        public string NName { get; set; }
        public int? Training { get; set; }
        public string TrainingBez { get; set; }
        public bool? Vormittag { get; set; }
        public bool? Nachmittag { get; set; }
        public bool? Abend { get; set; }
        public bool? Adult { get; set; }
        public string Datum { get; set; }
        public bool? Enabled { get; set; }
        public int? Location { get; set; }
        public decimal? Wert { get; set; }
        public string Uhrzeit { get; set; }
        public decimal? WertV { get; set; }
        public decimal? WertN { get; set; }
        public decimal? WertA { get; set; }
        public DateTime? TSEin { get; set; }
        public DateTime? TSAus { get; set; }
        public bool? EiszeitenESHERSVormittag { get; set; }
        public bool? EiszeitenESHERSAbend { get; set; }
        public decimal? WertEiszeitenESHERS { get; set; }


        public static async Task<List<Anzeige>> GetForMonitorAnzeigeAsync(int location, DateTime ein, double trainingsverbot, double erhoehterTarif, double negativesGuthaben)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                List<Anzeige> lstAnzeige = new List<Anzeige>();

                var resultgastkarten = UOW.UOW.uow.Query<kunden>().Where(x => x.VName.ToLower().Contains("gast")).Select(x => x.KdNr).ToList();

                var resultgebucht = await UOW.UOW.uow.Query<gebucht>()
                    .Where(
                        x => ((x.TSEin != null && x.TSEin.Value.Date == ein.Date && x.TSAus == null) ||
                                (x.TSEin != null && x.TSEin.Value.Date == ein.Date &&
                                    x.TSAus != null &&
                                    x.TSAus.Value >= ein &&
                                    resultgastkarten.Contains(x.KdNr))) &&
                            x.Location == location &&
                            x.KdNr != "00001")
                    .OrderBy(x => x.ID)
                    .ToListAsync();


                int index = 0;
                foreach (var result in resultgebucht)
                {
                    string vorname = result.VName;
                    string nachname = result.NName;
                    DateTime datum = result.TSEin.Value.Date;
                    string verband = string.Empty;
                    string typ = string.Empty;
                    string kuerklasse = string.Empty;
                    string zutritt = string.Empty;
                    string akader = string.Empty;
                    double guthaben = 0;

                    var resultkunde = await UOW.UOW.uow
                        .Query<kunden>()
                        .Where(x => x.KdNr == result.KdNr)
                        .FirstOrDefaultAsync();

                    if (resultkunde != null)
                    {
                        verband = resultkunde.Verband;
                        typ = resultkunde.Typ;
                        kuerklasse = resultkunde.Kuerklasse;
                        akader = resultkunde.COVIDOK == null
                            ? string.Empty
                            : Convert.ToBoolean(resultkunde.COVIDOK) == false ? string.Empty : "X";

                        if (resultgastkarten.Contains(resultkunde.KdNr))
                        {
                            zutritt = "GASTKARTE";
                        }
                        else
                        {
                            if (resultkunde.Guthaben != null)
                            {
                                guthaben = Convert.ToDouble(resultkunde.Guthaben);
                            }

                            if (guthaben < trainingsverbot)
                            {
                                zutritt = "TRAININGSVERBOT";
                            }
                            else if (guthaben >= trainingsverbot && guthaben < erhoehterTarif)
                            {
                                zutritt = "ERHÖHTER TARIF";
                            }
                            else if (guthaben >= erhoehterTarif && guthaben < negativesGuthaben)
                            {
                                zutritt = "NEGATIVES GUTHABEN";
                            }
                            else
                            {
                                zutritt = "";
                            }
                        }
                    }

                    lstAnzeige.Add(new Anzeige(index,verband, vorname, nachname, datum.ToString("dd.MM.yyyy"), typ, zutritt, kuerklasse, guthaben, akader));
                    index += 1;
                }
                return lstAnzeige;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static async Task<List<Gebucht>> GetForBuchungsDetail(List<string> kundennummern, int? location = null, DateTime? from = null, DateTime? to = null)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                List<Gebucht> lstGebucht = new List<Gebucht>();

                List<gebucht> lstgebucht = new List<gebucht>();
                if (location == null)
                {
                    if (from == null && to == null)
                    {
                        lstgebucht = await UOW.UOW.uow.Query<gebucht>().Where(x => kundennummern.Contains(x.KdNr)).ToListAsync();
                    }
                    else if (from != null && to == null)
                    {
                        lstgebucht = await UOW.UOW.uow.Query<gebucht>().Where(x => kundennummern.Contains(x.KdNr) && x.TSEin.Value.Date >= from.Value.Date).ToListAsync();

                    }
                    else if (from == null && to != null)
                    {
                        lstgebucht = await UOW.UOW.uow.Query<gebucht>().Where(x => kundennummern.Contains(x.KdNr) && x.TSEin.Value.Date <= to.Value.Date).ToListAsync();
                    }
                    else
                    {
                        lstgebucht = await UOW.UOW.uow.Query<gebucht>().Where(x => kundennummern.Contains(x.KdNr) && x.TSEin.Value.Date >= from.Value.Date && x.TSEin.Value.Date <= to.Value.Date).ToListAsync();
                    }
                }
                else
                {
                    if (from == null && to == null)
                    {
                        lstgebucht = await UOW.UOW.uow.Query<gebucht>().Where(x => kundennummern.Contains(x.KdNr) && x.Location == location.Value).ToListAsync();
                    }
                    else if (from != null && to == null)
                    {
                        lstgebucht = await UOW.UOW.uow.Query<gebucht>().Where(x => kundennummern.Contains(x.KdNr) && x.Location == location.Value && x.TSEin.Value.Date >= from.Value.Date).ToListAsync();

                    }
                    else if (from == null && to != null)
                    {
                        lstgebucht = await UOW.UOW.uow.Query<gebucht>().Where(x => kundennummern.Contains(x.KdNr) && x.Location == location.Value && x.TSEin.Value.Date <= to.Value.Date).ToListAsync();
                    }
                    else
                    {
                        lstgebucht = await UOW.UOW.uow.Query<gebucht>().Where(x => kundennummern.Contains(x.KdNr) && x.Location == location.Value && x.TSEin.Value.Date >= from.Value.Date && x.TSEin.Value.Date <= to.Value.Date).ToListAsync();
                    }
                }

                foreach (var result in lstgebucht)
                {
                    Gebucht gebucht = new Gebucht();
                    gebucht.Abend = result.Abend;
                    gebucht.Adult = result.Adult;
                    gebucht.Datum = result.Datum;
                    gebucht.EiszeitenESHERSAbend = result.EiszeitenESHERSAbend;
                    gebucht.EiszeitenESHERSVormittag = result.EiszeitenESHERSVormittag;
                    gebucht.Enabled = result.Enabled;
                    gebucht.ID = result.ID;
                    gebucht.KdNr = result.KdNr;
                    gebucht.Location = result.Location;
                    gebucht.Nachmittag = result.Nachmittag;
                    gebucht.NName = result.NName;
                    gebucht.Training = result.Training;
                    gebucht.TrainingBez = result.TrainingBez;
                    gebucht.TSAus = result.TSAus;
                    gebucht.TSEin = result.TSEin;
                    gebucht.Uhrzeit = result.Uhrzeit;
                    gebucht.VName = result.VName;
                    gebucht.Vormittag = result.Vormittag;
                    gebucht.Wert = result.Wert;
                    gebucht.WertA = result.WertA;
                    gebucht.WertEiszeitenESHERS = result.WertEiszeitenESHERS;
                    gebucht.WertN = result.WertN;
                    gebucht.WertV = result.WertV;

                    lstGebucht.Add(gebucht);
                }

                return lstGebucht;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> GetIdForBuchungsdetail(string kdnr, DateTime ein)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == kdnr && x.TSEin.Value.Date == ein.Date).OrderByDescending(x => x.TSEin).FirstOrDefaultAsync();
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

        public static async Task<Gebucht> GetById(int id)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    Gebucht gebucht = new Gebucht();
                    gebucht.Abend = result.Abend;
                    gebucht.Adult = result.Adult;
                    gebucht.Datum = result.Datum;
                    gebucht.EiszeitenESHERSAbend = result.EiszeitenESHERSAbend;
                    gebucht.EiszeitenESHERSVormittag = result.EiszeitenESHERSVormittag;
                    gebucht.Enabled = result.Enabled;
                    gebucht.ID = result.ID;
                    gebucht.KdNr = result.KdNr;
                    gebucht.Location = result.Location;
                    gebucht.Nachmittag = result.Nachmittag;
                    gebucht.NName = result.NName;
                    gebucht.Training = result.Training;
                    gebucht.TrainingBez = result.TrainingBez;
                    gebucht.TSAus = result.TSAus;
                    gebucht.TSEin = result.TSEin;
                    gebucht.Uhrzeit = result.Uhrzeit;
                    gebucht.VName = result.VName;
                    gebucht.Vormittag = result.Vormittag;
                    gebucht.Wert = result.Wert;
                    gebucht.WertA = result.WertA;
                    gebucht.WertEiszeitenESHERS = result.WertEiszeitenESHERS;
                    gebucht.WertN = result.WertN;
                    gebucht.WertV = result.WertV;

                    return gebucht;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> GetByKdNrTSEin(string kdnr, DateTime ein)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }


                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == kdnr && x.TSEin != null && x.TSEin.Value.Date == ein.Date && x.TSAus == null).OrderByDescending(x => x.TSEin).FirstOrDefaultAsync();
                if (result != null)
                {
                    Gebucht gebucht = new Gebucht();
                    gebucht.Abend = result.Abend;
                    gebucht.Adult = result.Adult;
                    gebucht.Datum = result.Datum;
                    gebucht.EiszeitenESHERSAbend = result.EiszeitenESHERSAbend;
                    gebucht.EiszeitenESHERSVormittag = result.EiszeitenESHERSVormittag;
                    gebucht.Enabled = result.Enabled;
                    gebucht.ID = result.ID;
                    gebucht.KdNr = result.KdNr;
                    gebucht.Location = result.Location;
                    gebucht.Nachmittag = result.Nachmittag;
                    gebucht.NName = result.NName;
                    gebucht.Training = result.Training;
                    gebucht.TrainingBez = result.TrainingBez;
                    gebucht.TSAus = result.TSAus;
                    gebucht.TSEin = result.TSEin;
                    gebucht.Uhrzeit = result.Uhrzeit;
                    gebucht.VName = result.VName;
                    gebucht.Vormittag = result.Vormittag;
                    gebucht.Wert = result.Wert;
                    gebucht.WertA = result.WertA;
                    gebucht.WertEiszeitenESHERS = result.WertEiszeitenESHERS;
                    gebucht.WertN = result.WertN;
                    gebucht.WertV = result.WertV;

                    return gebucht;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> GetByKdNrTSEin(string kdnr, DateTime ein, int location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == kdnr && x.TSEin != null && x.TSEin.Value.Date == ein.Date && x.TSAus == null && x.Location == location).OrderByDescending(x => x.TSEin).FirstOrDefaultAsync();
                if (result != null)
                {
                    Gebucht gebucht = new Gebucht();
                    gebucht.Abend = result.Abend;
                    gebucht.Adult = result.Adult;
                    gebucht.Datum = result.Datum;
                    gebucht.EiszeitenESHERSAbend = result.EiszeitenESHERSAbend;
                    gebucht.EiszeitenESHERSVormittag = result.EiszeitenESHERSVormittag;
                    gebucht.Enabled = result.Enabled;
                    gebucht.ID = result.ID;
                    gebucht.KdNr = result.KdNr;
                    gebucht.Location = result.Location;
                    gebucht.Nachmittag = result.Nachmittag;
                    gebucht.NName = result.NName;
                    gebucht.Training = result.Training;
                    gebucht.TrainingBez = result.TrainingBez;
                    gebucht.TSAus = result.TSAus;
                    gebucht.TSEin = result.TSEin;
                    gebucht.Uhrzeit = result.Uhrzeit;
                    gebucht.VName = result.VName;
                    gebucht.Vormittag = result.Vormittag;
                    gebucht.Wert = result.Wert;
                    gebucht.WertA = result.WertA;
                    gebucht.WertEiszeitenESHERS = result.WertEiszeitenESHERS;
                    gebucht.WertN = result.WertN;
                    gebucht.WertV = result.WertV;

                    return gebucht;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> GetIdByKdNrTSEin(string kdnr, DateTime ein, int location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == kdnr && x.TSEin != null && x.TSEin.Value.Date == ein.Date && x.TSAus == null && x.Location == location).OrderByDescending(x => x.TSEin).FirstOrDefaultAsync();
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

        public static async Task<Gebucht> GetByKdNrTSEinAndNotTSAus(string kdnr, DateTime ein, int location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == kdnr && x.Location == location && x.TSEin.Value.Date == ein.Date).OrderByDescending(x => x.TSEin).FirstOrDefaultAsync();
                if (result != null)
                {
                    Gebucht gebucht = new Gebucht();
                    gebucht.Abend = result.Abend;
                    gebucht.Adult = result.Adult;
                    gebucht.Datum = result.Datum;
                    gebucht.EiszeitenESHERSAbend = result.EiszeitenESHERSAbend;
                    gebucht.EiszeitenESHERSVormittag = result.EiszeitenESHERSVormittag;
                    gebucht.Enabled = result.Enabled;
                    gebucht.ID = result.ID;
                    gebucht.KdNr = result.KdNr;
                    gebucht.Location = result.Location;
                    gebucht.Nachmittag = result.Nachmittag;
                    gebucht.NName = result.NName;
                    gebucht.Training = result.Training;
                    gebucht.TrainingBez = result.TrainingBez;
                    gebucht.TSAus = result.TSAus;
                    gebucht.TSEin = result.TSEin;
                    gebucht.Uhrzeit = result.Uhrzeit;
                    gebucht.VName = result.VName;
                    gebucht.Vormittag = result.Vormittag;
                    gebucht.Wert = result.Wert;
                    gebucht.WertA = result.WertA;
                    gebucht.WertEiszeitenESHERS = result.WertEiszeitenESHERS;
                    gebucht.WertN = result.WertN;
                    gebucht.WertV = result.WertV;

                    return gebucht;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> GetByKdNrTSEinTSAusLocation(string kdnr, DateTime ein, int location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == kdnr && x.TSEin.Value.Date == ein.Date && x.TSAus != null && x.Location == location).OrderByDescending(x => x.TSEin).FirstOrDefaultAsync();
                if (result != null)
                {
                    Gebucht gebucht = new Gebucht();
                    gebucht.Abend = result.Abend;
                    gebucht.Adult = result.Adult;
                    gebucht.Datum = result.Datum;
                    gebucht.EiszeitenESHERSAbend = result.EiszeitenESHERSAbend;
                    gebucht.EiszeitenESHERSVormittag = result.EiszeitenESHERSVormittag;
                    gebucht.Enabled = result.Enabled;
                    gebucht.ID = result.ID;
                    gebucht.KdNr = result.KdNr;
                    gebucht.Location = result.Location;
                    gebucht.Nachmittag = result.Nachmittag;
                    gebucht.NName = result.NName;
                    gebucht.Training = result.Training;
                    gebucht.TrainingBez = result.TrainingBez;
                    gebucht.TSAus = result.TSAus;
                    gebucht.TSEin = result.TSEin;
                    gebucht.Uhrzeit = result.Uhrzeit;
                    gebucht.VName = result.VName;
                    gebucht.Vormittag = result.Vormittag;
                    gebucht.Wert = result.Wert;
                    gebucht.WertA = result.WertA;
                    gebucht.WertEiszeitenESHERS = result.WertEiszeitenESHERS;
                    gebucht.WertN = result.WertN;
                    gebucht.WertV = result.WertV;

                    return gebucht;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> UpdateTSEin(int id, DateTime ein)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.TSEin = ein;
                    result.TSAus = null;
                    await UOW.UOW.SaveAsync();
                }

                return await GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateTSAus(int id, DateTime aus)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.TSAus = aus;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> UpdateTSEinTSAus(int id, DateTime ein, DateTime aus)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.TSEin = ein;
                    result.TSAus = aus;
                }

                return await GetById(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> Add(string kdnr, DateTime ein, int location, string nname, string vname)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                gebucht g = new gebucht(UOW.UOW.uow);
                g.Datum = ein.ToString("dd.MM.yyyy");
                g.KdNr = kdnr;
                g.Location = location;
                g.NName = nname;
                g.TSEin = ein;
                g.Uhrzeit = ein.ToString("HH:mm");
                g.VName = vname;

                await UOW.UOW.uow.CommitChangesAsync();

                Gebucht gebucht = new Gebucht();
                gebucht.Abend = g.Abend;
                gebucht.Adult = g.Adult;
                gebucht.Datum = g.Datum;
                gebucht.EiszeitenESHERSAbend = g.EiszeitenESHERSAbend;
                gebucht.EiszeitenESHERSVormittag = g.EiszeitenESHERSVormittag;
                gebucht.Enabled = g.Enabled;
                gebucht.ID = g.ID;
                gebucht.KdNr = g.KdNr;
                gebucht.Location = g.Location;
                gebucht.Nachmittag = g.Nachmittag;
                gebucht.NName = g.NName;
                gebucht.Training = g.Training;
                gebucht.TrainingBez = g.TrainingBez;
                gebucht.TSAus = g.TSAus;
                gebucht.TSEin = g.TSEin;
                gebucht.Uhrzeit = g.Uhrzeit;
                gebucht.VName = g.VName;
                gebucht.Vormittag = g.Vormittag;
                gebucht.Wert = g.Wert;
                gebucht.WertA = g.WertA;
                gebucht.WertEiszeitenESHERS = g.WertEiszeitenESHERS;
                gebucht.WertN = g.WertN;
                gebucht.WertV = g.WertV;

                return gebucht;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> Add(string kdnr, DateTime ein, DateTime aus, int location, string nname, string vname)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                gebucht g = new gebucht(UOW.UOW.uow);
                g.Datum = ein.ToString("dd.MM.yyyy");
                g.KdNr = kdnr;
                g.Location = location;
                g.NName = nname;
                g.TSEin = ein;
                g.TSAus = aus;
                g.Uhrzeit = ein.ToString("HH:mm");
                g.VName = vname;

                await UOW.UOW.uow.CommitChangesAsync();

                Gebucht gebucht = new Gebucht();
                gebucht.Abend = g.Abend;
                gebucht.Adult = g.Adult;
                gebucht.Datum = g.Datum;
                gebucht.EiszeitenESHERSAbend = g.EiszeitenESHERSAbend;
                gebucht.EiszeitenESHERSVormittag = g.EiszeitenESHERSVormittag;
                gebucht.Enabled = g.Enabled;
                gebucht.ID = g.ID;
                gebucht.KdNr = g.KdNr;
                gebucht.Location = g.Location;
                gebucht.Nachmittag = g.Nachmittag;
                gebucht.NName = g.NName;
                gebucht.Training = g.Training;
                gebucht.TrainingBez = g.TrainingBez;
                gebucht.TSAus = g.TSAus;
                gebucht.TSEin = g.TSEin;
                gebucht.Uhrzeit = g.Uhrzeit;
                gebucht.VName = g.VName;
                gebucht.Vormittag = g.Vormittag;
                gebucht.Wert = g.Wert;
                gebucht.WertA = g.WertA;
                gebucht.WertEiszeitenESHERS = g.WertEiszeitenESHERS;
                gebucht.WertN = g.WertN;
                gebucht.WertV = g.WertV;

                return gebucht;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> Add(string kdnr, int location, string nname, string vname, int training, string trainingbez,
            bool vormittag, bool nachmittag, bool abend, bool adult, string datum, string uhrzeit, decimal wert)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                gebucht g = new gebucht(UOW.UOW.uow);
                g.Datum = datum;
                g.KdNr = kdnr;
                g.Location = location;
                g.NName = nname;
                g.Uhrzeit = uhrzeit;
                g.VName = vname;
                g.Training = training;
                g.TrainingBez = trainingbez;
                g.Vormittag = vormittag;
                g.Nachmittag = nachmittag;
                g.Abend = abend;
                g.Adult = adult;
                g.Wert = wert;
                await UOW.UOW.SaveAsync();

                Gebucht gebucht = new Gebucht();
                gebucht.Abend = g.Abend;
                gebucht.Adult = g.Adult;
                gebucht.Datum = g.Datum;
                gebucht.EiszeitenESHERSAbend = g.EiszeitenESHERSAbend;
                gebucht.EiszeitenESHERSVormittag = g.EiszeitenESHERSVormittag;
                gebucht.Enabled = g.Enabled;
                gebucht.ID = g.ID;
                gebucht.KdNr = g.KdNr;
                gebucht.Location = g.Location;
                gebucht.Nachmittag = g.Nachmittag;
                gebucht.NName = g.NName;
                gebucht.Training = g.Training;
                gebucht.TrainingBez = g.TrainingBez;
                gebucht.TSAus = g.TSAus;
                gebucht.TSEin = g.TSEin;
                gebucht.Uhrzeit = g.Uhrzeit;
                gebucht.VName = g.VName;
                gebucht.Vormittag = g.Vormittag;
                gebucht.Wert = g.Wert;
                gebucht.WertA = g.WertA;
                gebucht.WertEiszeitenESHERS = g.WertEiszeitenESHERS;
                gebucht.WertN = g.WertN;
                gebucht.WertV = g.WertV;

                return gebucht;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> AddForBarverkaufAsync(string kdnr, int location, string nname, string vname, int training, string trainingbez,
            bool vormittag, bool nachmittag, bool abend, bool adult, DateTime datum, decimal wert, DateTime ende)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                gebucht g = new gebucht(UOW.UOW.uow);
                g.Datum = datum.ToString("dd.MM.yyyy");
                g.KdNr = kdnr;
                g.Location = location;
                g.NName = nname;
                g.Uhrzeit = datum.ToString("HH:mm");
                g.VName = vname;
                g.Training = training;
                g.TrainingBez = trainingbez;
                g.Vormittag = vormittag;
                g.Nachmittag = nachmittag;
                g.Abend = abend;
                g.Adult = adult;
                g.Wert = wert;
                g.TSEin = datum;
                g.TSAus = ende;
                await UOW.UOW.SaveAsync();

                Gebucht gebucht = new Gebucht();
                gebucht.Abend = g.Abend;
                gebucht.Adult = g.Adult;
                gebucht.Datum = g.Datum;
                gebucht.EiszeitenESHERSAbend = g.EiszeitenESHERSAbend;
                gebucht.EiszeitenESHERSVormittag = g.EiszeitenESHERSVormittag;
                gebucht.Enabled = g.Enabled;
                gebucht.ID = g.ID;
                gebucht.KdNr = g.KdNr;
                gebucht.Location = g.Location;
                gebucht.Nachmittag = g.Nachmittag;
                gebucht.NName = g.NName;
                gebucht.Training = g.Training;
                gebucht.TrainingBez = g.TrainingBez;
                gebucht.TSAus = g.TSAus;
                gebucht.TSEin = g.TSEin;
                gebucht.Uhrzeit = g.Uhrzeit;
                gebucht.VName = g.VName;
                gebucht.Vormittag = g.Vormittag;
                gebucht.Wert = g.Wert;
                gebucht.WertA = g.WertA;
                gebucht.WertEiszeitenESHERS = g.WertEiszeitenESHERS;
                gebucht.WertN = g.WertN;
                gebucht.WertV = g.WertV;

                return gebucht;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> Update(string kdnr, int location, int training, string trainingbez,
            bool vormittag, bool nachmittag, bool abend, bool adult, string datum)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var g = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == kdnr && x.Location == location && x.Datum == datum).OrderByDescending(x => x.ID).FirstOrDefaultAsync();
                if (g == null)
                {
                    return null;
                }

                g.Training = training;
                g.TrainingBez = trainingbez;
                g.Location = location;
                g.Vormittag = vormittag;
                g.Nachmittag = nachmittag;
                g.Abend = abend;
                g.Adult = adult;

                await UOW.UOW.SaveAsync();

                Gebucht gebucht = new Gebucht();
                gebucht.Abend = g.Abend;
                gebucht.Adult = g.Adult;
                gebucht.Datum = g.Datum;
                gebucht.EiszeitenESHERSAbend = g.EiszeitenESHERSAbend;
                gebucht.EiszeitenESHERSVormittag = g.EiszeitenESHERSVormittag;
                gebucht.Enabled = g.Enabled;
                gebucht.ID = g.ID;
                gebucht.KdNr = g.KdNr;
                gebucht.Location = g.Location;
                gebucht.Nachmittag = g.Nachmittag;
                gebucht.NName = g.NName;
                gebucht.Training = g.Training;
                gebucht.TrainingBez = g.TrainingBez;
                gebucht.TSAus = g.TSAus;
                gebucht.TSEin = g.TSEin;
                gebucht.Uhrzeit = g.Uhrzeit;
                gebucht.VName = g.VName;
                gebucht.Vormittag = g.Vormittag;
                gebucht.Wert = g.Wert;
                gebucht.WertA = g.WertA;
                gebucht.WertEiszeitenESHERS = g.WertEiszeitenESHERS;
                gebucht.WertN = g.WertN;
                gebucht.WertV = g.WertV;

                return gebucht;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> Update(Gebucht gebucht)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.ID == gebucht.ID).FirstOrDefaultAsync();

                result.Abend = gebucht.Abend;
                result.Adult = gebucht.Adult;
                result.EiszeitenESHERSAbend = gebucht.EiszeitenESHERSAbend;
                result.EiszeitenESHERSVormittag = gebucht.EiszeitenESHERSVormittag;
                result.Nachmittag = gebucht.Nachmittag;
                result.Training = gebucht.Training;
                result.TrainingBez = gebucht.TrainingBez;
                result.Vormittag = gebucht.Vormittag;
                result.Wert = gebucht.Wert;
                result.WertA = gebucht.WertA;
                result.WertEiszeitenESHERS = gebucht.WertEiszeitenESHERS;
                result.WertN = gebucht.WertN;
                result.WertV = gebucht.WertV;

                return await Gebucht.GetById(result.ID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<decimal> GetBereitsGebucht(string kdnr, DateTime ein)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                decimal bereitsGebucht = 0;

                var resultGebuchtAll = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == kdnr && x.TSEin.Value.Date == ein.Date).ToListAsync();
                foreach (var item in resultGebuchtAll)
                {
                    bereitsGebucht += Convert.ToDecimal(item.Wert);
                }

                return bereitsGebucht;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> CheckBuchungOtherLocation(string kdnr, DateTime ein, int location, bool vm, bool nm, bool abend, bool adult, bool erseshvm, bool erseshabend)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }
                var result = await UOW.UOW.uow.Query<gebucht>()
                    .Where(x => x.KdNr == kdnr &&
                        x.Location != location &&
                        x.TSEin.Value.Date == ein.Date)
                    .ToListAsync();

                foreach (var item in result)
                {
                    if (vm && item.Vormittag == true)
                        return true;
                    else if (nm && item.Nachmittag == true)
                        return true;
                    if (abend && item.Abend == true)
                        return true;
                    if (adult && item.Adult == true)
                        return true;
                    if (erseshvm && item.EiszeitenESHERSVormittag == true)
                        return true;
                    if (erseshabend && item.EiszeitenESHERSAbend == true)
                        return true;
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> CheckIfLoginToOtherLocation(string kdnr, DateTime ein, int location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == kdnr && x.TSEin != null && x.TSAus == null && x.TSEin.Value.Date == ein.Date && x.Location != location).FirstOrDefaultAsync();
                if (result != null)
                {
                    Gebucht gebucht = new Gebucht();
                    gebucht.Abend = result.Abend;
                    gebucht.Adult = result.Adult;
                    gebucht.Datum = result.Datum;
                    gebucht.EiszeitenESHERSAbend = result.EiszeitenESHERSAbend;
                    gebucht.EiszeitenESHERSVormittag = result.EiszeitenESHERSVormittag;
                    gebucht.Enabled = result.Enabled;
                    gebucht.ID = result.ID;
                    gebucht.KdNr = result.KdNr;
                    gebucht.Location = result.Location;
                    gebucht.Nachmittag = result.Nachmittag;
                    gebucht.NName = result.NName;
                    gebucht.Training = result.Training;
                    gebucht.TrainingBez = result.TrainingBez;
                    gebucht.TSAus = result.TSAus;
                    gebucht.TSEin = result.TSEin;
                    gebucht.Uhrzeit = result.Uhrzeit;
                    gebucht.VName = result.VName;
                    gebucht.Vormittag = result.Vormittag;
                    gebucht.Wert = result.Wert;
                    gebucht.WertA = result.WertA;
                    gebucht.WertEiszeitenESHERS = result.WertEiszeitenESHERS;
                    gebucht.WertN = result.WertN;
                    gebucht.WertV = result.WertV;

                    return gebucht;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Gebucht> GetByKdNrDatumAsync(string kdnr, string datum)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == kdnr && x.Datum == datum).OrderByDescending(x => x.TSEin).FirstOrDefaultAsync();
                if (result != null)
                {
                    Gebucht gebucht = new Gebucht();
                    gebucht.Abend = result.Abend;
                    gebucht.Adult = result.Adult;
                    gebucht.Datum = result.Datum;
                    gebucht.EiszeitenESHERSAbend = result.EiszeitenESHERSAbend;
                    gebucht.EiszeitenESHERSVormittag = result.EiszeitenESHERSVormittag;
                    gebucht.Enabled = result.Enabled;
                    gebucht.ID = result.ID;
                    gebucht.KdNr = result.KdNr;
                    gebucht.Location = result.Location;
                    gebucht.Nachmittag = result.Nachmittag;
                    gebucht.NName = result.NName;
                    gebucht.Training = result.Training;
                    gebucht.TrainingBez = result.TrainingBez;
                    gebucht.TSAus = result.TSAus;
                    gebucht.TSEin = result.TSEin;
                    gebucht.Uhrzeit = result.Uhrzeit;
                    gebucht.VName = result.VName;
                    gebucht.Vormittag = result.Vormittag;
                    gebucht.Wert = result.Wert;
                    gebucht.WertA = result.WertA;
                    gebucht.WertEiszeitenESHERS = result.WertEiszeitenESHERS;
                    gebucht.WertN = result.WertN;
                    gebucht.WertV = result.WertV;

                    return gebucht;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> GetIdByKdNrDatumLocation(string kdnr, string datum, int location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == kdnr && x.Datum == datum && x.Location == location).OrderByDescending(x => x.TSEin).FirstOrDefaultAsync();
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

        public static async Task UpdateWertAenderungAsync(List<WertAenderung> aenderungen)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                foreach (var item in aenderungen)
                {
                    string Datum = item.Datum.ToString("dd.MM.yyyy");

                    var result = await UOW.UOW.uow.Query<gebucht>().Where(x => x.KdNr == item.KdNr && x.Datum == Datum && x.Location == item.Location).FirstOrDefaultAsync();
                    if (result == null)
                    {
                        continue;
                    }


                    if (item.Wert != null)
                        result.Wert = item.Wert;
                    if (item.WertESHERS != null)
                        result.WertEiszeitenESHERS = item.WertESHERS;
                    if (item.Vormittag != null)
                        result.Vormittag = item.Vormittag;
                    if (item.Nachmittag != null)
                        result.Nachmittag = item.Nachmittag;
                    if (item.Adult != null)
                        result.Adult = item.Adult;
                    if (item.Abend != null)
                        result.Abend = item.Abend;
                    if (item.ESHERSAbend != null)
                        result.EiszeitenESHERSAbend = item.ESHERSAbend;
                    if (item.ESHERSVorm != null)
                        result.EiszeitenESHERSVormittag = item.ESHERSVorm;

                    result.TrainingBez = GenerateTrainingText(result);
                }

                await UOW.UOW.SaveAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GenerateTrainingText(gebucht row)
        {
            string TrainingText = string.Empty;
            if (row.EiszeitenESHERSVormittag != null && Convert.ToBoolean(row.EiszeitenESHERSVormittag) == true)
            {
                TrainingText += "Eiszeiten ESH/ERS ";
            }
            if (row.Vormittag != null && Convert.ToBoolean(row.Vormittag) == true)
            {
                TrainingText += "Vormittag ";
            }
            if (row.Nachmittag != null && Convert.ToBoolean(row.Nachmittag) == true)
            {
                TrainingText += "Nachmittag ";
            }
            if (row.Abend != null && Convert.ToBoolean(row.Abend) == true)
            {
                TrainingText += "Abend ";
            }
            if (row.Adult != null && Convert.ToBoolean(row.Adult) == true)
            {
                TrainingText += "Adult ";
            }
            if (row.EiszeitenESHERSAbend != null && Convert.ToBoolean(row.EiszeitenESHERSAbend) == true)
            {
                TrainingText += "Eiszeiten ESH/ERS";
            }
            return TrainingText.Trim();
        }

        public static async Task<List<Anwesenheitsliste>> GetAnwesenheitslisteAsync(bool sportler, bool trainer, bool adult, bool gast,
            DateTime? von, DateTime? bis, int? location, string info1, string info2, string info3, string info4, string info5, string info6,
            string info7, string info8, string info9, string info10)
        {
            string kdnr = string.Empty;
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }



                var result = await UOW.UOW.uow.ExecuteSprocAsync(new System.Threading.CancellationToken(), "GET_ESHERS_ANWESENHEITSLISTE_VEREIN",
                    new OperandValue(sportler ? 1 : 0),
                    new OperandValue(trainer ? 1 : 0),
                    new OperandValue(adult ? 1 : 0),
                    new OperandValue(gast ? 1 : 0),
                    new OperandValue(von == null ? null : von.Value.ToString("yyyyMMdd")),
                    new OperandValue(bis == null ? null : bis.Value.ToString("yyyyMMdd")),
                    new OperandValue(location == null ? null : location.Value.ToString()),
                    new OperandValue(info1),
                    new OperandValue(info2),
                    new OperandValue(info3),
                    new OperandValue(info3),
                    new OperandValue(info4),
                    new OperandValue(info5),
                    new OperandValue(info6),
                    new OperandValue(info7),
                    new OperandValue(info8),
                    new OperandValue(info10));

                if (result == null)
                {
                    return null;
                }

                List<Anwesenheitsliste> anwesende = new List<Anwesenheitsliste>();

                var resultanwesend = result.ResultSet.FirstOrDefault();
                if (resultanwesend != null)
                {
                    foreach (var item in resultanwesend.Rows)
                    {
                        kdnr = item.Values[0].ToString();
                        DateTime? Aus = null;
                        if (item.Values[14] != null)
                        {
                            Aus = Convert.ToDateTime(item.Values[14]);
                        }

                        Anwesenheitsliste cAnw = new Anwesenheitsliste();
                        cAnw.Parken = 0;
                        cAnw.KdNr = item.Values[0].ToString();
                        cAnw.VName = item.Values[1].ToString();
                        cAnw.NName = item.Values[2].ToString();
                        cAnw.Ort = Convert.ToInt32(item.Values[3]);
                        cAnw.Datum = Convert.ToDateTime(item.Values[4]);
                        if (item.Values[5] != null && Convert.ToInt16(item.Values[5]) == 1)
                        {
                            cAnw.Vormittag = 1;
                        }
                        if (item.Values[6] != null && Convert.ToInt16(item.Values[6]) == 1)
                        {
                            cAnw.Nachmittag = 1;
                        }
                        if (item.Values[7] != null && Convert.ToInt16(item.Values[7]) == 1)
                        {
                            cAnw.Abend = 1;
                        }
                        if (item.Values[8] != null && Convert.ToInt16(item.Values[8]) == 1)
                        {
                            cAnw.Adult = 1;
                        }
                        cAnw.Zeit = item.Values[9].ToString();
                        cAnw.Wert = item.Values[10] == null ? 0 : Convert.ToDecimal(item.Values[10]);
                        cAnw.abgebuchterBetrag = item.Values[11] == null ? 0 : Convert.ToDecimal(item.Values[11]);
                        cAnw.Barverkauf = item.Values[12] == null ? 0 : Convert.ToDecimal(item.Values[12]);
                        if (item.Values[13] != null)
                        {
                            cAnw.TSEin = Convert.ToDateTime(item.Values[13]);
                        }
                        if (item.Values[14] != null)
                        {
                            cAnw.TSAus = Convert.ToDateTime(item.Values[14]);
                        }
                        cAnw.Gesperrt = Convert.ToInt16(item.Values[15]);
                        cAnw.Verband = item.Values[16].ToString();
                        cAnw.EKLNr = item.Values[17].ToString();
                        cAnw.WertV = item.Values[18] == null ? 0 : Convert.ToDecimal(item.Values[18]);
                        cAnw.WertN = item.Values[19] == null ? 0 : Convert.ToDecimal(item.Values[19]);
                        cAnw.WertA = item.Values[20] == null ? 0 : Convert.ToDecimal(item.Values[20]);

                        if (item.Values[21] != null && Convert.ToInt16(item.Values[21]) == 1)
                        {
                            cAnw.EiszeitenESHERSVormittag = 1;
                        }
                        if (item.Values[22] != null && Convert.ToInt16(item.Values[22]) == 1)
                        {
                            cAnw.EiszeitenESHERSAbend = 1;
                        }
                        cAnw.WertEiszeitenESHERS = item.Values[23] == null ? 0 : Convert.ToDecimal(item.Values[23]);

                        cAnw.Zusatzgebuehr = 0;
                        cAnw.BuchungEismeister = 0;

                        if (anwesende.Count() == 0)
                        {
                            anwesende.Add(cAnw);
                        }
                        else
                        {
                            Anwesenheitsliste resultanwlst = anwesende.FirstOrDefault(x => x.KdNr == cAnw.KdNr && x.Ort == cAnw.Ort && x.Datum.Date.CompareTo(cAnw.Datum.Date) == 0);
                            if (resultanwlst != null)
                            {
                                if (cAnw.Vormittag == 1)
                                    resultanwlst.Vormittag = cAnw.Vormittag;
                                if (cAnw.Nachmittag == 1)
                                    resultanwlst.Nachmittag = cAnw.Nachmittag;
                                if (cAnw.Abend == 1)
                                    resultanwlst.Abend = cAnw.Abend;
                                if (cAnw.Adult == 1)
                                    resultanwlst.Adult = cAnw.Adult;
                                if (cAnw.EiszeitenESHERSVormittag == 1)
                                    resultanwlst.EiszeitenESHERSVormittag = cAnw.EiszeitenESHERSVormittag;
                                if (cAnw.EiszeitenESHERSAbend == 1)
                                    resultanwlst.EiszeitenESHERSAbend = cAnw.EiszeitenESHERSAbend;

                                resultanwlst.Wert = cAnw.Wert;
                                resultanwlst.Zeit = cAnw.Zeit;

                            }
                            else
                            {
                                Anwesenheitsliste resultanwlst1 = anwesende.FirstOrDefault(x => x.KdNr == cAnw.KdNr && x.Datum.Date.CompareTo(cAnw.Datum.Date) == 0);
                                if (resultanwlst1 != null)
                                {
                                    Decimal deAbg = resultanwlst1.abgebuchterBetrag;
                                    Decimal deWert = 0;
                                    List<Anwesenheitsliste> resultanwlst2 = anwesende.Where(x => x.KdNr == cAnw.KdNr && x.Datum.Date.CompareTo(cAnw.Datum.Date) == 0).ToList();
                                    foreach (var item2 in resultanwlst2)
                                    {
                                        deWert = item2.Wert;
                                    }
                                }

                                anwesende.Add(cAnw);
                            }
                        }

                    }
                }

                return anwesende;
            }
            catch (Exception ex)
            {
                throw new Exception(kdnr + Environment.NewLine + ex.Message);
            }
        }

        public static async Task<List<Anwesenheitsliste>> GetAnwesenheitslisteAsync(bool sportler, bool trainer, bool adult, bool gast, DateTime? von, DateTime? bis, int? location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }



                var result = await UOW.UOW.uow.ExecuteSprocAsync(new System.Threading.CancellationToken(), "GET_ESHERS_ANWESENHEITSLISTE",
                    new OperandValue(sportler ? 1 : 0),
                    new OperandValue(trainer ? 1 : 0),
                    new OperandValue(adult ? 1 : 0),
                    new OperandValue(gast ? 1 : 0),
                    new OperandValue(von == null ? null : von.Value.ToString("yyyyMMdd")),
                    new OperandValue(bis == null ? null : bis.Value.ToString("yyyyMMdd")),
                    new OperandValue(location == null ? null : location.Value.ToString()));

                if (result == null)
                {
                    return null;
                }

                List<Anwesenheitsliste> anwesende = new List<Anwesenheitsliste>();

                var resultanwesend = result.ResultSet.FirstOrDefault();
                if (resultanwesend != null)
                {
                    foreach (var item in resultanwesend.Rows)
                    {
                        DateTime? Aus = null;
                        if (item.Values[14] != null)
                        {
                            Aus = Convert.ToDateTime(item.Values[14]);
                        }

                        Anwesenheitsliste cAnw = new Anwesenheitsliste();
                        cAnw.Parken = 0;
                        cAnw.KdNr = item.Values[0].ToString();
                        cAnw.VName = item.Values[1].ToString();
                        cAnw.NName = item.Values[2].ToString();
                        cAnw.Ort = Convert.ToInt32(item.Values[3]);
                        cAnw.Datum = Convert.ToDateTime(item.Values[4]);
                        if (item.Values[5] != null && Convert.ToInt16(item.Values[5]) == 1)
                        {
                            cAnw.Vormittag = 1;
                        }
                        if (item.Values[6] != null && Convert.ToInt16(item.Values[6]) == 1)
                        {
                            cAnw.Nachmittag = 1;
                        }
                        if (item.Values[7] != null && Convert.ToInt16(item.Values[7]) == 1)
                        {
                            cAnw.Abend = 1;
                        }
                        if (item.Values[8] != null && Convert.ToInt16(item.Values[8]) == 1)
                        {
                            cAnw.Adult = 1;
                        }
                        cAnw.Zeit = item.Values[9] == null ? "" : item.Values[9].ToString();
                        cAnw.Wert = item.Values[10] == null ? 0 : Convert.ToDecimal(item.Values[10]);
                        cAnw.abgebuchterBetrag = item.Values[11] == null ? 0 : Convert.ToDecimal(item.Values[11]);
                        cAnw.Barverkauf = item.Values[12] == null ? 0 : Convert.ToDecimal(item.Values[12]);
                        if (item.Values[13] != null)
                        {
                            cAnw.TSEin = Convert.ToDateTime(item.Values[13]);
                        }
                        if (item.Values[14] != null)
                        {
                            cAnw.TSAus = Convert.ToDateTime(item.Values[14]);
                        }
                        cAnw.Gesperrt = Convert.ToInt16(item.Values[15]);
                        cAnw.Verband = item.Values[16].ToString();
                        cAnw.EKLNr = item.Values[17].ToString();
                        cAnw.WertV = item.Values[18] == null ? 0 : Convert.ToDecimal(item.Values[18]);
                        cAnw.WertN = item.Values[19] == null ? 0 : Convert.ToDecimal(item.Values[19]);
                        cAnw.WertA = item.Values[20] == null ? 0 : Convert.ToDecimal(item.Values[20]);

                        if (item.Values[21] != null && Convert.ToInt16(item.Values[21]) == 1)
                        {
                            cAnw.EiszeitenESHERSVormittag = 1;
                        }
                        if (item.Values[22] != null && Convert.ToInt16(item.Values[22]) == 1)
                        {
                            cAnw.EiszeitenESHERSAbend = 1;
                        }
                        cAnw.WertEiszeitenESHERS = item.Values[23] == null ? 0 : Convert.ToDecimal(item.Values[23]);

                        cAnw.Zusatzgebuehr = 0;
                        cAnw.BuchungEismeister = 0;

                        if (anwesende.Count() == 0)
                        {
                            anwesende.Add(cAnw);
                        }
                        else
                        {
                            Anwesenheitsliste resultanwlst = anwesende.FirstOrDefault(x => x.KdNr == cAnw.KdNr && x.Ort == cAnw.Ort && x.Datum.Date.CompareTo(cAnw.Datum.Date) == 0);
                            if (resultanwlst != null)
                            {
                                if (cAnw.Vormittag == 1)
                                    resultanwlst.Vormittag = cAnw.Vormittag;
                                if (cAnw.Nachmittag == 1)
                                    resultanwlst.Nachmittag = cAnw.Nachmittag;
                                if (cAnw.Abend == 1)
                                    resultanwlst.Abend = cAnw.Abend;
                                if (cAnw.Adult == 1)
                                    resultanwlst.Adult = cAnw.Adult;
                                if (cAnw.EiszeitenESHERSVormittag == 1)
                                    resultanwlst.EiszeitenESHERSVormittag = cAnw.EiszeitenESHERSVormittag;
                                if (cAnw.EiszeitenESHERSAbend == 1)
                                    resultanwlst.EiszeitenESHERSAbend = cAnw.EiszeitenESHERSAbend;

                                resultanwlst.Wert = cAnw.Wert;
                                resultanwlst.Zeit = cAnw.Zeit;

                            }
                            else
                            {
                                Anwesenheitsliste resultanwlst1 = anwesende.FirstOrDefault(x => x.KdNr == cAnw.KdNr && x.Datum.Date.CompareTo(cAnw.Datum.Date) == 0);
                                if (resultanwlst1 != null)
                                {
                                    Decimal deAbg = resultanwlst1.abgebuchterBetrag;
                                    Decimal deWert = 0;
                                    List<Anwesenheitsliste> resultanwlst2 = anwesende.Where(x => x.KdNr == cAnw.KdNr && x.Datum.Date.CompareTo(cAnw.Datum.Date) == 0).ToList();
                                    foreach (var item2 in resultanwlst2)
                                    {
                                        deWert = item2.Wert;
                                    }
                                }

                                anwesende.Add(cAnw);
                            }
                        }

                    }
                }

                return anwesende;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<int> CheckEinAus(string kdnr, DateTime ein, int location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.ExecuteSprocAsync(new System.Threading.CancellationToken(), "CHECK_EIN_AUS_ESH", new OperandValue(kdnr), new OperandValue(ein.ToString("yyyyMMddHHmmss")), new OperandValue(location));
                var returnValue = Convert.ToInt32(result.ResultSet.FirstOrDefault().Rows.FirstOrDefault().Values.FirstOrDefault());
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<int> CheckEinbuchen(string kdnr, int location, DateTime ein, bool buchungssystem, bool eismeister)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.ExecuteSprocAsync(new System.Threading.CancellationToken(), "CHECK_EINBUCHEN", new OperandValue(kdnr),
                                            new OperandValue(location),
                                            new OperandValue(ein.ToString("yyyyMMddHHmmss")),
                                            new OperandValue(buchungssystem ? 1 : 0),
                                            new OperandValue(eismeister ? 1 : 0));
                var returnValue = Convert.ToInt32(result.ResultSet.FirstOrDefault().Rows.FirstOrDefault().Values.FirstOrDefault());
                return returnValue;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static Int32 inAnzeigeGesperrt;
        private static Int32 inWertTrainingsverbot;
        private static Int32 inVorlaufzeit;
        private static Int32 inNachlaufzeit;
        private static Int32 inUmstellungAbbuchung;
        private static Int32 inWertUmstellungAbbuchung;
        private static Boolean boAutosperre;
        private static String stArtNrParken;
        private static Boolean EiszeitenESHERSAktiv;

        private static Decimal deVormittag = 0;
        private static Decimal deNachmittag = 0;
        private static Decimal deAbend = 0;
        private static Decimal deEiszeitenESHERS = 0;

        private static bool IsTrainingVormittag = false;
        private static bool IsTrainingNachmittag = false;
        private static bool IsTrainingAbend = false;
        private static bool IsTrainingEiszeitenESHERS = false;
        private static bool IsTrainingEiszeitenESHERSAbend = false;

        private static string TrainingBezeichnung = "";


        //public static async Task<int> CheckAusbuchen(string kdnr, int location, DateTime now, bool buchungssystem, bool eismeister, bool zusatzgebuehr, string username)
        //{
        //    try
        //    {
        //        if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
        //        {
        //            UOW.UOW.Connect();
        //        }


        //        var result = await UOW.UOW.uow.ExecuteSprocAsync(new System.Threading.CancellationToken(), "CHECK_AUSBUCHEN_V2",
        //            new OperandValue(kdnr),
        //            new OperandValue(location),
        //            new OperandValue(now.ToString("yyyyMMddHHmmss")),
        //            new OperandValue(buchungssystem ? 1 : 0),
        //            new OperandValue(eismeister ? 1 : 0));

        //        var returnValue = Convert.ToInt32(result.ResultSet.FirstOrDefault().Rows.FirstOrDefault().Values.FirstOrDefault());
        //        if (returnValue == 0)
        //        {
        //            var returnkunde = result.ResultSet.Skip(1).FirstOrDefault();
        //            var kunde = returnkunde.Rows.FirstOrDefault().Values.ToList();

        //            string typ = kunde[12].ToString();

        //            var returnzeiten = result.ResultSet.Skip(2).FirstOrDefault();
        //            //var zeiten = returnzeiten.Rows.FirstOrDefault().Values.ToList();
        //            var returneintritt = result.ResultSet.Skip(3).FirstOrDefault();
        //            //var eintritte = returneintritt.Rows.FirstOrDefault().Values.ToList();
        //            var returnpreise = result.ResultSet.Skip(4).FirstOrDefault();
        //            var returninotherlocation = result.ResultSet.Skip(5).FirstOrDefault();


        //            decimal preis = Convert.ToDecimal(returnpreise.Rows.FirstOrDefault().Values[0]);
        //            decimal preisabend = Convert.ToDecimal(returnpreise.Rows.FirstOrDefault().Values[1]);
        //            decimal preiseiszeiteneshers = Convert.ToDecimal(returnpreise.Rows.FirstOrDefault().Values[2]);
        //            int gebuchtId = Convert.ToInt32(returnpreise.Rows.FirstOrDefault().Values[3]);
        //            DateTime dtEin = Convert.ToDateTime(returnpreise.Rows.FirstOrDefault().Values[4]);

        //            List<Training.Training> trainings = new List<BusinessLogic.Training.Training>();

        //            if (typ.ToLower() == "adult")
        //            {
        //                trainings.AddRange(GetAdultTrainings(returnzeiten, preis, preisabend));
        //                decimal Trainingwert = await GetAdultTrainingWert(returneintritt, trainings, preis, preisabend, kdnr, location, username);

        //                result = await UOW.UOW.uow.ExecuteSprocAsync(new System.Threading.CancellationToken(), "UPDATE_GEBUCHT",
        //                new OperandValue(kdnr),        // Kartennummer
        //                new OperandValue(gebuchtId),            // Tabelle gebucht ID
        //                new OperandValue(Trainingwert),         // Wert
        //                new OperandValue(0),                    // 0 für Wert VM Adults
        //                new OperandValue(0),                    // 0 für Wert NM Adults
        //                new OperandValue(0),                    // 0 für Wert Abend Adults
        //                new OperandValue(0),                    // 0 für Wert Eiszeiten ESH/ERS Adult
        //                new OperandValue(4),                    // 4 Trainingsnummer Adult
        //                new OperandValue(dtEin.ToString("yyyyMMddHHmmss")),  // TSEin,
        //                new OperandValue(now.ToString("yyyyMMddHHmmss")),  // TSAus,
        //                new OperandValue(0),                    // 0 = false Vormittagstraining Adult
        //                new OperandValue(0),                    // 0 = false Nachmittagstraining Adult
        //                new OperandValue(0),                    // 0 = false Abendtraining Adult
        //                new OperandValue(1),                    // 1 = true Adulttraining
        //                new OperandValue(0),                    // 0 = false Eiszeiten ESH/ERS Vormittag Adult
        //                new OperandValue(0),                    // 0 = false Eiszeiten ESH/ERS Abend Adult
        //                new OperandValue("Adult"),              // Trainingsbezeichnung
        //                new OperandValue(location),        // Location
        //                new OperandValue(zusatzgebuehr));                   // 0 = false Zusatzgebühr, wird nur über Admin Programm verwendet

        //                returnValue = Convert.ToInt32(result.ResultSet.FirstOrDefault().Rows.FirstOrDefault().Values.FirstOrDefault());

        //            }
        //            else
        //            {
        //                trainings.AddRange(GetTrainings(returnzeiten, preis, preisabend, preiseiszeiteneshers, typ));
        //                decimal Trainingwert = await GetTrainingWert(returneintritt, trainings, preis, preisabend, preiseiszeiteneshers, returninotherlocation, kdnr, location, username);

        //                result = await UOW.UOW.uow.ExecuteSprocAsync(new System.Threading.CancellationToken(), "UPDATE_GEBUCHT",
        //                        new OperandValue(kdnr),                                     // Kartennummer
        //                        new OperandValue(gebuchtId),                                // Tabelle gebucht ID
        //                        new OperandValue(Trainingwert),                             // Wert
        //                        new OperandValue(deVormittag),                              // 0 für Wert VM
        //                        new OperandValue(deNachmittag),                             // 0 für Wert NM
        //                        new OperandValue(deAbend),                                  // 0 für Wert Abend
        //                        new OperandValue(deEiszeitenESHERS),                        // 0 für Wert Eiszeiten ESH/ERS
        //                        new OperandValue(0),                                        // keine Trainingsnummer
        //                        new OperandValue(dtEin.ToString("yyyyMMddHHmmss")),         // TSEin,
        //                        new OperandValue(now.ToString("yyyyMMddHHmmss")),           // TSAus,
        //                        new OperandValue(IsTrainingVormittag),                      // Vormittagstraining
        //                        new OperandValue(IsTrainingNachmittag),                     // Nachmittagstraining
        //                        new OperandValue(IsTrainingAbend),                          // Abendtraining
        //                        new OperandValue(0),                                        // 0 = false Adulttraining
        //                        new OperandValue(IsTrainingEiszeitenESHERS),                // Eiszeiten ESH/ERS Vormittag
        //                        new OperandValue(IsTrainingEiszeitenESHERSAbend),           // Eiszeiten ESH/ERS Abend
        //                        new OperandValue(TrainingBezeichnung),                              // Trainingsbezeichnung
        //                        new OperandValue(location),                                 // Location
        //                        new OperandValue(zusatzgebuehr));                           // 0 = false Zusatzgebühr, wird nur über Admin Programm verwendet

        //                returnValue = Convert.ToInt32(result.ResultSet.FirstOrDefault().Rows.FirstOrDefault().Values.FirstOrDefault());
        //            }
        //        }

        //        return returnValue;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //private static IEnumerable<Training.Training> GetAdultTrainings(SelectStatementResult zeiten, decimal preis, decimal preisabend)
        //{
        //    int order = 0;
        //    foreach (var item in zeiten.Rows)
        //    {
        //        int status = Convert.ToInt32(item.Values[9]);
        //        if (status != 4)
        //            continue;

        //        Training.Training tr = new Training.Training();
        //        tr.Startzeit = Convert.ToDateTime(item.Values[2]);
        //        tr.Endezeit = Convert.ToDateTime(item.Values[3]);
        //        tr.Status = Convert.ToInt32(item.Values[9]);

        //        tr.TrainingBez = item.Values[5].ToString();
        //        tr.Order = order;

        //        if (tr.Startzeit.Hour > 13)
        //        {
        //            tr.Wert = preisabend;
        //            tr.Abend = true;
        //        }
        //        else
        //        {
        //            tr.Wert = preis;
        //            tr.Vormittag = true;
        //        }

        //        yield return tr;
        //        order += 1;
        //    }
        //}

        //private static IEnumerable<Training.Training> GetTrainings(SelectStatementResult zeiten, decimal preis, decimal preisabend, decimal preiseiszeiteneshers, string typ)
        //{
        //    int order = 0;
        //    foreach (var item in zeiten.Rows)
        //    {
        //        int status = Convert.ToInt32(item.Values[9]);
        //        //if (status == 4)
        //        //    continue;

        //        Training.Training tr = new Training.Training();
        //        tr.Startzeit = Convert.ToDateTime(item.Values[2]);
        //        tr.Endezeit = Convert.ToDateTime(item.Values[3]);
        //        tr.Status = Convert.ToInt32(item.Values[9]);

        //        tr.TrainingBez = item.Values[5].ToString();
        //        tr.Order = order;
        //        if (tr.Status == 0 || tr.Status == 4)
        //        {
        //            tr.Vormittag = true;
        //            tr.Wert = preis;
        //        }
        //        else if (tr.Status == 1)
        //        {
        //            tr.Nachmittag = true;
        //            tr.Wert = preis;
        //        }
        //        else if (tr.Status == 2 || tr.Status == 6 || tr.Status == 22)
        //        {
        //            tr.Abend = true;
        //            tr.Wert = preisabend;
        //            if (tr.Status == 6)
        //                tr.TrainingBez = "SYS Synchron Eislauf";
        //            if (tr.Status == 22)
        //                tr.TrainingBez = "SYS Synchron Eislauf Anmietung";

        //        }
        //        else if (tr.Status == 3)
        //        {
        //            tr.Wert = preiseiszeiteneshers;

        //            if (tr.Startzeit.Hour > 13)
        //            {
        //                tr.TrainingBez += " Abend";
        //                tr.EiszeitenESHERSAbend = true;
        //            }
        //            else
        //            {
        //                tr.EiszeitenESHERS = true;
        //            }

        //        }
        //        tr.ZeitenKalenderId = Convert.ToInt32(item.Values[0]);
        //        yield return tr;
        //        order += 1;
        //    }
        //}

        //private static async Task<decimal> GetAdultTrainingWert(SelectStatementResult eintritt, List<Training.Training> trainings, decimal preis, decimal preisabend, string kdnr, int location, string username)
        //{
        //    bool IsTraining1 = false;
        //    bool IsTraining2 = false;

        //    foreach (var item in eintritt.Rows)
        //    {
        //        DateTime Ein = Convert.ToDateTime(item.Values[3]);
        //        DateTime Aus = Convert.ToDateTime(item.Values[4]);

        //        foreach (var itemtraining in trainings)
        //        {
        //            TimeSpan trainingEnde = itemtraining.Endezeit.TimeOfDay;
        //            if (Ein.TimeOfDay.CompareTo(trainingEnde) <= 0)
        //            {
        //                if (itemtraining.Vormittag)
        //                {
        //                    IsTraining1 = true;
        //                    if (!await FrequenzGebucht.IsAlreadyStored(kdnr, location, itemtraining.Status, Ein))
        //                    {
        //                        await ZeitenKalender.ZeitenKalender.UpdateFrequenz(itemtraining.ZeitenKalenderId, username);
        //                        await FrequenzGebucht.Add(kdnr, location, itemtraining.Status, Ein, true);
        //                        await UOW.UOW.SaveAsync();
        //                    }
        //                }
        //                else
        //                {
        //                    IsTraining2 = true;
        //                    if (!await FrequenzGebucht.IsAlreadyStored(kdnr, location, itemtraining.Status, Ein))
        //                    {
        //                        await ZeitenKalender.ZeitenKalender.UpdateFrequenz(itemtraining.ZeitenKalenderId, username);
        //                        await FrequenzGebucht.Add(kdnr, location, itemtraining.Status, Ein, true);
        //                        await UOW.UOW.SaveAsync();
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    decimal Trainingwert = 0;
        //    if (IsTraining1)
        //    {
        //        Trainingwert += preis;
        //    }
        //    if (IsTraining2)
        //    {
        //        Trainingwert += preisabend;
        //    }

        //    return Trainingwert;
        //}

        //private static async Task<decimal> GetTrainingWert(SelectStatementResult eintritt, List<Training.Training> trainings, decimal preis,
        //    decimal preisabend, decimal preiseiszeiteneshers, SelectStatementResult returninotherlocation, string kdnr, int location, string username)
        //{
        //    foreach (var item in eintritt.Rows)
        //    {
        //        DateTime Ein = Convert.ToDateTime(item.Values[3]);
        //        DateTime Aus = Convert.ToDateTime(item.Values[4]);

        //        var trainingsein = trainings.Where(x => x.Startzeit.TimeOfDay.Subtract(new TimeSpan(0, inVorlaufzeit, 0)).CompareTo(Ein.TimeOfDay) <= 0).OrderBy(x => x.Startzeit).ToList();
        //        var resultein = trainingsein.Where(x => x.Endezeit.TimeOfDay.Subtract(new TimeSpan(0, inVorlaufzeit, 0)).CompareTo(Ein.TimeOfDay) >= 0).FirstOrDefault();

        //        var resultaus = trainings.Where(x => x.Endezeit.TimeOfDay.CompareTo(Aus.TimeOfDay.Subtract(new TimeSpan(0, inNachlaufzeit, 0))) >= 0 && x.Order >= resultein.Order).OrderBy(x => x.Endezeit).FirstOrDefault();

        //        if (resultaus == null)
        //        {
        //            resultaus = trainings.Where(x => x.Endezeit.TimeOfDay.CompareTo(Aus.TimeOfDay.Subtract(new TimeSpan(0, inNachlaufzeit, 0))) < 0).OrderByDescending(x => x.Endezeit).FirstOrDefault();
        //        }
        //        if (resultein == null)
        //        {
        //            resultein = resultaus;
        //        }

        //        int orderein = resultein.Order;
        //        int orderaus = resultaus.Order;


        //        for (int i = orderein; i <= orderaus; i++)
        //        {
        //            if (trainings[i].EiszeitenESHERS == true)
        //            {
        //                if (!await FrequenzGebucht.IsAlreadyStored(kdnr, location, trainings[i].Status, Ein))
        //                {
        //                    await ZeitenKalender.ZeitenKalender.UpdateFrequenz(trainings[i].ZeitenKalenderId, username);
        //                    await FrequenzGebucht.Add(kdnr, location, trainings[i].Status, Ein, true);
        //                    await UOW.UOW.SaveAsync();
        //                }

        //                IsTrainingEiszeitenESHERS = true;
        //                deEiszeitenESHERS = trainings[i].Wert;
        //                if (!TrainingBezeichnung.StartsWith("Eiszeiten ESH/ERS"))
        //                    TrainingBezeichnung += "Eiszeiten ESH/ERS ";
        //            }
        //            if (trainings[i].Vormittag == true)
        //            {
        //                if (!await FrequenzGebucht.IsAlreadyStored(kdnr, location, trainings[i].Status, Ein))
        //                {
        //                    await ZeitenKalender.ZeitenKalender.UpdateFrequenz(trainings[i].ZeitenKalenderId, username);
        //                    await FrequenzGebucht.Add(kdnr, location, trainings[i].Status, Ein, true);
        //                    await UOW.UOW.SaveAsync();
        //                }

        //                IsTrainingVormittag = true;
        //                deVormittag = trainings[i].Wert;
        //                if (!TrainingBezeichnung.Contains("Vormittag"))
        //                    TrainingBezeichnung += "Vormittag ";
        //            }
        //            if (trainings[i].Nachmittag == true)
        //            {
        //                if (!await FrequenzGebucht.IsAlreadyStored(kdnr, location, trainings[i].Status, Ein))
        //                {
        //                    await ZeitenKalender.ZeitenKalender.UpdateFrequenz(trainings[i].ZeitenKalenderId, username);
        //                    await FrequenzGebucht.Add(kdnr, location, trainings[i].Status, Ein, true);
        //                    await UOW.UOW.SaveAsync();
        //                }

        //                IsTrainingNachmittag = true;
        //                deNachmittag = trainings[i].Wert;
        //                if (!TrainingBezeichnung.Contains("Nachmittag"))
        //                    TrainingBezeichnung += "Nachmittag ";
        //            }
        //            if (trainings[i].Abend == true)
        //            {
        //                if (!await FrequenzGebucht.IsAlreadyStored(kdnr, location, trainings[i].Status, Ein))
        //                {
        //                    await ZeitenKalender.ZeitenKalender.UpdateFrequenz(trainings[i].ZeitenKalenderId, username);
        //                    await FrequenzGebucht.Add(kdnr, location, trainings[i].Status, Ein, true);
        //                    await UOW.UOW.SaveAsync();
        //                }

        //                IsTrainingAbend = true;
        //                deAbend = trainings[i].Wert;
        //                if (trainings[i].Status == 2)
        //                {
        //                    if (!TrainingBezeichnung.Contains("Abend"))
        //                        TrainingBezeichnung += "Abend ";
        //                }
        //                else if (trainings[i].Status == 6)
        //                {
        //                    if (!TrainingBezeichnung.Contains("SYS Synchron"))
        //                        TrainingBezeichnung += "SYS Synchron ";
        //                }
        //                else if (trainings[i].Status == 22)
        //                {
        //                    if (!TrainingBezeichnung.Contains("SYS Synchron Anmietung"))
        //                        TrainingBezeichnung += "SYS Synchron Anmietung ";
        //                }
        //            }
        //            if (trainings[i].EiszeitenESHERSAbend == true)
        //            {
        //                if (!await FrequenzGebucht.IsAlreadyStored(kdnr, location, trainings[i].Status, Ein))
        //                {
        //                    await ZeitenKalender.ZeitenKalender.UpdateFrequenz(trainings[i].ZeitenKalenderId, username);
        //                    await FrequenzGebucht.Add(kdnr, location, trainings[i].Status, Ein, true);
        //                    await UOW.UOW.SaveAsync();
        //                }

        //                IsTrainingEiszeitenESHERSAbend = true;
        //                deEiszeitenESHERS = trainings[i].Wert;
        //                if (!TrainingBezeichnung.Trim().EndsWith("Eiszeiten ESH/ERS"))
        //                    TrainingBezeichnung += "Eiszeiten ESH/ERS ";
        //            }
        //        }
        //    }

        //    TrainingBezeichnung = TrainingBezeichnung.Trim();
        //    decimal Trainingwert = 0;


        //    bool otherLocationEiszeitenESHERS = false;
        //    bool otherLocationVM = false;
        //    bool otherLocationNM = false;
        //    bool otherLocationAbend = false;
        //    bool otherLocationAdult = false;
        //    bool otherLocationEiszeitenESHERSAbend = false;


        //    if (returninotherlocation != null)
        //    {
        //        var otherLocation = returninotherlocation;
        //        foreach (var item in otherLocation.Rows)
        //        {
        //            if (Convert.ToBoolean(item.Values[0]) == true)
        //                otherLocationEiszeitenESHERS = true;
        //            if (Convert.ToBoolean(item.Values[1]) == true)
        //                otherLocationVM = true;
        //            if (Convert.ToBoolean(item.Values[2]) == true)
        //                otherLocationNM = true;
        //            if (Convert.ToBoolean(item.Values[3]) == true)
        //                otherLocationAbend = true;
        //            if (Convert.ToBoolean(item.Values[4]) == true)
        //                otherLocationAdult = true;
        //            if (Convert.ToBoolean(item.Values[5]) == true)
        //                otherLocationEiszeitenESHERSAbend = true;
        //        }
        //    }


        //    if (IsTrainingEiszeitenESHERS)
        //    {
        //        if (!otherLocationEiszeitenESHERS)
        //            Trainingwert += preiseiszeiteneshers;
        //    }
        //    if (IsTrainingVormittag && !IsTrainingEiszeitenESHERS)
        //    {
        //        if (!otherLocationVM)
        //            Trainingwert += preis;
        //    }
        //    if (IsTrainingNachmittag)
        //    {
        //        if (!otherLocationNM)
        //            Trainingwert += preis;
        //    }
        //    if (IsTrainingAbend)
        //    {
        //        if (!otherLocationAbend)
        //            Trainingwert += preis;
        //    }
        //    if (IsTrainingEiszeitenESHERSAbend && !IsTrainingAbend && !IsTrainingNachmittag)
        //    {
        //        if (!otherLocationEiszeitenESHERSAbend)
        //            Trainingwert += preiseiszeiteneshers;
        //    }

        //    return Trainingwert;
        //}




    }

    public class WertAenderung
    {
        public String KdNr { get; set; }
        public Decimal? Wert { get; set; }
        public DateTime Datum { get; set; }
        public bool? Vormittag { get; set; }
        public bool? Nachmittag { get; set; }
        public bool? Abend { get; set; }
        public bool? Adult { get; set; }
        public bool? ESHERSVorm { get; set; }
        public bool? ESHERSAbend { get; set; }
        public decimal? WertESHERS { get; set; }
        public int Location { get; set; }
        public int RowHandle { get; set; }
    }

    public class Anwesenheitsliste
    {
        public String KdNr { get; set; }
        public String VName { get; set; }
        public String NName { get; set; }
        public Int32 Ort { get; set; }
        public DateTime Datum { get; set; }
        public int Vormittag { get; set; }
        public int Nachmittag { get; set; }
        public int Abend { get; set; }
        public int Adult { get; set; }
        public String Zeit { get; set; }
        public Decimal Wert { get; set; }
        public Decimal abgebuchterBetrag { get; set; }
        public Decimal Barverkauf { get; set; }
        public int Gesperrt { get; set; }
        public String Verband { get; set; }
        public String EKLNr { get; set; }
        public DateTime? TSEin { get; set; }
        public DateTime? TSAus { get; set; }
        public Double Parken { get; set; }

        public Decimal WertV { get; set; }
        public Decimal WertN { get; set; }
        public Decimal WertA { get; set; }
        public int EiszeitenESHERSVormittag { get; set; }
        public int EiszeitenESHERSAbend { get; set; }
        public Decimal WertEiszeitenESHERS { get; set; }
        public String ZeitAnwesend { get; set; }
        public int Zusatzgebuehr { get; set; }
        public int BuchungEismeister { get; set; }

    }
}
