using DevExpress.Xpo;
using System;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterffrei : XPLiteObject
	{
		public zeiterffrei(Session session) : base(session) { }
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

		DateTime? fDatum;
		public DateTime? Datum
		{
			get { return fDatum; }
			set { SetPropertyValue<DateTime?>(nameof(Datum), ref fDatum, value); }
		}

		string fBemerkung;
		[Size(45)]
		public string Bemerkung
		{
			get { return fBemerkung; }
			set { SetPropertyValue<string>(nameof(Bemerkung), ref fBemerkung, value); }
		}
	}
}
