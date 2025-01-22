using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;


namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfAnwesend
    {
        public int ID { get; set; }
        public string MitarbeiterID { get; set; }
        public string Name { get; set; }
        public DateTime Datum { get; set; }
        public string Kommt { get; set; }
        public int SpielstaetteID { get; set; }
        public string Spielstaette { get; set; }

        public static async Task AddAsync(string mitarbeiterId, DateTime datum ,string kommt, int spielstaetteId)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var resultspielstaette = await Spielstaetten.Spielstaetten.GetByIdAsync(spielstaetteId);
                string spielstaette = resultspielstaette.Bezeichnung;

                var resultmitarbeiter = await ZeiterfMitarbeiter.GetById(mitarbeiterId);
                string name = resultmitarbeiter.Name;

                zeiterfanwesend anwesend = new zeiterfanwesend(UOW.UOW.uow)
                {
                    Datum = datum,
                    Kommt = kommt,
                    MitarbeiterId = mitarbeiterId,
                    Name = name,
                    Spielstaette = spielstaette,
                    SpielstaetteID = spielstaetteId,
            };

                await UOW.UOW.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task DeleteAsync(string mitarbeiterId, DateTime datum)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<zeiterfanwesend>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Date == datum.Date)
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    await UOW.UOW.uow.DeleteAsync(result);
                    await UOW.UOW.SaveAsync(); 
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<ZeiterfAnwesend>> Get(bool admin, bool engelmann, DateTime date, int location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<zeiterfanwesend>()
                    .Where(x => x.Datum.Date == date.Date)
                    .ToListAsync();

                if (result == null)
                {
                    return null;
                }

                List<ZeiterfAnwesend> anwesend = new List<ZeiterfAnwesend>();
                foreach (var item in result) 
                {
                    anwesend.Add(
                        new ZeiterfAnwesend()
                        {
                            Datum = item.Datum,
                            ID = item.ID,
                            Kommt = item.Kommt,
                            MitarbeiterID = item.MitarbeiterId,
                            Name = item.Name,
                            Spielstaette = item.Spielstaette,
                            SpielstaetteID = item.SpielstaetteID,
                        });
                }

                if (admin && engelmann)
                {
                    return anwesend;
                }
                else if (admin && !engelmann)
                {
                    return anwesend.Where(x => x.SpielstaetteID != 4).ToList();
                }
                else if (!admin && engelmann)
                {
                    return anwesend.Where(x => x.SpielstaetteID == 4).ToList();
                }
                else
                {
                    return anwesend.Where(x => x.SpielstaetteID == location).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
