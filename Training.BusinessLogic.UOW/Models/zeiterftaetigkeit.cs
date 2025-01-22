using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterftaetigkeit : XPLiteObject
	{
		public zeiterftaetigkeit(Session session) : base(session) { }
		public override void AfterConstruction() { base.AfterConstruction(); }

		int fID;
		[Key(true)]
		public int ID
		{
			get { return fID; }
			set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
		}
		
		string fTaetigkeit;
		[Size(200)]
		public string Taetigkeit
		{
			get { return fTaetigkeit; }
			set { SetPropertyValue<string>(nameof(Taetigkeit), ref fTaetigkeit, value); }
		}
	}
}
