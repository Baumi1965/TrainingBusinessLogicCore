using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace Training.BusinessLogic.Spielstaetten.ModelsLokal
{
    [Persistent(@"spielstaetten")]
    public class spielstaettenlokal : XPLiteObject
    {
        public spielstaettenlokal(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fID;
        [Persistent("ID")]
        [Indexed(Unique = true)]
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
        [Key]
        public string Guid
        {
            get => fGuid;
            set => SetPropertyValue<string>(nameof(Guid), ref fGuid, value);
        }
        
        protected override void OnSaving()
        {
            if (!IsDeleted && ID == 0)
            {
                // Find the max ID and increment (Auto-increment workaround)
                var maxId = Session.Evaluate<spielstaettenlokal>(CriteriaOperator.Parse("Max(ID)"), null);
                ID = (maxId as int? ?? 0) + 1;
            }
            
            base.OnSaving();
        }
    }
}