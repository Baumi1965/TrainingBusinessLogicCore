using System;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace Training.BusinessLogic.Kunden.ModelsLokal
{
    [Persistent("eklbegleitkarteparken")]
    public class eklbegleitkarteparkenlokal : XPLiteObject
    {
        public eklbegleitkarteparkenlokal(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        int fID;
        [Persistent("ID")]
        [Indexed(Unique = true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }

        int? fEKLBegleitkarteEintrittID;
        public int? EKLBegleitkarteEintrittID
        {
            get { return fEKLBegleitkarteEintrittID; }
            set { SetPropertyValue<int?>(nameof(EKLBegleitkarteEintrittID), ref fEKLBegleitkarteEintrittID, value); }
        }

        DateTime fTSParken;
        public DateTime TSParken
        {
            get { return fTSParken; }
            set { SetPropertyValue<DateTime>(nameof(TSParken), ref fTSParken, value); }
        }

        bool fValid;
        public bool Valid
        {
            get { return fValid; }
            set { SetPropertyValue<bool>(nameof(Valid), ref fValid, value); }
        }

        string fMessage;
        [Size(255)]
        public string Message
        {
            get { return fMessage; }
            set { SetPropertyValue<string>(nameof(Message), ref fMessage, value); }
        }

        int fLocation;
        public int Location
        {
            get { return fLocation; }
            set { SetPropertyValue<int>(nameof(Location), ref fLocation, value); }
        }

        string fGuid;
        [Key]
        public string Guid
        {
            get { return fGuid; }
            set { SetPropertyValue<string>(nameof(Guid), ref fGuid, value); }
        }

        
        protected override void OnSaving()
        {
            if (!IsDeleted && ID == 0)
            {
                // Find the max ID and increment (Auto-increment workaround)
                var maxId = Session.Evaluate<eklbegleitkarteparkenlokal>(CriteriaOperator.Parse("Max(ID)"), null);
                ID = (maxId as int? ?? 0) + 1;
            }
            
            base.OnSaving();
        }
    }
}