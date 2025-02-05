using System;
using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{
    public class sync : XPLiteObject
    {
        public sync(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        private int fID;
        [Key(true)]
        public int ID
        {
            get => fID;
            set => SetPropertyValue<int>(nameof(ID), ref fID, value);
        }

        private int fSpielstaetteId;
        public int SpielstaetteId
        {
            get => fSpielstaetteId;
            set => SetPropertyValue<int>(nameof(SpielstaetteId), ref fSpielstaetteId, value);
        }
        
        private string fGuid;
        public string Guid
        {
            get => fGuid;
            set => SetPropertyValue<string>(nameof(Guid), ref fGuid, value);
        }
        
        private string fAktion;
        public string Aktion
        {
            get => fAktion;
            set => SetPropertyValue<string>(nameof(Aktion), ref fAktion, value);
        }
        
        private string fTableName;
        public string TableName
        {
            get => fTableName;
            set => SetPropertyValue<string>(nameof(TableName), ref fTableName, value);
        }
        
        private string fDaten;
        public string Daten
        {
            get => fDaten;
            set => SetPropertyValue<string>(nameof(Daten), ref fDaten, value);
        }
        
        private DateTime fTSCreated;
        public DateTime TSCreated
        {
            get => fTSCreated;
            set => SetPropertyValue<DateTime>(nameof(TSCreated), ref fTSCreated, value);
        }   
        
        private DateTime? fTSStarted;
        public DateTime? TSStarted
        {
            get => fTSStarted;
            set => SetPropertyValue<DateTime?>(nameof(TSStarted), ref fTSStarted, value);
        }    
        
        private DateTime? fTSFinished;
        public DateTime? TSFinished
        {
            get => fTSFinished;
            set => SetPropertyValue<DateTime?>(nameof(TSFinished), ref fTSFinished, value);
        } 
        
        private string fErrorMessage;
        public string ErrorMessage
        {
            get => fErrorMessage;
            set => SetPropertyValue<string>(nameof(ErrorMessage), ref fErrorMessage, value);
        }
        
        private string fProperty;
        public string Property
        {
            get => fProperty;
            set => SetPropertyValue<string>(nameof(Property), ref fProperty, value);
        }
        
        
        private short fStatus;

        public short Status
        {
            get => fStatus;
            set => SetPropertyValue<short>(nameof(Status), ref fStatus, value);
        }
    }
}