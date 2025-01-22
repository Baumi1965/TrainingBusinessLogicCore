using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterfurlaubza : XPLiteObject
	{
		public zeiterfurlaubza(Session session) : base(session) { }
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

		string fSaldo;
		[Size(45)]
		public string Saldo
		{
			get { return fSaldo; }
			set { SetPropertyValue<string>(nameof(Saldo), ref fSaldo, value); }
		}

		string fAusbezahlt;
		[Size(45)]
		public string Ausbezahlt
		{
			get { return fAusbezahlt; }
			set { SetPropertyValue<string>(nameof(Ausbezahlt), ref fAusbezahlt, value); }
		}

		string fBemerkung;
		[Size(200)]
		public string Bemerkung
		{
			get { return fBemerkung; }
			set { SetPropertyValue<string>(nameof(Bemerkung), ref fBemerkung, value); }
		}

		int? fMonat;
		public int? Monat
		{
			get { return fMonat; }
			set { SetPropertyValue<int?>(nameof(Monat), ref fMonat, value); }
		}
		int? fJahr;
		public int? Jahr
		{
			get { return fJahr; }
			set { SetPropertyValue<int?>(nameof(Jahr), ref fJahr, value); }
		}
		int? fUrlaub;
		public int? Urlaub
		{
			get { return fUrlaub; }
			set { SetPropertyValue<int?>(nameof(Urlaub), ref fUrlaub, value); }
		}
		bool? fAbschluss;
		public bool? Abschluss
		{
			get { return fAbschluss; }
			set { SetPropertyValue<bool?>(nameof(Abschluss), ref fAbschluss, value); }
		}
	}
}
