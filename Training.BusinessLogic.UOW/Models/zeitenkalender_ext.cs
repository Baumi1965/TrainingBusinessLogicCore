using System;
using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeitenkalender_ext : XPLiteObject
    {
        public zeitenkalender_ext(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        
        int fZeitenKalenderId;
        public int ZeitenKalenderId
        {
            get { return fZeitenKalenderId; }
            set { SetPropertyValue<int>(nameof(ZeitenKalenderId), ref fZeitenKalenderId, value); }
        }

        int fSpielstaetteId;
        public int SpielstaetteId
        {
            get { return fSpielstaetteId; }
            set { SetPropertyValue<int>(nameof(SpielstaetteId), ref fSpielstaetteId, value); }
        }

        int? fAnzahlFrequenzschein;
        public int? AnzahlFrequenzschein
        {
            get { return fAnzahlFrequenzschein; }
            set { SetPropertyValue<int?>(nameof(AnzahlFrequenzschein), ref fAnzahlFrequenzschein, value); }
        }

        int? fAnzahlFrequenzscheinBegleitperson;
        public int? AnzahlFrequenzscheinBegleitperson
        {
            get { return fAnzahlFrequenzscheinBegleitperson; }
            set { SetPropertyValue<int?>(nameof(AnzahlFrequenzscheinBegleitperson), ref fAnzahlFrequenzscheinBegleitperson, value); }
        }

        double? fWertFrequenzschein;
        public double? WertFrequenzschein
        {
            get { return fWertFrequenzschein; }
            set { SetPropertyValue<double?>(nameof(WertFrequenzschein), ref fWertFrequenzschein, value); }
        }

        int? fAnzahlKassa;
        public int? AnzahlKassa
        {
            get { return fAnzahlKassa; }
            set { SetPropertyValue<int?>(nameof(AnzahlKassa), ref fAnzahlKassa, value); }
        }

        double? fWertKassa;
        public double? WertKassa
        {
            get { return fWertKassa; }
            set { SetPropertyValue<double?>(nameof(WertKassa), ref fWertKassa, value); }
        }

        DateTime fTS;
        public DateTime TS
        {
            get { return fTS; }
            set { SetPropertyValue<DateTime>(nameof(TS), ref fTS, value); }
        }

        DateTime fTS_Created;
        public DateTime TS_Created
        {
            get { return fTS_Created; }
            set { SetPropertyValue<DateTime>(nameof(TS_Created), ref fTS_Created, value); }
        }

        DateTime fTS_Modified;
        public DateTime TS_Modified
        {
            get { return fTS_Modified; }
            set { SetPropertyValue<DateTime>(nameof(TS_Modified), ref fTS_Modified, value); }
        }

        string fUser_Created;
        [Size(45)]
        [Nullable(false)]
        public string User_Created
        {
            get { return fUser_Created; }
            set { SetPropertyValue<string>(nameof(User_Created), ref fUser_Created, value); }
        }

        string fUser_Modified;
        [Size(45)]
        [Nullable(false)]
        public string User_Modified
        {
            get { return fUser_Modified; }
            set { SetPropertyValue<string>(nameof(User_Modified), ref fUser_Modified, value); }
        }
        
        int? fAnzahl;
        public int? Anzahl
        {
            get { return fAnzahl; }
            set { SetPropertyValue<int?>(nameof(Anzahl), ref fAnzahl, value); }
        }

        double? fWert;
        public double? Wert
        {
            get { return fWert; }
            set { SetPropertyValue<double?>(nameof(Wert), ref fWert, value); }
        }
    }   
}

