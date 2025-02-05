using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace Training.BusinessLogic.Artikel.ModelsLokal
{
    [Persistent(@"artikel")]
    public class artikellokal : XPLiteObject
    {
        public artikellokal(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
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
        
        int fID;
        [Persistent("ID")]
        [Indexed(Unique = true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fArtNr;
        [Size(255)]
        public string ArtNr
        {
            get { return fArtNr; }
            set { SetPropertyValue<string>(nameof(ArtNr), ref fArtNr, value); }
        }
        string fBezeichnung1;
        [Size(255)]
        public string Bezeichnung1
        {
            get { return fBezeichnung1; }
            set { SetPropertyValue<string>(nameof(Bezeichnung1), ref fBezeichnung1, value); }
        }
        string fBezeichnung2;
        [Size(255)]
        public string Bezeichnung2
        {
            get { return fBezeichnung2; }
            set { SetPropertyValue<string>(nameof(Bezeichnung2), ref fBezeichnung2, value); }
        }
        decimal? fWert;
        [ColumnDefaultValue(0.00)]
        public decimal? Wert
        {
            get { return fWert; }
            set { SetPropertyValue<decimal?>(nameof(Wert), ref fWert, value); }
        }
        int? fMWSt;
        public int? MWSt
        {
            get { return fMWSt; }
            set { SetPropertyValue<int?>(nameof(MWSt), ref fMWSt, value); }
        }
        bool? fBlock;
        [ColumnDefaultValue(false)]
        public bool? Block
        {
            get { return fBlock; }
            set { SetPropertyValue<bool?>(nameof(Block), ref fBlock, value); }
        }
        string fKonto;
        [Size(10)]
        public string Konto
        {
            get { return fKonto; }
            set { SetPropertyValue<string>(nameof(Konto), ref fKonto, value); }
        }
        bool? fEismeister;
        public bool? Eismeister
        {
            get { return fEismeister; }
            set { SetPropertyValue<bool?>(nameof(Eismeister), ref fEismeister, value); }
        }
        string fVerkaufsgruppe;
        public string Verkaufsgruppe
        {
            get { return fVerkaufsgruppe; }
            set { SetPropertyValue<string>(nameof(Verkaufsgruppe), ref fVerkaufsgruppe, value); }
        }
        bool? fAutoUmbuchung;
        public bool? AutoUmbuchung
        {
            get { return fAutoUmbuchung; }
            set { SetPropertyValue<bool?>(nameof(AutoUmbuchung), ref fAutoUmbuchung, value); }
        }

        protected override void OnSaving()
        {
            if (!IsDeleted && ID == 0)
            {
                // Find the max ID and increment (Auto-increment workaround)
                var maxId = Session.Evaluate<artikellokal>(CriteriaOperator.Parse("Max(ID)"), null);
                ID = (maxId as int? ?? 0) + 1;
            }
            
            base.OnSaving();
        }
    }
}