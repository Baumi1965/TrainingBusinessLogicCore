using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.ZeitenKalender
{
    public class ZeitenKalenderExt
    {
        public int Id { get; set; }
        public int ZeitenKalenderId { get; set; }
        public int SpielstaetteId { get; set; }
        public int? AnzahlFrequenzschein { get; set; }
        public int? AnzahlFrequenzscheinBegleitperson { get; set; }
        public double? WertFrequenzschein { get; set; }
        public int? AnzahlKassa { get; set; }
        public double? WertKassa { get; set; }
        public DateTime Ts { get; set; }
        public DateTime TsCreated { get; set; }
        public DateTime TsModified { get; set; }
        public string UserCreated { get; set; }
        public string UserModified { get; set; }


        public static async Task<ZeitenKalenderExt> GetAsync(int location, DateTime ts, int zeitenKalenderId, CancellationToken cancellationToken = default)
        {
            try
            {
                if (Uow._uow == null || !Uow._uow.IsConnected)
                {
                    Uow.Connect();
                }
                var ext = await Uow._uow.Query<zeitenkalender_ext>()
                    .FirstOrDefaultAsync(x => x.TS.Date == ts && x.SpielstaetteId == location && x.ZeitenKalenderId == zeitenKalenderId,
                        cancellationToken: cancellationToken);

                if (ext == null)
                {
                    return null;
                }

                return new ZeitenKalenderExt()
                {
                    Id = ext.ID,
                    ZeitenKalenderId = zeitenKalenderId,
                    SpielstaetteId = ext.SpielstaetteId,
                    AnzahlFrequenzschein = ext.AnzahlFrequenzschein,
                    AnzahlKassa = ext.AnzahlKassa,
                    AnzahlFrequenzscheinBegleitperson = ext.AnzahlFrequenzscheinBegleitperson,
                    WertKassa = ext.WertKassa,
                    WertFrequenzschein = ext.WertFrequenzschein,
                    Ts = ts,
                    TsModified = ext.TS_Modified,
                    TsCreated = ext.TS_Created,
                    UserModified = ext.User_Modified,
                    UserCreated = ext.User_Created,
                };                
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static async Task AddAsync(int zeitenkalenderId, int location, int anzahlFrequenzschein, int anzahlFrequenzscheinBegleitperson,
            double wertFrequenzschein, int anzahlKassa, double wertKassa,  DateTime ts)
        {
            try
            {
                if (Uow._uow == null || !Uow._uow.IsConnected)
                {
                    Uow.Connect();
                }
                
                var anzahl = anzahlFrequenzschein + anzahlFrequenzscheinBegleitperson +  anzahlKassa;
                var wert = wertFrequenzschein + wertKassa;
                
                var ext = new zeitenkalender_ext(Uow._uow)
                {
                    ZeitenKalenderId = zeitenkalenderId,
                    SpielstaetteId = location,
                    AnzahlFrequenzschein = anzahlFrequenzschein,
                    AnzahlFrequenzscheinBegleitperson = anzahlFrequenzscheinBegleitperson,
                    WertFrequenzschein = wertFrequenzschein,
                    AnzahlKassa = anzahlKassa,
                    WertKassa = wertKassa,
                    Anzahl = anzahl,
                    Wert = wert,
                    TS = ts,
                    TS_Created = DateTime.Now,
                    TS_Modified = DateTime.Now,
                    User_Created = "Training.KassaSync",
                    User_Modified = "Training.KassaSync"
                };

                await Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static async Task UpdateAsync(int id, int anzahlFrequenzschein, int anzahlFrequenzscheinBegleitperson,
            double wertFrequenzschein, int anzahlKassa, double wertKassa)
        {
            try
            {
                if (Uow._uow == null || !Uow._uow.IsConnected)
                {
                    Uow.Connect();
                }

                var anzahl = anzahlFrequenzschein + anzahlFrequenzscheinBegleitperson +  anzahlKassa;
                var wert = wertFrequenzschein + wertKassa;
                
                var ext = await Uow._uow.Query<zeitenkalender_ext>()
                    .Where(x => x.ID == id)
                    .FirstOrDefaultAsync();

                if (ext != null)
                {
                    ext.AnzahlFrequenzschein += anzahlFrequenzschein;
                    ext.AnzahlFrequenzscheinBegleitperson += anzahlFrequenzscheinBegleitperson;
                    ext.WertFrequenzschein += wertFrequenzschein;
                    ext.AnzahlKassa += anzahlKassa;
                    ext.WertKassa += wertKassa;
                    ext.Anzahl += anzahl;
                    ext.Wert += wert;
                    ext.TS_Modified = DateTime.Now;
                    ext.User_Modified = "Training.KassaSync";
                }
                await Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }    
}
