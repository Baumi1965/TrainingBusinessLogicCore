using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterfmitarbeiterfinger : XPLiteObject
    {
        public zeiterfmitarbeiterfinger(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }

        int fMitarbeiterId;
        public int MitarbeiterId
        {
            get { return fMitarbeiterId; }
            set { SetPropertyValue<int>(nameof(MitarbeiterId), ref fMitarbeiterId, value); }
        }

        string fFinger;
        public string Finger
        {
            get { return fFinger; }
            set { SetPropertyValue<string>(nameof(Finger), ref fFinger, value); }
        }

        string fSecretKey;
        [Size(100)]
        public string SecretKey
        {
            get { return fSecretKey; }
            set { SetPropertyValue<string>(nameof(SecretKey), ref fSecretKey, value); }
        }

        string fFingerIndex;
        [Size(45)]            
        public string FingerIndex
        {
            get { return fFingerIndex; }
            set { SetPropertyValue<string>(nameof(FingerIndex), ref fFingerIndex, value); }
        }
    }
}
