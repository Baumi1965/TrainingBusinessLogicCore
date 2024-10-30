using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Einstellungen
{
    public class Einstellungen
    {
        public int ID { get; set; }
        public string Setting { get; set; }
        public string Value { get; set; }
        public int Spielstaette { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public bool ReadOnly { get; set; }
        public Einstellungen()
        {

        }

        public Einstellungen(string setting, string value, int spielstaette, string category = "", string description = "", string type = "", bool readOnly = false)
        {
            Setting = setting;
            Value = value;
            Spielstaette = spielstaette;
            Category = category;
            Description = description;
            Type = type;
            ReadOnly = readOnly;
        }

        public static async Task<List<Einstellungen>> GetAsync()
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                List<Einstellungen> lstEinstellungen = new List<Einstellungen>();

                var ein = await UOW.UOW.uow.Query<einstellungen>().ToListAsync();
                foreach (var item in ein)
                {
                    lstEinstellungen.Add(new Einstellungen
                    {
                        ID = item.ID,
                        Setting = item.Setting,
                        Value = item.Value,
                        Spielstaette = item.Spielstaette,
                        Category = item.Category,
                        Description = item.Description,
                        Type = item.Type,
                        ReadOnly = item.ReadOnly != null && Convert.ToBoolean(item.ReadOnly),
                    });
                }
                return lstEinstellungen;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Einstellungen> GetByKeyAsync(string key)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                List<Einstellungen> lstEinstellungen = new List<Einstellungen>();

                var ein = await UOW.UOW.uow.Query<einstellungen>().Where(x => x.Setting == key).FirstOrDefaultAsync();
                if (ein != null)
                {
                    Einstellungen Einstellung = new Einstellungen
                    {
                        ID = ein.ID,
                        Setting = ein.Setting,
                        Value = ein.Value,
                        Spielstaette = ein.Spielstaette,
                        Category = ein.Category,
                        Description = ein.Description,
                        Type = ein.Type,
                        ReadOnly = ein.ReadOnly != null && Convert.ToBoolean(ein.ReadOnly),
                    };
                    return Einstellung;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<Einstellungen> GetByPartOfKeyAndLocationAsync(string key, int location)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                List<Einstellungen> lstEinstellungen = new List<Einstellungen>();

                var ein = await UOW.UOW.uow.Query<einstellungen>().Where(x => x.Setting.StartsWith(key) && x.Spielstaette == location).FirstOrDefaultAsync();
                Einstellungen Einstellung = new Einstellungen
                {
                    ID = ein.ID,
                    Setting = ein.Setting,
                    Value = ein.Value,
                    Spielstaette = ein.Spielstaette,
                    Category = ein.Category,
                    Description = ein.Description,
                    Type = ein.Type,
                    ReadOnly = ein.ReadOnly != null && Convert.ToBoolean(ein.ReadOnly),
                };
                return Einstellung;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddOrUpdateAsync(string key, string value, int location = 0, string category = "", string description = "", string type = "")
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = await UOW.UOW.uow.Query<einstellungen>().Where(x => x.Setting == key).FirstOrDefaultAsync();
                if (result == null)
                {
                    einstellungen e = new einstellungen(UOW.UOW.uow);
                    e.Setting = key;
                    e.Value = value;
                    e.Spielstaette = location;
                    e.Category = category;
                    e.Description = description;
                    e.Type = type;
                    e.ReadOnly = false;
                }
                else
                {
                    result.Value = value;
                    result.Category = category;
                    result.Description = description;
                    result.Type = type;
                }
                await UOW.UOW.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddOrUpdateAsync(List<Einstellungen> settings)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                foreach (var item in settings)
                {
                    var result = await UOW.UOW.uow.Query<einstellungen>().Where(x => x.Setting == item.Setting).FirstOrDefaultAsync();
                    if (result == null)
                    {
                        einstellungen e = new einstellungen(UOW.UOW.uow);
                        e.Setting = item.Setting;
                        e.Value = item.Value;
                        e.Spielstaette = item.Spielstaette;
                        e.Category = item.Category;
                        e.Description = item.Description;
                        e.Type = item.Type;
                        e.ReadOnly = false;
                    }
                    else
                    {
                        result.Value = item.Value;
                        result.Category = item.Category;
                        result.Description = item.Description;
                        result.Type = item.Type;
                    }
                }


                await UOW.UOW.SaveAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
