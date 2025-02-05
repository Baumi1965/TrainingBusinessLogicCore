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
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

				var result = await UOW.Uow._uow.Query<zeiterfmodellpause>().ToListAsync();
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
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = UOW.Uow._uow.Query<zeiterfmodellpause>().Where(x => x.ModellID == modelId).ToList();
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
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<zeiterfmodellpause>().Where(x => x.ID == id).FirstOrDefaultAsync();
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
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<zeiterfmodellpause>().Where(x => x.ModellID == modelId).CountAsync();
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
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                zeiterfmodellpause mp = new zeiterfmodellpause(UOW.Uow._uow)
                {
                    ModellID = modelId,
                    PauseStart = pauseStart,
                };
                await UOW.Uow.SaveAsync(); 
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
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<zeiterfmodellpause>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result == null)
                {
                    zeiterfmodellpause mp = new zeiterfmodellpause(UOW.Uow._uow)
                    {
                        PauseStart = pauseStart,
                    };
                }
                else
                {
                    result.PauseStart = pauseStart;
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

                var result = await UOW.Uow._uow.Query<zeiterfmodellpause>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (result == null)
                {
                    return;
                }

                await UOW.Uow._uow.DeleteAsync(result);
                await UOW.Uow.SaveAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
