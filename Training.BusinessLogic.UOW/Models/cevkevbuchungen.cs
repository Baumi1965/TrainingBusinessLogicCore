using System;
using DevExpress.Xpo;
namespace Training.BusinessLogic.UOW.Models
{

    public class cevkevbuchungen : XPLiteObject
    {
        public cevkevbuchungen(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
                int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fKdNr;
        [Size(45)]
        [Nullable(false)]
        public string KdNr
        {
            get { return fKdNr; }
            set { SetPropertyValue<string>(nameof(KdNr), ref fKdNr, value); }
        }
        bool fCEV;
        public bool CEV
        {
            get { return fCEV; }
            set { SetPropertyValue<bool>(nameof(CEV), ref fCEV, value); }
        }
        bool fKEV;
        public bool KEV
        {
            get { return fKEV; }
            set { SetPropertyValue<bool>(nameof(KEV), ref fKEV, value); }
        }
        string fVorname;
        [Size(45)]
        [Nullable(false)]
        public string Vorname
        {
            get { return fVorname; }
            set { SetPropertyValue<string>(nameof(Vorname), ref fVorname, value); }
        }
        string fNachname;
        [Size(45)]
        [Nullable(false)]
        public string Nachname
        {
            get { return fNachname; }
            set { SetPropertyValue<string>(nameof(Nachname), ref fNachname, value); }
        }
        DateTime fDatum;
        public DateTime Datum
        {
            get { return fDatum; }
            set { SetPropertyValue<DateTime>(nameof(Datum), ref fDatum, value); }
        }
        string fZeit;
        [Size(10)]
        [Nullable(false)]
        public string Zeit
        {
            get { return fZeit; }
            set { SetPropertyValue<string>(nameof(Zeit), ref fZeit, value); }
        }
        bool? fVormittag;
        public bool? Vormittag
        {
            get { return fVormittag; }
            set { SetPropertyValue<bool?>(nameof(Vormittag), ref fVormittag, value); }
        }
        bool? fNachmittag;
        public bool? Nachmittag
        {
            get { return fNachmittag; }
            set { SetPropertyValue<bool?>(nameof(Nachmittag), ref fNachmittag, value); }
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

    }

}
