using System;
using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{
    public class sync_status : XPLiteObject
    {
        public sync_status(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        private int fID;
        [Key(true)]
        public int ID
        {
            get => fID;
            set => SetPropertyValue<int>(nameof(ID), ref fID, value);
        }

        private int fLocation;
        public int Location
        {
            get => fLocation;
            set => SetPropertyValue<int>(nameof(Location), ref fLocation, value);
        }
        
        private bool fFirstImport;
        public bool FirstImport
        {
            get => fFirstImport;
            set => SetPropertyValue<bool>(nameof(FirstImport), ref fFirstImport, value);
        }

        private DateTime fTS_FirstImport;
        public DateTime TS_FirstImport
        {
            get => fTS_FirstImport;
            set => SetPropertyValue<DateTime>(nameof(TS_FirstImport), ref fTS_FirstImport, value);
        }
        
        private DateTime? fTS_Import;
        public DateTime? TS_Import
        {
            get => fTS_Import;
            set => SetPropertyValue<DateTime?>(nameof(TS_Import), ref fTS_Import, value);
        }

    }
}