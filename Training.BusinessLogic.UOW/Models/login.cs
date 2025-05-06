using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Training.BusinessLogic.UOW.Models
{

    public partial class login : XPLiteObject
    {
        public login(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
              int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }
        string fBenutzername;
        [Size(45)]
        [Nullable(false)]
        public string Benutzername
        {
            get { return fBenutzername; }
            set { SetPropertyValue<string>(nameof(Benutzername), ref fBenutzername, value); }
        }
        string fPasswort;
        [Nullable(false)]
        public string Passwort
        {
            get { return fPasswort; }
            set { SetPropertyValue<string>(nameof(Passwort), ref fPasswort, value); }
        }
        bool fAdmin;
        public bool Admin
        {
            get { return fAdmin; }
            set { SetPropertyValue<bool>(nameof(Admin), ref fAdmin, value); }
        }
        bool fEismeister;
        public bool Eismeister
        {
            get { return fEismeister; }
            set { SetPropertyValue<bool>(nameof(Eismeister), ref fEismeister, value); }
        }
        bool fGesperrt;
        public bool Gesperrt
        {
            get { return fGesperrt; }
            set { SetPropertyValue<bool>(nameof(Gesperrt), ref fGesperrt, value); }
        }
        string fInfo1;
        [Size(45)]
        [Nullable(false)]
        public string Info1
        {
            get { return fInfo1; }
            set { SetPropertyValue<string>(nameof(Info1), ref fInfo1, value); }
        }
        string fInfo2;
        [Size(45)]
        [Nullable(false)]
        public string Info2
        {
            get { return fInfo2; }
            set { SetPropertyValue<string>(nameof(Info2), ref fInfo2, value); }
        }
        string fInfo3;
        [Size(45)]
        [Nullable(false)]
        public string Info3
        {
            get { return fInfo3; }
            set { SetPropertyValue<string>(nameof(Info3), ref fInfo3, value); }
        }
        string fInfo4;
        [Size(45)]
        [Nullable(false)]
        public string Info4
        {
            get { return fInfo4; }
            set { SetPropertyValue<string>(nameof(Info4), ref fInfo4, value); }
        }
        string fInfo5;
        [Size(45)]
        [Nullable(false)]
        public string Info5
        {
            get { return fInfo5; }
            set { SetPropertyValue<string>(nameof(Info5), ref fInfo5, value); }
        }
        string fInfo6;
        [Size(45)]
        public string Info6
        {
            get { return fInfo6; }
            set { SetPropertyValue<string>(nameof(Info6), ref fInfo6, value); }
        }
        string fInfo7;
        [Size(45)]
        public string Info7
        {
            get { return fInfo7; }
            set { SetPropertyValue<string>(nameof(Info7), ref fInfo7, value); }
        }
        string fInfo8;
        [Size(45)]
        public string Info8
        {
            get { return fInfo8; }
            set { SetPropertyValue<string>(nameof(Info8), ref fInfo8, value); }
        }
        string fInfo9;
        [Size(45)]
        public string Info9
        {
            get { return fInfo9; }
            set { SetPropertyValue<string>(nameof(Info9), ref fInfo9, value); }
        }
        string fInfo10;
        [Size(45)]
        public string Info10
        {
            get { return fInfo10; }
            set { SetPropertyValue<string>(nameof(Info10), ref fInfo10, value); }
        }
        bool? fEngelmann;
        public bool? Engelmann
        {
            get { return fEngelmann; }
            set { SetPropertyValue<bool?>(nameof(Engelmann), ref fEngelmann, value); }
        }
        bool? fDSGV;
        public bool? DSGV
        {
            get { return fDSGV; }
            set { SetPropertyValue<bool?>(nameof(DSGV), ref fDSGV, value); }
        }
        string fDSGVVerantwortlich;
        [Size(45)]
        public string DSGVVerantwortlich
        {
            get { return fDSGVVerantwortlich; }
            set { SetPropertyValue<string>(nameof(DSGVVerantwortlich), ref fDSGVVerantwortlich, value); }
        }
        bool? fParkenEisring;
        public bool? ParkenEisring
        {
            get { return fParkenEisring; }
            set { SetPropertyValue<bool?>(nameof(ParkenEisring), ref fParkenEisring, value); }
        }
        string fBarcodeParken;
        [Size(45)]
        public string BarcodeParken
        {
            get { return fBarcodeParken; }
            set { SetPropertyValue<string>(nameof(BarcodeParken), ref fBarcodeParken, value); }
        }

        string fGuid;
        [Size(45)]
        public string Guid
        {
            get { return fGuid; }
            set { SetPropertyValue<string>(nameof(Guid), ref fGuid, value); }
        }
    }

}
