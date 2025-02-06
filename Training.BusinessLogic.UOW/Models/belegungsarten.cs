using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{
    public class belegungsarten : XPLiteObject
    {
        public belegungsarten(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        int fID;
        [Key(true)]
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
        public string Guid
        {
            get { return fGuid; }
            set { SetPropertyValue<string>(nameof(Guid), ref fGuid, value); }
        }
    }
}