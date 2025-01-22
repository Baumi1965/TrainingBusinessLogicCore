using DevExpress.Xpo;
using System;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterfmitarbeiter : XPLiteObject
	{
		public zeiterfmitarbeiter(Session session) : base(session) { }
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

		string fFinger;
		public string Finger
		{
			get { return fFinger; }
			set { SetPropertyValue<string>(nameof(Finger), ref fFinger, value); }
		}

		DateTime? fEintrittsdatum;
		public DateTime? Eintrittsdatum
		{
			get { return fEintrittsdatum; }
			set { SetPropertyValue<DateTime?>(nameof(Eintrittsdatum), ref fEintrittsdatum, value); }
		}

		DateTime? fAustrittsdatum;
		public DateTime? Austrittsdatum
		{
			get { return fAustrittsdatum; }
			set { SetPropertyValue<DateTime?>(nameof(Austrittsdatum), ref fAustrittsdatum, value); }
		}

		string fZAAktuell;
		[Size(45)]
		public string ZAAktuell
		{
			get { return fZAAktuell; }
			set { SetPropertyValue<string>(nameof(ZAAktuell), ref fZAAktuell, value); }
		}

		decimal? fUrlaubAktuell;
		public decimal? UrlaubAktuell
		{
			get { return fUrlaubAktuell; }
			set { SetPropertyValue<decimal?>(nameof(UrlaubAktuell), ref fUrlaubAktuell, value); }
		}

		DateTime? fDatumUrlaubNeu;
		public DateTime? DatumUrlaubNeu
		{
			get { return fDatumUrlaubNeu; }
			set { SetPropertyValue<DateTime?>(nameof(DatumUrlaubNeu), ref fDatumUrlaubNeu, value); }
		}

		int? fJahresurlaub;
		public int? Jahresurlaub
		{
			get { return fJahresurlaub; }
			set { SetPropertyValue<int?>(nameof(Jahresurlaub), ref fJahresurlaub, value); }
		}

		bool? fGesperrt;
		public bool? Gesperrt
		{
			get { return fGesperrt; }
			set { SetPropertyValue<bool?>(nameof(Gesperrt), ref fGesperrt, value); }
		}

		int? fZeitmodell;
		public int? Zeitmodell
		{
			get { return fZeitmodell; }
			set { SetPropertyValue<int?>(nameof(Zeitmodell), ref fZeitmodell, value); }
		}

		string fSecretKey;
		[Size(100)]
		public string SecretKey
		{
			get { return fSecretKey; }
			set { SetPropertyValue<string>(nameof(SecretKey), ref fSecretKey, value); }
		}

		int? fSpielstaetteID;
		public int? SpielstaetteID
		{
			get { return fSpielstaetteID; }
			set { SetPropertyValue<int?>(nameof(SpielstaetteID), ref fSpielstaetteID, value); }
		}

		string fSVNr;
		[Size(45)]
		public string SVNr
		{
			get { return fSVNr; }
			set { SetPropertyValue<string>(nameof(SVNr), ref fSVNr, value); }
		}

		DateTime? fGebDatum;
		public DateTime? GebDatum
		{
			get { return fGebDatum; }
			set { SetPropertyValue<DateTime?>(nameof(GebDatum), ref fGebDatum, value); }
		}

		string fGeschlecht;
		[Size(45)]
		public string Geschlecht
		{
			get { return fGeschlecht; }
			set { SetPropertyValue<string>(nameof(Geschlecht), ref fGeschlecht, value); }
		}

		string fTaetigkeit;
		[Size(45)]
		public string Taetigkeit
		{
			get { return fTaetigkeit; }
			set { SetPropertyValue<string>(nameof(Taetigkeit), ref fTaetigkeit, value); }
		}

		bool? fAngestellter;
		public bool? Angestellter
		{
			get { return fAngestellter; }
			set { SetPropertyValue<bool?>(nameof(Angestellter), ref fAngestellter, value); }
		}

		bool? fGeringfuegig;
		public bool? Geringfuegig
		{
			get { return fGeringfuegig; }
			set { SetPropertyValue<bool?>(nameof(Geringfuegig), ref fGeringfuegig, value); }
		}

		int? fWochenarbeitstage;
		public int? Wochenarbeitstage
		{
			get { return fWochenarbeitstage; }
			set { SetPropertyValue<int?>(nameof(Wochenarbeitstage), ref fWochenarbeitstage, value); }
		}

		double? fWochenstunden;
		public double? Wochenstunden
		{
			get { return fWochenstunden; }
			set { SetPropertyValue<double?>(nameof(Wochenstunden), ref fWochenstunden, value); }
		}

		string fUeberwiegendeTaetigkeit;
		[Size(200)]
		public string UeberwiegendeTaetigkeit
		{
			get { return fUeberwiegendeTaetigkeit; }
			set { SetPropertyValue<string>(nameof(UeberwiegendeTaetigkeit), ref fUeberwiegendeTaetigkeit, value); }
		}

		int? fDienstplanfarbe;
		public int? Dienstplanfarbe
		{
			get { return fDienstplanfarbe; }
			set { SetPropertyValue<int?>(nameof(Dienstplanfarbe), ref fDienstplanfarbe, value); }
		}

		bool? fAuszahlungsliste;
		public bool? Auszahlungsliste
		{
			get { return fAuszahlungsliste; }
			set { SetPropertyValue<bool?>(nameof(Auszahlungsliste), ref fAuszahlungsliste, value); }
		}

		bool? fNH3;
		public bool? NH3
		{
			get { return fNH3; }
			set { SetPropertyValue<bool?>(nameof(NH3), ref fNH3, value); }
		}

		bool? fNH3Plus;
		public bool? NH3Plus
		{
			get { return fNH3Plus; }
			set { SetPropertyValue<bool?>(nameof(NH3Plus), ref fNH3Plus, value); }
		}
	}
}
