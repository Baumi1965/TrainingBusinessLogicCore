
using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace Training.BusinessLogic.Belegungsart.ModelsLokal
{
    [Persistent(@"belegungsarten")]
    public class belegungsartlokal : XPLiteObject
    {
        public belegungsartlokal(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fID;
        
        [Persistent("ID")]
        [Indexed(Unique = true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fBelegungsart;
        [Size(45)]
        [Nullable(false)]
        public string Belegungsart
        {
            get { return fBelegungsart; }
            set { SetPropertyValue<string>(nameof(Belegungsart), ref fBelegungsart, value); }
        }
        int? fFarbe;
        public int? Farbe
        {
            get { return fFarbe; }
            set { SetPropertyValue<int?>(nameof(Farbe), ref fFarbe, value); }
        }
        bool fBuchungssystem;
        public bool Buchungssystem
        {
            get { return fBuchungssystem; }
            set { SetPropertyValue<bool>(nameof(Buchungssystem), ref fBuchungssystem, value); }
        }
        int fKey;
        public int Key
        {
            get { return fKey; }
            set { SetPropertyValue<int>(nameof(Key), ref fKey, value); }
        }
        string fKurztext;
        [Size(10)]
        [Nullable(true)]
        public string Kurztext
        {
            get { return fKurztext; }
            set { SetPropertyValue<string>(nameof(Kurztext), ref fKurztext, value); }
        }
        bool fAbbuchen;
        public bool Abbuchen
        {
            get { return fAbbuchen; }
            set { SetPropertyValue<bool>(nameof(Abbuchen), ref fAbbuchen, value); }
        }
        bool fKassaFrequenzschein;
        public bool KassaFrequenzschein
        {
            get { return fKassaFrequenzschein; }
            set { SetPropertyValue<bool>(nameof(KassaFrequenzschein), ref fKassaFrequenzschein, value); }
        }
        
        string fGuid;
        [Key]
        public string Guid
        {
            get
            {
                return fGuid;
            }
            set
            {
                SetPropertyValue<string>(nameof(Guid), ref fGuid, value);
            }
        }
        
        protected override void OnSaving()
        {
            if (!IsDeleted && ID == 0)
            {
                // Find the max ID and increment (Auto-increment workaround)
                var maxId = Session.Evaluate<belegungsartlokal>(CriteriaOperator.Parse("Max(ID)"), null);
                ID = (maxId as int? ?? 0) + 1;
            }
            
            base.OnSaving();
        }
    }    
}
