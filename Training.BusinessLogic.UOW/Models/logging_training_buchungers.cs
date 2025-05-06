using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BusinessLogic.UOW.Models
{
    public class logging_training_buchungers : XPLiteObject
    {
        public logging_training_buchungers(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        long fId;
        [Key(true)]
        public long Id
        {
            get { return fId; }
            set { SetPropertyValue<long>(nameof(Id), ref fId, value); }
        }
        string fException;
        public string Exception
        {
            get { return fException; }
            set { SetPropertyValue<string>(nameof(Exception), ref fException, value); }
        }
        string fLogLevel;
        public string LogLevel
        {
            get { return fLogLevel; }
            set { SetPropertyValue<string>(nameof(LogLevel), ref fLogLevel, value); }
        }

        string fMessage;
        public string Message
        {
            get { return fMessage; }
            set { SetPropertyValue<string>(nameof(Message), ref fMessage, value); }
        }

        string fMessageTemplate;
        public string MessageTemplate
        {
            get { return fMessageTemplate; }
            set { SetPropertyValue<string>(nameof(MessageTemplate), ref fMessageTemplate, value); }
        }

        string fProperties;
        public string Properties
        {
            get { return fProperties; }
            set { SetPropertyValue<string>(nameof(Properties), ref fProperties, value); }
        }

        DateTime? fTimestamp;
        public DateTime? Timestamp
        {
            get { return fTimestamp; }
            set { SetPropertyValue<DateTime?>(nameof(Timestamp), ref fTimestamp, value); }
        }
    }
}
