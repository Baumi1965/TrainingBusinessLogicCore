using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.ModelsKassa
{
    public class artikel : XPLiteObject
    {
        public artikel(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }

        private string fSP;

        public string SP
        {
            get { return fSP; } 
            set { SetPropertyValue<string>(nameof(SP), ref fSP, value); }
        }

        private string fArtikel;

        public string Artikel
        {
            get { return fArtikel; } 
            set { SetPropertyValue<string>(nameof(Artikel), ref fArtikel, value); }
        }
        
        private bool? fTrainingZeiten;

        public bool? TrainingZeiten
        {
            get { return fTrainingZeiten; } 
            set { SetPropertyValue<bool?>(nameof(TrainingZeiten), ref fTrainingZeiten, value); }
        }

        

    }
}