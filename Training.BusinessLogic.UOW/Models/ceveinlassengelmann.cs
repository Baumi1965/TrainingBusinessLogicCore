using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{
    public class ceveinlassengelmann : XPLiteObject
    {
        public ceveinlassengelmann(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fWeekday;
        [Size(45)]
        [Nullable(false)]
        public string Weekday
        {
            get { return fWeekday; }
            set { SetPropertyValue<string>(nameof(Weekday), ref fWeekday, value); }
        }
        string fTimeFrom;
        [Size(45)]
        [Nullable(false)]
        public string TimeFrom
        {
            get { return fTimeFrom; }
            set { SetPropertyValue<string>(nameof(TimeFrom), ref fTimeFrom, value); }
        }
        string fTimeTill;
        [Size(45)]
        [Nullable(false)]
        public string TimeTill
        {
            get { return fTimeTill; }
            set { SetPropertyValue<string>(nameof(TimeTill), ref fTimeTill, value); }
        }

        bool fActive;
        public bool Active
        {
            get { return fActive; }
            set { SetPropertyValue<bool>(nameof(Active), ref fActive, value); }
        }
        int fWeekdayNr;
        public int WeekdayNr
        {
            get { return fWeekdayNr; }
            set { SetPropertyValue<int>(nameof(WeekdayNr), ref fWeekdayNr, value); }
        }
    }
}
