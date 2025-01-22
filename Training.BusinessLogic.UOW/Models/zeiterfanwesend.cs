using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterfanwesend : XPLiteObject
    {
        public zeiterfanwesend(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }

        string fMitarbeiterId;
        [Size(45)]
        public string MitarbeiterId
        {
            get { return fMitarbeiterId; }
            set { SetPropertyValue<string>(nameof(MitarbeiterId), ref fMitarbeiterId, value); }
        }

        string fName;
        [Size(200)]
        public string Name
        {
            get { return fName; }
            set { SetPropertyValue<string>(nameof(Name), ref fName, value); }
        }

        DateTime fDatum;
        public DateTime Datum
        {
            get { return fDatum; }
            set { SetPropertyValue<DateTime>(nameof(Datum), ref fDatum, value); }
        }

        string fKommt;
        [Size(45)]
        public string Kommt
        {
            get { return fKommt; }
            set { SetPropertyValue<string>(nameof(Kommt), ref fKommt, value); }
        }

        int fSpielstaetteID;
        [Size(45)]
        public int SpielstaetteID
        {
            get { return fSpielstaetteID; }
            set { SetPropertyValue<int>(nameof(SpielstaetteID), ref fSpielstaetteID, value); }
        }

        string fSpielstaette;
        [Size(45)]
        public string Spielstaette
        {
            get { return fSpielstaette; }
            set { SetPropertyValue<string>(nameof(Spielstaette), ref fSpielstaette, value); }
        }
    }
}
