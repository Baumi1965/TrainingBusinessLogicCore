using DevExpress.Xpo;
using System;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterfbuchung : XPLiteObject
	{
		public zeiterfbuchung(Session session) : base(session) { }
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
		string fZeitKommt;
		[Size(45)]
		public string ZeitKommt
		{
			get { return fZeitKommt; }
			set { SetPropertyValue<string>(nameof(ZeitKommt), ref fZeitKommt, value); }
		}
		string fZeitGeht;
		[Size(45)]
		public string ZeitGeht
		{
			get { return fZeitGeht; }
			set { SetPropertyValue<string>(nameof(ZeitGeht), ref fZeitGeht, value); }
		}
		string fKennung;
		[Size(45)]
		public string Kennung
		{
			get { return fKennung; }
			set { SetPropertyValue<string>(nameof(Kennung), ref fKennung, value); }
		}
		string fGrund;
		[Size(45)]
		public string Grund
		{
			get { return fGrund; }
			set { SetPropertyValue<string>(nameof(Grund), ref fGrund, value); }
		}
		string fKommtBuchung;
		[Size(45)]
		public string KommtBuchung
		{
			get { return fKommtBuchung; }
			set { SetPropertyValue<string>(nameof(KommtBuchung), ref fKommtBuchung, value); }
		}
		string fGehtBuchung;
		[Size(45)]
		public string GehtBuchung
		{
			get { return fGehtBuchung; }
			set { SetPropertyValue<string>(nameof(GehtBuchung), ref fGehtBuchung, value); }
		}
		string fEdituserID;
		[Size(45)]
		public string EdituserID
		{
			get { return fEdituserID; }
			set { SetPropertyValue<string>(nameof(EdituserID), ref fEdituserID, value); }
		}
		string fBemerkung;
		[Size(255)]
		public string Bemerkung
		{
			get { return fBemerkung; }
			set { SetPropertyValue<string>(nameof(Bemerkung), ref fBemerkung, value); }
		}
		string fTaetigkeitsbereich;
		[Size(200)]
		public string Taetigkeitsbereich
		{
			get { return fTaetigkeitsbereich; }
			set { SetPropertyValue<string>(nameof(Taetigkeitsbereich), ref fTaetigkeitsbereich, value); }
		}
		string fNH3Zeit;
		[Size(45)]
		public string NH3Zeit
		{
			get { return fNH3Zeit; }
			set { SetPropertyValue<string>(nameof(NH3Zeit), ref fNH3Zeit, value); }
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

		int? fStatus;
		public int? Status
		{
			get { return fStatus; }
			set { SetPropertyValue<int?>(nameof(Status), ref fStatus, value); }
		}
		int? fArbeitszeitID;
		public int? ArbeitszeitID
		{
			get { return fArbeitszeitID; }
			set { SetPropertyValue<int?>(nameof(ArbeitszeitID), ref fArbeitszeitID, value); }
		}
		int? fSP;
		public int? SP
		{
			get { return fSP; }
			set { SetPropertyValue<int?>(nameof(SP), ref fSP, value); }
		}
		int? fNH3Prozent;
		public int? NH3Prozent
		{
			get { return fNH3Prozent; }
			set { SetPropertyValue<int?>(nameof(NH3Prozent), ref fNH3Prozent, value); }
		}

		bool? fEdit;
		public bool? Edit
		{
			get { return fEdit; }
			set { SetPropertyValue<bool?>(nameof(Edit), ref fEdit, value); }
		}
		bool? fAbschluss;
		public bool? Abschluss
		{
			get { return fAbschluss; }
			set { SetPropertyValue<bool?>(nameof(Abschluss), ref fAbschluss, value); }
		}
		bool? fNH3;
		public bool? NH3
		{
			get { return fNH3; }
			set { SetPropertyValue<bool?>(nameof(NH3), ref fNH3, value); }
		}

		DateTime? fTSEdit;
		public DateTime? TSEdit
		{
			get { return fTSEdit; }
			set { SetPropertyValue<DateTime?>(nameof(TSEdit), ref fTSEdit, value); }
		}
		DateTime? fTSGeaendert;
		public DateTime? TSGeaendert
		{
			get { return fTSGeaendert; }
			set { SetPropertyValue<DateTime?>(nameof(TSGeaendert), ref fTSGeaendert, value); }
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
	}
}
