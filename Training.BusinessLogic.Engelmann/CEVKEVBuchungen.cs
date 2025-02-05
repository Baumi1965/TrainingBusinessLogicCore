using System;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Engelmann
{
    public class CEVKEVBuchungen
    {
        public int ID { get; set; }
        public string KdNr { get; set; }
        public bool CEV { get; set; }
        public bool KEV { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public DateTime Datum { get; set; }
        public string Zeit { get; set; }
        public bool? Vormittag { get; set; }
        public bool? Nachmittag { get; set; }
        public DateTime? TSEin { get; set; }
        public DateTime? TSAus { get; set; }

        public static async Task AddAsync(bool cev, bool kev, DateTime ein, string kdnr, string nachname, string vorname, bool nachmittag,
            bool vormittag, DateTime? aus)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                cevkevbuchungen cevkev = new cevkevbuchungen(UOW.Uow._uow)
                {
                    CEV = cev,
                    Datum = ein,
                    KdNr = kdnr,
                    KEV = kev,
                    Nachname = nachname,
                    Vorname = vorname,
                    Vormittag = vormittag,
                    Nachmittag = nachmittag,
                    Zeit = ein.ToString("HH:mm:ss"),
                    TSEin = ein,
                    TSAus = aus
                };

                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task AddOrUpdateAsync(bool cev, bool kev, DateTime ein, string kdnr, string nachname, string vorname, bool nachmittag,
            bool vormittag, DateTime? aus, DateTime searchEin)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var resbuch = await UOW.Uow._uow.Query<cevkevbuchungen>().Where(x => x.KdNr == kdnr && x.TSEin == searchEin).FirstOrDefaultAsync();
                if (resbuch == null)
                {
                    cevkevbuchungen cevkev = new cevkevbuchungen(UOW.Uow._uow)
                    {
                        CEV = cev,
                        Datum = ein,
                        KdNr = kdnr,
                        KEV = kev,
                        Nachname = nachname,
                        Vorname = vorname,
                        Vormittag = vormittag,
                        Nachmittag = nachmittag,
                        Zeit = ein.ToString("HH:mm:ss"),
                        TSEin = ein,
                        TSAus = aus
                    };
                }
                else
                {
                    resbuch.Datum = ein;
                    resbuch.Zeit = ein.ToString("HH:mm:ss");
                    resbuch.TSEin = ein;
                    resbuch.TSAus = aus;
                    resbuch.Vormittag = vormittag;
                    resbuch.Nachmittag = nachmittag;
                }
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

                var result = await UOW.Uow._uow.Query<cevkevbuchungen>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    await UOW.Uow.DeleteAsync(result);
				}
			}
			catch (Exception)
			{

				throw;
			}
		}
    }
}
