using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfUebersicht
    {
        public string Datum { get; set; }
        public string Tag { get; set; }
        public string Sollstunden { get; set; }
        public string Beginn { get; set; }
        public string Ende { get; set; }
        public string Pause { get; set; }
        public string IstStunden { get; set; }
        public string Saldo { get; set; }
        public string Bemerkung { get; set; }
        public string Dienstgang { get; set; }
        public string Urlaub { get; set; }
        public string Wochensaldo { get; set; }
        public bool Feiertag { get; set; }
        public bool Krankenstand { get; set; }
        public bool Edit { get; set; }
        public string BeginnBuchung { get; set; }
        public string EndeBuchung { get; set; }
        public string GeaendertVon { get; set; }
        public DateTime? TSGeaendert { get; set; }
        public string Anwesenheit { get; set; }
        public bool Frei { get; set; }
        public string ESH { get; set; }
        public string ERS { get; set; }
        public string Engelmann { get; set; }
        public string Taetigkeitsbereich { get; set; }
        public DateTime? DBTSKommt { get; set; }
        public DateTime? DBTSGeht { get; set; }
        public bool NH3 { get; set; }
        public string NH3Prozent { get; set; }
        public string NH3Zeit { get; set; }

        private static List<Feiertag> lstFTFix = new List<Feiertag>();
        private static List<Feiertag> lstFTVar = new List<Feiertag>();

        public static bool Abgeschlossen = false;

        public static async Task<List<ZeiterfUebersicht>> GetMonthForMitarbeiter(string mitarbeiterId, DateTime date)
        {
            Abgeschlossen = false;
            DateTime dtVon = new DateTime(date.Year, date.Month, 1);
            DateTime dtBis = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

            var monatsliste = new List<ZeiterfUebersicht>();

            List<ZeiterfMonatsabschluss> lstAbschluss = await ZeiterfMonatsabschluss.GetByMitarbeiterAndDate(mitarbeiterId, dtVon, dtBis);
            if (lstAbschluss.Count > 0)
            {
                foreach (var item in lstAbschluss)
                {
                    monatsliste.Add(
                        new ZeiterfUebersicht
                        {
                            Anwesenheit = item.Anwesenheit,
                            Beginn = item.Beginn,
                            BeginnBuchung = item.BeginnBuchung,
                            Bemerkung = item.Bemerkung,
                            Datum = item.Datum.Value.ToString("dd.MM.yyyy"),
                            DBTSGeht = null,
                            DBTSKommt = null,
                            Dienstgang = item.Dienstgang,
                            Edit = Convert.ToBoolean(item.Edit),
                            Ende = item.Ende,
                            EndeBuchung = item.EndeBuchung,
                            Engelmann = item.Engelmann,
                            ERS = item.ERS,
                            ESH = item.ESH,
                            Feiertag = Convert.ToBoolean(item.Feiertag),
                            Frei = Convert.ToBoolean(item.Frei),
                            GeaendertVon = item.GeaendertVon,
                            IstStunden = item.IstStunden,
                            Krankenstand = Convert.ToBoolean(item.Krankenstand),
                            NH3 = Convert.ToBoolean(item.NH3),
                            NH3Prozent = item.NH3Prozent,
                            NH3Zeit = item.NH3Zeit,
                            Pause = item.Pause,
                            Saldo = item.Saldo,
                            Sollstunden = item.Sollstunden,
                            Taetigkeitsbereich = item.Taetigkeitsbereich,
                            Tag = item.Tag,
                            TSGeaendert = null,
                            Urlaub = item.Urlaub,
                            Wochensaldo = item.Wochensaldo,
                        });
                }
                Abgeschlossen = true;
                return monatsliste;
            }
            else
            {
                String stTaetigkeit = "";

                Int32 inArbeitszeitModell = 0;
                DateTime dtEintritt = new DateTime();
                DateTime dtAustritt = new DateTime();

                Int32 inPause = 0;
                Int32 inPauseDauer = 0;

                Boolean isMoWork = false;
                Boolean isDiWork = false;
                Boolean isMiWork = false;
                Boolean isDoWork = false;
                Boolean isFrWork = false;
                Boolean isSaWork = false;
                Boolean isSoWork = false;

                TimeSpan tsMo = new TimeSpan();
                TimeSpan tsDi = new TimeSpan();
                TimeSpan tsMi = new TimeSpan();
                TimeSpan tsDo = new TimeSpan();
                TimeSpan tsFr = new TimeSpan();
                TimeSpan tsSa = new TimeSpan();
                TimeSpan tsSo = new TimeSpan();

                TimeSpan tsBeginnMo = new TimeSpan();
                TimeSpan tsEndeMo = new TimeSpan();
                TimeSpan tsBeginnDi = new TimeSpan();
                TimeSpan tsEndeDi = new TimeSpan();
                TimeSpan tsBeginnMi = new TimeSpan();
                TimeSpan tsEndeMi = new TimeSpan();
                TimeSpan tsBeginnDo = new TimeSpan();
                TimeSpan tsEndeDo = new TimeSpan();
                TimeSpan tsBeginnFr = new TimeSpan();
                TimeSpan tsEndeFr = new TimeSpan();
                TimeSpan tsBeginnSa = new TimeSpan();
                TimeSpan tsEndeSa = new TimeSpan();
                TimeSpan tsBeginnSo = new TimeSpan();
                TimeSpan tsEndeSo = new TimeSpan();

                Int32 inWochenarbeitsTage = 0;
                Double dbWochenarbeitszeit = 0;
                Double dbTagesSollArbeitszeit = 0;

                DateTime dtMonat = date;
                Int32 inDaysInMonth = DateTime.DaysInMonth(dtMonat.Year, dtMonat.Month);

                DateTime dtStart = new DateTime(dtMonat.Year, dtMonat.Month, 1);
                DateTime dtEnde = new DateTime(dtMonat.Year, dtMonat.Month, inDaysInMonth);
                List<ZeiterfSubmodel> lstSubModel = new List<ZeiterfSubmodel>();

                ZeiterfMitarbeiter ma = await ZeiterfMitarbeiter.GetById(mitarbeiterId);
                if (ma != null)
                {
                    inArbeitszeitModell = ma.Zeitmodell == null ? 0 : Convert.ToInt32(ma.Zeitmodell);
                    if (ma.Eintrittsdatum != null)
                    {
                        dtEintritt = Convert.ToDateTime(ma.Eintrittsdatum);
                        dtEintritt = new DateTime(dtEintritt.Year, dtEintritt.Month, dtEintritt.Day, 0, 0, 0);
                    }
                    if (ma.Austrittsdatum != null)
                    {
                        dtAustritt = Convert.ToDateTime(ma.Austrittsdatum);
                        dtAustritt = new DateTime(dtAustritt.Year, dtAustritt.Month, dtAustritt.Day, 23, 59, 59);

                    }
                    inWochenarbeitsTage = ma.Wochenarbeitstage == null ? 0 : Convert.ToInt32(ma.Wochenarbeitstage);
                    dbWochenarbeitszeit = ma.Wochenstunden == null ? 0 : Convert.ToDouble(ma.Wochenstunden);
                    stTaetigkeit = string.IsNullOrEmpty(ma.Taetigkeit) ? "" : ma.Taetigkeit.ToString();
                }

                if (inWochenarbeitsTage == 0)
                {
                    throw new Exception("Anzahl Wochenarbeitstage = 0\r\nMitarbeiterstammdaten prüfen");
                }
                if (dbWochenarbeitszeit == 0)
                {
                    throw new Exception("Wochenarbeitszeit = 0\r\nMitarbeiterstammdaten prüfen");
                }

                if (inWochenarbeitsTage == 7)
                {
                    dbTagesSollArbeitszeit = dbWochenarbeitszeit / inWochenarbeitsTage;
                }


                ZeiterfModell modell = await ZeiterfModell.GetById(inArbeitszeitModell);
                if (modell != null)
                {
                    inPause = Convert.ToInt32(modell.AnzahlAutoPause);
                    inPauseDauer = Convert.ToInt32(modell.DauerAutoPause);

                    isMoWork = IsWorkDay(DayOfWeek.Monday, modell);
                    isDiWork = IsWorkDay(DayOfWeek.Tuesday, modell);
                    isMiWork = IsWorkDay(DayOfWeek.Wednesday, modell);
                    isDoWork = IsWorkDay(DayOfWeek.Thursday, modell);
                    isFrWork = IsWorkDay(DayOfWeek.Friday, modell);
                    isSaWork = IsWorkDay(DayOfWeek.Saturday, modell);
                    isSoWork = IsWorkDay(DayOfWeek.Sunday, modell);

                    GetTimestamps(modell, DayOfWeek.Monday, ref tsMo, ref tsBeginnMo, ref tsEndeMo);
                    GetTimestamps(modell, DayOfWeek.Tuesday, ref tsDi, ref tsBeginnDi, ref tsEndeDi);
                    GetTimestamps(modell, DayOfWeek.Wednesday, ref tsMi, ref tsBeginnMi, ref tsEndeMi);
                    GetTimestamps(modell, DayOfWeek.Thursday, ref tsDo, ref tsBeginnDo, ref tsEndeDo);
                    GetTimestamps(modell, DayOfWeek.Friday, ref tsFr, ref tsBeginnFr, ref tsEndeFr);
                    GetTimestamps(modell, DayOfWeek.Saturday, ref tsSa, ref tsBeginnSa, ref tsEndeSa);
                    GetTimestamps(modell, DayOfWeek.Sunday, ref tsSo, ref tsBeginnSo, ref tsEndeSo);

                    await GetSubmodells(inArbeitszeitModell, dtStart, dtEnde, lstSubModel);
                }

                monatsliste.Clear();

                List<DateTime> lstDatumUrlaub = new List<DateTime>();
                await GetUrlaub(mitarbeiterId, dtStart, dtEnde, lstDatumUrlaub);

                List<DateTime> lstDatumKrank = new List<DateTime>();
                await GetKrank(mitarbeiterId, dtStart, dtEnde, lstDatumKrank);

                List<ZeitFrei> lstFrei = new List<ZeitFrei>();
                await GetFrei(mitarbeiterId, dtStart, dtEnde, lstFrei);

                List<ZeitKurzarbeit> lstKurzarbeit = new List<ZeitKurzarbeit>();
                await GetKurzarbeit(mitarbeiterId, dtStart, dtEnde, lstKurzarbeit);

                List<clsZeitErfNH3> lstNH3 = new List<clsZeitErfNH3>();
                await GetNH3(mitarbeiterId, dtStart, dtEnde, lstNH3);


                List<clsZeiterfassungUebersicht> lstZeit = new List<clsZeiterfassungUebersicht>();

                List<ZeiterfBuchung> zeiterfBuchungs = await ZeiterfBuchung.GetByMitarbeiterAndDate(mitarbeiterId, dtStart, dtEnde);
                if (zeiterfBuchungs.Count > 0)
                {
                    foreach (var itemBuchung in zeiterfBuchungs)
                    {
                        DateTime dtListDatum = Convert.ToDateTime(itemBuchung.Datum);

                        var itemZeit = lstZeit.Find(x => x.Datum.Date.CompareTo(dtListDatum.Date) == 0);
                        if (itemZeit != null)
                        {
                            itemZeit.GehtBuchung = GetGehtBuchung(itemBuchung);
                            itemZeit.Geht = GetZeitGeht(tsEndeMo, tsEndeDi, tsEndeMi, tsEndeDo, tsEndeFr, tsEndeSa, tsEndeSo, itemBuchung, dtListDatum);

                            if (!string.IsNullOrWhiteSpace(itemBuchung.ZeitKommt) && !string.IsNullOrWhiteSpace(itemBuchung.ZeitGeht))
                            {
                                Int32 inSp = Convert.ToInt32(itemBuchung.SP);
                                TimeSpan tsSPBeginn = TimeSpan.Parse(itemBuchung.ZeitKommt);
                                TimeSpan tsSPEnde = TimeSpan.Parse(itemBuchung.ZeitGeht);

                                if (tsSPEnde.Hours < 4)
                                {
                                    tsSPEnde = tsSPEnde.Add(new TimeSpan(24, 0, 0));
                                }

                                try
                                {
                                    TimeSpan tsSP = tsSPEnde.Subtract(tsSPBeginn);
                                    if (inSp == 1)
                                    {
                                        itemZeit.ESH = GetZeitSpielstaetteEsh(
                                            tsSP,
                                            itemZeit.ESH);
                                    }
                                    else if (inSp == 2)
                                    {
                                        itemZeit.ERS = GetZeitSpielstaetteErs(
                                            tsSP,
                                            itemZeit.ERS);
                                    }
                                    else if (inSp == 4)
                                    {
                                        itemZeit.Engelmann = GetZeitSpielstaetteEngelmann(
                                            tsSP,
                                            itemZeit.Engelmann);
                                    }
                                }
                                catch (Exception)
                                {

                                }
                            }

                            itemZeit.GeaendertVon = itemBuchung.EdituserID ?? "";
                            if (itemBuchung.TSGeaendert != null)
                            {
                                itemZeit.TSGeaendert = Convert.ToDateTime(itemBuchung.TSGeaendert);
                            }

                            if (!string.IsNullOrWhiteSpace(itemBuchung.Grund))
                            {
                                itemZeit.Dienstgang = itemBuchung.Grund;
                            }

                            if (string.IsNullOrWhiteSpace(itemBuchung.Bemerkung))
                            {
                                itemZeit.Bemerkung = itemBuchung.Bemerkung;
                            }


                            itemZeit.Taetigkeitsbereich = !string.IsNullOrWhiteSpace(itemBuchung.Taetigkeitsbereich) ?
                                itemBuchung.Taetigkeitsbereich : stTaetigkeit;
                        }
                        else
                        {
                            clsZeiterfassungUebersicht cZeit = new clsZeiterfassungUebersicht();
                            cZeit.Datum = Convert.ToDateTime(itemBuchung.Datum);
                            cZeit.Edit = itemBuchung.Edit == null ? false : Convert.ToBoolean(itemBuchung.Edit);

                            cZeit.Kommt = GetZeitGehtKommt(cZeit.Datum, cZeit.Edit, lstSubModel, tsBeginnMo, tsBeginnDi,
                                tsBeginnMi, tsBeginnDo, tsBeginnFr, tsBeginnSa, tsBeginnSo, itemBuchung, true);

                            cZeit.Geht = GetZeitGehtKommt(cZeit.Datum, cZeit.Edit, lstSubModel, tsEndeMo, tsEndeDi,
                                tsEndeMi, tsEndeDo, tsEndeFr, tsEndeSa, tsEndeSo, itemBuchung, false);

                            cZeit.KommtBuchung = string.IsNullOrWhiteSpace(itemBuchung.KommtBuchung) ? itemBuchung.ZeitKommt : itemBuchung.KommtBuchung;
                            cZeit.GehtBuchung = string.IsNullOrWhiteSpace(itemBuchung.GehtBuchung) ? itemBuchung.ZeitGeht : itemBuchung.GehtBuchung;
                            cZeit.GeaendertVon = itemBuchung.EdituserID;

                            if (itemBuchung.TSEdit != null)
                                cZeit.TSGeaendert = Convert.ToDateTime(itemBuchung.TSEdit);

                            cZeit.Dienstgang = itemBuchung.Grund;
                            cZeit.Bemerkung = itemBuchung.Bemerkung;
                            cZeit.Taetigkeitsbereich = string.IsNullOrWhiteSpace(itemBuchung.Taetigkeitsbereich) ? stTaetigkeit : itemBuchung.Taetigkeitsbereich;

                            if (itemBuchung.DBTSKOMMT != null)
                                cZeit.DBTSKommt = Convert.ToDateTime(itemBuchung.DBTSKOMMT);
                            if (itemBuchung.DBTSGEHT != null)
                                cZeit.DBTSGeht = Convert.ToDateTime(itemBuchung.DBTSGEHT);

                            if (!string.IsNullOrWhiteSpace(cZeit.Kommt) && !string.IsNullOrWhiteSpace(cZeit.Geht))
                            {
                                try
                                {
                                    Int32 inSp = Convert.ToInt32(itemBuchung.SP);
                                    TimeSpan tsSPBeginn = TimeSpan.Parse(cZeit.Kommt);
                                    TimeSpan tsSPEnde = TimeSpan.Parse(cZeit.Geht);

                                    if (tsSPEnde.Hours < 4)
                                    {
                                        tsSPEnde = tsSPEnde.Add(new TimeSpan(24, 0, 0));
                                    }

                                    TimeSpan tsSP = tsSPEnde.Subtract(tsSPBeginn);
                                    if (inSp == 1)
                                    {
                                        cZeit.ESH = new DateTime(tsSP.Ticks).ToString("HH:mm");
                                    }
                                    else if (inSp == 2)
                                    {
                                        cZeit.ERS = new DateTime(tsSP.Ticks).ToString("HH:mm");
                                    }
                                    else if (inSp == 4)
                                    {
                                        cZeit.Engelmann = new DateTime(tsSP.Ticks).ToString("HH:mm");
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            lstZeit.Add(cZeit);
                        }
                    }
                }

                FeiertagLogic ftl = FeiertagLogic.GetInstance(dtMonat.Year);
                lstFTFix = ftl.FesteFeiertage;
                lstFTVar = ftl.VariableFeiertage;

                for (int i = 0; i < inDaysInMonth; i++)
                {
                    DateTime dtTag = new DateTime(dtMonat.Year, dtMonat.Month, i + 1);
                    ZeiterfUebersicht row = new ZeiterfUebersicht();
                    row.Datum = dtTag.ToString("dd.MM.yyyy");
                    row.Tag = GetTag(dtTag);
                    row.Krankenstand = false;
                    row.Frei = false;
                    row.Feiertag = false;
                    row.IstStunden = "00:00";

                    TimeSpan tsSoll = new TimeSpan();
                    if (inWochenarbeitsTage == 7)
                    {
                        tsSoll = TimeSpan.FromHours(dbTagesSollArbeitszeit);
                    }
                    else
                    {
                        switch (dtTag.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                if (isMoWork)
                                    tsSoll = tsMo;
                                break;
                            case DayOfWeek.Tuesday:
                                if (isDiWork)
                                    tsSoll = tsDi;
                                break;
                            case DayOfWeek.Wednesday:
                                if (isMiWork)
                                    tsSoll = tsMi;
                                break;
                            case DayOfWeek.Thursday:
                                if (isDoWork)
                                    tsSoll = tsDo;
                                break;
                            case DayOfWeek.Friday:
                                if (isFrWork)
                                    tsSoll = tsFr;
                                break;
                            case DayOfWeek.Saturday:
                                if (isSaWork)
                                    tsSoll = tsSa;
                                break;
                            case DayOfWeek.Sunday:
                                if (isSoWork)
                                    tsSoll = tsSo;
                                break;
                            default:
                                break;
                        }
                    }

                    if (dtEintritt.Date.CompareTo(dtTag) > 0)
                    {
                        tsSoll = new TimeSpan(0, 0, 0);
                    }

                    if (dtAustritt.CompareTo(new DateTime()) > 0 && dtAustritt.CompareTo(dtTag) <= 0)
                    {
                        tsSoll = new TimeSpan(0, 0, 0);
                    }

                    row.Sollstunden = new DateTime(tsSoll.Ticks).ToString("HH:mm");

                    row.Feiertag = CheckIfFeiertag(dtTag);

                    var resultZeit = lstZeit.Find(x => x.Datum.Date.CompareTo(dtTag.Date) == 0);
                    if (resultZeit != null)
                    {
                        row.Beginn = resultZeit.Kommt;
                        row.Ende = resultZeit.Geht;
                        row.Edit = resultZeit.Edit;
                        row.BeginnBuchung = resultZeit.KommtBuchung;
                        row.EndeBuchung = resultZeit.GehtBuchung;
                        row.GeaendertVon = resultZeit.GeaendertVon;
                        row.Dienstgang = resultZeit.Dienstgang;
                        row.Bemerkung = resultZeit.Bemerkung;
                        row.ESH = resultZeit.ESH;
                        row.ERS = resultZeit.ERS;
                        row.Engelmann = resultZeit.Engelmann;
                        row.Taetigkeitsbereich = resultZeit.Taetigkeitsbereich;

                        if (resultZeit.DBTSKommt != null)
                        {
                            row.DBTSKommt = Convert.ToDateTime(resultZeit.DBTSKommt);
                        }
                        if (resultZeit.DBTSGeht != null)
                        {
                            row.DBTSGeht = Convert.ToDateTime(resultZeit.DBTSGeht);
                        }
                        if (resultZeit.TSGeaendert != null)
                        {
                            row.TSGeaendert = Convert.ToDateTime(resultZeit.TSGeaendert);
                        }

                        if (!string.IsNullOrWhiteSpace(resultZeit.Geht))
                        {
                            if (!string.IsNullOrWhiteSpace(resultZeit.Kommt))
                            {
                                TimeSpan tsBeginn1 = TimeSpan.Parse(resultZeit.Kommt);
                                TimeSpan tsEnde1 = TimeSpan.Parse(resultZeit.Geht);
                                TimeSpan tsPause = new TimeSpan(0, inPause * inPauseDauer, 0);
                                TimeSpan tsIst = tsEnde1.Subtract(tsBeginn1);
                                if (tsIst.TotalMinutes > 360 && tsIst.TotalMinutes < 600)
                                {
                                    tsPause = new TimeSpan(0, 30, 0);
                                }
                                else if (tsIst.TotalMinutes < 360)
                                {
                                    tsPause = new TimeSpan(0, 0, 0);
                                }
                                row.Pause = new DateTime(tsPause.Ticks).ToString("HH:mm");

                                tsIst = tsEnde1.Subtract(tsBeginn1).Subtract(tsPause);
                                TimeSpan tsAnwesend = tsIst;
                                tsAnwesend = tsAnwesend.Add(tsPause);

                                if (row.Dienstgang.Trim() == "")
                                {
                                    TimeSpan tsEngelmann = new TimeSpan(0);
                                    TimeSpan tsERS = new TimeSpan(0);
                                    TimeSpan tsESH = new TimeSpan(0);

                                    if (!string.IsNullOrWhiteSpace(resultZeit.Engelmann))
                                    {
                                        tsEngelmann = TimeSpan.Parse(resultZeit.Engelmann);
                                    }

                                    if (!string.IsNullOrWhiteSpace(resultZeit.ERS))
                                    {
                                        tsERS = TimeSpan.Parse(resultZeit.ERS);
                                    }

                                    if (!string.IsNullOrWhiteSpace(resultZeit.ESH))
                                    {
                                        tsESH = TimeSpan.Parse(resultZeit.ESH);
                                    }

                                    tsAnwesend = new TimeSpan(tsEngelmann.Ticks + tsERS.Ticks + tsESH.Ticks);
                                    tsIst = tsAnwesend.Subtract(tsPause);
                                }


                                Int64 lgTicksIstSaldo = tsIst.Ticks;

                                row.IstStunden = GetIstStunden(tsIst);

                                row.Anwesenheit = GetAnwesenheit(tsAnwesend);

                                TimeSpan tsSollSaldo = TimeSpan.Parse(row.Sollstunden);
                                Int64 lgTicksSollSaldo = tsSollSaldo.Ticks;
                                Int64 lgTicksSaldoSaldo = lgTicksIstSaldo - lgTicksSollSaldo;
                            }
                        }
                        else
                        {
                            TimeSpan tsSollSaldo = TimeSpan.Parse(row.Sollstunden);
                            Int64 lgTicksSollSaldo = tsSollSaldo.Ticks;
                            Int64 lgTicksSaldoSaldo = lgTicksSollSaldo;
                            lgTicksSaldoSaldo *= -1;
                        }

                    }


                    DateTime resultUrlaub = lstDatumUrlaub.Find(x => x.Date.CompareTo(dtTag.Date) == 0);
                    if (resultUrlaub != new DateTime())
                    {
                        row.Urlaub = "1";
                        row.Sollstunden = "00:00";
                        row.IstStunden = "00:00";
                        row.Saldo = "00:00";
                    }

                    DateTime resultKrank = lstDatumKrank.Find(x => x.Date.CompareTo(dtTag.Date) == 0);
                    if (resultKrank != new DateTime())
                    {
                        row.Krankenstand = true;
                        row.Sollstunden = "00:00";
                        row.IstStunden = "00:00";
                        row.Saldo = "00:00";
                    }

                    var resultFrei = lstFrei.Find(x => x.Datum.Date.CompareTo(dtTag.Date) == 0);
                    if (resultFrei != null)
                    {
                        row.Frei = true;
                        row.Sollstunden = "00:00";
                        row.IstStunden = "00:00";
                        row.Saldo = "00:00";
                        row.Bemerkung = resultFrei.Bemerkung;
                    }

                    var resultKurzArbeit = lstKurzarbeit.Find(x => x.Datum.Date.CompareTo(dtTag.Date) == 0);
                    if (resultKurzArbeit != null)
                    {
                        row.Sollstunden = resultKurzArbeit.Soll;
                    }

                    var resultNH3 = lstNH3.Find(x => x.Datum.Date.CompareTo(dtTag.Date) == 0);
                    if (resultNH3 != null)
                    {
                        TimeSpan tsNH3Beginn = new TimeSpan(Convert.ToInt32(resultNH3.NH3Start.Split(':')[0]), Convert.ToInt32(resultNH3.NH3Start.Split(':')[1]), 0);

                        if (!string.IsNullOrEmpty(row.Beginn))
                        {
                            TimeSpan tsRBeginn = new TimeSpan(Convert.ToInt32(row.Beginn.Split(':')[0]), Convert.ToInt32(row.Beginn.Split(':')[1]), 0);

                            if (tsRBeginn.CompareTo(tsNH3Beginn) > 0)
                            {
                                resultNH3.NH3Start = new DateTime(tsRBeginn.Ticks).ToString("HH:mm");
                                tsNH3Beginn = tsRBeginn;
                            }
                        }

                        Object o = row.Ende;

                        if (o != null && o != DBNull.Value)
                        {

                            if (resultNH3.NH3Ende != "" && row.Ende != "")
                            {
                                TimeSpan tsREnde = new TimeSpan(Convert.ToInt32(row.Ende.Split(':')[0]), Convert.ToInt32(row.Ende.Split(':')[1]), 0);
                                TimeSpan tsNH3Ende = new TimeSpan(Convert.ToInt32(resultNH3.NH3Ende.Split(':')[0]), Convert.ToInt32(resultNH3.NH3Ende.Split(':')[1]), 0);

                                if (tsNH3Ende.CompareTo(tsREnde) > 0)
                                {
                                    resultNH3.NH3Ende = new DateTime(tsREnde.Ticks).ToString("HH:mm");
                                    tsNH3Ende = tsREnde;
                                }
                                if (tsNH3Ende.CompareTo(tsNH3Beginn) >= 0)
                                {
                                    TimeSpan tsNH3Diff = tsNH3Ende.Subtract(tsNH3Beginn);
                                    resultNH3.NH3Zeit = new DateTime(tsNH3Diff.Ticks).ToString("HH:mm");
                                }
                                else
                                {
                                    DateTime dtToday = DateTime.Today;
                                    DateTime dtMidnight = dtToday.AddDays(1).AddSeconds(-1);
                                    TimeSpan tsNH3Diff = dtMidnight.TimeOfDay.Subtract(tsNH3Beginn).Add(tsNH3Ende).Add(new TimeSpan(0, 0, 1));
                                    resultNH3.NH3Zeit = new DateTime(tsNH3Diff.Ticks).ToString("HH:mm");
                                }
                            }
                        }
                        row.NH3 = true;
                        TimeSpan tsNH3Row = new TimeSpan(Convert.ToInt32(resultNH3.NH3Zeit.Split(':')[0]), Convert.ToInt32(resultNH3.NH3Zeit.Split(':')[1]), 0);
                        TimeSpan tsPause = new TimeSpan(0, inPause * inPauseDauer, 0);
                        if (tsNH3Row.TotalMinutes > 360 && tsNH3Row.TotalMinutes < 600)
                        {
                            tsPause = new TimeSpan(0, 30, 0);
                        }
                        else if (tsNH3Row.TotalMinutes < 360)
                        {
                            tsPause = new TimeSpan(0, 0, 0);
                        }
                        tsNH3Row = tsNH3Row.Subtract(tsPause);
                        row.NH3Zeit = new DateTime(tsNH3Row.Ticks).ToString("HH:mm");
                    }

                    monatsliste.Add(row);
                }
                Abgeschlossen = false;
                return monatsliste;
            }
        }

 

        private static string GetZeitGehtKommt(DateTime datum, bool edit, List<ZeiterfSubmodel> lstSubModel, TimeSpan tsMo, TimeSpan tsDi, TimeSpan tsMi, TimeSpan tsDo, TimeSpan tsFr, TimeSpan tsSa, TimeSpan tsSo, ZeiterfBuchung itemBuchung, bool kommt)
        {
            TimeSpan ts = new TimeSpan();
            switch (datum.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    {
                        ts = tsSo;
                        var itemSubModel = lstSubModel.Find(x => x.Datum.Date.CompareTo(datum.Date) == 0);
                        if (itemSubModel != null)
                        {
                            ts = kommt ? TimeSpan.Parse(itemSubModel.SoBeginn) : TimeSpan.Parse(itemSubModel.SoEnde);
                        }
                    }
                    break;
                case DayOfWeek.Monday:
                    {
                        ts = tsMo;
                        var itemSubModel = lstSubModel.Find(x => x.Datum.Date.CompareTo(datum.Date) == 0);
                        if (itemSubModel != null)
                        {
                            ts = kommt ? TimeSpan.Parse(itemSubModel.MoBeginn) : TimeSpan.Parse(itemSubModel.MoEnde);
                        }
                    }
                    break;
                case DayOfWeek.Tuesday:
                    {
                        ts = tsDi;
                        var itemSubModel = lstSubModel.Find(x => x.Datum.Date.CompareTo(datum.Date) == 0);
                        if (itemSubModel != null)
                        {
                            ts = kommt ? TimeSpan.Parse(itemSubModel.DiBeginn) : TimeSpan.Parse(itemSubModel.DiEnde);
                        }
                    }
                    break;
                case DayOfWeek.Wednesday:
                    {
                        ts = tsMi;
                        var itemSubModel = lstSubModel.Find(x => x.Datum.Date.CompareTo(datum.Date) == 0);
                        if (itemSubModel != null)
                        {
                            ts = kommt ? TimeSpan.Parse(itemSubModel.MiBeginn) : TimeSpan.Parse(itemSubModel.MiEnde);
                        }
                    }
                    break;
                case DayOfWeek.Thursday:
                    {
                        ts = tsDo;
                        var itemSubModel = lstSubModel.Find(x => x.Datum.Date.CompareTo(datum.Date) == 0);
                        if (itemSubModel != null)
                        {
                            ts = kommt ? TimeSpan.Parse(itemSubModel.DoBeginn) : TimeSpan.Parse(itemSubModel.DoEnde);
                        }
                    }
                    break;
                case DayOfWeek.Friday:
                    {
                        ts = tsFr;
                        var itemSubModel = lstSubModel.Find(x => x.Datum.Date.CompareTo(datum.Date) == 0);
                        if (itemSubModel != null)
                        {
                            ts = kommt ? TimeSpan.Parse(itemSubModel.FrBeginn) : TimeSpan.Parse(itemSubModel.FrEnde);
                        }
                    }
                    break;
                case DayOfWeek.Saturday:
                    {
                        ts = tsSa;
                        var itemSubModel = lstSubModel.Find(x => x.Datum.Date.CompareTo(datum.Date) == 0);
                        if (itemSubModel != null)
                        {
                            ts = kommt ? TimeSpan.Parse(itemSubModel.SaBeginn) : TimeSpan.Parse(itemSubModel.SaEnde);
                        }
                    }
                    break;
                default:
                    break;
            }

            if (kommt)
            {
                String stKB = itemBuchung.ZeitKommt;
                try
                {
                    TimeSpan tsKB = TimeSpan.Parse(stKB);
                    if (!edit)
                    {
                        if (!string.IsNullOrWhiteSpace(stKB))
                        {
                            if (ts.Ticks < tsKB.Ticks)
                                return itemBuchung.ZeitKommt;
                            else
                                return new DateTime(ts.Ticks).ToString("HH:mm");
                        }
                        return string.Empty;
                    }
                    else
                    {
                        return itemBuchung.ZeitKommt;
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
            else
            {
                String stGB = itemBuchung.ZeitGeht;
                try
                {
                    if (!edit)
                    {
                        if (!string.IsNullOrWhiteSpace(stGB))
                        {
                            TimeSpan tsGB = TimeSpan.Parse(stGB);
                            if (ts.Ticks > tsGB.Ticks)
                                return itemBuchung.ZeitGeht;
                            else
                                return new DateTime(ts.Ticks).ToString("HH:mm");
                        }
                        return string.Empty;
                    }
                    else
                    {
                        return itemBuchung.ZeitGeht;
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        private static string GetZeitSpielstaetteEsh(TimeSpan tsSP, string zeitEsh)
        {
            if (!string.IsNullOrWhiteSpace(zeitEsh))
            {
                TimeSpan tsESH = TimeSpan.Parse(zeitEsh);
                tsSP = tsSP.Add(tsESH);
                return new DateTime(tsSP.Ticks).ToString("HH:mm");
            }
            else
            {
                return new DateTime(tsSP.Ticks).ToString("HH:mm");
            }
        }

        private static string GetZeitSpielstaetteErs(TimeSpan tsSP, string zeitErs)
        {
            if (!string.IsNullOrWhiteSpace(zeitErs))
            {
                TimeSpan tsESH = TimeSpan.Parse(zeitErs);
                tsSP = tsSP.Add(tsESH);
                return new DateTime(tsSP.Ticks).ToString("HH:mm");
            }
            else
            {
                return new DateTime(tsSP.Ticks).ToString("HH:mm");
            }
        }

        private static string GetZeitSpielstaetteEngelmann(TimeSpan tsSP, string zeitEngelmann)
        {
            if (!string.IsNullOrWhiteSpace(zeitEngelmann))
            {
                TimeSpan tsESH = TimeSpan.Parse(zeitEngelmann);
                tsSP = tsSP.Add(tsESH);
                return new DateTime(tsSP.Ticks).ToString("HH:mm");
            }
            else
            {
                return new DateTime(tsSP.Ticks).ToString("HH:mm");
            }
        }

        private static string GetZeitGeht(TimeSpan tsEndeMo, TimeSpan tsEndeDi, TimeSpan tsEndeMi, TimeSpan tsEndeDo, TimeSpan tsEndeFr, TimeSpan tsEndeSa, TimeSpan tsEndeSo, ZeiterfBuchung itemBuchung, DateTime dtListDatum)
        {
            TimeSpan tsG = new TimeSpan();

            switch (dtListDatum.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    tsG = tsEndeSo;
                    break;
                case DayOfWeek.Monday:
                    tsG = tsEndeMo;
                    break;
                case DayOfWeek.Tuesday:
                    tsG = tsEndeDi;
                    break;
                case DayOfWeek.Wednesday:
                    tsG = tsEndeMi;
                    break;
                case DayOfWeek.Thursday:
                    tsG = tsEndeDo;
                    break;
                case DayOfWeek.Friday:
                    tsG = tsEndeFr;
                    break;
                case DayOfWeek.Saturday:
                    tsG = tsEndeSa;
                    break;
                default:
                    break;
            }

            String stGB = itemBuchung.ZeitGeht;
            if (!string.IsNullOrEmpty(stGB))
            {
                TimeSpan tsGB = TimeSpan.Parse(stGB);
                if (tsG.Ticks > tsGB.Ticks)
                {
                    return itemBuchung.ZeitGeht;
                }
                else
                {
                    return new DateTime(tsG.Ticks).ToString("HH:mm");
                }
            }
            else
            {
                return string.Empty;
            }
        }

        private static string GetGehtBuchung(ZeiterfBuchung itemBuchung)
        {
            if (string.IsNullOrWhiteSpace(itemBuchung.GehtBuchung))
            {
                return itemBuchung.ZeitGeht;
            }
            else
            {
                String stGeht = itemBuchung.ZeitGeht;
                TimeSpan tsGeht1 = TimeSpan.Parse(stGeht);

                String stGehtBuchung = itemBuchung.GehtBuchung;
                if (!string.IsNullOrWhiteSpace(stGehtBuchung))
                {
                    TimeSpan tsGeht2 = TimeSpan.Parse(stGehtBuchung);
                    if (tsGeht1.CompareTo(tsGeht2) > 0)
                        return stGeht;
                    else
                        return stGehtBuchung;
                }
                else
                {
                    return stGeht;
                }
            }
        }

        private static async Task GetUrlaub(string stMitarbeiterID, DateTime dtStart, DateTime dtEnde, List<DateTime> lstDatumUrlaub)
        {
            List<ZeiterfUrlaub> zeiterfUrlaubs = await ZeiterfUrlaub.GetByMitarbeiterAndDate(stMitarbeiterID, dtStart, dtEnde);
            foreach (var item in zeiterfUrlaubs)
            {
                lstDatumUrlaub.Add(Convert.ToDateTime(item.Datum));
            }
        }

        private static async Task GetKrank(string stMitarbeiterID, DateTime dtStart, DateTime dtEnde, List<DateTime> lstDatumKrank)
        {
            List<ZeiterfKrank> zeiterfKranks = await ZeiterfKrank.GetByMitarbeiterAndDate(stMitarbeiterID, dtStart, dtEnde);
            foreach (var item in zeiterfKranks)
            {
                lstDatumKrank.Add(Convert.ToDateTime(item.Datum));
            }
        }

        private static async Task GetFrei(string stMitarbeiterID, DateTime dtStart, DateTime dtEnde, List<ZeitFrei> lstFrei)
        {
            List<ZeiterfFrei> zeiterfFreis = await ZeiterfFrei.GetByMitarbeiterAndDate(stMitarbeiterID, dtStart, dtEnde);
            foreach (var item in zeiterfFreis)
            {
                lstFrei.Add(new ZeitFrei(Convert.ToDateTime(item.Datum), item.Bemerkung));
            }
        }

        private static async Task GetKurzarbeit(string stMitarbeiterID, DateTime dtStart, DateTime dtEnde, List<ZeitKurzarbeit> lstKurzarbeit)
        {
            List<ZeiterfKurzarbeit> zeiterfKurzarbeits = await ZeiterfKurzarbeit.GetByMitarbeiterAndDate(stMitarbeiterID, dtStart, dtEnde);
            foreach (var item in zeiterfKurzarbeits)
            {
                lstKurzarbeit.Add(new ZeitKurzarbeit(Convert.ToDateTime(item.Datum), item.Soll));
            }
        }

        private static async Task GetNH3(string stMitarbeiterID, DateTime dtStart, DateTime dtEnde, List<clsZeitErfNH3> lstNH3)
        {
            List<ZeiterfNH3> zeiterfNH3s = await ZeiterfNH3.GetByMitarbeiterAndDate(stMitarbeiterID, dtStart, dtEnde);
            if (zeiterfNH3s != null)
            {
                foreach (var item in zeiterfNH3s)
                {
                    clsZeitErfNH3 cNH3 = new clsZeitErfNH3();
                    cNH3.Datum = Convert.ToDateTime(item.Datum);
                    cNH3.NH3Start = item.NH3Kommt;
                    cNH3.NH3Ende = string.IsNullOrEmpty(item.NH3Geht) ? "" : item.NH3Geht;
                    if (!string.IsNullOrEmpty(cNH3.NH3Ende))
                    {
                        var nh3result = lstNH3.Find(x => x.Datum.Date.CompareTo(cNH3.Datum.Date) == 0);
                        if (nh3result != null)
                        {
                            TimeSpan tsNH3Start = new TimeSpan(Convert.ToInt32(cNH3.NH3Start.Split(':')[0]), Convert.ToInt32(cNH3.NH3Start.Split(':')[1]), 0);
                            TimeSpan tsNH3Ende = new TimeSpan(Convert.ToInt32(cNH3.NH3Ende.Split(':')[0]), Convert.ToInt32(cNH3.NH3Ende.Split(':')[1]), 0);
                            TimeSpan tsNH3AktZeit = new TimeSpan();
                            if (!string.IsNullOrEmpty(item.NH3Zeit))
                            {
                                tsNH3AktZeit = new TimeSpan(Convert.ToInt32(item.NH3Zeit.Split(':')[0]), Convert.ToInt32(item.NH3Zeit.Split(':')[1]), 0);
                            }
                            nh3result.NH3Ende = new DateTime(tsNH3Ende.Ticks).ToString("HH:mm");

                            if (tsNH3Ende.CompareTo(tsNH3Start) > 0)
                            {
                                TimeSpan tsNH3Zeit = tsNH3Ende.Subtract(tsNH3Start);
                                cNH3.NH3Zeit = ((tsNH3Zeit.Days * 24) + tsNH3Zeit.Hours).ToString().PadLeft(2, '0') + ":" + tsNH3Zeit.Minutes.ToString().PadLeft(2, '0');
                                tsNH3Zeit = tsNH3Zeit.Add(tsNH3AktZeit);
                                nh3result.NH3Zeit = ((tsNH3Zeit.Days * 24) + tsNH3Zeit.Hours).ToString().PadLeft(2, '0') + ":" + tsNH3Zeit.Minutes.ToString().PadLeft(2, '0');
                            }
                            else
                            {
                                DateTime dtNH3StartZeit = new DateTime(item.Datum.Value.Year,
                                    item.Datum.Value.Month, item.Datum.Value.Day, tsNH3Start.Hours, tsNH3Start.Minutes, tsNH3Start.Seconds);

                                TimeSpan tsNH3Zeit = item.Datum.Value.Date.AddDays(1).Subtract(dtNH3StartZeit);
                                tsNH3Zeit += tsNH3Ende;
                                cNH3.NH3Zeit = ((tsNH3Zeit.Days * 24) + tsNH3Zeit.Hours).ToString().PadLeft(2, '0') + ":" + tsNH3Zeit.Minutes.ToString().PadLeft(2, '0');
                                tsNH3Zeit = tsNH3Zeit.Add(tsNH3AktZeit);
                                nh3result.NH3Zeit = ((tsNH3Zeit.Days * 24) + tsNH3Zeit.Hours).ToString().PadLeft(2, '0') + ":" + tsNH3Zeit.Minutes.ToString().PadLeft(2, '0');
                            }
                        }
                        else
                        {
                            TimeSpan tsNH3Start = new TimeSpan(Convert.ToInt32(cNH3.NH3Start.Split(':')[0]), Convert.ToInt32(cNH3.NH3Start.Split(':')[1]), 0);
                            TimeSpan tsNH3Ende = new TimeSpan(Convert.ToInt32(cNH3.NH3Ende.Split(':')[0]), Convert.ToInt32(cNH3.NH3Ende.Split(':')[1]), 0);

                            if (tsNH3Ende.CompareTo(tsNH3Start) > 0)
                            {
                                TimeSpan tsNH3Zeit = tsNH3Ende.Subtract(tsNH3Start);
                                cNH3.NH3Zeit = ((tsNH3Zeit.Days * 24) + tsNH3Zeit.Hours).ToString().PadLeft(2, '0') + ":" + tsNH3Zeit.Minutes.ToString().PadLeft(2, '0');
                                lstNH3.Add(cNH3);
                            }
                            else
                            {
                                DateTime dtNH3StartZeit = new DateTime(item.Datum.Value.Year,
                                    item.Datum.Value.Month, item.Datum.Value.Day, tsNH3Start.Hours, tsNH3Start.Minutes, tsNH3Start.Seconds);

                                TimeSpan tsNH3Zeit = item.Datum.Value.Date.AddDays(1).Subtract(dtNH3StartZeit);
                                tsNH3Zeit += tsNH3Ende;
                                cNH3.NH3Zeit = ((tsNH3Zeit.Days * 24) + tsNH3Zeit.Hours).ToString().PadLeft(2, '0') + ":" + tsNH3Zeit.Minutes.ToString().PadLeft(2, '0');
                                lstNH3.Add(cNH3);
                            }
                        }
                    }
                }
            }
        }


        private static async Task GetSubmodells(int inArbeitszeitModell, DateTime dtStart, DateTime dtEnde, List<ZeiterfSubmodel> lstSubModel)
        {
            var submodels = await ZeiterfModell.GetSubmodels(inArbeitszeitModell);
            if (submodels != null)
            {
                foreach (var item in submodels)
                {
                    if (item.GueltigVon != null && item.GueltigBis != null)
                    {
                        DateTime dtSubGueltigVon = Convert.ToDateTime(item.GueltigVon);
                        DateTime dtSubGueltigBis = Convert.ToDateTime(item.GueltigBis);

                        if (dtStart.Date.CompareTo(dtSubGueltigBis.Date) > 0 || dtEnde.Date.CompareTo(dtSubGueltigVon.Date) < 0)
                        {
                            continue;
                        }
                        else if (dtStart.Date.CompareTo(dtSubGueltigVon.Date) <= 0 && dtEnde.Date.CompareTo(dtSubGueltigBis.Date) >= 0)
                        {
                            DateTime dtSub = dtSubGueltigVon;
                            while (true)
                            {
                                ZeiterfSubmodel SubModel = AddSubmodell(item, dtSub);
                                lstSubModel.Add(SubModel);

                                dtSub = dtSub.AddDays(1);
                                if (dtSub.CompareTo(dtSubGueltigBis) > 0)
                                    break;

                            }

                        }
                        else if (dtStart.Date.CompareTo(dtSubGueltigVon.Date) > 0 && dtEnde.Date.CompareTo(dtSubGueltigBis.Date) >= 0)
                        {
                            DateTime dtSub = dtStart;
                            while (true)
                            {
                                ZeiterfSubmodel SubModel = AddSubmodell(item, dtSub);
                                lstSubModel.Add(SubModel);

                                dtSub = dtSub.AddDays(1);
                                if (dtSub.CompareTo(dtSubGueltigBis) > 0)
                                    break;

                            }
                        }

                        else if (dtStart.Date.CompareTo(dtSubGueltigVon.Date) <= 0 && dtEnde.Date.CompareTo(dtSubGueltigBis.Date) < 0)
                        {
                            DateTime dtSub = dtSubGueltigVon;
                            while (true)
                            {
                                ZeiterfSubmodel SubModel = AddSubmodell(item, dtSub);
                                lstSubModel.Add(SubModel);

                                dtSub = dtSub.AddDays(1);
                                if (dtSub.CompareTo(dtEnde) > 0)
                                    break;

                            }
                        }
                        else if (dtStart.Date.CompareTo(dtSubGueltigVon.Date) > 0 && dtEnde.Date.CompareTo(dtSubGueltigBis.Date) < 0)
                        {
                            DateTime dtSub = dtStart;
                            while (true)
                            {
                                ZeiterfSubmodel SubModel = AddSubmodell(item, dtSub);
                                lstSubModel.Add(SubModel);

                                dtSub = dtSub.AddDays(1);
                                if (dtSub.CompareTo(dtEnde) > 0)
                                    break;

                            }
                        }
                    }
                }
            }
        }

        private static ZeiterfSubmodel AddSubmodell(ZeiterfModell item, DateTime dtSub)
        {
            ZeiterfSubmodel SubModel = new ZeiterfSubmodel();
            SubModel.Datum = dtSub;
            SubModel.MoBeginn = item.MoBeginn;
            SubModel.MoEnde = item.MoEnde;
            SubModel.DiBeginn = item.DiBeginn;
            SubModel.DiEnde = item.DiEnde;
            SubModel.MiBeginn = item.MiBeginn;
            SubModel.MiEnde = item.MiEnde;
            SubModel.DoBeginn = item.DoBeginn;
            SubModel.DoEnde = item.DoEnde;
            SubModel.FrBeginn = item.FrBeginn;
            SubModel.FrEnde = item.FrEnde;
            SubModel.SaBeginn = item.SaBeginn;
            SubModel.SaEnde = item.SaEnde;
            SubModel.SoBeginn = item.SoBeginn;
            SubModel.SoEnde = item.SoEnde;
            return SubModel;
        }

        private static void GetTimestamps(ZeiterfModell modell, DayOfWeek weekday, ref TimeSpan ts, ref TimeSpan tsBeginn, ref TimeSpan tsEnde)
        {
            switch (weekday)
            {
                case DayOfWeek.Sunday:
                    if (!string.IsNullOrWhiteSpace(modell.SoBeginn) && !string.IsNullOrWhiteSpace(modell.SoEnde))
                    {
                        tsBeginn = TimeSpan.Parse(modell.SoBeginn);
                        tsEnde = TimeSpan.Parse(modell.SoEnde);
                        ts = tsEnde.Subtract(tsBeginn);
                    }
                    break;
                case DayOfWeek.Monday:
                    if (!string.IsNullOrWhiteSpace(modell.MoBeginn) && !string.IsNullOrWhiteSpace(modell.MoEnde))
                    {
                        tsBeginn = TimeSpan.Parse(modell.MoBeginn);
                        tsEnde = TimeSpan.Parse(modell.MoEnde);
                        ts = tsEnde.Subtract(tsBeginn);
                    }
                    break;
                case DayOfWeek.Tuesday:
                    if (!string.IsNullOrWhiteSpace(modell.DiBeginn) && !string.IsNullOrWhiteSpace(modell.DiEnde))
                    {
                        tsBeginn = TimeSpan.Parse(modell.DiBeginn);
                        tsEnde = TimeSpan.Parse(modell.DiEnde);
                        ts = tsEnde.Subtract(tsBeginn);
                    }
                    break;
                case DayOfWeek.Wednesday:
                    if (!string.IsNullOrWhiteSpace(modell.MiBeginn) && !string.IsNullOrWhiteSpace(modell.MiEnde))
                    {
                        tsBeginn = TimeSpan.Parse(modell.MiBeginn);
                        tsEnde = TimeSpan.Parse(modell.MiEnde);
                        ts = tsEnde.Subtract(tsBeginn);
                    }
                    break;
                case DayOfWeek.Thursday:
                    if (!string.IsNullOrWhiteSpace(modell.DoBeginn) && !string.IsNullOrWhiteSpace(modell.DoEnde))
                    {
                        tsBeginn = TimeSpan.Parse(modell.DoBeginn);
                        tsEnde = TimeSpan.Parse(modell.DoEnde);
                        ts = tsEnde.Subtract(tsBeginn);
                    }
                    break;
                case DayOfWeek.Friday:
                    if (!string.IsNullOrWhiteSpace(modell.FrBeginn) && !string.IsNullOrWhiteSpace(modell.FrEnde))
                    {
                        tsBeginn = TimeSpan.Parse(modell.FrBeginn);
                        tsEnde = TimeSpan.Parse(modell.FrEnde);
                        ts = tsEnde.Subtract(tsBeginn);
                    }
                    break;
                case DayOfWeek.Saturday:
                    if (!string.IsNullOrWhiteSpace(modell.SaBeginn) && !string.IsNullOrWhiteSpace(modell.SaEnde))
                    {
                        tsBeginn = TimeSpan.Parse(modell.SaBeginn);
                        tsEnde = TimeSpan.Parse(modell.SaEnde);
                        ts = tsEnde.Subtract(tsBeginn);
                    }
                    break;
                default:
                    break;
            }
        }

        private static bool IsWorkDay(DayOfWeek weekday, ZeiterfModell modell)
        {
            if (weekday == DayOfWeek.Monday)
                return modell.Mo == null ? false : Convert.ToBoolean(modell.Mo);
            else if (weekday == DayOfWeek.Tuesday)
                return modell.Di == null ? false : Convert.ToBoolean(modell.Di);
            else if (weekday == DayOfWeek.Wednesday)
                return modell.Mi == null ? false : Convert.ToBoolean(modell.Mi);
            else if (weekday == DayOfWeek.Thursday)
                return modell.Do == null ? false : Convert.ToBoolean(modell.Do);
            else if (weekday == DayOfWeek.Friday)
                return modell.Fr == null ? false : Convert.ToBoolean(modell.Fr);
            else if (weekday == DayOfWeek.Saturday)
                return modell.Sa == null ? false : Convert.ToBoolean(modell.Sa);
            else
                return modell.So == null ? false : Convert.ToBoolean(modell.So);
        }

        private static string GetTag(DateTime dtTag)
        {
            if (dtTag.DayOfWeek == DayOfWeek.Monday)
                return "Montag";
            else if (dtTag.DayOfWeek == DayOfWeek.Tuesday)
                return "Dienstag";
            else if (dtTag.DayOfWeek == DayOfWeek.Wednesday)
                return "Mittwoch";
            else if (dtTag.DayOfWeek == DayOfWeek.Thursday)
                return "Donnerstag";
            else if (dtTag.DayOfWeek == DayOfWeek.Friday)
                return "Freitag";
            else if (dtTag.DayOfWeek == DayOfWeek.Saturday)
                return "Samstag";
            else if (dtTag.DayOfWeek == DayOfWeek.Sunday)
                return "Sonntag";
            else
                return "";
        }

        private static string GetAnwesenheit(TimeSpan tsAnwesend)
        {
            Int64 lgTicksAnwesend = tsAnwesend.Ticks;
            if (lgTicksAnwesend >= 0)
                return new DateTime(tsAnwesend.Ticks).ToString("HH:mm");
            else
            {
                lgTicksAnwesend *= -1;
                return "-" + new DateTime(lgTicksAnwesend).ToString("HH:mm");
            }
        }

        private static string GetIstStunden(TimeSpan tsIst)
        {
            Int64 lgTicks = tsIst.Ticks;
            if (lgTicks >= 0)
            {
                return new DateTime(lgTicks).ToString("HH:mm");
            }
            else
            {
                lgTicks *= -1;
                return "-" + new DateTime(lgTicks).ToString("HH:mm");
            }
        }

        private static bool CheckIfFeiertag(DateTime dtTag)
        {
            var resultFixFt = lstFTFix.Find(x => x.Datum.Date.CompareTo(dtTag.Date) == 0);
            if (resultFixFt != null)
            {
                return true;
            }

            var resultVarFt = lstFTVar.Find(x => x.Datum.Date.CompareTo(dtTag.Date) == 0);
            if (resultVarFt != null)
            {
                return true;
            }

            return false;
        }
    }

    public class clsZeiterfassungUebersicht
    {
        public DateTime Datum { get; set; }
        public string Kommt { get; set; }
        public string Geht { get; set; }
        public string Grund { get; set; }
        public bool Edit { get; set; }
        public string KommtBuchung { get; set; }
        public string GehtBuchung { get; set; }
        public string GeaendertVon { get; set; }
        public DateTime? TSGeaendert { get; set; }
        public string Bemerkung { get; set; }
        public string Dienstgang { get; set; }
        public string ERS { get; set; }
        public string ESH { get; set; }
        public string Engelmann { get; set; }
        public string Taetigkeitsbereich { get; set; }
        public string Spielstaette { get; set; }
        public int ID { get; set; }
        public DateTime? DBTSKommt { get; set; }
        public DateTime? DBTSGeht { get; set; }
        public bool NH3 { get; set; }
        public string NH3Kommt { get; set; }
        public string NH3Geht { get; set; }
        public string NH3Prozent { get; set; }
        public string NH3Zeit { get; set; }
    }
}
