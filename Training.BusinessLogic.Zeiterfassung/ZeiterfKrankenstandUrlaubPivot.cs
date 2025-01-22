using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfKrankenstandUrlaubPivot
    {
        public string MitarbeiterID { get; set; }
        public string MitarbeiterName { get; set; }
        public string Kennung { get; set; }

        public int? Jaenner { get; set; } = 0;
        public int? Februar { get; set; } = 0;
        public int? Maerz { get; set; } = 0;
        public int? April { get; set; } = 0;
        public int? Mai { get; set; } = 0;
        public int? Juni { get; set; } = 0;
        public int? Juli { get; set; } = 0;
        public int? August { get; set; } = 0;
        public int? September { get; set; } = 0;
        public int? Oktober { get; set; } = 0;
        public int? November { get; set; } = 0;
        public int? Dezember { get; set; } = 0; 

        public async static Task<ZeiterfKrankenstandUrlaubPivot> GetUrlaubByMitarbeiterAndDateAsync(string mitarbeiterId, string name, int year)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                DateTime von = new DateTime(year, 1, 1);
                DateTime bis = new DateTime(year, 12, 31);

                var urlaub = await UOW.UOW.uow.Query<zeiterfurlaub>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date >= von.Date && x.Datum.Value.Date <= bis.Date)
                    .OrderBy(x => x.Datum)
                    .ToListAsync();

                ZeiterfKrankenstandUrlaubPivot ku = new ZeiterfKrankenstandUrlaubPivot();

                ku.MitarbeiterID = mitarbeiterId;
                ku.MitarbeiterName = name;
                ku.Kennung = "URLAUB";
                if (urlaub != null && urlaub.Count >= 0)
                {
                    foreach (var item in urlaub)
                    {
                        DateTime datum = Convert.ToDateTime(item.Datum);
                        int month = datum.Month;

                        switch (month)
                        {
                            case 1:
                                ku.Jaenner += 1;
                                break;
                            case 2:
                                ku.Februar += 1;
                                break;
                            case 3:
                                ku.Maerz += 1;
                                break;
                            case 4:
                                ku.April += 1;
                                break;
                            case 5:
                                ku.Mai += 1;
                                break;
                            case 6:
                                ku.Juni += 1;
                                break;
                            case 7:
                                ku.Juli += 1;
                                break;
                            case 8:
                                ku.August += 1;
                                break;
                            case 9:
                                ku.September += 1;
                                break;
                            case 10:
                                ku.Oktober += 1;
                                break;
                            case 11:
                                ku.November += 1;
                                break;
                            case 12:
                                ku.Dezember += 1;
                                break;
                        }
                    }
                }
 

                if (ku.Jaenner == 0) { ku.Jaenner = null; }
                if (ku.Februar == 0) { ku.Februar = null; }
                if (ku.Maerz == 0) { ku.Maerz = null; }
                if (ku.April == 0) { ku.April = null; }
                if (ku.Mai == 0) { ku.Mai = null; }
                if (ku.Juni == 0) { ku.Juni = null; }
                if (ku.Juli == 0) { ku.Juli = null; }
                if (ku.August == 0) { ku.August = null; }
                if (ku.September == 0) { ku.September = null; }
                if (ku.Oktober == 0) { ku.Oktober = null; }
                if (ku.November == 0) { ku.November = null; }
                if (ku.Dezember == 0) { ku.Dezember = null; }

                return ku;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async static Task<ZeiterfKrankenstandUrlaubPivot> GetKrankByMitarbeiterAndDateAsync(string mitarbeiterId, string name, int year)
        {
            try
            {
                if (UOW.UOW.uow == null || !UOW.UOW.uow.IsConnected)
                {
                    UOW.UOW.Connect();
                }

                DateTime von = new DateTime(year, 1, 1);
                DateTime bis = new DateTime(year, 12, 31);

                var krank = await UOW.UOW.uow
                    .Query<zeiterfkrank>()
                    .Where(
                        x => x.MitarbeiterId == mitarbeiterId &&
                            x.Datum.Value.Date >= von.Date &&
                            x.Datum.Value.Date <= bis.Date)
                    .OrderBy(x => x.Datum)
                    .ToListAsync();


                ZeiterfKrankenstandUrlaubPivot ku = new ZeiterfKrankenstandUrlaubPivot();

                ku.MitarbeiterID = mitarbeiterId;
                ku.MitarbeiterName = name;
                ku.Kennung = "KRANK";
                if (krank != null && krank.Count >= 0)
                {
                    foreach (var item in krank)
                    {
                        DateTime datum = Convert.ToDateTime(item.Datum);
                        int month = datum.Month;

                        switch (month)
                        {
                            case 1:
                                ku.Jaenner += 1;
                                break;
                            case 2:
                                ku.Februar += 1;
                                break;
                            case 3:
                                ku.Maerz += 1;
                                break;
                            case 4:
                                ku.April += 1;
                                break;
                            case 5:
                                ku.Mai += 1;
                                break;
                            case 6:
                                ku.Juni += 1;
                                break;
                            case 7:
                                ku.Juli += 1;
                                break;
                            case 8:
                                ku.August += 1;
                                break;
                            case 9:
                                ku.September += 1;
                                break;
                            case 10:
                                ku.Oktober += 1;
                                break;
                            case 11:
                                ku.November += 1;
                                break;
                            case 12:
                                ku.Dezember += 1;
                                break;
                        }
                    }
                }


                if (ku.Jaenner == 0) { ku.Jaenner = null; }
                if (ku.Februar == 0) { ku.Februar = null; }
                if (ku.Maerz == 0) { ku.Maerz = null; }
                if (ku.April == 0) { ku.April = null; }
                if (ku.Mai == 0) { ku.Mai = null; }
                if (ku.Juni == 0) { ku.Juni = null; }
                if (ku.Juli == 0) { ku.Juli = null; }
                if (ku.August == 0) { ku.August = null; }
                if (ku.September == 0) { ku.September = null; }
                if (ku.Oktober == 0) { ku.Oktober = null; }
                if (ku.November == 0) { ku.November = null; }
                if (ku.Dezember == 0) { ku.Dezember = null; }

                return ku;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
