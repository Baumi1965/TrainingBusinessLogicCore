using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{

    public class spielstaetten : XPLiteObject

    {
        public spielstaetten(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fBezeichnung;
        [Size(255)]
        public string Bezeichnung
        {
            get { return fBezeichnung; }
            set { SetPropertyValue<string>(nameof(Bezeichnung), ref fBezeichnung, value); }
        }
        string fAdresse1;
        [Size(255)]
        public string Adresse1
        {
            get { return fAdresse1; }
            set { SetPropertyValue<string>(nameof(Adresse1), ref fAdresse1, value); }
        }
        string fAdresse2;
        [Size(255)]
        public string Adresse2
        {
            get { return fAdresse2; }
            set { SetPropertyValue<string>(nameof(Adresse2), ref fAdresse2, value); }
        }
        string fPLZ;
        [Size(255)]
        public string PLZ
        {
            get { return fPLZ; }
            set { SetPropertyValue<string>(nameof(PLZ), ref fPLZ, value); }
        }
        string fOrt;
        [Size(255)]
        public string Ort
        {
            get { return fOrt; }
            set { SetPropertyValue<string>(nameof(Ort), ref fOrt, value); }
        }
        string fColor;
        [Size(45)]
        public string Color
        {
            get { return fColor; }
            set { SetPropertyValue<string>(nameof(Color), ref fColor, value); }
        }

        int? fBTSTicketId;
        public int? BTSTicketId
        {
            get { return fBTSTicketId; }
            set { SetPropertyValue<int?>(nameof(BTSTicketId), ref fBTSTicketId, value); }
        }
        
        private string fGuid;
        public string Guid
        {
            get => fGuid;
            set => SetPropertyValue<string>(nameof(Guid), ref fGuid, value);
        }
    }

}
