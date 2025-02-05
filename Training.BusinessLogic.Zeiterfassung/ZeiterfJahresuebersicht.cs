using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung;

public class ZeiterfJahresuebersicht
{
        public string Monat { get; set; }
        public string Jahr { get; set; }
        public string GeleisteteStunden { get; set; }
        public string ESH { get; set; }
        public string ERS { get; set; }
        public string Engelmann { get; set; }
        public string Sollstunden { get; set; }
        public string MehrMinder { get; set; }
        public string Ausbezahlt { get; set; }
        public string RestStunden { get; set; }
        public string Urlaub { get; set; }
        public string UrlaubNeu { get; set; }
        public string Krank { get; set; }
        public string NA { get; set; }
        public string Bemerkung { get; set; }
        public string NH3Stunden { get; set; }
        public string NH3Prozent { get; set; }

        public bool IsAbgeschlossen { get; set; }



        public static async Task<List<ZeiterfJahresuebersicht>> Get(string mitarbeiterId, DateTime date)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var mitarbeiter = await ZeiterfMitarbeiter.GetById(mitarbeiterId);
                DateTime urlaubNeu = Convert.ToDateTime(mitarbeiter.DatumUrlaubNeu);
                int jahresurlaub = Convert.ToInt32(mitarbeiter.Jahresurlaub);

                var urlaubza = await ZeiterfUrlaubZA.GetByMitarbeiterAndYearAsync(mitarbeiterId, date);

                string zaUebertrag = "00:00";
                DateTime dateUebertrag = date.AddMonths(-1);
                var urlaubzauebertrag = await ZeiterfUrlaubZA.GetByMitarbeiterAndDateAsync(mitarbeiterId, dateUebertrag);
                if (urlaubzauebertrag != null)
                {
                    zaUebertrag = urlaubzauebertrag.Saldo;
                    if (zaUebertrag == "0")
                    {
                        zaUebertrag = "00:00";
                    }                    
                }


                var uebersicht = new List<ZeiterfJahresuebersicht>();

                for (int i = 0; i < 12; i++)
                {
                    var monatsuebersicht = await GetForMonth(mitarbeiterId, date);

                    if (date.Month == urlaubNeu.Month)
                    {
                        monatsuebersicht.UrlaubNeu = jahresurlaub.ToString();
                    }

                    var urlaubzamonth = urlaubza.Where(x => x.Jahr == date.Year && x.Monat == date.Month)
                        .FirstOrDefault();
                    if (urlaubzamonth != null)
                    {
                        monatsuebersicht.Ausbezahlt = urlaubzamonth.Ausbezahlt;
                        monatsuebersicht.Bemerkung = urlaubzamonth.Bemerkung;
                    }
                    else
                    {
                        monatsuebersicht.Ausbezahlt = "";
                        monatsuebersicht.Bemerkung = "";
                    }

                    monatsuebersicht.RestStunden = CalcRestStunden(
                        i == 0 ? true : false,
                        monatsuebersicht.Ausbezahlt,
                        monatsuebersicht.MehrMinder,
                        zaUebertrag);


                    uebersicht.Add(monatsuebersicht);
                    date = date.AddMonths(1);
                }

