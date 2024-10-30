using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BusinessLogic.UOW.Models
{
    public class cevpreise :XPLiteObject
    {
        public cevpreise(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }

        int fSaison;
        public int Saison
        {
            get { return fSaison; }
            set { SetPropertyValue<int>(nameof(Saison), ref fSaison, value); }
        }

        decimal fMB;
        public decimal MB
        {
            get { return fMB; }
            set { SetPropertyValue<decimal>(nameof(MB), ref fMB, value); }
        }

        decimal fSK;
        public decimal SK
        {
            get { return fSK; }
            set { SetPropertyValue<decimal>(nameof(SK), ref fSK, value); }
        }

        decimal fMBAdult;
        public decimal MBAdult
        {
            get { return fMBAdult; }
            set { SetPropertyValue<decimal>(nameof(MBAdult), ref fMBAdult, value); }
        }

        decimal fMBTeil;
        public decimal MBTeil
        {
            get { return fMBTeil; }
            set { SetPropertyValue<decimal>(nameof(MBTeil), ref fMBTeil, value); }
        }

        decimal fMBGast;
        public decimal MBGast
        {
            get { return fMBGast; }
            set { SetPropertyValue<decimal>(nameof(MBGast), ref fMBGast, value); }
        }

        decimal fSKAdult;
        public decimal SKAdult
        {
            get { return fSKAdult; }
            set { SetPropertyValue<decimal>(nameof(SKAdult), ref fSKAdult, value); }
        }

        decimal fSKTeil;
        public decimal SKTeil
        {
            get { return fSKTeil; }
            set { SetPropertyValue<decimal>(nameof(SKTeil), ref fSKTeil, value); }
        }
    }
}
