using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Engelmann
{
    public class CEVEinlassEngelmann
    {
        public int ID { get; set; }
        public string Weekday { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTill { get; set; }
        public bool Active { get; set; }

        public static async Task<List<CEVEinlassEngelmann>> GetAsync()
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<ceveinlassengelmann>().OrderBy(x => x.ID).ToListAsync();
                if (result == null)
                {
                    return null;
                }
                else
                {
                    List<CEVEinlassEngelmann> einlass = new List<CEVEinlassEngelmann>();
                    foreach (var item in result)
                    {
                        einlass.Add(
                            new CEVEinlassEngelmann
                            {
                                ID = item.ID,
                                Weekday = item.Weekday,
                                TimeFrom = item.TimeFrom,
                                TimeTill = item.TimeTill,
                                Active = item.Active,
                            });
                    }
                    return einlass;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> CheckForDateAsync(DateTime ein)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                int VorlaufZeit = 30;
                int NachlaufZeit = 30;

                var setting = await Einstellungen.Einstellungen.GetByKeyAsync("VorlaufzeitEngelmann");
                if (setting != null)
                {
                    VorlaufZeit = Convert.ToInt32(setting.Value);
                }

                setting = await Einstellungen.Einstellungen.GetByKeyAsync("NachlaufzeitEngelmann");
                if (setting != null)
                {
                    NachlaufZeit = Convert.ToInt32(setting.Value);
                }



                int weekdayNr = 0;
                DayOfWeek dayOfWeek = ein.DayOfWeek;
                switch (dayOfWeek)
                {
                    case DayOfWeek.Monday:
                        weekdayNr = 0;                        
                        break;
                    case DayOfWeek.Tuesday:
                        weekdayNr = 1;
                        break;
                    case DayOfWeek.Wednesday:
                        weekdayNr = 2;
                        break;
                    case DayOfWeek.Thursday:
                        weekdayNr = 3;
                        break;
                    case DayOfWeek.Friday:
                        weekdayNr = 4;
                        break;
                    case DayOfWeek.Saturday:
                        weekdayNr = 5;
                        break;
                    case DayOfWeek.Sunday:
                        weekdayNr = 6;
                        break;
                    default:
                        throw new Exception("ungültiger wochentag");
                }

                var result = await UOW.Uow._uow.Query<ceveinlassengelmann>().Where(x => x.Active == true && x.WeekdayNr == weekdayNr).OrderBy(x => x.ID).ToListAsync();
                if (result == null || result.Count == 0)
                {
                    return false;
                }

                foreach (var item in result)
                {
                    DateTime TimeFrom = new DateTime(
                        ein.Year,
                        ein.Month,
                        ein.Day,
                        Convert.ToInt32(item.TimeFrom.Split(':')[0]), Convert.ToInt32(item.TimeFrom.Split(':')[1]), 0);

                    DateTime TimeTill = new DateTime(
                        ein.Year,
                        ein.Month,
                        ein.Day,
                        Convert.ToInt32(item.TimeTill.Split(':')[0]), Convert.ToInt32(item.TimeTill.Split(':')[1]), 59);


                    if (TimeFrom.TimeOfDay.Subtract(new TimeSpan(0,VorlaufZeit,0)).CompareTo(ein.TimeOfDay) <= 0 
                        && TimeTill.TimeOfDay.CompareTo(ein.TimeOfDay.Subtract(new TimeSpan(0,NachlaufZeit,0))) >= 0)
                    {
                        return true;
                    }

                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static async Task<bool> ExistsAsync(string weekday, string timefrom)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var result = await UOW.Uow._uow.Query<ceveinlassengelmann>().Where(x => x.Weekday == weekday && x.TimeFrom == timefrom).FirstOrDefaultAsync();
                if (result == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task AddAsync(string weekday, string from, string till, bool active)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                Weekdays dayofWeek = (Weekdays)Enum.Parse(typeof(Weekdays), weekday);
                int weekdayNr = (int)dayofWeek;


                ceveinlassengelmann einlass = new ceveinlassengelmann(UOW.Uow._uow);
                einlass.Weekday = weekday;
                einlass.TimeFrom = from;
                einlass.TimeTill = till;
                einlass.Active = active;
                einlass.WeekdayNr = weekdayNr;

                await UOW.Uow.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateAsync(int id, string weekday, string from, string till, bool active)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                var einlass = await UOW.Uow._uow.Query<ceveinlassengelmann>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (einlass == null)
                {
                    return;
                }

                Weekdays dayofWeek = (Weekdays)Enum.Parse(typeof(Weekdays), weekday);
                int weekdayNr = (int)dayofWeek;

                einlass.Weekday = weekday;
                einlass.TimeFrom = from;
                einlass.TimeTill = till;
                einlass.Active = active;
                einlass.WeekdayNr = weekdayNr;

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

                var einlass = await UOW.Uow._uow.Query<ceveinlassengelmann>().Where(x => x.ID == id).FirstOrDefaultAsync();
                if (einlass == null)
                {
                    return;
                }

                await UOW.Uow.DeleteAsync(einlass);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public enum Weekdays
    {
        Montag = 0,
        Dienstag = 1,
        Mittwoch = 2,
        Donnerstag = 3,
        Freitag = 4,
        Samstag = 5,
        Sonntag = 6
    }
}
