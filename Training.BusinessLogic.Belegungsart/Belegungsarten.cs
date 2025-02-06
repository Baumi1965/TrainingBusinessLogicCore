using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Belegungsart
{
    public class Belegungsarten
    {
        public int ID { get; set; }
        public string Belegungsart { get; set; }
        public int? Farbe { get; set; }
        public bool Buchungssystem { get; set; }
        public int Key { get; set; }
        public string Kurztext { get; set; }
        public bool Abbuchen { get; set; }
        public bool KassaFrequenzschein { get; set; }
        public Guid Guid { get; set; }
        
        public static async Task<List<Belegungsarten>> GetAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var ba = await UOW.Uow._uow.Query<belegungsarten>().ToListAsync();

                List<Belegungsarten> lstBelegungsarten = new List<Belegungsarten>();

                foreach (var item in ba)
                {
                    lstBelegungsarten.Add(
                        new Belegungsarten
                        {
                            ID = item.ID,
                            Belegungsart = item.Belegungsart,
                            Farbe = item.Farbe,
                            Buchungssystem = item.Buchungssystem,
                            Key = item.Key,
                            Kurztext = item.Kurztext,
                            Abbuchen = item.Abbuchen,
                            KassaFrequenzschein = item.KassaFrequenzschein,
                            Guid =  new Guid(item.Guid),
                        });
                }
                return lstBelegungsarten;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}