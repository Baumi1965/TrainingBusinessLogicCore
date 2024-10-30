using DevExpress.Xpo;
using System;
namespace Training.BusinessLogic.UOW.Models
{

    public class kunden : XPLiteObject
    {
        public kunden(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fKdNr;
        [Indexed(Name = @"KdNr")]
        [Size(255)]
        public string KdNr
        {
            get { return fKdNr; }
            set { SetPropertyValue<string>(nameof(KdNr), ref fKdNr, value); }
        }
        string fVerband;
        [Size(255)]
        public string Verband
        {
            get { return fVerband; }
            set { SetPropertyValue<string>(nameof(Verband), ref fVerband, value); }
        }
        string fEKLNr;
        [Size(255)]
        public string EKLNr
        {
            get { return fEKLNr; }
            set { SetPropertyValue<string>(nameof(EKLNr), ref fEKLNr, value); }
        }
        string fVName;
        [Size(255)]
        public string VName
        {
            get { return fVName; }
            set { SetPropertyValue<string>(nameof(VName), ref fVName, value); }
        }
        string fNName;
        [Size(255)]
        public string NName
        {
            get { return fNName; }
            set { SetPropertyValue<string>(nameof(NName), ref fNName, value); }
        }
        string fAdresse;
        [Size(255)]
        public string Adresse
        {
            get { return fAdresse; }
            set { SetPropertyValue<string>(nameof(Adresse), ref fAdresse, value); }
        }
        string fPLZ;
        [Size(255)]
        public string PLZ
        {
            get { return fPLZ; }
            set { SetPropertyValue<string>(nameof(PLZ), ref fPLZ, value); }
        }
        string fOrt;
        [Size(255)]
        public string Ort
        {
            get { return fOrt; }
            set { SetPropertyValue<string>(nameof(Ort), ref fOrt, value); }
        }
        string fTel;
        [Size(255)]
        public string Tel
        {
            get { return fTel; }
            set { SetPropertyValue<string>(nameof(Tel), ref fTel, value); }
        }
        string fEMail;
        [Size(255)]
        public string EMail
        {
            get { return fEMail; }
            set { SetPropertyValue<string>(nameof(EMail), ref fEMail, value); }
        }
        string fGeburtsdatum;
        [Size(255)]
        public string Geburtsdatum
        {
            get { return fGeburtsdatum; }
            set { SetPropertyValue<string>(nameof(Geburtsdatum), ref fGeburtsdatum, value); }
        }
        string fTyp;
        [Indexed(Name = @"Typ")]
        [Size(255)]
        public string Typ
        {
            get { return fTyp; }
            set { SetPropertyValue<string>(nameof(Typ), ref fTyp, value); }
        }
        decimal? fGuthaben;
        [ColumnDefaultValue(0.00)]
        public decimal? Guthaben
        {
            get { return fGuthaben; }
            set { SetPropertyValue<decimal?>(nameof(Guthaben), ref fGuthaben, value); }
        }
        bool? fBlock;
        [ColumnDefaultValue(false)]
        public bool? Block
        {
            get { return fBlock; }
            set { SetPropertyValue<bool?>(nameof(Block), ref fBlock, value); }
        }
        decimal? fWert;
        [ColumnDefaultValue(0.00)]
        public decimal? Wert
        {
            get { return fWert; }
            set { SetPropertyValue<decimal?>(nameof(Wert), ref fWert, value); }
        }
        bool? fSperre;
        [ColumnDefaultValue(false)]
        public bool? Sperre
        {
            get { return fSperre; }
            set { SetPropertyValue<bool?>(nameof(Sperre), ref fSperre, value); }
        }
        string fBegruendung;
        [Size(2000)]
        public string Begruendung
        {
            get { return fBegruendung; }
            set { SetPropertyValue<string>(nameof(Begruendung), ref fBegruendung, value); }
        }
        string fInfo1;
        [Size(1000)]
        public string Info1
        {
            get { return fInfo1; }
            set { SetPropertyValue<string>(nameof(Info1), ref fInfo1, value); }
        }
        string fInfo2;
        [Size(1000)]
        public string Info2
        {
            get { return fInfo2; }
            set { SetPropertyValue<string>(nameof(Info2), ref fInfo2, value); }
        }
        string fInfo3;
        [Size(1000)]
        public string Info3
        {
            get { return fInfo3; }
            set { SetPropertyValue<string>(nameof(Info3), ref fInfo3, value); }
        }
        string fInfo4;
        [Size(1000)]
        public string Info4
        {
            get { return fInfo4; }
            set { SetPropertyValue<string>(nameof(Info4), ref fInfo4, value); }
        }
        string fInfo5;
        [Size(1000)]
        public string Info5
        {
            get { return fInfo5; }
            set { SetPropertyValue<string>(nameof(Info5), ref fInfo5, value); }
        }
        string fKuerklasse;
        [Size(45)]
        public string Kuerklasse
        {
            get { return fKuerklasse; }
            set { SetPropertyValue<string>(nameof(Kuerklasse), ref fKuerklasse, value); }
        }
        bool? fFreigabeVerein;
        public bool? FreigabeVerein
        {
            get { return fFreigabeVerein; }
            set { SetPropertyValue<bool?>(nameof(FreigabeVerein), ref fFreigabeVerein, value); }
        }
        DateTime? fDatumFreigabe;
        public DateTime? DatumFreigabe
        {
            get { return fDatumFreigabe; }
            set { SetPropertyValue<DateTime?>(nameof(DatumFreigabe), ref fDatumFreigabe, value); }
        }
        string fFreigabeInfo1;
        [Size(255)]
        public string FreigabeInfo1
        {
            get { return fFreigabeInfo1; }
            set { SetPropertyValue<string>(nameof(FreigabeInfo1), ref fFreigabeInfo1, value); }
        }
        string fFreigabeInfo2;
        [Size(255)]
        public string FreigabeInfo2
        {
            get { return fFreigabeInfo2; }
            set { SetPropertyValue<string>(nameof(FreigabeInfo2), ref fFreigabeInfo2, value); }
        }
        string fFreigabeInfo3;
        [Size(255)]
        public string FreigabeInfo3
        {
            get { return fFreigabeInfo3; }
            set { SetPropertyValue<string>(nameof(FreigabeInfo3), ref fFreigabeInfo3, value); }
        }
        bool? fAktiv;
        public bool? Aktiv
        {
            get { return fAktiv; }
            set { SetPropertyValue<bool?>(nameof(Aktiv), ref fAktiv, value); }
        }
        string fPasswort;
        [Size(45)]
        [ColumnDefaultValue("")]
        public string Passwort
        {
            get { return fPasswort; }
            set { SetPropertyValue<string>(nameof(Passwort), ref fPasswort, value); }
        }
        string fTrainer;
        [Size(45)]
        public string Trainer
        {
            get { return fTrainer; }
            set { SetPropertyValue<string>(nameof(Trainer), ref fTrainer, value); }
        }
        string fIBAN;
        [Size(45)]
        public string IBAN
        {
            get { return fIBAN; }
            set { SetPropertyValue<string>(nameof(IBAN), ref fIBAN, value); }
        }
        int? fPreiseID;
        public int? PreiseID
        {
            get { return fPreiseID; }
            set { SetPropertyValue<int?>(nameof(PreiseID), ref fPreiseID, value); }
        }
        string fLizenzNr;
        [Size(45)]
        public string LizenzNr
        {
            get { return fLizenzNr; }
            set { SetPropertyValue<string>(nameof(LizenzNr), ref fLizenzNr, value); }
        }
        bool? fSperreVerein;
        public bool? SperreVerein
        {
            get { return fSperreVerein; }
            set { SetPropertyValue<bool?>(nameof(SperreVerein), ref fSperreVerein, value); }
        }
        string fVerbandNation;
        [Size(45)]
        public string VerbandNation
        {
            get { return fVerbandNation; }
            set { SetPropertyValue<string>(nameof(VerbandNation), ref fVerbandNation, value); }
        }
        string fVerbandLizenz;
        [Size(45)]
        public string VerbandLizenz
        {
            get { return fVerbandLizenz; }
            set { SetPropertyValue<string>(nameof(VerbandLizenz), ref fVerbandLizenz, value); }
        }
        DateTime? fVerbandGueltigBis;
        public DateTime? VerbandGueltigBis
        {
            get { return fVerbandGueltigBis; }
            set { SetPropertyValue<DateTime?>(nameof(VerbandGueltigBis), ref fVerbandGueltigBis, value); }
        }
        string fVerbandVerein;
        [Size(45)]
        public string VerbandVerein
        {
            get { return fVerbandVerein; }
            set { SetPropertyValue<string>(nameof(VerbandVerein), ref fVerbandVerein, value); }
        }
        string fSVNummer;
        [Size(45)]
        public string SVNummer
        {
            get { return fSVNummer; }
            set { SetPropertyValue<string>(nameof(SVNummer), ref fSVNummer, value); }
        }
        string fSteuernummer;
        [Size(45)]
        public string Steuernummer
        {
            get { return fSteuernummer; }
            set { SetPropertyValue<string>(nameof(Steuernummer), ref fSteuernummer, value); }
        }
        string fArbeitsbewilligung;
        [Size(200)]
        public string Arbeitsbewilligung
        {
            get { return fArbeitsbewilligung; }
            set { SetPropertyValue<string>(nameof(Arbeitsbewilligung), ref fArbeitsbewilligung, value); }
        }
        bool? fKaestchenEisring;
        public bool? KaestchenEisring
        {
            get { return fKaestchenEisring; }
            set { SetPropertyValue<bool?>(nameof(KaestchenEisring), ref fKaestchenEisring, value); }
        }
        bool? fKaestchenStadthalle;
        public bool? KaestchenStadthalle
        {
            get { return fKaestchenStadthalle; }
            set { SetPropertyValue<bool?>(nameof(KaestchenStadthalle), ref fKaestchenStadthalle, value); }
        }
        string fKaestchenEisringNr;
        [Size(45)]
        public string KaestchenEisringNr
        {
            get { return fKaestchenEisringNr; }
            set { SetPropertyValue<string>(nameof(KaestchenEisringNr), ref fKaestchenEisringNr, value); }
        }
        string fKaestchenStadthalleNr;
        [Size(45)]
        public string KaestchenStadthalleNr
        {
            get { return fKaestchenStadthalleNr; }
            set { SetPropertyValue<string>(nameof(KaestchenStadthalleNr), ref fKaestchenStadthalleNr, value); }
        }
        decimal? fWertAbend;
        [ColumnDefaultValue(0.00)]
        public decimal? WertAbend
        {
            get { return fWertAbend; }
            set { SetPropertyValue<decimal?>(nameof(WertAbend), ref fWertAbend, value); }
        }
        bool? fDSGVO;
        public bool? DSGVO
        {
            get { return fDSGVO; }
            set { SetPropertyValue<bool?>(nameof(DSGVO), ref fDSGVO, value); }
        }
        DateTime? fTS_DSGV;
        public DateTime? TS_DSGV
        {
            get { return fTS_DSGV; }
            set { SetPropertyValue<DateTime?>(nameof(TS_DSGV), ref fTS_DSGV, value); }
        }
        string fUser_DSGV;
        [Size(45)]
        public string User_DSGV
        {
            get { return fUser_DSGV; }
            set { SetPropertyValue<string>(nameof(User_DSGV), ref fUser_DSGV, value); }
        }
        bool? fKarte;
        public bool? Karte
        {
            get { return fKarte; }
            set { SetPropertyValue<bool?>(nameof(Karte), ref fKarte, value); }
        }
        string fGeschlecht;
        [Size(1)]
        public string Geschlecht
        {
            get { return fGeschlecht; }
            set { SetPropertyValue<string>(nameof(Geschlecht), ref fGeschlecht, value); }
        }
        bool? fCOVIDOK;
        public bool? COVIDOK
        {
            get { return fCOVIDOK; }
            set { SetPropertyValue<bool?>(nameof(COVIDOK), ref fCOVIDOK, value); }
        }
        bool? fParkplatzEisring;
        public bool? ParkplatzEisring
        {
            get { return fParkplatzEisring; }
            set { SetPropertyValue<bool?>(nameof(ParkplatzEisring), ref fParkplatzEisring, value); }
        }
        bool? fMahnungsmail;
        public bool? Mahnungsmail
        {
            get { return fMahnungsmail; }
            set { SetPropertyValue<bool?>(nameof(Mahnungsmail), ref fMahnungsmail, value); }
        }
        DateTime? fDatumMahnungsmail;
        public DateTime? DatumMahnungsmail
        {
            get { return fDatumMahnungsmail; }
            set { SetPropertyValue<DateTime?>(nameof(DatumMahnungsmail), ref fDatumMahnungsmail, value); }
        }
        string fTrainerLizenz;
        [Size(45)]
        public string TrainerLizenz
        {
            get { return fTrainerLizenz; }
            set { SetPropertyValue<string>(nameof(TrainerLizenz), ref fTrainerLizenz, value); }
        }
        bool? fEiszeitenESHERS;
        public bool? EiszeitenESHERS
        {
            get { return fEiszeitenESHERS; }
            set { SetPropertyValue<bool?>(nameof(EiszeitenESHERS), ref fEiszeitenESHERS, value); }
        }
    }
}
