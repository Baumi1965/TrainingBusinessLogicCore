using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BusinessLogic.Engelmann
{
    public class CEVOffeneBetraege
    {
        public string Mitgliedsnummer { get; set; }
        public string Nachname { get; set; }
        public string Vorname { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Adresse { get; set; }
        public string Zugehoerigkeit { get; set; }
        public string EMail { get; set; }
        public decimal MB { get; set; }
        public decimal SK { get; set; }
        public decimal Adult { get; set; }
        public decimal Offen { get; set; }
        public string Trainer { get; set; }
        public int Saison { get; set; }

        public static async Task<List<CEVOffeneBetraege>> GetBySaisonAsync(int saison)
        {
            try
            {
                if(UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                List<CEVOffeneBetraege> offeneBetraege = new List<CEVOffeneBetraege>();

                var result = await UOW.UOW.uow
                    .ExecuteSprocAsync(new System.Threading.CancellationToken(), "GET_CEV_OFFENEBETRAEGE", saison);
                var returnvorschreibung = result.ResultSet.FirstOrDefault();

                if(returnvorschreibung != null)
                {
                    foreach(var item in returnvorschreibung.Rows)
                    {
                        offeneBetraege.Add(
                            new CEVOffeneBetraege()
                            {
                                Mitgliedsnummer = item.Values[0].ToString(),
                                Nachname = item.Values[1].ToString(),
                                Vorname = item.Values[2].ToString(),
                                PLZ = item.Values[3].ToString(),
                                Ort = item.Values[4].ToString(),
                                Adresse = item.Values[5].ToString(),
                                Zugehoerigkeit = item.Values[6].ToString(),
                                EMail = item.Values[7].ToString(),
                                MB = Convert.ToDecimal(item.Values[8].ToString()),
                                SK = Convert.ToDecimal(item.Values[9].ToString()),
                                Adult = Convert.ToDecimal(item.Values[10].ToString()),
                                Offen = Convert.ToDecimal(item.Values[11].ToString()),
                                Trainer = item.Values[12].ToString(),
                                Saison = Convert.ToInt32(item.Values[13]),
                            });
                    }
                }

                return offeneBetraege;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}