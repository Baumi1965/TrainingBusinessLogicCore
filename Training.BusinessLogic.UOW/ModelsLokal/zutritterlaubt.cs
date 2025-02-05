using System;
using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.ModelsLokal
{
    public class zutritterlaubt : XPLiteObject
    {
        public zutritterlaubt(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private int fID;
        [Key(true)]
        public int ID
        {
            get => fID;
            set => SetPropertyValue<int>(nameof(ID), ref fID, value);
        }
        
        private string fBarcode;
        public string Barcode
        {
            get => fBarcode;
            set => SetPropertyValue<string>(nameof(Barcode), ref fBarcode, value);
        }

        private string fGuid;
        public string Guid
        {
            get => fGuid;
            set => SetPropertyValue<string>(nameof(Guid), ref fGuid, value);
        }
        
        private bool fValid;
        public bool Valid
        {
            get => fValid;
            set => SetPropertyValue<bool>(nameof(Valid), ref fValid, value);
        }        
        
        private bool fEiskunstlaeufer;
        public bool Eiskunstlaeufer
        {
            get => fEiskunstlaeufer;
            set => SetPropertyValue<bool>(nameof(Eiskunstlaeufer), ref fEiskunstlaeufer, value);
        }    
        
        private bool fKassa;
        public bool Kassa
        {
            get => fKassa;
            set => SetPropertyValue<bool>(nameof(Kassa), ref fKassa, value);
        }    
        
        private bool fParkplatz;
        public bool Parkplatz
        {
            get => fParkplatz;
            set => SetPropertyValue<bool>(nameof(Parkplatz), ref fParkplatz, value);
        }    
        
        private bool fBegleitkarte;
        public bool Begleitkarte
        {
            get => fBegleitkarte;
            set => SetPropertyValue<bool>(nameof(Begleitkarte), ref fBegleitkarte, value);
        }
        
        private bool fBegleitkarteParkplatz;
        public bool BegleitkarteParkplatz
        {
            get => fBegleitkarteParkplatz;
            set => SetPropertyValue<bool>(nameof(BegleitkarteParkplatz), ref fBegleitkarteParkplatz, value);
        }    
        
        private DateTime fTSCreated;
        public DateTime TSCreated
        {
            get => fTSCreated;
            set => SetPropertyValue<DateTime>(nameof(TSCreated), ref fTSCreated, value);
        }   
        
        private DateTime fTSModified;
        public DateTime TSModified
        {
            get => fTSModified;
            set => SetPropertyValue<DateTime>(nameof(TSModified), ref fTSModified, value);
        }           
        
    }
}