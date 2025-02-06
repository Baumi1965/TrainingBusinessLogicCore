using System;
using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{
    public class eklbegleitkarte :XPLiteObject
    {
        public eklbegleitkarte(Session session) : base(session) { }
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
        public string KdNr
        {
            get { return fKdNr; }
            set { SetPropertyValue<string>(nameof(KdNr), ref fKdNr, value); }
        }

        string fBarcode;
        [Size(45)]
        public string Barcode
        {
            get { return fBarcode; }
            set { SetPropertyValue<string>(nameof(Barcode), ref fBarcode, value); }
        }

        bool fParkplatz;
        public bool Parkplatz
        {
            get { return fParkplatz; }
            set { SetPropertyValue<bool>(nameof(Parkplatz), ref fParkplatz, value); }
        }

        DateTime? fTSAktivierung;
        public DateTime? TSAktivierung
        {
            get { return fTSAktivierung; }
            set { SetPropertyValue<DateTime?>(nameof(TSAktivierung), ref fTSAktivierung, value); }
        }

        DateTime? fTSEklEin;
        public DateTime? TSEklEin
        {
            get { return fTSEklEin; }
            set { SetPropertyValue<DateTime?>(nameof(TSEklEin), ref fTSEklEin, value); }
        }

        DateTime? fTSEklAus;
        public DateTime? TSEklAus
        {
            get { return fTSEklAus; }
            set { SetPropertyValue<DateTime?>(nameof(TSEklAus), ref fTSEklAus, value); }
        }

        int? fLocation;
        public int? Location
        {
            get { return fLocation; }
            set { SetPropertyValue<int?>(nameof(Location), ref fLocation, value); }
        }

        string fGuid;
        public string Guid
        {
            get { return fGuid; }
            set { SetPropertyValue<string>(nameof(Guid), ref fGuid, value); }
        }
    }
}