                return uebersicht;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static async Task<ZeiterfJahresuebersicht> GetForMonth(string mitarbeiterid, DateTime date)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                DateTime von = new DateTime(date.Year, date.Month, 1);
                DateTime bis = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year,date.Month));

                ZeiterfJahresuebersicht uebersicht = new ZeiterfJahresuebersicht();

                uebersicht.Monat = date.ToString("MMMM");
                uebersicht.Jahr = date.Year.ToString();

                var abschluss = await UOW.Uow._uow.Query<zeiterfmonatabschluss>()
                                    .Where(x => x.MitarbeiterId == mitarbeiterid && x.Datum.Value.Date >= von.Date && x.Datum.Value.Date <= bis.Date)
                                    .ToListAsync();

                int urlaub = 0;
                int krank = 0;

                
                if (abschluss != null && abschluss.Count > 0)
                {
                    long geleisteteStunden = 0;
                    long istStunden = 0;
                    long sollStunden = 0;
                    long saldo = 0;
                    long esh = 0;
                    long ers = 0;
                    long engelmann = 0;
                    long nh3Stunden = 0;

                    foreach (var item in abschluss)
                    {
                        urlaub += !string.IsNullOrEmpty(item.Urlaub) ? Convert.ToInt32(item.Urlaub) : 0;
                        krank += item.Krankenstand != null ? Convert.ToInt32(item.Krankenstand) : 0;

                        if (!string.IsNullOrEmpty(item.Anwesenheit))
                        {
                            geleisteteStunden = CalcGeleisteteStunden(geleisteteStunden, item.Anwesenheit);
                        }

                        if (!string.IsNullOrEmpty(item.Sollstunden))
                        {
                            sollStunden = CalcSollStunden(sollStunden, item.Sollstunden);  
                        }
                        
                        if (!string.IsNullOrEmpty(item.Iststunden))
                        {
                            istStunden = CalcIstStunden(istStunden, item.Iststunden);
                        }
                        
                        if (!string.IsNullOrEmpty(item.ESH))
                        {
                            esh = CalcEsh(esh, item.ESH);
                        }
                        if (!string.IsNullOrEmpty(item.ERS))
                        {
                            ers = CalcErs(ers, item.ERS);
                        }
                        if (!string.IsNullOrEmpty(item.Engelmann))
                        {
                            engelmann = CalcEngelmann(engelmann, item.Engelmann);
                        }
                        if (!string.IsNullOrEmpty(item.NH3Zeit))
                        {
                            nh3Stunden = CalcNh3Stunden(nh3Stunden, item.NH3Zeit);
                        }
                    }

                    if (geleisteteStunden < 0)
                    {
                        TimeSpan tsGeleistetSumme = new TimeSpan(geleisteteStunden * -1);
                        uebersicht.GeleisteteStunden =  "-" + (tsGeleistetSumme.Days * 24 + tsGeleistetSumme.Hours).ToString().PadLeft(2, '0') + ":" + tsGeleistetSumme.Minutes.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        TimeSpan tsGeleistetSumme = new TimeSpan(geleisteteStunden);
                        uebersicht.GeleisteteStunden =  (tsGeleistetSumme.Days * 24 + tsGeleistetSumme.Hours).ToString().PadLeft(2, '0') + ":" + tsGeleistetSumme.Minutes.ToString().PadLeft(2, '0');
                    }
                    
                    TimeSpan tsSollSumme = new TimeSpan(sollStunden);
                    uebersicht.Sollstunden = (tsSollSumme.Days * 24 + tsSollSumme.Hours).ToString().PadLeft(2, '0') + ":" + tsSollSumme.Minutes.ToString().PadLeft(2, '0');

                    saldo = geleisteteStunden - sollStunden;
                    if (saldo < 0)
                    {
                        TimeSpan tsDiff = new TimeSpan(saldo * -1);
                        uebersicht.MehrMinder =  "-" + (tsDiff.Days * 24 + tsDiff.Hours).ToString().PadLeft(2, '0') + ":" + tsDiff.Minutes.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        TimeSpan tsDiff = new TimeSpan(saldo);
                        uebersicht.MehrMinder = (tsDiff.Days * 24 + tsDiff.Hours).ToString().PadLeft(2, '0') + ":" + tsDiff.Minutes.ToString().PadLeft(2, '0');
                    }

                    TimeSpan ts = new TimeSpan(esh);
                    uebersicht.ESH = (ts.Days * 24 + ts.Hours).ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0');

                    ts = new TimeSpan(ers);
                    uebersicht.ERS = (ts.Days * 24 + ts.Hours).ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0');

                    ts = new TimeSpan(engelmann);
                    uebersicht.Engelmann = (ts.Days * 24 + ts.Hours).ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0');

                    ts = new TimeSpan(nh3Stunden);
                    uebersicht.NH3Stunden = (ts.Days * 24 + ts.Hours).ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0');

                    double nh3Value = 0;
                    if (nh3Stunden > 0)
                    {
                        nh3Value = nh3Stunden * 100;
                        nh3Value = nh3Value / istStunden;
                    }
                    uebersicht.NH3Prozent = nh3Value.ToString("0.00") + "%";


                    uebersicht.Urlaub = urlaub.ToString();
                    uebersicht.Krank = krank.ToString();
                    uebersicht.IsAbgeschlossen = true;
                }
                else
                {
                    var resultuebersicht = await ZeiterfUebersicht.GetMonthForMitarbeiter(mitarbeiterid, date);
                    if (resultuebersicht != null)
                    {
                        long geleisteteStunden = 0;
                        long istStunden = 0;
                        long sollStunden = 0;
                        long saldo = 0;
                        long esh = 0;
                        long ers = 0;
                        long engelmann = 0;
                        long nh3Stunden = 0;

                        foreach (var item in resultuebersicht)
                        {
                            urlaub += !string.IsNullOrEmpty(item.Urlaub) ? Convert.ToInt32(item.Urlaub) : 0;
                            krank += Convert.ToInt32(item.Krankenstand);

                            if (!string.IsNullOrEmpty(item.Anwesenheit))
                            {
                                geleisteteStunden = CalcGeleisteteStunden(geleisteteStunden, item.Anwesenheit);
                            }

                            if (!string.IsNullOrEmpty(item.Sollstunden))
                            {
                                sollStunden = CalcSollStunden(sollStunden, item.Sollstunden);
                            }

                            if (!string.IsNullOrEmpty(item.IstStunden))
                            {
                                istStunden = CalcIstStunden(istStunden, item.IstStunden);
                            }

                            if (!string.IsNullOrEmpty(item.ESH))
                            {
                                esh = CalcEsh(esh, item.ESH);
                            }
                            if (!string.IsNullOrEmpty(item.ERS))
                            {
                                ers = CalcErs(ers, item.ERS);
                            }
                            if (!string.IsNullOrEmpty(item.Engelmann))
                            {
                                engelmann = CalcEngelmann(engelmann, item.Engelmann);
                            }
                            if (!string.IsNullOrEmpty(item.NH3Zeit))
                            {
                                nh3Stunden = CalcNh3Stunden(nh3Stunden, item.NH3Zeit);
                            }
                        }

                        if (geleisteteStunden < 0)
                        {
                            TimeSpan tsGeleistetSumme = new TimeSpan(geleisteteStunden * -1);
                            uebersicht.GeleisteteStunden = "-" + (tsGeleistetSumme.Days * 24 + tsGeleistetSumme.Hours).ToString().PadLeft(2, '0') + ":" + tsGeleistetSumme.Minutes.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            TimeSpan tsGeleistetSumme = new TimeSpan(geleisteteStunden);
                            uebersicht.GeleisteteStunden = (tsGeleistetSumme.Days * 24 + tsGeleistetSumme.Hours).ToString().PadLeft(2, '0') + ":" + tsGeleistetSumme.Minutes.ToString().PadLeft(2, '0');
                        }

                        TimeSpan tsSollSumme = new TimeSpan(sollStunden);
                        uebersicht.Sollstunden = (tsSollSumme.Days * 24 + tsSollSumme.Hours).ToString().PadLeft(2, '0') + ":" + tsSollSumme.Minutes.ToString().PadLeft(2, '0');

                        saldo = geleisteteStunden - sollStunden;
                        if (saldo < 0)
                        {
                            TimeSpan tsDiff = new TimeSpan(saldo * -1);
                            uebersicht.MehrMinder = "-" + (tsDiff.Days * 24 + tsDiff.Hours).ToString().PadLeft(2, '0') + ":" + tsDiff.Minutes.ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            TimeSpan tsDiff = new TimeSpan(saldo);
                            uebersicht.MehrMinder = (tsDiff.Days * 24 + tsDiff.Hours).ToString().PadLeft(2, '0') + ":" + tsDiff.Minutes.ToString().PadLeft(2, '0');
                        }

                        TimeSpan ts = new TimeSpan(esh);
                        uebersicht.ESH = (ts.Days * 24 + ts.Hours).ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0');

                        ts = new TimeSpan(ers);
                        uebersicht.ERS = (ts.Days * 24 + ts.Hours).ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0');

                        ts = new TimeSpan(engelmann);
                        uebersicht.Engelmann = (ts.Days * 24 + ts.Hours).ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0');

                        ts = new TimeSpan(nh3Stunden);
                        uebersicht.NH3Stunden = (ts.Days * 24 + ts.Hours).ToString().PadLeft(2, '0') + ":" + ts.Minutes.ToString().PadLeft(2, '0');

                        double nh3Value = 0;
                        if (nh3Stunden > 0)
                        {
                            nh3Value = nh3Stunden * 100;
                            nh3Value = nh3Value / istStunden;
                        }
                        uebersicht.NH3Prozent = nh3Value.ToString("0.00") + "%";


                        uebersicht.Urlaub = urlaub.ToString();
                        uebersicht.Krank = krank.ToString();
                        uebersicht.IsAbgeschlossen = false;
                    }

                }



                return uebersicht;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static long CalcGeleisteteStunden(long geleisteteStunden, string anwesenheit)
        {
            {
                if (anwesenheit.StartsWith("-"))
                {
                    TimeSpan tsGeleistet = new TimeSpan(Convert.ToInt32(anwesenheit.Split(':')[0].Substring(1)), Convert.ToInt32(anwesenheit.Split(':')[1]), 0);
                    geleisteteStunden -= tsGeleistet.Ticks;
                }
                else
                {
                    TimeSpan tsGeleistet = new TimeSpan(Convert.ToInt32(anwesenheit.Split(':')[0]), Convert.ToInt32(anwesenheit.Split(':')[1]), 0);
                    geleisteteStunden += tsGeleistet.Ticks;
                }
            }

            return geleisteteStunden;
        }

        private static long CalcIstStunden(long stunden, string istStunden)
        {
            TimeSpan ts = new TimeSpan(Convert.ToInt32(istStunden.Split(':')[0]), Convert.ToInt32(istStunden.Split(':')[1]), 0);
            stunden += ts.Ticks;

            return stunden;
        }

        private static long CalcSollStunden(long stunden, string sollStunden)
        {
            {
                    TimeSpan tsSoll = new TimeSpan(Convert.ToInt32(sollStunden.Split(':')[0]), Convert.ToInt32(sollStunden.Split(':')[1]), 0);
                    stunden += tsSoll.Ticks;
            }

            return stunden;
        }

        private static long CalcEsh(long stunden, string esh)
        {
            {
                TimeSpan ts = new TimeSpan(Convert.ToInt32(esh.Split(':')[0]), Convert.ToInt32(esh.Split(':')[1]), 0);
                stunden += ts.Ticks;
            }

            return stunden;
        }

        private static long CalcErs(long stunden, string ers)
        {
            {
                TimeSpan ts = new TimeSpan(Convert.ToInt32(ers.Split(':')[0]), Convert.ToInt32(ers.Split(':')[1]), 0);
                stunden += ts.Ticks;
            }

            return stunden;
        }

        private static long CalcEngelmann(long stunden, string engelmann)
        {
            {
                TimeSpan ts = new TimeSpan(Convert.ToInt32(engelmann.Split(':')[0]), Convert.ToInt32(engelmann.Split(':')[1]), 0);
                stunden += ts.Ticks;
            }

            return stunden;
        }

        private static long CalcNh3Stunden(long stunden, string nh3Zeit)
        {
            TimeSpan ts = new TimeSpan(Convert.ToInt32(nh3Zeit.Split(':')[0]), Convert.ToInt32(nh3Zeit.Split(':')[1]), 0);
            stunden += ts.Ticks;

            return stunden;
        }


        private static string restYear;

        private static string CalcRestStunden(bool first, string ausbezahlt, string mehrminder, string uebertrag)
        {
            if (first)
            {
                restYear = uebertrag;
            }

            if (string.IsNullOrEmpty(ausbezahlt))
            {
                TimeSpan tsRestYear = new TimeSpan();
                if (restYear.StartsWith("-"))
                {
                    tsRestYear = new TimeSpan(Convert.ToInt32(restYear.Substring(1).Split(':')[0]), Convert.ToInt32(restYear.Substring(1).Split(':')[1]), 0);
                }
                else
                {
                    tsRestYear = new TimeSpan(Convert.ToInt32(restYear.Split(':')[0]), Convert.ToInt32(restYear.Split(':')[1]), 0);
                }

                TimeSpan tsMehrMinder = new TimeSpan();
                Int64 inRest = 0;
                if (mehrminder.StartsWith("-"))
                {
                    tsMehrMinder = new TimeSpan(Convert.ToInt32(mehrminder.Substring(1).Split(':')[0]), Convert.ToInt32(mehrminder.Substring(1).Split(':')[1]), 0);
                    if (restYear.StartsWith("-"))
                    {
                        inRest = tsRestYear.Ticks + tsMehrMinder.Ticks;
                        inRest *= -1;
                    }
                    else
                        inRest = tsRestYear.Ticks - tsMehrMinder.Ticks;
                }
                else
                {
                    tsMehrMinder = new TimeSpan(Convert.ToInt32(mehrminder.Split(':')[0]), Convert.ToInt32(mehrminder.Split(':')[1]), 0);
                    if (restYear.StartsWith("-"))
                    {
                        inRest = (tsRestYear.Ticks * -1) + tsMehrMinder.Ticks;
                    }
                    else
                        inRest = tsRestYear.Ticks + tsMehrMinder.Ticks;
                }

                if (inRest < 0)
                {
                    TimeSpan tsRest = new TimeSpan(inRest * -1);
                    restYear = "-" + (tsRest.Days * 24 + tsRest.Hours).ToString().PadLeft(2, '0') + ":" + tsRest.Minutes.ToString().PadLeft(2, '0');
                    return restYear;
                }
                else
                {
                    TimeSpan tsRest = new TimeSpan(inRest);
                    restYear = (tsRest.Days * 24 + tsRest.Hours).ToString().PadLeft(2, '0') + ":" + tsRest.Minutes.ToString().PadLeft(2, '0');
                    return restYear;
                }
            }
            else
            {
                TimeSpan tAusbezahlt = new TimeSpan();
                long lgAusbezahlt = 0;
                if (ausbezahlt.StartsWith("-"))
                {
                    if (ausbezahlt.Contains(':'))
                    {
                        tAusbezahlt = new TimeSpan(Convert.ToInt32(ausbezahlt.Split(':')[0].Substring(1)), Convert.ToInt32(ausbezahlt.Split(':')[1]), 0);
                    }
                    else
                    {
                        tAusbezahlt = new TimeSpan(Convert.ToInt32(ausbezahlt.Substring(1)), 0, 0);
                    }

                    lgAusbezahlt = tAusbezahlt.Ticks;
                    lgAusbezahlt *= -1;

                }
                else
                {
                    if (ausbezahlt.Contains(':'))
                    {
                        tAusbezahlt = new TimeSpan(Convert.ToInt32(ausbezahlt.Split(':')[0]), Convert.ToInt32(ausbezahlt.Split(':')[1]), 0);
                    }
                    else
                    {
                        tAusbezahlt = new TimeSpan(Convert.ToInt32(ausbezahlt), 0, 0);
                    }
                    lgAusbezahlt = tAusbezahlt.Ticks;
                }

                TimeSpan tsRestYear = new TimeSpan();
                if (restYear.StartsWith("-"))
                {
                    tsRestYear = new TimeSpan(Convert.ToInt32(restYear.Substring(1).Split(':')[0]), Convert.ToInt32(restYear.Substring(1).Split(':')[1]), 0);
                }
                else
                {
                    tsRestYear = new TimeSpan(Convert.ToInt32(restYear.Split(':')[0]), Convert.ToInt32(restYear.Split(':')[1]), 0);
                }

                TimeSpan tsMehrMinder = new TimeSpan();
                Int64 inRest = 0;
                if (mehrminder.StartsWith("-"))
                {
                    tsMehrMinder = new TimeSpan(Convert.ToInt32(mehrminder.Substring(1).Split(':')[0]), Convert.ToInt32(mehrminder.Substring(1).Split(':')[1]), 0);
                    if (restYear.StartsWith("-"))
                    {
                        inRest = tsRestYear.Ticks + tsMehrMinder.Ticks + lgAusbezahlt;
                        inRest *= -1;
                    }
                    else
                        inRest = tsRestYear.Ticks - tsMehrMinder.Ticks - lgAusbezahlt;
                }
                else
                {
                    tsMehrMinder = new TimeSpan(Convert.ToInt32(mehrminder.Split(':')[0]), Convert.ToInt32(mehrminder.Split(':')[1]), 0);
                    if (restYear.StartsWith("-"))
                    {
                        inRest = (tsRestYear.Ticks * -1) + tsMehrMinder.Ticks - lgAusbezahlt;
                    }
                    else
                        inRest = tsRestYear.Ticks + tsMehrMinder.Ticks - lgAusbezahlt;
                }

                if (inRest < 0)
                {
                    TimeSpan tsRest = new TimeSpan(inRest * -1);
                    restYear = "-" + (tsRest.Days * 24 + tsRest.Hours).ToString().PadLeft(2, '0') + ":" + tsRest.Minutes.ToString().PadLeft(2, '0');
                    return restYear;
                }
                else
                {
                    TimeSpan tsRest = new TimeSpan(inRest);
                    restYear = (tsRest.Days * 24 + tsRest.Hours).ToString().PadLeft(2, '0') + ":" + tsRest.Minutes.ToString().PadLeft(2, '0');
                    return restYear;
                }
            }
        }

}