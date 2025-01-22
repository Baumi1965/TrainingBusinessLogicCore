using DevExpress.Xpo;
using System;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterfmodell : XPLiteObject
	{
		public zeiterfmodell(Session session) : base(session) { }
		public override void AfterConstruction() { base.AfterConstruction(); }

		int fID;
		[Key(true)]
		public int ID
		{
			get { return fID; }
			set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
		}

		int? fAnzahlAutoPause;
		public int? AnzahlAutoPause
		{
			get { return fAnzahlAutoPause; }
			set { SetPropertyValue<int?>(nameof(AnzahlAutoPause), ref fAnzahlAutoPause, value); }
		}

		int? fDauerAutoPause;
		public int? DauerAutoPause
		{
			get { return fDauerAutoPause; }
			set { SetPropertyValue<int?>(nameof(DauerAutoPause), ref fDauerAutoPause, value); }
		}

		int? fHauptmodellID;
		public int? HauptmodellID
		{
			get { return fHauptmodellID; }
			set { SetPropertyValue<int?>(nameof(HauptmodellID), ref fHauptmodellID, value); }
		}

		double? fWochenstunden;
		public double? Wochenstunden
		{
			get { return fWochenstunden; }
			set { SetPropertyValue<double?>(nameof(Wochenstunden), ref fWochenstunden, value); }
		}

		bool? fMo;
		public bool? Mo
		{
			get { return fMo; }
			set { SetPropertyValue<bool?>(nameof(Mo), ref fMo, value); }
		}

		bool? fDi;
		public bool? Di
		{
			get { return fDi; }
			set { SetPropertyValue<bool?>(nameof(Di), ref fDi, value); }
		}

		bool? fMi;
		public bool? Mi
		{
			get { return fMi; }
			set { SetPropertyValue<bool?>(nameof(Mi), ref fMi, value); }
		}

		bool? fDo;
		public bool? Do
		{
			get { return fDo; }
			set { SetPropertyValue<bool?>(nameof(Do), ref fDo, value); }
		}

		bool? fFr;
		public bool? Fr
		{
			get { return fFr; }
			set { SetPropertyValue<bool?>(nameof(Fr), ref fFr, value); }
		}

		bool? fSa;
		public bool? Sa
		{
			get { return fSa; }
			set { SetPropertyValue<bool?>(nameof(Sa), ref fSa, value); }
		}

		bool? fSo;
		public bool? So
		{
			get { return fSo; }
			set { SetPropertyValue<bool?>(nameof(So), ref fSo, value); }
		}

		bool? fHauptmodell;
		public bool? Hauptmodell
		{
			get { return fHauptmodell; }
			set { SetPropertyValue<bool?>(nameof(Hauptmodell), ref fHauptmodell, value); }
		}

		bool? fSubmodell;
		public bool? Submodell
		{
			get { return fSubmodell; }
			set { SetPropertyValue<bool?>(nameof(Submodell), ref fSubmodell, value); }
		}

		DateTime? fGueltigVon;
		public DateTime? GueltigVon
		{	
			get { return fGueltigVon; }
			set { SetPropertyValue<DateTime?>(nameof(GueltigVon), ref fGueltigVon, value); }
		}

		DateTime? fGueltigBis;
		public DateTime? GueltigBis
		{
			get { return fGueltigBis; }
			set { SetPropertyValue<DateTime?>(nameof(GueltigBis), ref fGueltigBis, value); }
		}

		string fModell;
		[Size(45)]
		public string Modell
		{
			get { return fModell; }
			set { SetPropertyValue<string>(nameof(Modell), ref fModell, value); }
		}

		string fMoBeginn;
		[Size(5)]
		public string MoBeginn
		{
			get { return fMoBeginn; }
			set { SetPropertyValue<string>(nameof(MoBeginn), ref fMoBeginn, value); }
		}

		string fMoEnde;
		[Size(5)]
		public string MoEnde
		{
			get { return fMoEnde; }
			set { SetPropertyValue<string>(nameof(MoEnde), ref fMoEnde, value); }
		}

		string fDiBeginn;
		[Size(5)]
		public string DiBeginn
		{
			get { return fDiBeginn; }
			set { SetPropertyValue<string>(nameof(DiBeginn), ref fDiBeginn, value); }
		}

		string fDiEnde;
		[Size(5)]
		public string DiEnde
		{
			get { return fDiEnde; }
			set { SetPropertyValue<string>(nameof(DiEnde), ref fDiEnde, value); }
		}

		string fMiBeginn;
		[Size(5)]
		public string MiBeginn
		{
			get { return fMiBeginn; }
			set { SetPropertyValue<string>(nameof(MiBeginn), ref fMiBeginn, value); }
		}

		string fMiEnde;
		[Size(5)]
		public string MiEnde
		{
			get { return fMiEnde; }
			set { SetPropertyValue<string>(nameof(MiEnde), ref fMiEnde, value); }
		}

		string fDoBeginn;
		[Size(5)]
		public string DoBeginn
		{
			get { return fDoBeginn; }
			set { SetPropertyValue<string>(nameof(DoBeginn), ref fDoBeginn, value); }
		}

		string fDoEnde;
		[Size(5)]
		public string DoEnde
		{
			get { return fDoEnde; }
			set { SetPropertyValue<string>(nameof(DoEnde), ref fDoEnde, value); }
		}

		string fFrBeginn;
		[Size(5)]
		public string FrBeginn
		{
			get { return fFrBeginn; }
			set { SetPropertyValue<string>(nameof(FrBeginn), ref fFrBeginn, value); }
		}

		string fFrEnde;
		[Size(5)]
		public string FrEnde
		{
			get { return fFrEnde; }
			set { SetPropertyValue<string>(nameof(FrEnde), ref fFrEnde, value); }
		}

		string fSaBeginn;
		[Size(5)]
		public string SaBeginn
		{
			get { return fSaBeginn; }
			set { SetPropertyValue<string>(nameof(SaBeginn), ref fSaBeginn, value); }
		}

		string fSaEnde;
		[Size(5)]
		public string SaEnde
		{
			get { return fSaEnde; }
			set { SetPropertyValue<string>(nameof(SaEnde), ref fSaEnde, value); }
		}

		string fSoBeginn;
		[Size(5)]
		public string SoBeginn
		{
			get { return fSoBeginn; }
			set { SetPropertyValue<string>(nameof(SoBeginn), ref fSoBeginn, value); }
		}

		string fSoEnde;
		[Size(5)]
		public string SoEnde
		{
			get { return fSoEnde; }
			set { SetPropertyValue<string>(nameof(SoEnde), ref fSoEnde, value); }
		}

	}
}
