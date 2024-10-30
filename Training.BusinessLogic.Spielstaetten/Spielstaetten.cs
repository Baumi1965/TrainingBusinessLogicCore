using DevExpress.Xpo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Training.BusinessLogic.UOW.Models;
using System.Linq;

namespace Training.BusinessLogic.Spielstaetten
{
    public class Spielstaetten
    {
        public int ID { get; set; }
        public string Bezeichnung { get; set; }
        public string Adresse1 { get; set; }
        public string Adresse2 { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Color { get; set; }



        public static async Task<List<Spielstaetten>> GetAsync()
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var spielstaetten = await UOW.UOW.uow.Query<spielstaetten>().ToListAsync();
                List<Spielstaetten> lstSpielstaetten = new List<Spielstaetten>();
                foreach (var item in spielstaetten)
                {
                    lstSpielstaetten.Add(
                        new Spielstaetten
                        {
                            Adresse1 = item.Adresse1,
                            Adresse2 = item.Adresse2,
                            Bezeichnung = item.Bezeichnung,
                            Color = item.Color,
                            PLZ = item.PLZ,
                            Ort = item.Ort,
                            ID = item.ID
                        });
                }
                return lstSpielstaetten;
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

                var spielstaette = await UOW.UOW.uow.Query<spielstaetten>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (spielstaette != null)
                {
                    await UOW.UOW.DeleteAsync(spielstaette);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddAsync(string bezeichnung, string adresse1, string adresse2, string plz, string ort)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                spielstaetten spielstaette = new spielstaetten(UOW.UOW.uow)
                {
                    Bezeichnung = bezeichnung,
                    Adresse1 = adresse1,
                    Adresse2 = adresse2,
                    PLZ = plz,
                    Ort = ort,
                    Color = null
                };

                await UOW.UOW.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAsync(int id, string bezeichnung, string adresse1, string adresse2, string plz, string ort)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var spielstaette = await UOW.UOW.uow.Query<spielstaetten>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (spielstaette != null)
                {
                    spielstaette.Bezeichnung = bezeichnung;
                    spielstaette.Adresse1 = adresse1;
                    spielstaette.Adresse2 = adresse2;
                    spielstaette.PLZ = plz;
                    spielstaette.Ort = ort;
                };

                await UOW.UOW.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Spielstaetten>> GetForSchedulerAsync()
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var spielstaetten = await UOW.UOW.uow.Query<spielstaetten>().Where(x => x.Bezeichnung.ToLower().Contains("eisring") || x.Bezeichnung.ToLower().Contains("eisstadthalle")).ToListAsync();
                List<Spielstaetten> lstSpielstaetten = new List<Spielstaetten>();
                foreach (var item in spielstaetten)
                {
                    lstSpielstaetten.Add(
                        new Spielstaetten
                        {
                            Adresse1 = item.Adresse1,
                            Adresse2 = item.Adresse2,
                            Bezeichnung = item.Bezeichnung,
                            Color = item.Color,
                            PLZ = item.PLZ,
                            Ort = item.Ort,
                            ID = item.ID
                        });
                }
                return lstSpielstaetten;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Spielstaetten>> GetWithoutFreiflaecheAsync()
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var spielstaetten = await UOW.UOW.uow.Query<spielstaetten>().Where(x => !x.Bezeichnung.ToLower().Contains("frei")).ToListAsync();
                List<Spielstaetten> lstSpielstaetten = new List<Spielstaetten>();
                foreach (var item in spielstaetten)
                {
                    lstSpielstaetten.Add(
                        new Spielstaetten
                        {
                            Adresse1 = item.Adresse1,
                            Adresse2 = item.Adresse2,
                            Bezeichnung = item.Bezeichnung,
                            Color = item.Color,
                            PLZ = item.PLZ,
                            Ort = item.Ort,
                            ID = item.ID
                        });
                }
                return lstSpielstaetten;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Spielstaetten>> GetOnlyESHERSAsync()
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var spielstaetten = await UOW.UOW.uow.Query<spielstaetten>().Where(x => !x.Bezeichnung.ToLower().Contains("frei") && (x.Bezeichnung.ToLower().Contains("eisring") || x.Bezeichnung.ToLower().Contains("eisstadthalle"))).ToListAsync();

                List<Spielstaetten> lstSpielstaetten = new List<Spielstaetten>();
                foreach (var item in spielstaetten)
                {
                    lstSpielstaetten.Add(
                        new Spielstaetten
                        {
                            Adresse1 = item.Adresse1,
                            Adresse2 = item.Adresse2,
                            Bezeichnung = item.Bezeichnung,
                            Color = item.Color,
                            PLZ = item.PLZ,
                            Ort = item.Ort,
                            ID = item.ID
                        });
                }
                return lstSpielstaetten;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<Spielstaetten>> GetOnlyEngelmannAsync()
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var spielstaetten = await UOW.UOW.uow.Query<spielstaetten>().Where(x => x.Bezeichnung.ToLower().Contains("Engelmann")).ToListAsync();
                List<Spielstaetten> lstSpielstaetten = new List<Spielstaetten>();
                foreach (var item in spielstaetten)
                {
                    lstSpielstaetten.Add(
                        new Spielstaetten
                        {
                            Adresse1 = item.Adresse1,
                            Adresse2 = item.Adresse2,
                            Bezeichnung = item.Bezeichnung,
                            Color = item.Color,
                            PLZ = item.PLZ,
                            Ort = item.Ort,
                            ID = item.ID
                        });
                }
                return lstSpielstaetten;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Spielstaetten> GetByIdAsync(int id)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var spielstaette = await UOW.UOW.uow.Query<spielstaetten>().Where(x => x.ID == id).FirstOrDefaultAsync();
                return new Spielstaetten
                {
                    Adresse1 = spielstaette.Adresse1,
                    Adresse2 = spielstaette.Adresse2,
                    Bezeichnung = spielstaette.Bezeichnung,
                    Color = spielstaette.Color,
                    ID = spielstaette.ID,
                    Ort = spielstaette.Ort,
                    PLZ = spielstaette.PLZ
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Spielstaetten> GetByNameAsync(string name)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var spielstaette = await UOW.UOW.uow.Query<spielstaetten>().Where(x => x.Bezeichnung == name).FirstOrDefaultAsync();
                return new Spielstaetten
                {
                    Adresse1 = spielstaette.Adresse1,
                    Adresse2 = spielstaette.Adresse2,
                    Bezeichnung = spielstaette.Bezeichnung,
                    Color = spielstaette.Color,
                    ID = spielstaette.ID,
                    Ort = spielstaette.Ort,
                    PLZ = spielstaette.PLZ
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
