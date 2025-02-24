using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.ModelsKassa
{
    public class schulverzeichnis : XPLiteObject
    {
        public schulverzeichnis(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    
        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        
        private string fSKZ;
        public string SKZ
        {
            get { return fSKZ; }
            set { SetPropertyValue<string>(nameof(SKZ), ref fSKZ, value); }
        }
        
        private string fTyp1;
        
        public string Typ1
        {
            get { return fTyp1; }
            set { SetPropertyValue<string>(nameof(Typ1), ref fTyp1, value); }
        }
    }
}