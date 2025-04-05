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
         
        int? fAnzahlErw;
        public int? AnzahlErw
        {
            get { return fAnzahlErw; }
            set { SetPropertyValue<int?>(nameof(AnzahlErw), ref fAnzahlErw, value); }
        }

        int? fAnzahlJugend;
        public int? AnzahlJugend
        {
            get { return fAnzahlJugend; }
            set { SetPropertyValue<int?>(nameof(AnzahlJugend), ref fAnzahlJugend, value); }
        }

        private int? fAnzahlKinder;
        public int? AnzahlKinder
        {
            get { return fAnzahlKinder; }
            set { SetPropertyValue<int?>(nameof(AnzahlKinder), ref fAnzahlKinder, value); }
        }


    }
}