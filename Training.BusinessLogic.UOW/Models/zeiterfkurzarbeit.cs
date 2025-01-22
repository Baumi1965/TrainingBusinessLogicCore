using DevExpress.Xpo;
using System;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterfkurzarbeit : XPLiteObject
	{
		public zeiterfkurzarbeit(Session session) : base(session) { }
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

		string fSoll;
		[Size(45)]
		public string Soll
		{
			get { return fSoll; }
			set { SetPropertyValue<string>(nameof(Soll), ref fSoll, value); }
		}
	}
}
