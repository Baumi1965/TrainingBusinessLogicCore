using System;
using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.ModelsKassa
{
    public class sp: XPLiteObject
    {
        public sp(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fMandant;
        [Size(255)]
        public string Mandant
        {
            get { return fMandant; }
            set { SetPropertyValue<string>(nameof(Mandant), ref fMandant, value); }
        }
        string fSpName;
        [Size(255)]
        public string SpName
        {
            get { return fSpName; }
            set { SetPropertyValue<string>(nameof(SpName), ref fSpName, value); }
        }
        string fSp;
        [Size(2)]
        public string Sp
        {
            get { return fSp; }
            set { SetPropertyValue<string>(nameof(Sp), ref fSp, value); }
        }
        string fStrasse1;
        [Size(255)]
        public string Strasse1
        {
            get { return fStrasse1; }
            set { SetPropertyValue<string>(nameof(Strasse1), ref fStrasse1, value); }
        }
        string fStrasse2;
        [Size(255)]
        public string Strasse2
        {
            get { return fStrasse2; }
            set { SetPropertyValue<string>(nameof(Strasse2), ref fStrasse2, value); }
        }
        string fPLZ;
        [Size(10)]
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
        string fNation;
        [Size(255)]
        public string Nation
        {
            get { return fNation; }
            set { SetPropertyValue<string>(nameof(Nation), ref fNation, value); }
        }
        string fTel;
        [Size(255)]
        public string Tel
        {
            get { return fTel; }
            set { SetPropertyValue<string>(nameof(Tel), ref fTel, value); }
        }
        string fFax;
        [Size(255)]
        public string Fax
        {
            get { return fFax; }
            set { SetPropertyValue<string>(nameof(Fax), ref fFax, value); }
        }
        string feMail;
        [Size(255)]
        public string eMail
        {
            get { return feMail; }
            set { SetPropertyValue<string>(nameof(eMail), ref feMail, value); }
        }
        string fINet;
        [Size(255)]
        public string INet
        {
            get { return fINet; }
            set { SetPropertyValue<string>(nameof(INet), ref fINet, value); }
        }
        string fGeschZeit;
        [Size(255)]
        public string GeschZeit
        {
            get { return fGeschZeit; }
            set { SetPropertyValue<string>(nameof(GeschZeit), ref fGeschZeit, value); }
        }
        bool fAktiv;
        public bool Aktiv
        {
            get { return fAktiv; }
            set { SetPropertyValue<bool>(nameof(Aktiv), ref fAktiv, value); }
        }
        DateTime fSaisonbeginn;
        public DateTime Saisonbeginn
        {
            get { return fSaisonbeginn; }
            set { SetPropertyValue<DateTime>(nameof(Saisonbeginn), ref fSaisonbeginn, value); }
        }
        DateTime fSaisonende;
        public DateTime Saisonende
        {
            get { return fSaisonende; }
            set { SetPropertyValue<DateTime>(nameof(Saisonende), ref fSaisonende, value); }
        }
        int fHobexMandant;
        public int HobexMandant
        {
            get { return fHobexMandant; }
            set { SetPropertyValue<int>(nameof(HobexMandant), ref fHobexMandant, value); }
        }
    }
}