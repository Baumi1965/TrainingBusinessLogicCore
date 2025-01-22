using DevExpress.Xpo;
using System;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterfnh3 : XPLiteObject
	{
		public zeiterfnh3(Session session) : base(session) { }
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

		int? fSP;
		public int? SP
		{
			get { return fSP; }
			set { SetPropertyValue<int?>(nameof(SP), ref fSP, value); }
		}

		DateTime? fDBTSKOMMT;
		public DateTime? DBTSKOMMT
		{
			get { return fDBTSKOMMT; }
			set { SetPropertyValue<DateTime?>(nameof(DBTSKOMMT), ref fDBTSKOMMT, value); }
		}

		DateTime? fDBTSGEHT;
		public DateTime? DBTSGEHT
		{
			get { return fDBTSGEHT; }
			set { SetPropertyValue<DateTime?>(nameof(DBTSGEHT), ref fDBTSGEHT, value); }
		}

		bool? fNH3;
		public bool? NH3
		{
			get { return fNH3; }
			set { SetPropertyValue<bool?>(nameof(NH3), ref fNH3, value); }
		}

		string fNH3Zeit;
		[Size(45)]
		public string NH3Zeit
		{
			get { return fNH3Zeit; }
			set { SetPropertyValue<string>(nameof(NH3Zeit), ref fNH3Zeit, value); }
		}

		int? fNH3Prozent;
		public int? NH3Prozent
		{
			get { return fNH3Prozent; }
			set { SetPropertyValue<int?>(nameof(NH3Prozent), ref fNH3Prozent, value); }
		}

		string fNH3Kommt;
		[Size(45)]
		public string NH3Kommt
		{
			get { return fNH3Kommt; }
			set { SetPropertyValue<string>(nameof(NH3Kommt), ref fNH3Kommt, value); }
		}

		string fNH3Geht;
		[Size(45)]
		public string NH3Geht
		{
			get { return fNH3Geht; }
			set { SetPropertyValue<string>(nameof(NH3Geht), ref fNH3Geht, value); }
		}

		string fEdituserID;
		[Size(45)]
		public string EdituserID
		{
			get { return fEdituserID; }
			set { SetPropertyValue<string>(nameof(EdituserID), ref fEdituserID, value); }
		}

		DateTime? fTSEdit;
		public DateTime? TSEdit
		{
			get { return fTSEdit; }
			set { SetPropertyValue<DateTime?>(nameof(TSEdit), ref fTSEdit, value); }
		}

		string fBemerkung;
		[Size(200)]
		public string Bemerkung
		{
			get { return fBemerkung; }
			set { SetPropertyValue<string>(nameof(Bemerkung), ref fBemerkung, value); }
		}
	}
}
