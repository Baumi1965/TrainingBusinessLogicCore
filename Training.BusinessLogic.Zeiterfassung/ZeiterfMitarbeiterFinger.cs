using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfMitarbeiterFinger
    {
        public int ID { get; set; }
        public int MitarbeiterId { get; set; }
        public string Finger { get; set; }
        public string SecretKey { get; set; }
        public string FingerIndex { get; set; }

        public async static Task AddOrUpdateAsync(int mitarbeiterId, string finger, string secretKey, string fingerIndex)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }


                var result = await UOW.UOW.uow
                    .Query<zeiterfmitarbeiterfinger>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.FingerIndex == fingerIndex)
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    var ma = new zeiterfmitarbeiterfinger(UOW.UOW.uow)
                    {
                        Finger = finger,
                        SecretKey = secretKey,
                        FingerIndex = fingerIndex,
                        MitarbeiterId = mitarbeiterId
                    };
                }
                else
                {
                    result.FingerIndex = fingerIndex;
                    result.Finger = finger;
                    result.SecretKey = secretKey;
                }

                await UOW.UOW.SaveAsync(); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<ZeiterfMitarbeiterFinger> Get()
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                var result = UOW.UOW.uow
                    .Query<zeiterfmitarbeiterfinger>()
                    .ToList();

                if (result == null)
                {
                    return null;
                }

                List<ZeiterfMitarbeiterFinger> fingers = new List<ZeiterfMitarbeiterFinger> ();

                foreach (var item in result)
                {
                    fingers.Add(
                        new ZeiterfMitarbeiterFinger
                        {
                            FingerIndex = item.FingerIndex,
                            Finger = item.Finger,
                            SecretKey = item.SecretKey,
                            ID = item.ID,
                            MitarbeiterId = item.MitarbeiterId,
                        });
                }

                return fingers;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
