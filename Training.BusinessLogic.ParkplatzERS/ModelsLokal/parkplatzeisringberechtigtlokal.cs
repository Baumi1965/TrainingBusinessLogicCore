using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace Training.BusinessLogic.ParkplatzERS.ModelsLokal
{
    [Persistent("parkplatzeisringberechtigt")]
    public class parkplatzeisringberechtigtlokal : XPLiteObject
    {
        public parkplatzeisringberechtigtlokal(Session session) : base(session) { }
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
        string fBarcode;
        [Indexed(Name = @"Barcode")]
        [Size(45)]
        [Nullable(false)]
        public string Barcode
        {
            get { return fBarcode; }
            set { SetPropertyValue<string>(nameof(Barcode), ref fBarcode, value); }
        }
        bool fBerechtigt;
        public bool Berechtigt
        {
            get { return fBerechtigt; }
            set { SetPropertyValue<bool>(nameof(Berechtigt), ref fBerechtigt, value); }
        }
        
        protected override void OnSaving()
        {
            if (!IsDeleted && ID == 0)
            {
                // Find the max ID and increment (Auto-increment workaround)
                var maxId = Session.Evaluate<parkplatzeisringberechtigtlokal>(CriteriaOperator.Parse("Max(ID)"), null);
                ID = (maxId as int? ?? 0) + 1;
            }
            
            base.OnSaving();
        }
    }
}