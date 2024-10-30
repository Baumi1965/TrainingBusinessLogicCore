using DevExpress.Xpo;
using System;

namespace Training.BusinessLogic.UOW.Models
{
    public class cevverkauf : XPLiteObject
    {
        public cevverkauf(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fMitgliedsnummer;
        [Size(45)]
        [Nullable(false)]
        public string Mitgliedsnummer
        {
            get { return fMitgliedsnummer; }
            set { SetPropertyValue<string>(nameof(Mitgliedsnummer), ref fMitgliedsnummer, value); }
        }
        int fSaison;
        public int Saison
        {
            get { return fSaison; }
            set { SetPropertyValue<int>(nameof(Saison), ref fSaison, value); }
        }
        decimal fCEVBetrag;
        public decimal CEVBetrag
        {
            get { return fCEVBetrag ; }
            set { SetPropertyValue<decimal>(nameof(CEVBetrag), ref fCEVBetrag, value); }
        }
        bool fSaisonkarteEngelmann;
        public bool SaisonkarteEngelmann
        {
            get { return fSaisonkarteEngelmann; }
            set { SetPropertyValue<bool>(nameof(SaisonkarteEngelmann), ref fSaisonkarteEngelmann, value); }
        }
        decimal fEngelmannBetrag;
        public decimal EngelmannBetrag
        {
            get { return fEngelmannBetrag; }
            set { SetPropertyValue<decimal>(nameof(EngelmannBetrag), ref fEngelmannBetrag, value); }
        }
        bool fBezahltCEV;
        public bool BezahltCEV
        {
            get { return fBezahltCEV; }
            set { SetPropertyValue<bool>(nameof(BezahltCEV), ref fBezahltCEV, value); }
        }
        bool fBezahltEngelmann;
        public bool BezahltEngelmann
        {
            get { return fBezahltEngelmann; }
            set { SetPropertyValue<bool>(nameof(BezahltEngelmann), ref fBezahltEngelmann, value); }
        }
        DateTime? fDatumBezahltCEV;
        public DateTime? DatumBezahltCEV
        {
            get { return fDatumBezahltCEV; }
            set { SetPropertyValue<DateTime?>(nameof(DatumBezahltCEV), ref fDatumBezahltCEV, value); }
        }
        DateTime? fDatumBezahltEngelmann;
        public DateTime? DatumBezahltEngelmann
        {
            get { return fDatumBezahltEngelmann; }
            set { SetPropertyValue<DateTime?>(nameof(DatumBezahltEngelmann), ref fDatumBezahltEngelmann, value); }
        }
        bool fTeilbetrag;
        public bool Teilbetrag
        {
            get { return fTeilbetrag; }
            set { SetPropertyValue<bool>(nameof(Teilbetrag), ref fTeilbetrag, value); }
        }
        bool fAdult;
        public bool Adult
        {
            get { return fAdult; }
            set { SetPropertyValue<bool>(nameof(Adult), ref fAdult, value); }
        }
        decimal fCEVBetragBezahlt;
        public decimal CEVBetragBezahlt
        {
            get { return fCEVBetragBezahlt; }
            set { SetPropertyValue<decimal>(nameof(CEVBetragBezahlt), ref fCEVBetragBezahlt, value); }
        }
        decimal fEngelmannBetragBezahlt;
        public decimal EngelmannBetragBezahlt
        {
            get { return fEngelmannBetragBezahlt; }
            set { SetPropertyValue<decimal>(nameof(EngelmannBetragBezahlt), ref fEngelmannBetragBezahlt, value); }
        }
        bool? fGast;
        public bool? Gast
        {
            get { return fGast; }
            set { SetPropertyValue<bool?>(nameof(Gast), ref fGast, value); }
        }
        bool? fErlagschein;
        public bool? Erlagschein
        {
            get { return fErlagschein; }
            set { SetPropertyValue<bool?>(nameof(Erlagschein), ref fErlagschein, value); }
        }
        bool? fMail;
        public bool? Mail
        {
            get { return fMail; }
            set { SetPropertyValue<bool?>(nameof(Mail), ref fMail, value); }
        }
    }
}
