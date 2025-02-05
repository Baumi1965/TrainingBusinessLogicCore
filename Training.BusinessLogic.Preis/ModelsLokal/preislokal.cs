using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace Training.BusinessLogic.Preis.ModelsLokal
{
    [Persistent("preise")]
    public class preislokal : XPLiteObject
    {
        public preislokal(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        
        int fID;
        [Persistent("ID")]
        [Indexed(Unique = true)]
		public int ID
		{
			get { return fID; }
			set { SetPropertyValue<int>(nameof(ID), ref fID, value); }
		}
		string fBezeichnung;
		[Nullable(false)]
		public string Bezeichnung
		{
			get { return fBezeichnung; }
			set { SetPropertyValue<string>(nameof(Bezeichnung), ref fBezeichnung, value); }
		}
		decimal fPreis;
		public decimal Preis
		{
			get { return fPreis; }
			set { SetPropertyValue<decimal>(nameof(Preis), ref fPreis, value); }
		}
		decimal? fPreisAbend;
		public decimal? PreisAbend
		{
			get { return fPreisAbend; }
			set { SetPropertyValue<decimal?>(nameof(PreisAbend), ref fPreisAbend, value); }
		}
		decimal? fPreisNachmittag;
		public decimal? PreisNachmittag
		{
			get { return fPreisNachmittag; }
			set { SetPropertyValue<decimal?>(nameof(PreisNachmittag), ref fPreisNachmittag, value); }
		}
		decimal? fPreisVormittag1;
		public decimal? PreisVormittag1
		{
			get { return fPreisVormittag1; }
			set { SetPropertyValue<decimal?>(nameof(PreisVormittag1), ref fPreisVormittag1, value); }
		}
		decimal? fPreisNachmittag1;
		public decimal? PreisNachmittag1
		{
			get { return fPreisNachmittag1; }
			set { SetPropertyValue<decimal?>(nameof(PreisNachmittag1), ref fPreisNachmittag1, value); }
		}
		decimal? fPreisAbend1;
		public decimal? PreisAbend1
		{
			get { return fPreisAbend1; }
			set { SetPropertyValue<decimal?>(nameof(PreisAbend1), ref fPreisAbend1, value); }
		}
		decimal? fPreisVormittag;
		public decimal? PreisVormittag
		{
			get { return fPreisVormittag; }
			set { SetPropertyValue<decimal?>(nameof(PreisVormittag), ref fPreisVormittag, value); }
		}
		int? fEinheit;
		public int? Einheit
		{
			get { return fEinheit; }
			set { SetPropertyValue<int?>(nameof(Einheit), ref fEinheit, value); }
		}
		double? fPreisEinheit;
		public double? PreisEinheit
		{
			get { return fPreisEinheit; }
			set { SetPropertyValue<double?>(nameof(PreisEinheit), ref fPreisEinheit, value); }
		}
		int? fVorlaufzeit;
		public int? Vorlaufzeit
		{
			get { return fVorlaufzeit; }
			set { SetPropertyValue<int?>(nameof(Vorlaufzeit), ref fVorlaufzeit, value); }
		}
		int? fNachlaufzeit;
		public int? Nachlaufzeit
		{
			get { return fNachlaufzeit; }
			set { SetPropertyValue<int?>(nameof(Nachlaufzeit), ref fNachlaufzeit, value); }
		}
		decimal? fPreisEiszeitenESHERS;
		public decimal? PreisEiszeitenESHERS
		{
			get { return fPreisEiszeitenESHERS; }
			set { SetPropertyValue<decimal?>(nameof(PreisEiszeitenESHERS), ref fPreisEiszeitenESHERS, value); }
		}

        string fGuid;
        [Key]
        public string Guid
        {
            get { return fGuid; }
            set { SetPropertyValue<string>(nameof(Guid), ref fGuid, value); }
        }

        protected override void OnSaving()
        {
	        if (!IsDeleted && ID == 0)
	        {
		        // Find the max ID and increment (Auto-increment workaround)
		        var maxId = Session.Evaluate<preislokal>(CriteriaOperator.Parse("Max(ID)"), null);
		        ID = (maxId as int? ?? 0) + 1;
	        }
            
	        base.OnSaving();
        }
    }    
}
