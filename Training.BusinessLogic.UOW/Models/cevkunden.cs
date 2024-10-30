using System;
using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{

    public class cevkunden : XPLiteObject
    {
        public cevkunden(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
            int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fMitgliedsnummer;
        [Size(45)]
        [Nullable(false)]
        public string Mitgliedsnummer
        {
            get { return fMitgliedsnummer; }
            set { SetPropertyValue<string>(nameof(Mitgliedsnummer), ref fMitgliedsnummer, value); }
        }
        string fNachname;
        [Size(45)]
        [Nullable(false)]
        public string Nachname
        {
            get { return fNachname; }
            set { SetPropertyValue<string>(nameof(Nachname), ref fNachname, value); }
        }
        string fVorname;
        [Size(45)]
        [Nullable(false)]
        public string Vorname
        {
            get { return fVorname; }
            set { SetPropertyValue<string>(nameof(Vorname), ref fVorname, value); }
        }
        string fGebDatum;
        [Size(45)]
        [Nullable(false)]
        public string GebDatum
        {
            get { return fGebDatum; }
            set { SetPropertyValue<string>(nameof(GebDatum), ref fGebDatum, value); }
        }
        string fAdresse;
        [Nullable(false)]
        public string Adresse
        {
            get { return fAdresse; }
            set { SetPropertyValue<string>(nameof(Adresse), ref fAdresse, value); }
        }
        string fLand;
        [Size(10)]
        [Nullable(false)]
        public string Land
        {
            get { return fLand; }
            set { SetPropertyValue<string>(nameof(Land), ref fLand, value); }
        }
        string fPLZ;
        [Size(10)]
        [Nullable(false)]
        public string PLZ
        {
            get { return fPLZ; }
            set { SetPropertyValue<string>(nameof(PLZ), ref fPLZ, value); }
        }
        string fOrt;
        [Nullable(false)]
        public string Ort
        {
            get { return fOrt; }
            set { SetPropertyValue<string>(nameof(Ort), ref fOrt, value); }
        }
        string fTel;
        [Size(45)]
        [Nullable(false)]
        public string Tel
        {
            get { return fTel; }
            set { SetPropertyValue<string>(nameof(Tel), ref fTel, value); }
        }
        string fTel2;
        [Size(45)]
        [Nullable(false)]
        public string Tel2
        {
            get { return fTel2; }
            set { SetPropertyValue<string>(nameof(Tel2), ref fTel2, value); }
        }
        string fEMail;
        [Nullable(false)]
        public string EMail
        {
            get { return fEMail; }
            set { SetPropertyValue<string>(nameof(EMail), ref fEMail, value); }
        }
        string fKaestchen;
        [Size(10)]
        [Nullable(false)]
        public string Kaestchen
        {
            get { return fKaestchen; }
            set { SetPropertyValue<string>(nameof(Kaestchen), ref fKaestchen, value); }
        }
        string fZusatzinfo;
        [Size(1000)]
        [Nullable(false)]
        public string Zusatzinfo
        {
            get { return fZusatzinfo; }
            set { SetPropertyValue<string>(nameof(Zusatzinfo), ref fZusatzinfo, value); }
        }
        bool fSaisonkarteEngelmann;
        public bool SaisonkarteEngelmann
        {
            get { return fSaisonkarteEngelmann; }
            set { SetPropertyValue<bool>(nameof(SaisonkarteEngelmann), ref fSaisonkarteEngelmann, value); }
        }
        bool fEhrenkarte;
        public bool Ehrenkarte
        {
            get { return fEhrenkarte; }
            set { SetPropertyValue<bool>(nameof(Ehrenkarte), ref fEhrenkarte, value); }
        }
        string fZusatzinfo2;
        [Size(1000)]
        [Nullable(false)]
        public string Zusatzinfo2
        {
            get { return fZusatzinfo2; }
            set { SetPropertyValue<string>(nameof(Zusatzinfo2), ref fZusatzinfo2, value); }
        }
        string fZusatzinfo3;
        [Size(1000)]
        [Nullable(false)]
        public string Zusatzinfo3
        {
            get { return fZusatzinfo3; }
            set { SetPropertyValue<string>(nameof(Zusatzinfo3), ref fZusatzinfo3, value); }
        }
        string fZusatzinfo4;
        [Size(1000)]
        [Nullable(false)]
        public string Zusatzinfo4
        {
            get { return fZusatzinfo4; }
            set { SetPropertyValue<string>(nameof(Zusatzinfo4), ref fZusatzinfo4, value); }
        }
        string fZusatzinfo5;
        [Size(1000)]
        [Nullable(false)]
        public string Zusatzinfo5
        {
            get { return fZusatzinfo5; }
            set { SetPropertyValue<string>(nameof(Zusatzinfo5), ref fZusatzinfo5, value); }
        }
        byte[] fFoto;
        [Size(SizeAttribute.Unlimited)]
        [MemberDesignTimeVisibility(true)]
        public byte[] Foto
        {
            get { return fFoto; }
            set { SetPropertyValue<byte[]>(nameof(Foto), ref fFoto, value); }
        }
        string fMitgliedsnummerAlt;
        [Size(45)]
        [ColumnDefaultValue("")]
        public string MitgliedsnummerAlt
        {
            get { return fMitgliedsnummerAlt; }
            set { SetPropertyValue<string>(nameof(MitgliedsnummerAlt), ref fMitgliedsnummerAlt, value); }
        }
        string fKundennummerEisring;
        [Size(45)]
        [Nullable(false)]
        public string KundennummerEisring
        {
            get { return fKundennummerEisring; }
            set { SetPropertyValue<string>(nameof(KundennummerEisring), ref fKundennummerEisring, value); }
        }
        bool fKundeEisring;
        public bool KundeEisring
        {
            get { return fKundeEisring; }
            set { SetPropertyValue<bool>(nameof(KundeEisring), ref fKundeEisring, value); }
        }
        string fTrainerNr;
        [Size(45)]
        [Nullable(false)]
        public string TrainerNr
        {
            get { return fTrainerNr; }
            set { SetPropertyValue<string>(nameof(TrainerNr), ref fTrainerNr, value); }
        }
        string fTrainer;
        [Size(45)]
        [Nullable(false)]
        public string Trainer
        {
            get { return fTrainer; }
            set { SetPropertyValue<string>(nameof(Trainer), ref fTrainer, value); }
        }
        string fMitgliedSeit;
        [Size(45)]
        public string MitgliedSeit
        {
            get { return fMitgliedSeit; }
            set { SetPropertyValue<string>(nameof(MitgliedSeit), ref fMitgliedSeit, value); }
        }
        bool fAbmeldung;
        public bool Abmeldung
        {
            get { return fAbmeldung; }
            set { SetPropertyValue<bool>(nameof(Abmeldung), ref fAbmeldung, value); }
        }
        bool fAnmeldung;
        public bool Anmeldung
        {
            get { return fAnmeldung; }
            set { SetPropertyValue<bool>(nameof(Anmeldung), ref fAnmeldung, value); }
        }
        string fAbmeldungMit;
        [Size(200)]
        public string AbmeldungMit
        {
            get { return fAbmeldungMit; }
            set { SetPropertyValue<string>(nameof(AbmeldungMit), ref fAbmeldungMit, value); }
        }
        string fZugehoerigkeit;
        [Size(10)]
        public string Zugehoerigkeit
        {
            get { return fZugehoerigkeit; }
            set { SetPropertyValue<string>(nameof(Zugehoerigkeit), ref fZugehoerigkeit, value); }
        }
        string fStaatsangehoerigkeit;
        [Size(45)]
        [ColumnDefaultValue("")]
        public string Staatsangehoerigkeit
        {
            get { return fStaatsangehoerigkeit; }
            set { SetPropertyValue<string>(nameof(Staatsangehoerigkeit), ref fStaatsangehoerigkeit, value); }
        }
        string fLizenznummer;
        [Size(45)]
        [Nullable(false)]
        public string Lizenznummer
        {
            get { return fLizenznummer; }
            set { SetPropertyValue<string>(nameof(Lizenznummer), ref fLizenznummer, value); }
        }
        string fIBAN;
        [Size(45)]
        public string IBAN
        {
            get { return fIBAN; }
            set { SetPropertyValue<string>(nameof(IBAN), ref fIBAN, value); }
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
        string fVerbandGueltigBis;
        [Size(45)]
        public string VerbandGueltigBis
        {
            get { return fVerbandGueltigBis; }
            set { SetPropertyValue<string>(nameof(VerbandGueltigBis), ref fVerbandGueltigBis, value); }
        }
        string fVerbandVerein;
        [Size(45)]
        public string VerbandVerein
        {
            get { return fVerbandVerein; }
            set { SetPropertyValue<string>(nameof(VerbandVerein), ref fVerbandVerein, value); }
        }
        string fTyp;
        [Size(45)]
        public string Typ
        {
            get { return fTyp; }
            set { SetPropertyValue<string>(nameof(Typ), ref fTyp, value); }
        }
        bool? fMBVerrechnung;
        public bool? MBVerrechnung
        {
            get { return fMBVerrechnung; }
            set { SetPropertyValue<bool?>(nameof(MBVerrechnung), ref fMBVerrechnung, value); }
        }
        int? fMBGebucht;
        public int? MBGebucht
        {
            get { return fMBGebucht; }
            set { SetPropertyValue<int?>(nameof(MBGebucht), ref fMBGebucht, value); }
        }
        bool? fDSGV;
        public bool? DSGV
        {
            get { return fDSGV; }
            set { SetPropertyValue<bool?>(nameof(DSGV), ref fDSGV, value); }
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
        bool? fNurKaestchen;
        public bool? NurKaestchen
        {
            get { return fNurKaestchen; }
            set { SetPropertyValue<bool?>(nameof(NurKaestchen), ref fNurKaestchen, value); }
        }
    }

}
