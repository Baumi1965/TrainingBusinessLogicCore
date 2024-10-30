using System;
using DevExpress.Xpo;
namespace Training.BusinessLogic.UOW.Models
{

    public class kevkunden : XPLiteObject
    {
        public kevkunden(Session session) : base(session) { }
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
        public string GebDatum
        {
            get { return fGebDatum; }
            set { SetPropertyValue<string>(nameof(GebDatum), ref fGebDatum, value); }
        }
        string fAdresse;
        public string Adresse
        {
            get { return fAdresse; }
            set { SetPropertyValue<string>(nameof(Adresse), ref fAdresse, value); }
        }
        string fLand;
        [Size(10)]
        public string Land
        {
            get { return fLand; }
            set { SetPropertyValue<string>(nameof(Land), ref fLand, value); }
        }
        string fPLZ;
        [Size(10)]
        public string PLZ
        {
            get { return fPLZ; }
            set { SetPropertyValue<string>(nameof(PLZ), ref fPLZ, value); }
        }
        string fOrt;
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
        string fEMail;
        [Nullable(false)]
        public string EMail
        {
            get { return fEMail; }
            set { SetPropertyValue<string>(nameof(EMail), ref fEMail, value); }
        }
        string fKaestchen;
        [Size(10)]
        public string Kaestchen
        {
            get { return fKaestchen; }
            set { SetPropertyValue<string>(nameof(Kaestchen), ref fKaestchen, value); }
        }
        string fZusatzinfo;
        [Size(1000)]
        public string Zusatzinfo
        {
            get { return fZusatzinfo; }
            set { SetPropertyValue<string>(nameof(Zusatzinfo), ref fZusatzinfo, value); }
        }
        bool? fHalbjahreskarte;
        public bool? Halbjahreskarte
        {
            get { return fHalbjahreskarte; }
            set { SetPropertyValue<bool?>(nameof(Halbjahreskarte), ref fHalbjahreskarte, value); }
        }
        bool? fEhrenkarte;
        public bool? Ehrenkarte
        {
            get { return fEhrenkarte; }
            set { SetPropertyValue<bool?>(nameof(Ehrenkarte), ref fEhrenkarte, value); }
        }
        bool? fOrdner;
        public bool? Ordner
        {
            get { return fOrdner; }
            set { SetPropertyValue<bool?>(nameof(Ordner), ref fOrdner, value); }
        }
        string fZusatzinfo2;
        [Size(1000)]
        public string Zusatzinfo2
        {
            get { return fZusatzinfo2; }
            set { SetPropertyValue<string>(nameof(Zusatzinfo2), ref fZusatzinfo2, value); }
        }
        string fZusatzinfo3;
        [Size(1000)]
        public string Zusatzinfo3
        {
            get { return fZusatzinfo3; }
            set { SetPropertyValue<string>(nameof(Zusatzinfo3), ref fZusatzinfo3, value); }
        }
        string fZusatzinfo4;
        [Size(1000)]
        public string Zusatzinfo4
        {
            get { return fZusatzinfo4; }
            set { SetPropertyValue<string>(nameof(Zusatzinfo4), ref fZusatzinfo4, value); }
        }
        string fZusatzinfo5;
        [Size(1000)]
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
        string fKundennummerEisring;
        [Size(45)]
        public string KundennummerEisring
        {
            get { return fKundennummerEisring; }
            set { SetPropertyValue<string>(nameof(KundennummerEisring), ref fKundennummerEisring, value); }
        }
        bool? fKundeEisring;
        public bool? KundeEisring
        {
            get { return fKundeEisring; }
            set { SetPropertyValue<bool?>(nameof(KundeEisring), ref fKundeEisring, value); }
        }
        string fMitgliedsnummerAlt;
        [Size(45)]
        public string MitgliedsnummerAlt
        {
            get { return fMitgliedsnummerAlt; }
            set { SetPropertyValue<string>(nameof(MitgliedsnummerAlt), ref fMitgliedsnummerAlt, value); }
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
        int? fSaison;
        public int? Saison
        {
            get { return fSaison; }
            set { SetPropertyValue<int?>(nameof(Saison), ref fSaison, value); }
        }

    }

}
