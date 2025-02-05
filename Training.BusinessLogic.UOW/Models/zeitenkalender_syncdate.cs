using System;
using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeitenkalender_syncdate : XPLiteObject
    {
        public zeitenkalender_syncdate(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }

        int fSpielstaetteId;
        public int SpielstaetteId
        {
            get { return fSpielstaetteId; }
            set { SetPropertyValue<int>(nameof(SpielstaetteId), ref fSpielstaetteId, value); }
        }

        DateTime fSyncTimestamp;
        public DateTime SyncTimestamp
        {
            get { return fSyncTimestamp; }
            set { SetPropertyValue<DateTime>(nameof(SyncTimestamp), ref fSyncTimestamp, value); }
        }
    }
}