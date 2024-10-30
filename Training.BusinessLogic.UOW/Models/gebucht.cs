using DevExpress.Xpo;
using System;

namespace Training.BusinessLogic.UOW.Models
{

    public class gebucht : XPLiteObject
    {
        public gebucht(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fKdNr;
        [Size(255)]
        public string KdNr
        {
            get { return fKdNr; }
            set { SetPropertyValue<string>(nameof(KdNr), ref fKdNr, value); }
        }
        string fVName;
        [Size(255)]
        public string VName
        {
            get { return fVName; }
            set { SetPropertyValue<string>(nameof(VName), ref fVName, value); }
        }
        string fNName;
        [Size(255)]
        public string NName
        {
            get { return fNName; }
            set { SetPropertyValue<string>(nameof(NName), ref fNName, value); }
        }
        int? fTraining;
        public int? Training
        {
            get { return fTraining; }
            set { SetPropertyValue<int?>(nameof(Training), ref fTraining, value); }
        }
        string fTrainingBez;
        [Size(255)]
        public string TrainingBez
        {
            get { return fTrainingBez; }
            set { SetPropertyValue<string>(nameof(TrainingBez), ref fTrainingBez, value); }
        }
        bool? fVormittag;
        [ColumnDefaultValue(false)]
        public bool? Vormittag
        {
            get { return fVormittag; }
            set { SetPropertyValue<bool?>(nameof(Vormittag), ref fVormittag, value); }
        }
        bool? fNachmittag;
        [ColumnDefaultValue(false)]
        public bool? Nachmittag
        {
            get { return fNachmittag; }
            set { SetPropertyValue<bool?>(nameof(Nachmittag), ref fNachmittag, value); }
        }
        bool? fAbend;
        [ColumnDefaultValue(false)]
        public bool? Abend
        {
            get { return fAbend; }
            set { SetPropertyValue<bool?>(nameof(Abend), ref fAbend, value); }
        }
        bool? fAdult;
        [ColumnDefaultValue(false)]
        public bool? Adult
        {
            get { return fAdult; }
            set { SetPropertyValue<bool?>(nameof(Adult), ref fAdult, value); }
        }
        string fDatum;
        [Size(255)]
        public string Datum
        {
            get { return fDatum; }
            set { SetPropertyValue<string>(nameof(Datum), ref fDatum, value); }
        }
        bool? fEnabled;
        [ColumnDefaultValue(false)]
        public bool? Enabled
        {
            get { return fEnabled; }
            set { SetPropertyValue<bool?>(nameof(Enabled), ref fEnabled, value); }
        }
        int? fLocation;
        public int? Location
        {
            get { return fLocation; }
            set { SetPropertyValue<int?>(nameof(Location), ref fLocation, value); }
        }
        decimal? fWert;
        [ColumnDefaultValue(0.00)]
        public decimal? Wert
        {
            get { return fWert; }
            set { SetPropertyValue<decimal?>(nameof(Wert), ref fWert, value); }
        }
        string fUhrzeit;
        [Size(255)]
        public string Uhrzeit
        {
            get { return fUhrzeit; }
            set { SetPropertyValue<string>(nameof(Uhrzeit), ref fUhrzeit, value); }
        }
        decimal? fWertV;
        public decimal? WertV
        {
            get { return fWertV; }
            set { SetPropertyValue<decimal?>(nameof(WertV), ref fWertV, value); }
        }
        decimal? fWertN;
        public decimal? WertN
        {
            get { return fWertN; }
            set { SetPropertyValue<decimal?>(nameof(WertN), ref fWertN, value); }
        }
        decimal? fWertA;
        public decimal? WertA
        {
            get { return fWertA; }
            set { SetPropertyValue<decimal?>(nameof(WertA), ref fWertA, value); }
        }
        DateTime? fTSEin;
        public DateTime? TSEin
        {
            get { return fTSEin; }
            set { SetPropertyValue<DateTime?>(nameof(TSEin), ref fTSEin, value); }
        }
        DateTime? fTSAus;
        public DateTime? TSAus
        {
            get { return fTSAus; }
            set { SetPropertyValue<DateTime?>(nameof(TSAus), ref fTSAus, value); }
        }
        bool? fEiszeitenESHERSVormittag;
        public bool? EiszeitenESHERSVormittag
        {
            get { return fEiszeitenESHERSVormittag; }
            set { SetPropertyValue<bool?>(nameof(EiszeitenESHERSVormittag), ref fEiszeitenESHERSVormittag, value); }
        }
        bool? fEiszeitenESHERSAbend;
        public bool? EiszeitenESHERSAbend
        {
            get { return fEiszeitenESHERSAbend; }
            set { SetPropertyValue<bool?>(nameof(EiszeitenESHERSAbend), ref fEiszeitenESHERSAbend, value); }
        }
        decimal? fWertEiszeitenESHERS;
        public decimal? WertEiszeitenESHERS
        {
            get { return fWertEiszeitenESHERS; }
            set { SetPropertyValue<decimal?>(nameof(WertEiszeitenESHERS), ref fWertEiszeitenESHERS, value); }
        }

    }

}
