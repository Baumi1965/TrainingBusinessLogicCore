using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training.BusinessLogic.Engelmann
{
    public class CEVVorschreibung
    {
        public string Mitgliedsnummer { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Adresse { get; set; }
        public string EMail { get; set; }
        public decimal MB { get; set; }
        public decimal SK { get; set; }
        public decimal Offen { get; set; }
        public string Trainer { get; set; }
        public int Saison { get; set; }
        public string Zugehoerigkeit { get; set; }

        public static async Task<List<CEVVorschreibung>> GetBySaison(int saison)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                List<CEVVorschreibung> vorschreibungen = new List<CEVVorschreibung>();

                var result = await UOW.UOW.uow.ExecuteSprocAsync(new System.Threading.CancellationToken(), "GET_CEV_MB", saison);
                var returnvorschreibung = result.ResultSet.FirstOrDefault();

                if (returnvorschreibung != null)
                {
                    foreach (var item in returnvorschreibung.Rows)
                    {
                        vorschreibungen.Add(new CEVVorschreibung()
                        {
                            Mitgliedsnummer = item.Values[0].ToString(),
                            Nachname = item.Values[1].ToString(),
                            Vorname = item.Values[2].ToString(),
                            PLZ = item.Values[3].ToString(),
                            Ort = item.Values[4].ToString(),
                            Adresse = item.Values[5].ToString(),
                            EMail = item.Values[6].ToString(),
                            MB = Convert.ToDecimal(item.Values[7].ToString()),
                            SK = Convert.ToDecimal(item.Values[8].ToString()),
                            Offen = Convert.ToDecimal(item.Values[9].ToString()),
                            Trainer = item.Values[10].ToString(),
                            Saison = Convert.ToInt32(item.Values[11]),
                            Zugehoerigkeit = item.Values[12].ToString(),
                        });
                    }
                }

                return vorschreibungen;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
