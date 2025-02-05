using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Training.BusinessLogic.UOW.Models
{

	public partial class zeitenkalender : XPLiteObject
	{
		public zeitenkalender(Session session) : base(session) { }
		public override void AfterConstruction() { base.AfterConstruction(); }
		
				int fID;
		[Key(true)]
		public int ID
		{
			get { return fID; }
			set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
		}
		int? fType;
		public int? Type
		{
			get { return fType; }
			set { SetPropertyValue<int?>(nameof(Type), ref fType, value); }
		}
		DateTime? fStartDatum;
		public DateTime? StartDatum
		{
			get { return fStartDatum; }
			set { SetPropertyValue<DateTime?>(nameof(StartDatum), ref fStartDatum, value); }
		}
		DateTime? fEndeDatum;
		public DateTime? EndeDatum
		{
			get { return fEndeDatum; }
			set { SetPropertyValue<DateTime?>(nameof(EndeDatum), ref fEndeDatum, value); }
		}
		bool? fGanztaegig;
		public bool? Ganztaegig
		{
			get { return fGanztaegig; }
			set { SetPropertyValue<bool?>(nameof(Ganztaegig), ref fGanztaegig, value); }
		}
		string fBetreff;
		[Size(45)]
		public string Betreff
		{
			get { return fBetreff; }
			set { SetPropertyValue<string>(nameof(Betreff), ref fBetreff, value); }
		}
		int? fSpielstaetteID;
		public int? SpielstaetteID
		{
			get { return fSpielstaetteID; }
			set { SetPropertyValue<int?>(nameof(SpielstaetteID), ref fSpielstaetteID, value); }
		}
		string fSpielstaette;
		[Size(45)]
		public string Spielstaette
		{
			get { return fSpielstaette; }
			set { SetPropertyValue<string>(nameof(Spielstaette), ref fSpielstaette, value); }
		}
		string fBeschreibung;
		[Size(SizeAttribute.Unlimited)]
		public string Beschreibung
		{
			get { return fBeschreibung; }
			set { SetPropertyValue<string>(nameof(Beschreibung), ref fBeschreibung, value); }
		}
		int? fStatus;
		public int? Status
		{
			get { return fStatus; }
			set { SetPropertyValue<int?>(nameof(Status), ref fStatus, value); }
		}
		int? fLabel;
		public int? Label
		{
			get { return fLabel; }
			set { SetPropertyValue<int?>(nameof(Label), ref fLabel, value); }
		}
		string fTrainingID;
		[Size(45)]
		public string TrainingID
		{
			get { return fTrainingID; }
			set { SetPropertyValue<string>(nameof(TrainingID), ref fTrainingID, value); }
		}
		string fTraining;
		[Size(45)]
		public string Training
		{
			get { return fTraining; }
			set { SetPropertyValue<string>(nameof(Training), ref fTraining, value); }
		}
		string fReminderInfo;
		[Size(SizeAttribute.Unlimited)]
		public string ReminderInfo
		{
			get { return fReminderInfo; }
			set { SetPropertyValue<string>(nameof(ReminderInfo), ref fReminderInfo, value); }
		}
		string fRecurrenceInfo;
		[Size(SizeAttribute.Unlimited)]
		public string RecurrenceInfo
		{
			get { return fRecurrenceInfo; }
			set { SetPropertyValue<string>(nameof(RecurrenceInfo), ref fRecurrenceInfo, value); }
		}
		string fZusatztext;
		[Size(SizeAttribute.Unlimited)]
		public string Zusatztext
		{
			get { return fZusatztext; }
			set { SetPropertyValue<string>(nameof(Zusatztext), ref fZusatztext, value); }
		}
		string fVerband;
		[Size(45)]
		public string Verband
		{
			get { return fVerband; }
			set { SetPropertyValue<string>(nameof(Verband), ref fVerband, value); }
		}
		string fAnsprechpartner;
		public string Ansprechpartner
		{
			get { return fAnsprechpartner; }
			set { SetPropertyValue<string>(nameof(Ansprechpartner), ref fAnsprechpartner, value); }
		}
		string fTel;
		[Size(45)]
		public string Tel
		{
			get { return fTel; }
			set { SetPropertyValue<string>(nameof(Tel), ref fTel, value); }
		}
		bool? fNichtBespielbar;
		public bool? NichtBespielbar
		{
			get { return fNichtBespielbar; }
			set { SetPropertyValue<bool?>(nameof(NichtBespielbar), ref fNichtBespielbar, value); }
		}
		string fFrequenz;
		[Size(45)]
		public string Frequenz
		{
			get { return fFrequenz; }
			set { SetPropertyValue<string>(nameof(Frequenz), ref fFrequenz, value); }
		}
		bool? fMatchuhr;
		public bool? Matchuhr
		{
			get { return fMatchuhr; }
			set { SetPropertyValue<bool?>(nameof(Matchuhr), ref fMatchuhr, value); }
		}
		bool? fExtraEis;
		public bool? ExtraEis
		{
			get { return fExtraEis; }
			set { SetPropertyValue<bool?>(nameof(ExtraEis), ref fExtraEis, value); }
		}
		bool fBuchungssystem;
		[ColumnDefaultValue(false)]
		public bool Buchungssystem
		{
			get { return fBuchungssystem; }
			set { SetPropertyValue<bool>(nameof(Buchungssystem), ref fBuchungssystem, value); }
		}

		DateTime? fTSChanged;
		public DateTime? TSChanged
		{
			get { return fTSChanged; }
			set { SetPropertyValue<DateTime?>(nameof(TSChanged), ref fTSChanged, value); }
		}

		string fUserChanged;
		[Size(45)]
		public string UserChanged
		{
			get { return fUserChanged; }
			set { SetPropertyValue<string>(nameof(UserChanged), ref fUserChanged, value); }
		}
		string fBenutzer;
		[Size(200)]
		public string Benutzer
		{
			get { return fBenutzer; }
			set { SetPropertyValue<string>(nameof(Benutzer), ref fBenutzer, value); }
		}

        string fAbgebuchterBetrag;
        [Size(45)]
        public string AbgebuchterBetrag
        {
            get { return fAbgebuchterBetrag; }
            set { SetPropertyValue<string>(nameof(AbgebuchterBetrag), ref fAbgebuchterBetrag, value); }
        }

	}

}
