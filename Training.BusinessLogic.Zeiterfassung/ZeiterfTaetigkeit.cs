using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfTaetigkeit
	{
        public int ID { get; set; }
        public string Taetigkeit { get; set; }

		public static async Task<List<ZeiterfTaetigkeit>> Get()
		{
			try
			{
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

				var result = await UOW.Uow._uow.Query<zeiterftaetigkeit>().ToListAsync();
				List<ZeiterfTaetigkeit> list = new List<ZeiterfTaetigkeit>();
				foreach (var item in result)
				{
					list.Add(
						new ZeiterfTaetigkeit
						{
							ID = item.ID,
							Taetigkeit = item.Taetigkeit,
						});
				}
				return list;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
