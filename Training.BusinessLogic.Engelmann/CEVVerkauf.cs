using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Engelmann
{
    public class CEVVerkauf
    {
        public int ID { get; set; }
        public string Mitgliedsnummer { get; set; }
        public int Saison { get; set; }
        public decimal CEVBetrag { get; set; }
        public bool SaisonkarteEngelmann { get; set; }
        public decimal EngelmannBetrag { get; set; }
        public bool BezahltCEV { get; set; }
        public bool BezahltEngelmann { get; set; }
        public DateTime? DatumBezahltCEV { get; set; }
        public DateTime? DatumBezahltEngelmann { get; set; }
        public bool Teilbetrag { get; set; }
        public bool Adult { get; set; }
        public decimal CEVBetragBezahlt { get; set; }
        public decimal EngelmannBetragBezahlt { get; set; }
        public bool? Gast { get; set; }
        public bool? Erlagschein { get; set; }
        public bool? Mail { get; set; }

        public static async Task<List<CEVVerkauf>> GetNichtBezahltByMitgliedsnummerAsync(string mitgliedsnummer)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<cevverkauf>().Where(x => x.Mitgliedsnummer == mitgliedsnummer && (x.BezahltCEV == false || x.BezahltEngelmann == false)).OrderBy(x => x.ID).ToListAsync();
                if (result == null)
                    return null;

                List<CEVVerkauf> cv = new List<CEVVerkauf>();
                foreach (var x in result)
                {
                    cv.Add(new CEVVerkauf
                    {
                        Adult = x.Adult,
                        BezahltCEV = x.BezahltCEV,
                        BezahltEngelmann = x.BezahltEngelmann,
                        ID = x.ID,
                        CEVBetrag = x.CEVBetrag,
                        CEVBetragBezahlt = x.CEVBetragBezahlt,
                        DatumBezahltCEV = x.DatumBezahltCEV,
                        DatumBezahltEngelmann = x.DatumBezahltEngelmann,
                        EngelmannBetrag = x.EngelmannBetrag,
                        EngelmannBetragBezahlt = x.EngelmannBetragBezahlt,
                        Erlagschein = x.Erlagschein,
                        Gast = x.Gast,
                        Mail = x.Mail,
                        Mitgliedsnummer = x.Mitgliedsnummer,
                        Saison = x.Saison,
                        SaisonkarteEngelmann = x.SaisonkarteEngelmann,
                        Teilbetrag = x.Teilbetrag,
                    });
                }
                return cv;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<CEVVerkauf>> GetByMitgliedsnummerAsync(string mitgliedsnummer, int saison = 0)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<cevverkauf>().Where(x => x.Mitgliedsnummer == mitgliedsnummer).OrderByDescending(x => x.Saison).ToListAsync();
                if (result == null)
                    return null;

                List<CEVVerkauf> cv = new List<CEVVerkauf>();
                foreach (var x in result)
                {
                    cv.Add(new CEVVerkauf
                    {
                        Adult = x.Adult,
                        BezahltCEV = x.BezahltCEV,
                        BezahltEngelmann = x.BezahltEngelmann,
                        ID = x.ID,
                        CEVBetrag = x.CEVBetrag,
                        CEVBetragBezahlt = x.CEVBetragBezahlt,
                        DatumBezahltCEV = x.DatumBezahltCEV,
                        DatumBezahltEngelmann = x.DatumBezahltEngelmann,
                        EngelmannBetrag = x.EngelmannBetrag,
                        EngelmannBetragBezahlt = x.EngelmannBetragBezahlt,
                        Erlagschein = x.Erlagschein,
                        Gast = x.Gast,
                        Mail = x.Mail,
                        Mitgliedsnummer = x.Mitgliedsnummer,
                        Saison = x.Saison,
                        SaisonkarteEngelmann = x.SaisonkarteEngelmann,
                        Teilbetrag = x.Teilbetrag,
                    });
                }
                return cv;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<CEVVerkauf> GetByMitgliedsnummerSaisonAsync(string mitgliedsnummer, int saison)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<cevverkauf>().Where(x => x.Mitgliedsnummer == mitgliedsnummer && x.Saison == saison).FirstOrDefaultAsync();
                if (result == null)
                    return null;

                return new CEVVerkauf
                {
                    Adult = result.Adult,
                    BezahltCEV = result.BezahltCEV,
                    BezahltEngelmann = result.BezahltEngelmann,
                    ID = result.ID,
                    CEVBetrag = result.CEVBetrag,
                    CEVBetragBezahlt = result.CEVBetragBezahlt,
                    DatumBezahltCEV = result.DatumBezahltCEV,
                    DatumBezahltEngelmann = result.DatumBezahltEngelmann,
                    EngelmannBetrag = result.EngelmannBetrag,
                    EngelmannBetragBezahlt = result.EngelmannBetragBezahlt,
                    Erlagschein = result.Erlagschein,
                    Gast = result.Gast,
                    Mail = result.Mail,
                    Mitgliedsnummer = result.Mitgliedsnummer,
                    Saison = result.Saison,
                    SaisonkarteEngelmann = result.SaisonkarteEngelmann,
                    Teilbetrag = result.Teilbetrag,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task DeleteAsync(int Id, bool saveImmediatly)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<cevverkauf>().Where(x => x.ID == Id).FirstOrDefaultAsync();
                if (result != null)
                {
                    await UOW.Uow._uow.DeleteAsync(result);
                }

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

        public static async Task UpdateAsync(int Id, decimal betragCEVBezahlt, decimal betragEngelmannBezahlt, bool bezahltCEV, bool bezahltEngelmann,
            DateTime? datumBezahltCEV, DateTime? datumBezahltEngelmann)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<cevverkauf>().Where(x => x.ID == Id).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.CEVBetragBezahlt = betragCEVBezahlt;
                    result.EngelmannBetragBezahlt = betragEngelmannBezahlt;
                    result.BezahltCEV = bezahltCEV;
                    result.BezahltEngelmann = bezahltEngelmann;
                    result.DatumBezahltCEV = datumBezahltCEV;
                    result.DatumBezahltEngelmann = datumBezahltEngelmann;
                }

                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static async Task AddAsync(string mitgliedsnr, int saison, decimal cevBetrag,bool saisonkarteEngelmann, decimal engelmannBetrag,
            decimal betragCEVBezahlt, decimal betragEngelmannBezahlt, bool bezahltCEV, bool bezahltEngelmann,bool teilBetrag, bool adult,
            DateTime? datumBezahltCEV, DateTime? datumBezahltEngelmann, bool gast)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                cevverkauf cevverkauf = new cevverkauf(UOW.Uow._uow)
                {
                    Mitgliedsnummer = mitgliedsnr,
                    Saison = saison,
                    CEVBetrag = cevBetrag,
                    SaisonkarteEngelmann = saisonkarteEngelmann,
                    EngelmannBetrag = engelmannBetrag,
                    Teilbetrag = teilBetrag,
                    Adult = adult,
                    CEVBetragBezahlt = betragCEVBezahlt,
                    EngelmannBetragBezahlt = betragEngelmannBezahlt,
                    BezahltCEV = bezahltCEV,
                    BezahltEngelmann = bezahltEngelmann,
                    DatumBezahltCEV = datumBezahltCEV,
                    DatumBezahltEngelmann = datumBezahltEngelmann,
                    Gast = gast,
                };

                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAsync(int id, int saison, decimal cevBetrag, bool saisonkarteEngelmann, decimal engelmannBetrag,
            decimal betragCEVBezahlt, decimal betragEngelmannBezahlt, bool bezahltCEV, bool bezahltEngelmann, bool teilBetrag, bool adult,
            DateTime? datumBezahltCEV, DateTime? datumBezahltEngelmann, bool gast)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var cevVerkauf = await UOW.Uow._uow.Query<cevverkauf>().Where(x => x.ID == id).FirstOrDefaultAsync();        
                if (cevVerkauf != null)
                {
                    cevVerkauf.Saison = saison;
                    cevVerkauf.CEVBetrag = cevBetrag;
                    cevVerkauf.SaisonkarteEngelmann = saisonkarteEngelmann;
                    cevVerkauf.EngelmannBetrag = engelmannBetrag;
                    cevVerkauf.Teilbetrag = teilBetrag;
                    cevVerkauf.Adult = adult;
                    cevVerkauf.CEVBetragBezahlt = betragCEVBezahlt;
                    cevVerkauf.EngelmannBetragBezahlt = betragEngelmannBezahlt;
                    cevVerkauf.BezahltCEV = bezahltCEV;
                    cevVerkauf.BezahltEngelmann = bezahltEngelmann;
                    cevVerkauf.DatumBezahltCEV = datumBezahltCEV;
                    cevVerkauf.DatumBezahltEngelmann = datumBezahltEngelmann;
                    cevVerkauf.Gast = gast;
                };

                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateErlagscheinAsync(int saison, List<string> mitgliedsnummern)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
                var result = await UOW.Uow._uow.Query<cevverkauf>().Where(x => x.Saison == saison && mitgliedsnummern.Contains(x.Mitgliedsnummer)).ToListAsync();
                foreach (var item in result)
                {
                    item.Erlagschein = true;
                }
                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateMailAsync(int saison, string mitgliedsnummer)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }
                var result = await UOW.Uow._uow.Query<cevverkauf>().Where(x => x.Saison == saison && x.Mitgliedsnummer == mitgliedsnummer).FirstOrDefaultAsync();
                if (result != null)
                {
                    result.Mail = true;
                }

                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
