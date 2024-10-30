using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;


namespace Training.BusinessLogic.Engelmann
{
    public class CEVTraining
    {
        public int ID { get; set; }
        public int? TrNr { get; set; }
        public string Bezeichnung { get; set; }
        public string ZeitVon1 { get; set; }
        public string ZeitBis1 { get; set; }
        public bool? Vormittag { get; set; }
        public bool? Nachmittag { get; set; }

        public static async Task<List<CEVTraining>> GetAsync()
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                List<CEVTraining> list = new List<CEVTraining>();
                var result = await UOW.UOW.uow.Query<cevtraining>().ToListAsync();
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        list.Add(new CEVTraining
                        {
                            Bezeichnung = item.Bezeichnung,
                            ID = item.ID,
                            TrNr = item.TrNr,
                            ZeitVon1 = item.ZeitVon1,
                            ZeitBis1 = item.ZeitBis1,
                            Vormittag = item.Vormittag,
                            Nachmittag = item.Nachmittag,
                        });
                    }
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> MaxTrainingNrAsync()
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<cevtraining>().Select(x => x.TrNr).MaxAsync();
                if (result == null)
                {
                    return 1;
                }

                if (result.Value == 15)
                {
                    throw new Exception("Maximale Anzahl an Trainingsdatensätzen erreicht");
                }

                return result.Value + 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddAsync(int trainingNr, string bezeichnung, string von, string bis, bool vormittag, bool nachmittag)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                cevtraining training = new cevtraining(UOW.UOW.uow)
                {
                    Bezeichnung = bezeichnung,
                    Nachmittag = nachmittag,
                    TrNr = trainingNr,
                    Vormittag = vormittag,
                    ZeitBis1 = bis,
                    ZeitVon1 = von,
                };

                await UOW.UOW.SaveAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAsync(int id, string bezeichnung, string von, string bis, bool vormittag, bool nachmittag)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var training = await UOW.UOW.uow.Query<cevtraining>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (training == null)
                {
                    return;

                }

                training.Bezeichnung = bezeichnung;
                training.Nachmittag = nachmittag;
                training.Vormittag = vormittag;
                training.ZeitBis1 = bis;
                training.ZeitVon1 = von;

                await UOW.UOW.SaveAsync();

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
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var training = await UOW.UOW.uow.Query<cevtraining>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (training == null)
                {
                    return;

                }

                await UOW.UOW.DeleteAsync(training);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
