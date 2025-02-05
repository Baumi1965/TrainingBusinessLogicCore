using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Engelmann
{
    public class CEVPreis
    {
        public int ID { get; set; }
        public int Saison { get; set; }
        public decimal MB { get; set; }
        public decimal SK { get; set; }
        public decimal MBAdult { get; set; }
        public decimal MBTeil { get; set; }
        public decimal MBGast { get; set; }
        public decimal SKAdult { get; set; }
        public decimal SKTeil { get; set; }


        public static async Task<CEVPreis> GetBySaisonAsync(int saison)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var preis = await UOW.Uow._uow.Query<cevpreise>().Where(x => x.Saison == saison).FirstOrDefaultAsync();
                if (preis == null)
                {
                    return null;
                }
                else
                {
                    return new CEVPreis
                    {
                        ID = preis.ID,
                        Saison = preis.Saison,
                        MB = preis.MB,
                        SK = preis.SK,
                        MBAdult = preis.MBAdult,
                        MBGast = preis.MBGast,
                        MBTeil = preis.MBTeil,
                        SKAdult = preis.SKAdult,
                        SKTeil = preis.SKTeil,
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<CEVPreis>> GetAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<cevpreise>().OrderBy(x => x.Saison).ToListAsync();
                if (result == null)
                {
                    return null;
                }
                else
                {
                    List<CEVPreis> preise = new List<CEVPreis>();
                    foreach (var item in result)
                    {
                        preise.Add(
                            new CEVPreis
                            {
                                ID = item.ID,
                                Saison = item.Saison,
                                MB = item.MB,
                                SK = item.SK,
                                MBAdult = item.MBAdult,
                                MBGast = item.MBGast,
                                MBTeil = item.MBTeil,
                                SKAdult = item.SKAdult,
                                SKTeil = item.SKTeil,
                            });
                    }
                    return preise;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> ExistsAsync(int saison)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<cevpreise>().Where(x => x.Saison == saison).FirstOrDefaultAsync();
                if (result == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddAsync(int saison, decimal mb, decimal sk, decimal mbTeil, decimal skteil, decimal mbAdult, decimal skAdult, decimal mbGast)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                cevpreise preis = new cevpreise(UOW.Uow._uow);
                preis.MB = mb;
                preis.MBGast = mbGast;
                preis.MBAdult = mbAdult;
                preis.MBTeil = mbTeil;
                preis.Saison = saison;
                preis.SK = sk;
                preis.SKAdult = skAdult;
                preis.SKTeil = skteil;

                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAsync(int id, int saison, decimal mb, decimal sk, decimal mbTeil, decimal skteil, decimal mbAdult, decimal skAdult, decimal mbGast)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var preis = await UOW.Uow._uow.Query<cevpreise>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (preis == null)
                {
                    return;
                }

                preis.MB = mb;
                preis.MBGast = mbGast;
                preis.MBAdult = mbAdult;
                preis.MBTeil = mbTeil;
                preis.Saison = saison;
                preis.SK = sk;
                preis.SKAdult = skAdult;
                preis.SKTeil = skteil;

                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task DeleteAsync(int id)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var preis = await UOW.Uow._uow.Query<cevpreise>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (preis == null)
                {
                    return;
                }

                await UOW.Uow.DeleteAsync(preis);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
