using DevExpress.Xpo;
using Training.BusinessLogic.UOW;
using Training.BusinessLogic.UOW.Models;


namespace Training.BusinessLogic.Benutzer
{
    public class Benutzer
	{
        public int ID { get; set; }
        public string Benutzername { get; set; }
        public string Passwort { get; set; }
        public bool Admin { get; set; }
        public bool Eismeister { get; set; }
        public bool Gesperrt { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public string Info4 { get; set; }
        public string Info5 { get; set; }
        public string Info6 { get; set; }
        public string Info7 { get; set; }
        public string Info8 { get; set; }
        public string Info9 { get; set; }
        public string Info10 { get; set; }
        public bool? Engelmann { get; set; }
        public bool? DSGV { get; set; }
        public string DSGVVerantwortlich { get; set; }
        public bool? ParkenEisring { get; set; }
        public string BarcodeParken { get; set; }
        public Guid Guid { get; set; }

        public static async Task<int> CountAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var cnt = await UOW.Uow._uow.Query<login>().CountAsync();
                return cnt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Benutzer>> GetAsync()
        {
            try
            {
                if (Uow._uow == null || !Uow._uow.IsConnected)
                {
                    Uow.Connect();
                }

                var benutzer = await Uow._uow.Query<login>().ToListAsync();
                var lstBenutzer = new List<Benutzer>();
                foreach (var item in benutzer)
                {
                    var b = new Benutzer
                    {
                        Admin = item.Admin,
                        BarcodeParken = item.BarcodeParken,
                        Benutzername = item.Benutzername,
                        DSGV = item.DSGV,
                        DSGVVerantwortlich = item.DSGVVerantwortlich,
                        Eismeister = item.Eismeister,
                        Engelmann = item.Engelmann,
                        Gesperrt = item.Gesperrt,
                        ID = item.ID,
                        Info1 = item.Info1,
                        Info2 = item.Info2,
                        Info3 = item.Info3,
                        Info4 = item.Info4,
                        Info5 = item.Info5,
                        Info6 = item.Info6,
                        Info7 = item.Info7,
                        Info8 = item.Info8,
                        Info9 = item.Info9,
                        Info10 = item.Info10,
                        ParkenEisring = item.ParkenEisring,
                        Guid = new Guid(item.Guid),
                    };

                    var xe = new XCrypt.XCryptEngine(XCrypt.XCryptEngine.AlgorithmType.Rijndael)
                    {
                        Key = "10329474",
                    };
                    b.Passwort = xe.Encrypt(item.Passwort);

                    lstBenutzer.Add(b);
                }

                return lstBenutzer;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
