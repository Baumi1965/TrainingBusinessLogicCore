using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Newtonsoft.Json;
using Training.BusinessLogic.Shared;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Kunden
{
    public class EKLBegleitkarte
    {
        public int ID { get; set; }
        public string Barcode { get; set; }
        public string KdNr { get; set; }
        public bool Parkplatz { get; set; }
        public DateTime? TSAktivierung { get; set; }
        public DateTime? TSEklEin { get; set; }
        public DateTime? TSEklAus { get; set; }
        public int? Location { get; set; }
        public Guid Guid { get; set; }

        public static async Task<Boolean> IsActivatedAsync(string barcode)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<eklbegleitkarte>().Where(x => x.Barcode == barcode).FirstOrDefaultAsync();
                return result != null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<EKLBegleitkarte>> GetAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var ekl = await UOW.Uow._uow.Query<eklbegleitkarte>().ToListAsync();
                if (ekl != null)
                {
                    return ekl.Select(item => new EKLBegleitkarte()
                        {
                            ID = item.ID,
                            Barcode = item.Barcode,
                            KdNr = item.KdNr,
                            Parkplatz = item.Parkplatz,
                            TSAktivierung = item.TSAktivierung,
                            TSEklAus = null,
                            TSEklEin = null,
                            Location = null,
                            Guid = new Guid(item.Guid),
                        })
                        .ToList();
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static async Task<EKLBegleitkarte> GetByBarcodeAsync(string barcode)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var item = await UOW.Uow._uow.Query<eklbegleitkarte>().Where(x => x.Barcode == barcode).FirstOrDefaultAsync();
                if (item != null)
                {
                    return new EKLBegleitkarte
                    {
                        ID = item.ID,
                        Barcode = item.Barcode,
                        KdNr = item.KdNr,
                        Parkplatz = item.Parkplatz,
                        TSAktivierung = item.TSAktivierung,
                        TSEklAus = null,
                        TSEklEin = null,
                        Location = null,
                        Guid = new Guid(item.Guid),
                    };
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddAsync(List<EKLBegleitkarte> begleitkarten)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                foreach (var item in begleitkarten)
                {
                    eklbegleitkarte eklbegleitkarte = new eklbegleitkarte(UOW.Uow._uow)
                    {
                        Barcode = item.Barcode,
                        KdNr = item.KdNr,
                        Parkplatz = item.Parkplatz,
                        TSAktivierung = item.TSAktivierung,
                        TSEklAus = null,
                        TSEklEin = null,
                        Location = null,
                        Guid = item.Guid.ToString(),
                    };

                    await UOW.Uow.SaveAsync();

                    var artikel = await GetByBarcodeAsync(item.Barcode);
                    var daten = JsonConvert.SerializeObject(artikel, Formatting.None);
                    await SyncRequest.AddAsync(new List<int>() { 2 }, "I", "EklBegleitkarte", Guid.NewGuid().ToString(), daten, true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}