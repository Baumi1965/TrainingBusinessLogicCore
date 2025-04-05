using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.ZeitenKalender
{
    public class ZeitenKalender
    {
        public int ID { get; set; }
        public int? Type { get; set; }
        public DateTime? StartDatum { get; set; }
        public DateTime? EndeDatum { get; set; }
        public bool? Ganztaegig { get; set; }
        public string Betreff { get; set; }
        public int? SpielstaetteID { get; set; }
        public string Spielstaette { get; set; }
        public string Beschreibung { get; set; }
        public int? Status { get; set; }
        public int? Label { get; set; }
        public string TrainingID { get; set; }
        public string Training { get; set; }
        public string ReminderInfo { get; set; }
        public string RecurrenceInfo { get; set; }
        public string Zusatztext { get; set; }
        public string Verband { get; set; }
        public string Ansprechpartner { get; set; }
        public string Tel { get; set; }
        public bool? NichtBespielbar { get; set; }
        public string Frequenz { get; set; }
        public bool? Matchuhr { get; set; }
        public bool? ExtraEis { get; set; }
        public bool Buchungssystem { get; set; }
        public DateTime? TSChanged { get; set; }
        public string UserChanged { get; set; }
        public String Benutzer { get; set; }
        public String AbgebuchterBetrag { get; set; }

        public static async Task<List<ZeitenKalender>> Get()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var zk = await UOW.Uow._uow.Query<zeitenkalender>().ToListAsync();
                List<ZeitenKalender> lstZK = new List<ZeitenKalender>();
                foreach (var item in zk)
                {
                    lstZK.Add(
                        new ZeitenKalender()
                        {
                            ID = item.ID,
                            Ansprechpartner = item.Ansprechpartner,
                            Beschreibung = item.Beschreibung,
                            Betreff = item.Betreff,
                            Buchungssystem = item.Buchungssystem,
                            EndeDatum = item.EndeDatum,
                            ExtraEis = item.ExtraEis,
                            Frequenz = item.Frequenz,
                            Ganztaegig = item.Ganztaegig,
                            Label = item.Label,
                            Matchuhr = item.Matchuhr,
                            NichtBespielbar = item.NichtBespielbar,
                            RecurrenceInfo = item.RecurrenceInfo,
                            ReminderInfo = item.ReminderInfo,
                            Spielstaette = item.Spielstaette,
                            SpielstaetteID = item.SpielstaetteID,
                            StartDatum = item.StartDatum,
                            Status = item.Status,
                            Tel = item.Tel,
                            Training = item.Training,
                            TrainingID = item.TrainingID,
                            Type = item.Type,
                            Verband = item.Verband,
                            Zusatztext = item.Zusatztext,
                            TSChanged = item.TSChanged,
                            UserChanged = item.UserChanged,
                            Benutzer = item.Benutzer,
                            AbgebuchterBetrag = item.AbgebuchterBetrag,
                        });
                }
                return lstZK;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<ZeitenKalender>> GetBySpielstaette(int spielstaette)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var zk = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.SpielstaetteID == spielstaette).ToListAsync();
                List<ZeitenKalender> lstZK = new List<ZeitenKalender>();
                foreach (var item in zk)
                {
                    lstZK.Add(
                        new ZeitenKalender()
                        {
                            ID = item.ID,
                            Ansprechpartner = item.Ansprechpartner,
                            Beschreibung = item.Beschreibung,
                            Betreff = item.Betreff,
                            Buchungssystem = item.Buchungssystem,
                            EndeDatum = item.EndeDatum,
                            ExtraEis = item.ExtraEis,
                            Frequenz = item.Frequenz,
                            Ganztaegig = item.Ganztaegig,
                            Label = item.Label,
                            Matchuhr = item.Matchuhr,
                            NichtBespielbar = item.NichtBespielbar,
                            RecurrenceInfo = item.RecurrenceInfo,
                            ReminderInfo = item.ReminderInfo,
                            Spielstaette = item.Spielstaette,
                            SpielstaetteID = item.SpielstaetteID,
                            StartDatum = item.StartDatum,
                            Status = item.Status,
                            Tel = item.Tel,
                            Training = item.Training,
                            TrainingID = item.TrainingID,
                            Type = item.Type,
                            Verband = item.Verband,
                            Zusatztext = item.Zusatztext,
                            TSChanged = item.TSChanged,
                            UserChanged = item.UserChanged,
                            Benutzer = item.Benutzer,
                            AbgebuchterBetrag = item.AbgebuchterBetrag,
                        });
                }
                return lstZK;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<ZeitenKalender> GetByDateLocationBelegungsartAsync(List<int> belegungsarten, DateTime startDate, List<int> location, TimeSpan time, CancellationToken cancellationToken
            = default)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var item = await UOW.Uow._uow.Query<zeitenkalender>()
                    .Where(x => x.Status.HasValue && belegungsarten.Contains(x.Status.Value) &&
                                location.Contains(x.SpielstaetteID.Value) && x.StartDatum.HasValue &&
                                x.StartDatum.Value.Date == startDate.Date &&
                                x.StartDatum.Value.AddHours(-2).TimeOfDay <= time && x.EndeDatum.HasValue &&
                                x.EndeDatum.Value.AddHours(2).TimeOfDay >= time)
                    .OrderBy(x => x.StartDatum)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (item == null)
                {
                    return null;
                }

                return new ZeitenKalender()
                {
                    ID = item.ID,
                    Ansprechpartner = item.Ansprechpartner,
                    Beschreibung = item.Beschreibung,
                    Betreff = item.Betreff,
                    Buchungssystem = item.Buchungssystem,
                    EndeDatum = item.EndeDatum,
                    ExtraEis = item.ExtraEis,
                    Frequenz = item.Frequenz,
                    Ganztaegig = item.Ganztaegig,
                    Label = item.Label,
                    Matchuhr = item.Matchuhr,
                    NichtBespielbar = item.NichtBespielbar,
                    RecurrenceInfo = item.RecurrenceInfo,
                    ReminderInfo = item.ReminderInfo,
                    Spielstaette = item.Spielstaette,
                    SpielstaetteID = item.SpielstaetteID,
                    StartDatum = item.StartDatum,
                    Status = item.Status,
                    Tel = item.Tel,
                    Training = item.Training,
                    TrainingID = item.TrainingID,
                    Type = item.Type,
                    Verband = item.Verband,
                    Zusatztext = item.Zusatztext,
                    TSChanged = item.TSChanged,
                    UserChanged = item.UserChanged,
                    Benutzer = item.Benutzer,
                    AbgebuchterBetrag = item.AbgebuchterBetrag,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        
        
        public static async Task<List<ZeitenKalender>> GetByDate(DateTime startdate, int spielstaette)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var zk = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.SpielstaetteID == spielstaette && x.StartDatum.Value.Date == startdate.Date).ToListAsync();
                List<ZeitenKalender> lstZK = new List<ZeitenKalender>();
                foreach (var item in zk)
                {
                    lstZK.Add(
                        new ZeitenKalender()
                        {
                            ID = item.ID,
                            Ansprechpartner = item.Ansprechpartner,
                            Beschreibung = item.Beschreibung,
                            Betreff = item.Betreff,
                            Buchungssystem = item.Buchungssystem,
                            EndeDatum = item.EndeDatum,
                            ExtraEis = item.ExtraEis,
                            Frequenz = item.Frequenz,
                            Ganztaegig = item.Ganztaegig,
                            Label = item.Label,
                            Matchuhr = item.Matchuhr,
                            NichtBespielbar = item.NichtBespielbar,
                            RecurrenceInfo = item.RecurrenceInfo,
                            ReminderInfo = item.ReminderInfo,
                            Spielstaette = item.Spielstaette,
                            SpielstaetteID = item.SpielstaetteID,
                            StartDatum = item.StartDatum,
                            Status = item.Status,
                            Tel = item.Tel,
                            Training = item.Training,
                            TrainingID = item.TrainingID,
                            Type = item.Type,
                            Verband = item.Verband,
                            Zusatztext = item.Zusatztext,
                            TSChanged = item.TSChanged,
                            UserChanged = item.UserChanged,
                            Benutzer = item.Benutzer,
                            AbgebuchterBetrag = item.AbgebuchterBetrag,
                        });
                }
                return lstZK;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<ZeitenKalender>> GetByDate(DateTime startdate, DateTime enddate, int? spielstaette = null)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                List<zeitenkalender> lstZk = new List<zeitenkalender>();

                if (spielstaette != null)
                {
                    lstZk = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.SpielstaetteID == spielstaette && x.StartDatum.Value.Date >= startdate.Date && x.StartDatum.Value.Date <= enddate.Date).ToListAsync();
                }
                else
                {
                    lstZk = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.StartDatum.Value.Date >= startdate.Date && x.StartDatum.Value.Date <= enddate.Date).ToListAsync();
                }

                List<ZeitenKalender> lstZK = new List<ZeitenKalender>();
                foreach (var item in lstZk)
                {
                    lstZK.Add(
                        new ZeitenKalender()
                        {
                            ID = item.ID,
                            Ansprechpartner = item.Ansprechpartner,
                            Beschreibung = item.Beschreibung,
                            Betreff = item.Betreff,
                            Buchungssystem = item.Buchungssystem,
                            EndeDatum = item.EndeDatum,
                            ExtraEis = item.ExtraEis,
                            Frequenz = item.Frequenz,
                            Ganztaegig = item.Ganztaegig,
                            Label = item.Label,
                            Matchuhr = item.Matchuhr,
                            NichtBespielbar = item.NichtBespielbar,
                            RecurrenceInfo = item.RecurrenceInfo,
                            ReminderInfo = item.ReminderInfo,
                            Spielstaette = item.Spielstaette,
                            SpielstaetteID = item.SpielstaetteID,
                            StartDatum = item.StartDatum,
                            Status = item.Status,
                            Tel = item.Tel,
                            Training = item.Training,
                            TrainingID = item.TrainingID,
                            Type = item.Type,
                            Verband = item.Verband,
                            Zusatztext = item.Zusatztext,
                            TSChanged = item.TSChanged,
                            UserChanged = item.UserChanged,
                            Benutzer = item.Benutzer,
                            AbgebuchterBetrag = item.AbgebuchterBetrag,
                        });
                }
                return lstZK;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<ZeitenKalender>> GetByDate(DateTime startdate, DateTime enddate, List<string> spielstaetten)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                List<zeitenkalender> lstZk = new List<zeitenkalender>();
                List<ZeitenKalender> lstZK = new List<ZeitenKalender>();

                if (spielstaetten.Count > 0 && !string.IsNullOrEmpty(spielstaetten[0]))
                {
                    lstZk.Clear();
                    var sp = await Spielstaetten.Spielstaetten.GetForSchedulerAsync();
                    foreach (string s in spielstaetten)
                    {
                        var spielst = sp.Where(x => x.Bezeichnung == s.Trim()).FirstOrDefault();
                        lstZk = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.SpielstaetteID == spielst.ID && x.StartDatum.Value.Date >= startdate.Date && x.StartDatum.Value.Date <= enddate.Date).ToListAsync();
                        foreach (var item in lstZk)
                        {
                            lstZK.Add(
                                new ZeitenKalender()
                                {
                                    ID = item.ID,
                                    Ansprechpartner = item.Ansprechpartner,
                                    Beschreibung = item.Beschreibung,
                                    Betreff = item.Betreff,
                                    Buchungssystem = item.Buchungssystem,
                                    EndeDatum = item.EndeDatum,
                                    ExtraEis = item.ExtraEis,
                                    Frequenz = item.Frequenz,
                                    Ganztaegig = item.Ganztaegig,
                                    Label = item.Label,
                                    Matchuhr = item.Matchuhr,
                                    NichtBespielbar = item.NichtBespielbar,
                                    RecurrenceInfo = item.RecurrenceInfo,
                                    ReminderInfo = item.ReminderInfo,
                                    Spielstaette = item.Spielstaette,
                                    SpielstaetteID = item.SpielstaetteID,
                                    StartDatum = item.StartDatum,
                                    Status = item.Status,
                                    Tel = item.Tel,
                                    Training = item.Training,
                                    TrainingID = item.TrainingID,
                                    Type = item.Type,
                                    Verband = item.Verband,
                                    Zusatztext = item.Zusatztext,
                                    TSChanged = item.TSChanged,
                                    UserChanged = item.UserChanged,
                                    Benutzer = item.Benutzer,
                                    AbgebuchterBetrag = item.AbgebuchterBetrag,
                                });
                        }
                    }
                }
                else
                {
                    lstZk = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.StartDatum.Value.Date >= startdate.Date && x.StartDatum.Value.Date <= enddate.Date).ToListAsync();
                    foreach (var item in lstZk)
                    {
                        lstZK.Add(
                            new ZeitenKalender()
                            {
                                ID = item.ID,
                                Ansprechpartner = item.Ansprechpartner,
                                Beschreibung = item.Beschreibung,
                                Betreff = item.Betreff,
                                Buchungssystem = item.Buchungssystem,
                                EndeDatum = item.EndeDatum,
                                ExtraEis = item.ExtraEis,
                                Frequenz = item.Frequenz,
                                Ganztaegig = item.Ganztaegig,
                                Label = item.Label,
                                Matchuhr = item.Matchuhr,
                                NichtBespielbar = item.NichtBespielbar,
                                RecurrenceInfo = item.RecurrenceInfo,
                                ReminderInfo = item.ReminderInfo,
                                Spielstaette = item.Spielstaette,
                                SpielstaetteID = item.SpielstaetteID,
                                StartDatum = item.StartDatum,
                                Status = item.Status,
                                Tel = item.Tel,
                                Training = item.Training,
                                TrainingID = item.TrainingID,
                                Type = item.Type,
                                Verband = item.Verband,
                                Zusatztext = item.Zusatztext,
                                TSChanged = item.TSChanged,
                                UserChanged = item.UserChanged,
                                Benutzer = item.Benutzer,
                                AbgebuchterBetrag = item.AbgebuchterBetrag,
                            });
                    }
                }

                return lstZK;
            }
            catch (Exception)
            {
                throw;
            }
        }

        
        
        public static async Task<List<ZeitenKalenderView>> GetViewByDate(DateTime startdate, DateTime enddate, int? spielstaette = null)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                List<zeitenkalender> lstZk = new List<zeitenkalender>();

                if (spielstaette != null)
                {
                    lstZk = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.SpielstaetteID == spielstaette && x.StartDatum.Value.Date >= startdate.Date && x.StartDatum.Value.Date <= enddate.Date).ToListAsync();
                }
                else
                {
                    lstZk = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.StartDatum.Value.Date >= startdate.Date && x.StartDatum.Value.Date <= enddate.Date).ToListAsync();
                }

                List<ZeitenKalenderView> lstZK = new List<ZeitenKalenderView>();
                foreach (var item in lstZk)
                {
                    ZeitenKalenderView view = new ZeitenKalenderView();
                    view.ID = item.ID;
                    view.Ansprechpartner = item.Ansprechpartner;
                    view.Beschreibung = item.Beschreibung;
                    view.Betreff = item.Betreff;
                    view.Buchungssystem = item.Buchungssystem == true ? 1 : 0;
                    view.EndeDatum = item.EndeDatum;
                    view.ExtraEis = item.ExtraEis == true ? 1 : 0;
                    if (string.IsNullOrEmpty(item.Frequenz))
                    {
                        view.Frequenz = null;
                    }
                    else
                    {
                        view.Frequenz = Convert.ToInt32(item.Frequenz);
                    }
                    view.Ganztaegig = item.Ganztaegig == null ? 0 : Convert.ToInt32(item.Ganztaegig);
                    view.Label = item.Label;
                    view.Matchuhr = item.Matchuhr == true ? 1 : 0;
                    view.NichtBespielbar = item.NichtBespielbar == true ? 1 : 0;
                    view.RecurrenceInfo = item.RecurrenceInfo;
                    view.ReminderInfo = item.ReminderInfo;
                    int sp = 0;
                    if (int.TryParse(item.Spielstaette, out sp))
                    {
                        view.Spielstaette = sp;
                    }
                    view.SpielstaetteID = item.SpielstaetteID;
                    view.StartDatum = item.StartDatum;
                    view.Status = item.Status;
                    view.Tel = item.Tel;
                    view.Training = item.Training;
                    view.TrainingID = item.TrainingID;
                    view.Type = item.Type;
                    view.Verband = item.Verband;
                    view.Zusatztext = item.Zusatztext;
                    view.TSChanged = item.TSChanged;
                    view.UserChanged = item.UserChanged;
                    view.Benutzer = item.Benutzer;
                    if (string.IsNullOrEmpty(item.AbgebuchterBetrag))
                    {
                        view.AbgebuchterBetrag = null;
                    }
                    else
                    {
                        view.AbgebuchterBetrag = Convert.ToDouble(item.AbgebuchterBetrag);
                    }
                    lstZK.Add(view);
                }
                return lstZK;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task Delete(int id)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var typ = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (typ != null)
                {
                    UOW.Uow._uow.Delete(typ);
                }
                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task Update(int id, ZeitenKalender apt)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var app = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (app != null)
                {
                    app.Beschreibung = apt.Beschreibung;
                    app.Label = apt.Label;
                    app.Betreff = apt.Betreff;
                    app.EndeDatum = apt.EndeDatum;
                    app.Spielstaette = apt.Spielstaette;
                    app.RecurrenceInfo = apt.RecurrenceInfo;
                    app.SpielstaetteID = apt.SpielstaetteID;
                    app.StartDatum = apt.StartDatum;
                    app.Status = apt.Status;
                    app.Training = apt.Training;
                    app.Type = apt.Type;
                    app.Verband = apt.Verband;
                    app.Ansprechpartner = apt.Ansprechpartner;
                    app.Tel = apt.Tel;
                    app.NichtBespielbar = apt.NichtBespielbar;
                    app.Matchuhr = apt.Matchuhr;
                    app.ExtraEis = apt.ExtraEis;
                    app.Frequenz = apt.Frequenz;
                    app.Buchungssystem = apt.Buchungssystem;
                    app.TSChanged = apt.TSChanged;
                    app.UserChanged = apt.UserChanged;
                    app.Benutzer = apt.Benutzer;
                    app.AbgebuchterBetrag = apt.AbgebuchterBetrag;
                }
                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task Update(int id, int frequenz, bool nichtbespielbar, bool matchuhr, bool extraeis, string benutzer, string userChanged)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var app = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (app != null)
                {
                    app.NichtBespielbar = nichtbespielbar;
                    app.Matchuhr = matchuhr;
                    app.ExtraEis = extraeis;
                    app.Frequenz = frequenz.ToString();
                    app.TSChanged = DateTime.Now;
                    app.UserChanged = userChanged;
                    app.Benutzer = benutzer;
                }
                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateFrequenz(int id, string userChanged, decimal wert = 0)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var app = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (app != null)
                {
                    if (!string.IsNullOrEmpty(app.Frequenz))
                    {
                        int Frequenz = 0;

                        int.TryParse(app.Frequenz, out Frequenz);
                        Frequenz += 1;
                        app.Frequenz = Frequenz.ToString();
                    }
                    else
                    {
                        app.Frequenz = "1";
                    }

                    if (!string.IsNullOrEmpty(app.AbgebuchterBetrag))
                    {
                        decimal abgebuchterBetrag = 0;
                        decimal.TryParse(app.AbgebuchterBetrag, out abgebuchterBetrag);
                        abgebuchterBetrag += wert;
                        app.AbgebuchterBetrag = abgebuchterBetrag.ToString("0.00");
                    }
                    else
                    {
                        app.AbgebuchterBetrag = wert.ToString("0.00");
                    }

                    app.TSChanged = DateTime.Now;
                    app.UserChanged = userChanged;
                }
                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task Add(ZeitenKalender apt)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                zeitenkalender app = new zeitenkalender(UOW.Uow._uow);
                app.Beschreibung = apt.Beschreibung;
                app.Label = apt.Label;
                app.Betreff = apt.Betreff;
                app.EndeDatum = apt.EndeDatum;
                app.Spielstaette = apt.Spielstaette;
                app.RecurrenceInfo = apt.RecurrenceInfo;
                app.SpielstaetteID = apt.SpielstaetteID;
                app.StartDatum = apt.StartDatum;
                app.Status = apt.Status;
                app.Training = apt.Training;
                app.Type = apt.Type;
                app.Verband = apt.Verband;
                app.Ansprechpartner = apt.Ansprechpartner;
                app.Tel = apt.Tel;
                app.NichtBespielbar = apt.NichtBespielbar;
                app.Matchuhr = apt.Matchuhr;
                app.ExtraEis = apt.ExtraEis;
                app.Frequenz = apt.Frequenz;
                app.Buchungssystem = apt.Buchungssystem;
                app.TSChanged = apt.TSChanged;
                app.UserChanged = apt.UserChanged;
                app.Benutzer = apt.Benutzer;
                app.AbgebuchterBetrag = apt.AbgebuchterBetrag;
                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<ZeitenKalender>> GetByDateAndBuchungssystemAndStatus(DateTime startdate, int location, int status)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                List<ZeitenKalender> lstZk = new List<ZeitenKalender>();

                var resultZeiten = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.Status == status && x.Buchungssystem == true && x.StartDatum.Value.Date == startdate.Date && x.SpielstaetteID == location).OrderBy(x => x.StartDatum).ToListAsync();

                if (resultZeiten != null)
                {
                    foreach (var item in resultZeiten)
                    {
                        lstZk.Add(
                            new ZeitenKalender()
                            {
                                ID = item.ID,
                                Ansprechpartner = item.Ansprechpartner,
                                Beschreibung = item.Beschreibung,
                                Betreff = item.Betreff,
                                Buchungssystem = item.Buchungssystem,
                                EndeDatum = item.EndeDatum,
                                ExtraEis = item.ExtraEis,
                                Frequenz = item.Frequenz,
                                Ganztaegig = item.Ganztaegig,
                                Label = item.Label,
                                Matchuhr = item.Matchuhr,
                                NichtBespielbar = item.NichtBespielbar,
                                RecurrenceInfo = item.RecurrenceInfo,
                                ReminderInfo = item.ReminderInfo,
                                Spielstaette = item.Spielstaette,
                                SpielstaetteID = item.SpielstaetteID,
                                StartDatum = item.StartDatum,
                                Status = item.Status,
                                Tel = item.Tel,
                                Training = item.Training,
                                TrainingID = item.TrainingID,
                                Type = item.Type,
                                Verband = item.Verband,
                                Zusatztext = item.Zusatztext,
                                TSChanged = item.TSChanged,
                                UserChanged = item.UserChanged,
                                Benutzer = item.Benutzer,
                                AbgebuchterBetrag = item.AbgebuchterBetrag,
                            });
                    }
                    return lstZk;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static async Task<List<ZeitenKalender>> GetByDateAndBuchungssystemAsync(DateTime startdate, int location)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                List<ZeitenKalender> lstZk = new List<ZeitenKalender>();

                var resultZeiten = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.Buchungssystem == true && x.StartDatum.Value.Date == startdate.Date && x.SpielstaetteID == location).OrderBy(x => x.StartDatum).ToListAsync();

                if (resultZeiten != null)
                {
                    foreach (var item in resultZeiten)
                    {
                        lstZk.Add(new ZeitenKalender()
                        {
                            ID = item.ID,
                            Ansprechpartner = item.Ansprechpartner,
                            Beschreibung = item.Beschreibung,
                            Betreff = item.Betreff,
                            Buchungssystem = item.Buchungssystem,
                            EndeDatum = item.EndeDatum,
                            ExtraEis = item.ExtraEis,
                            Frequenz = item.Frequenz,
                            Ganztaegig = item.Ganztaegig,
                            Label = item.Label,
                            Matchuhr = item.Matchuhr,
                            NichtBespielbar = item.NichtBespielbar,
                            RecurrenceInfo = item.RecurrenceInfo,
                            ReminderInfo = item.ReminderInfo,
                            Spielstaette = item.Spielstaette,
                            SpielstaetteID = item.SpielstaetteID,
                            StartDatum = item.StartDatum,
                            Status = item.Status,
                            Tel = item.Tel,
                            Training = item.Training,
                            TrainingID = item.TrainingID,
                            Type = item.Type,
                            Verband = item.Verband,
                            Zusatztext = item.Zusatztext,
                            TSChanged = item.TSChanged,
                            UserChanged = item.UserChanged,
                            Benutzer = item.Benutzer,
                            AbgebuchterBetrag = item.AbgebuchterBetrag,
                        });
                    }
                    return lstZk;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<ZeitenKalender>> GetByDateAndBuchungssystemAndExcludeStatus(DateTime startdate, int location, int status)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                List<ZeitenKalender> lstZk = new List<ZeitenKalender>();

                var resultZeiten = await UOW.Uow._uow.Query<zeitenkalender>().Where(x => x.Status != status && x.Buchungssystem == true && x.StartDatum.Value.Date == startdate.Date && x.SpielstaetteID == location).OrderBy(x => x.StartDatum).ToListAsync();

                if (resultZeiten != null)
                {
                    foreach (var item in resultZeiten)
                    {
                        lstZk.Add(new ZeitenKalender()
                        {
                            ID = item.ID,
                            Ansprechpartner = item.Ansprechpartner,
                            Beschreibung = item.Beschreibung,
                            Betreff = item.Betreff,
                            Buchungssystem = item.Buchungssystem,
                            EndeDatum = item.EndeDatum,
                            ExtraEis = item.ExtraEis,
                            Frequenz = item.Frequenz,
                            Ganztaegig = item.Ganztaegig,
                            Label = item.Label,
                            Matchuhr = item.Matchuhr,
                            NichtBespielbar = item.NichtBespielbar,
                            RecurrenceInfo = item.RecurrenceInfo,
                            ReminderInfo = item.ReminderInfo,
                            Spielstaette = item.Spielstaette,
                            SpielstaetteID = item.SpielstaetteID,
                            StartDatum = item.StartDatum,
                            Status = item.Status,
                            Tel = item.Tel,
                            Training = item.Training,
                            TrainingID = item.TrainingID,
                            Type = item.Type,
                            Verband = item.Verband,
                            Zusatztext = item.Zusatztext,
                            TSChanged = item.TSChanged,
                            UserChanged = item.UserChanged,
                            Benutzer = item.Benutzer,
                            AbgebuchterBetrag = item.AbgebuchterBetrag,
                        });
                    }
                    return lstZk;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<ZeitenBuchungView> GetEiszeitenBuchung(DateTime startdate, DateTime enddate, string verein, int? spielstaette = null, int? belegungsart = null)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var lstZK = from zk in UOW.Uow._uow.Query<zeitenkalender>()
                            join sp in UOW.Uow._uow.Query<spielstaetten>()
                            on zk.SpielstaetteID equals sp.ID
                            where zk.StartDatum.Value.Date >= startdate.Date && zk.StartDatum.Value.Date <= enddate.Date
                            select new
                            {
                                zk.ID,
                                zk.Betreff,
                                sp.Bezeichnung,
                                zk.StartDatum,
                                zk.SpielstaetteID,
                                zk.Status,
                                zk.EndeDatum,
                                zk.Verband,
                                zk.Ansprechpartner,
                                zk.Beschreibung,
                                zk.Benutzer,
                                zk.Frequenz,
                                zk.AbgebuchterBetrag,
                            };

                if (lstZK != null && lstZK.Count() > 0)
                {
                    if (spielstaette != null)
                    {
                        lstZK = lstZK.Where(x => x.SpielstaetteID == spielstaette);
                    }
                    if (belegungsart != null)
                    {
                        lstZK = lstZK.Where(x => x.Status == belegungsart);
                    }
                    if (!string.IsNullOrEmpty(verein))
                    {
                        lstZK = lstZK.Where(x => x.Verband == verein);
                    }
                }


                List<ZeitenBuchungView> lstView = new List<ZeitenBuchungView>();
                foreach (var item in lstZK)
                {
                    ZeitenBuchungView view = new ZeitenBuchungView();
                    view.ID = item.ID;
                    view.Betreff = item.Betreff;
                    view.Spielstaette = item.Bezeichnung;
                    view.Datum = item.StartDatum;
                    view.Tag = Enum.GetName(typeof(Weekdays.GermanWeekdays), item.StartDatum.Value.DayOfWeek);
                    view.Beginn = item.StartDatum.Value;
                    view.Ende = item.EndeDatum.Value;
                    view.Dauer = new DateTime(view.Ende.Value.Subtract(view.Beginn.Value).Ticks);
                    view.Verband = item.Verband;
                    view.Ansprechpartner = item.Ansprechpartner;
                    view.Beschreibung = item.Beschreibung;
                    view.Benutzer = item.Benutzer;
                    if (string.IsNullOrEmpty(item.Frequenz))
                    {
                        view.Frequenz = null;
                    }
                    else
                    {
                        view.Frequenz = Convert.ToInt32(item.Frequenz);
                    }
                    if (string.IsNullOrEmpty(item.AbgebuchterBetrag))
                    {
                        view.AbgebuchterBetrag = null;
                    }
                    else
                    {
                        view.AbgebuchterBetrag = Convert.ToDouble(item.AbgebuchterBetrag);
                    }
                    lstView.Add(view);
                }

                return lstView;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

    public class ZeitenKalenderView
    {
        public int ID { get; set; }
        public int? Type { get; set; }
        public DateTime? StartDatum { get; set; }
        public DateTime? EndeDatum { get; set; }
        public int? Ganztaegig { get; set; }
        public string Betreff { get; set; }
        public int? SpielstaetteID { get; set; }
        public int Spielstaette { get; set; }
        public string Beschreibung { get; set; }
        public int? Status { get; set; }
        public int? Label { get; set; }
        public string TrainingID { get; set; }
        public string Training { get; set; }
        public string ReminderInfo { get; set; }
        public string RecurrenceInfo { get; set; }
        public string Zusatztext { get; set; }
        public string Verband { get; set; }
        public string Ansprechpartner { get; set; }
        public string Tel { get; set; }
        public int NichtBespielbar { get; set; }
        public int? Frequenz { get; set; }
        public int Matchuhr { get; set; }
        public int ExtraEis { get; set; }
        public int Buchungssystem { get; set; }
        public DateTime? TSChanged { get; set; }
        public string UserChanged { get; set; }
        public string Benutzer { get; set; }
        public double? AbgebuchterBetrag { get; set; }
    }

    public class ZeitenBuchungView
    {
        public int ID { get; set; }
        public string Betreff { get; set; }
        public string Spielstaette { get; set; }
        public DateTime? Datum { get; set; }
        public string Tag { get; set; }
        public DateTime? Beginn { get; set; }
        public DateTime? Ende { get; set; }
        public DateTime? Dauer { get; set; }
        public string Verband { get; set; }
        public string Ansprechpartner { get; set; }
        public string Beschreibung { get; set; }
        public string Benutzer { get; set; }
        public int? Frequenz { get; set; }
        public double? AbgebuchterBetrag { get; set; }
    }

    public class Weekdays
    {
        public enum GermanWeekdays
        {
            Sonntag,
            Montag,
            Dienstag,
            Mittwoch,
            Donnerstag,
            Freitag,
            Samstag
        }
    }
}
