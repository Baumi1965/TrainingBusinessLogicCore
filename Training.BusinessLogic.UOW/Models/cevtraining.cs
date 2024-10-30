using DevExpress.Xpo;
namespace Training.BusinessLogic.UOW.Models
{

    public class cevtraining : XPLiteObject
    {
        public cevtraining(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        int? fTrNr;
        public int? TrNr
        {
            get { return fTrNr; }
            set { SetPropertyValue<int?>(nameof(TrNr), ref fTrNr, value); }
        }
        string fBezeichnung;
        [Size(255)]
        public string Bezeichnung
        {
            get { return fBezeichnung; }
            set { SetPropertyValue<string>(nameof(Bezeichnung), ref fBezeichnung, value); }
        }
        string fZeitVon1;
        [Size(255)]
        public string ZeitVon1
        {
            get { return fZeitVon1; }
            set { SetPropertyValue<string>(nameof(ZeitVon1), ref fZeitVon1, value); }
        }
        string fZeitBis1;
        [Size(255)]
        public string ZeitBis1
        {
            get { return fZeitBis1; }
            set { SetPropertyValue<string>(nameof(ZeitBis1), ref fZeitBis1, value); }
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
    }

}
