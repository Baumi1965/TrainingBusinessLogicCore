using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Artikel
{
    public class Artikel
    {
        public int ID { get; set; }
        public string ArtNr { get; set; }
        public string Bezeichnung1 { get; set; }
        public string Bezeichnung2 { get; set; }
        public decimal? Wert { get; set; }
        public int? MWSt { get; set; }
        public bool? Block { get; set; }
        public string Konto { get; set; }
        public bool? Eismeister { get; set; }
        public string Verkaufsgruppe { get; set; }
        public bool? AutoUmbuchung { get; set; }
        public Guid Guid { get; set; }
        
        public static async Task<List<Artikel>> GetAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var artikel = await UOW.Uow._uow.Query<artikel>().ToListAsync();
                return artikel.Select(item => new Artikel
                    {
                        ID = item.ID,
                        ArtNr = item.ArtNr,
                        Bezeichnung1 = item.Bezeichnung1,
                        Bezeichnung2 = item.Bezeichnung2,
                        Wert = item.Wert,
                        MWSt = item.MWSt,
                        Block = item.Block,
                        Konto = item.Konto,
                        Eismeister = item.Eismeister,
                        Verkaufsgruppe = item.Verkaufsgruppe,
                        AutoUmbuchung = item.AutoUmbuchung,
                        Guid = new Guid(item.Guid),
                    })
                    .ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}