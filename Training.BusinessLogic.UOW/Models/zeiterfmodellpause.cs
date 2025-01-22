using DevExpress.Xpo;

namespace Training.BusinessLogic.UOW.Models
{
    public class zeiterfmodellpause : XPLiteObject
	{
		public zeiterfmodellpause(Session session) : base(session) { }
		public override void AfterConstruction() { base.AfterConstruction(); }

		int fID;
		[Key(true)]
		public int ID
		{
			get { return fID; }
			set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
		}
		
		int? fModellID;
		public int? ModellID
		{
			get { return fModellID; }
			set { SetPropertyValue<int?>(nameof(ModellID), ref fModellID, value); }
		}

		string fPauseStart;
		[Size(5)]
		public string PauseStart
		{
			get { return fPauseStart; }
			set { SetPropertyValue<string>(nameof(PauseStart), ref fPauseStart, value); }
		}
	}
}
