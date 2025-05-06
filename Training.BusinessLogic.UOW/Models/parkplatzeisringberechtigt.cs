using System;
using DevExpress.Xpo;
namespace Training.BusinessLogic.UOW.Models
{

    public class parkplatzeisringberechtigt : XPLiteObject
    {
        public parkplatzeisringberechtigt(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fBarcode;
        [Indexed(Name = @"Barcode")]
        [Size(45)]
        [Nullable(false)]
        public string Barcode
        {
            get { return fBarcode; }
            set { SetPropertyValue<string>(nameof(Barcode), ref fBarcode, value); }
        }
        bool fBerechtigt;
        public bool Berechtigt
        {
            get { return fBerechtigt; }
            set { SetPropertyValue<bool>(nameof(Berechtigt), ref fBerechtigt, value); }
        }
        
        string fGuid;
        public string Guid
        {
            get { return fGuid; }
            set { SetPropertyValue<string>(nameof(Guid), ref fGuid, value); }
        }
    }

}
