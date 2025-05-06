using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.ParkplatzERS
{
    public class ParkplatzERSBerechtigt
    {
        public int ID { get; set; }
        public string Barcode { get; set; }
        public bool Berechtigt { get; set; }
        public Guid Guid { get; set; }
        
        public static async Task<List<ParkplatzERSBerechtigt>> GetAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var pp = await UOW.Uow._uow.Query<parkplatzeisringberechtigt>().ToListAsync();
                if (pp != null)
                {
                    return pp.Select(item => new ParkplatzERSBerechtigt()
                        {
                            ID = item.ID,
                            Barcode = item.Barcode,
                            Berechtigt = item.Berechtigt,
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
    }
}