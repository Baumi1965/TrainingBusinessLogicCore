using DevExpress.Xpo;
using System;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterfmonatabschluss : XPLiteObject
	{
		public zeiterfmonatabschluss(Session session) : base(session) { }
		public override void AfterConstruction() { base.AfterConstruction(); }

		int fID;
		[Key(true)]
		public int ID
		{
			get { return fID; }
			set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
		}
		DateTime? fDatum;
		public DateTime? Datum
		{
			get { return fDatum; }
			set { SetPropertyValue<DateTime?>(nameof(Datum), ref fDatum, value); }
		}
		string fTag;
		[Size(45)]
		public string Tag
		{
			get { return fTag; }
			set { SetPropertyValue<string>(nameof(Tag), ref fTag, value); }
		}
		string fTaetigkeitsbereich;
		[Size(200)]
		public string Taetigkeitsbereich
		{
			get { return fTaetigkeitsbereich; }
			set { SetPropertyValue<string>(nameof(Taetigkeitsbereich), ref fTaetigkeitsbereich, value); }
		}
		string fSollstunden;
		[Size(45)]
		public string Sollstunden
		{
			get { return fSollstunden; }
			set { SetPropertyValue<string>(nameof(Sollstunden), ref fSollstunden, value); }
		}
		string fBeginn;
		[Size(45)]
		public string Beginn
		{
			get { return fBeginn; }
			set { SetPropertyValue<string>(nameof(Beginn), ref fBeginn, value); }
		}
		string fEnde;
		[Size(45)]
		public string Ende
		{
			get { return fEnde; }
			set { SetPropertyValue<string>(nameof(Ende), ref fEnde, value); }
		}
		string fPause;
		[Size(45)]
		public string Pause
		{
			get { return fPause; }
			set { SetPropertyValue<string>(nameof(Pause), ref fPause, value); }
		}
		string fIststunden;
		[Size(45)]
		public string Iststunden
		{
			get { return fIststunden; }
			set { SetPropertyValue<string>(nameof(Iststunden), ref fIststunden, value); }
		}
		string fAnwesenheit;
		[Size(45)]
		public string Anwesenheit
		{
			get { return fAnwesenheit; }
			set { SetPropertyValue<string>(nameof(Anwesenheit), ref fAnwesenheit, value); }
		}
		string fSaldo;
		[Size(45)]
		public string Saldo
		{
			get { return fSaldo; }
			set { SetPropertyValue<string>(nameof(Saldo), ref fSaldo, value); }
		}
		string fBemerkung;
		[Size(200)]
		public string Bemerkung
		{
			get { return fBemerkung; }
			set { SetPropertyValue<string>(nameof(Bemerkung), ref fBemerkung, value); }
		}
		string fDienstgang;
		[Size(45)]
		public string Dienstgang
		{
			get { return fDienstgang; }
			set { SetPropertyValue<string>(nameof(Dienstgang), ref fDienstgang, value); }
		}
		string fUrlaub;
		[Size(45)]
		public string Urlaub
		{
			get { return fUrlaub; }
			set { SetPropertyValue<string>(nameof(Urlaub), ref fUrlaub, value); }
		}
		short? fKrankenstand;
		public short? Krankenstand
		{
			get { return fKrankenstand; }
			set { SetPropertyValue<short?>(nameof(Krankenstand), ref fKrankenstand, value); }
		}
		short? fFrei;
		public short? Frei
		{
			get { return fFrei; }
			set { SetPropertyValue<short?>(nameof(Frei), ref fFrei, value); }
		}
		string fWochensaldo;
		[Size(45)]
		public string Wochensaldo
		{
			get { return fWochensaldo; }
			set { SetPropertyValue<string>(nameof(Wochensaldo), ref fWochensaldo, value); }
		}
		short? fFeiertag;
		public short? Feiertag
		{
			get { return fFeiertag; }
			set { SetPropertyValue<short?>(nameof(Feiertag), ref fFeiertag, value); }
		}
		short? fEdit;
		public short? Edit
		{
			get { return fEdit; }
			set { SetPropertyValue<short?>(nameof(Edit), ref fEdit, value); }
		}
		string fBeginnBuchung;
		[Size(45)]
		public string BeginnBuchung
		{
			get { return fBeginnBuchung; }
			set { SetPropertyValue<string>(nameof(BeginnBuchung), ref fBeginnBuchung, value); }
		}
		string fEndeBuchung;
		[Size(45)]
		public string EndeBuchung
		{
			get { return fEndeBuchung; }
			set { SetPropertyValue<string>(nameof(EndeBuchung), ref fEndeBuchung, value); }
		}
		string fGeaendertVon;
		[Size(45)]
		public string GeaendertVon
		{
			get { return fGeaendertVon; }
			set { SetPropertyValue<string>(nameof(GeaendertVon), ref fGeaendertVon, value); }
		}
		DateTime? fGeaendertAm;
		public DateTime? GeaendertAm
		{
			get { return fGeaendertAm; }
			set { SetPropertyValue<DateTime?>(nameof(GeaendertAm), ref fGeaendertAm, value); }
		}
		string fEngelmann;
		[Size(45)]
		public string Engelmann
		{
			get { return fEngelmann; }
			set { SetPropertyValue<string>(nameof(Engelmann), ref fEngelmann, value); }
		}
		string fERS;
		[Size(45)]
		public string ERS
		{
			get { return fERS; }
			set { SetPropertyValue<string>(nameof(ERS), ref fERS, value); }
		}
		string fESH;
		[Size(45)]
		public string ESH
		{
			get { return fESH; }
			set { SetPropertyValue<string>(nameof(ESH), ref fESH, value); }
		}
		string fMitarbeiterId;
		[Size(45)]
		public string MitarbeiterId
		{
			get { return fMitarbeiterId; }
			set { SetPropertyValue<string>(nameof(MitarbeiterId), ref fMitarbeiterId, value); }
		}
		DateTime? fDBTS;
		public DateTime? DBTS
		{
			get { return fDBTS; }
			set { SetPropertyValue<DateTime?>(nameof(DBTS), ref fDBTS, value); }
		}
		bool? fNH3;
		public bool? NH3
		{
			get { return fNH3; }
			set { SetPropertyValue<bool?>(nameof(NH3), ref fNH3, value); }
		}
		string fNH3Prozent;
		[Size(45)]
		public string NH3Prozent
		{
			get { return fNH3Prozent; }
			set { SetPropertyValue<string>(nameof(NH3Prozent), ref fNH3Prozent, value); }
		}
		string fNH3Zeit;
		[Size(45)]
		public string NH3Zeit
		{
			get { return fNH3Zeit; }
			set { SetPropertyValue<string>(nameof(NH3Zeit), ref fNH3Zeit, value); }
		}
	}
}
