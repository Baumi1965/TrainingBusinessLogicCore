using DevExpress.Xpo;
using Training.BusinessLogic.UOW.ModelsKassa;

namespace Training.BusinessLogic.Kassa.Spielstaette
{
        public class KassaSpielstaette
        {
                public int Id { get; set; }
                public string Mandant { get; set; }
                public string SpName { get; set; }
                public string Sp { get; set; }
                public string Strasse1 { get; set; }
                public string Strasse2 { get; set; }
                public string PLZ { get; set; }
                public string Ort { get; set; }
                public string Nation { get; set; }
                public string Tel { get; set; }
                public string Fax { get; set; }
                public string eMail { get; set; }
                public string INet { get; set; }
                public string GeschZeit { get; set; }
                public bool Aktiv { get; set; }
                public DateTime Saisonbeginn { get; set; }
                public DateTime Saisonende { get; set; }
                public int HobexMandant { get; set; }

                public static async Task<KassaSpielstaette?> GetBySpielstaetteIdAsync(int spielstaetteId)
                {
                        try
                        {
                                if (UOW.Uow._uowKassa == null || !UOW.Uow._uowKassa.IsConnected)
                                {
                                        UOW.Uow.ConnectKassa();
                                }

                                var result = await UOW.Uow._uowKassa.Query<sp>()
                                        .FirstOrDefaultAsync(x => x.Sp == spielstaetteId.ToString());

                                if (result == null)
                                {
                                        return null;
                                }

                                return new KassaSpielstaette
                                {
                                        Id = result.ID,
                                        Mandant = result.Mandant,
                                        SpName = result.SpName,
                                        Sp = result.Sp,
                                        Strasse1 = result.Strasse1,
                                        Strasse2 = result.Strasse2,
                                        PLZ = result.PLZ,
                                        Ort = result.Ort,
                                        Nation = result.Nation,
                                        Tel = result.Tel,
                                        Fax = result.Fax,
                                        eMail = result.eMail,
                                        INet = result.INet,
                                        GeschZeit = result.GeschZeit,
                                        Saisonbeginn = result.Saisonbeginn,
                                        Saisonende = result.Saisonende,
                                        HobexMandant = result.HobexMandant,
                                        Aktiv = result.Aktiv,
                                };
                                
                        }
                        catch (Exception ex)
                        {
                                throw;
                        }
                }
        }
}