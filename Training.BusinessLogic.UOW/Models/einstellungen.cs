using DevExpress.Xpo;
using DevExpress.Xpo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BusinessLogic.UOW.Models
{
    public class einstellungen : XPLiteObject
    {
        public einstellungen(Session session) : base(session)
        {

        }

        int fID;
        [Key(true)]
        public int ID
        {
            get { return fID; }
            set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
        }

        string fSetting;
        public string Setting
        {
            get
            {
                return fSetting;
            }
            set
            {
                SetPropertyValue<string>(nameof(Setting), ref fSetting, value);
            }
        }

        string fValue;

        public string Value
        {
            get
            {
                return fValue;
            }
            set
            {
                SetPropertyValue<string>(nameof(Value), ref fValue, value);
            }
        }

        int fSpielstaette;
        public int Spielstaette
        {
            get { return fSpielstaette; }
            set { SetPropertyValue<int>(nameof(Spielstaette), ref fSpielstaette, value); }
        }

        string fCategory;
        [Size(45)]
        public string Category
        {
            get { return fCategory; }
            set { SetPropertyValue<string>(nameof(Category), ref fCategory, value); }
        }
        
        string fDescription;
        [Size(SizeAttribute.Unlimited)]
        public string Description
        {
            get { return fDescription; }
            set { SetPropertyValue<string>(nameof(Description), ref fDescription, value); }
        }
        
        string fType;
        [Size(45)]
        public string Type
        {
            get { return fType; }
            set { SetPropertyValue<string>(nameof(Type), ref fType, value); }
        }
        
        bool? fReadOnly;
        public bool? ReadOnly
        {
            get { return fReadOnly; }
            set { SetPropertyValue<bool?>(nameof(ReadOnly), ref fReadOnly, value); }
        }
    }
}
