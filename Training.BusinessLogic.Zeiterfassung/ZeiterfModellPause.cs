using DevExpress.Data.ODataLinq.Helpers;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfModellPause
	{
        public int ID { get; set; }
        public int? ModellID { get; set; }
        public string PauseStart { get; set; }

		public static async Task<List<ZeiterfModellPause>> Get()
		{
			try
			{
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

				var result = await UOW.UOW.uow.Query<zeiterfmodellpause>().ToListAsync();
				List<ZeiterfModellPause> list = new List<ZeiterfModellPause>();
				foreach (var item in result)
				{
					list.Add(
						new ZeiterfModellPause
						{
							ID = item.ID,
							ModellID = item.ModellID,
							PauseStart = item.PauseStart,
						});
				}
				return list;
			}
			catch (Exception)
			{
				throw;
			}
		}

        public static List<ZeiterfModellPause> GetByModelId(int modelId)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = UOW.UOW.uow.Query<zeiterfmodellpause>().Where(x => x.ModellID == modelId).ToList();
                List<ZeiterfModellPause> list = new List<ZeiterfModellPause>();
                foreach (var item in result)
                {
                    list.Add(
                        new ZeiterfModellPause
                        {
                            ID = item.ID,
                            ModellID = item.ModellID,
                            PauseStart = item.PauseStart,
                        });
                }
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static async Task<ZeiterfModellPause> GetById(int id)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<zeiterfmodellpause>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result == null)
                    return null;

                return new ZeiterfModellPause
                {
                    ID = result.ID,
                    ModellID = result.ModellID,
                    PauseStart = result.PauseStart,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<int> CountByModelId(int modelId)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<zeiterfmodellpause>().Where(x => x.ModellID == modelId).CountAsync();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task Add(int modelId, string pauseStart)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                zeiterfmodellpause mp = new zeiterfmodellpause(UOW.UOW.uow)
                {
                    ModellID = modelId,
                    PauseStart = pauseStart,
                };
                await UOW.UOW.SaveAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task Update(int id, string pauseStart)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<zeiterfmodellpause>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result == null)
                {
                    zeiterfmodellpause mp = new zeiterfmodellpause(UOW.UOW.uow)
                    {
                        PauseStart = pauseStart,
                    };
                }
                else
                {
                    result.PauseStart = pauseStart;
                }
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

                var result = await UOW.UOW.uow.Query<zeiterfmodellpause>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result == null)
                {
                    return;
                }

                await UOW.UOW.uow.DeleteAsync(result);
                await UOW.UOW.SaveAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
