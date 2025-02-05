using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BusinessLogic.UOW.Models;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfKrankenstandUrlaub
    {
        public string MitarbeiterID { get; set; }
        public string MitarbeiterName { get; set; }
        public int? U1 { get; set; } = 0;
        public int? K1 { get; set; } = 0;
        public int? U2 { get; set; } = 0;
        public int? K2 { get; set; } = 0;
        public int? U3 { get; set; } = 0;
        public int? K3 { get; set; } = 0;
        public int? U4 { get; set; } = 0;
        public int? K4 { get; set; } = 0;
        public int? U5 { get; set; } = 0;
        public int? K5 { get; set; } = 0;
        public int? U6 { get; set; } = 0;
        public int? K6 { get; set; } = 0;
        public int? U7 { get; set; } = 0;
        public int? K7 { get; set; } = 0;
        public int? U8 { get; set; } = 0;
        public int? K8 { get; set; } = 0;
        public int? U9 { get; set; } = 0;
        public int? K9 { get; set; } = 0;
        public int? U10 { get; set; } = 0;
        public int? K10 { get; set; } = 0;
        public int? U11 { get; set; } = 0;
        public int? K11 { get; set; } = 0;
        public int? U12 { get; set; } = 0;
        public int? K12 { get; set; } = 0;


        public async static Task<ZeiterfKrankenstandUrlaub> GetByMitarbeiterAndDate(string mitarbeiterId, string name, int year)
        {
            try
            {
                if (UOW.Uow._uow == null || !UOW.Uow._uow.IsConnected)
                {
                    UOW.Uow.Connect();
                }

                DateTime von = new DateTime(year, 1, 1);
                DateTime bis = new DateTime(year, 12, 31);

                var urlaub = await UOW.Uow._uow.Query<zeiterfurlaub>()
                    .Where(x => x.MitarbeiterId == mitarbeiterId && x.Datum.Value.Date >= von.Date && x.Datum.Value.Date <= bis.Date)
                    .OrderBy(x => x.Datum)
                    .ToListAsync();

                ZeiterfKrankenstandUrlaub ku = new ZeiterfKrankenstandUrlaub();
                
                ku.MitarbeiterID = mitarbeiterId;
                ku.MitarbeiterName = name;
                if(urlaub != null && urlaub.Count >= 0)
                {
                    foreach(var item in urlaub)
                    {
                        DateTime datum = Convert.ToDateTime(item.Datum);
                        int month = datum.Month;

                        switch(month)
                        {
                            case 1:
                                ku.U1 += 1;
                                break;
                            case 2:
                                ku.U2 += 1;
                                break;
                            case 3:
                                ku.U3 += 1;
                                break;
                            case 4:
                                ku.U4 += 1;
                                break;
                            case 5:
                                ku.U5 += 1;
                                break;
                            case 6:
                                ku.U6 += 1;
                                break;
                            case 7:
                                ku.U7 += 1;
                                break;
                            case 8:
                                ku.U8 += 1;
                                break;
                            case 9:
                                ku.U9 += 1;
                                break;
                            case 10:
                                ku.U10 += 1;
                                break;
                            case 11:
                                ku.U11 += 1;
                                break;
                            case 12:
                                ku.U12 += 1;
                                break;
                        }
                    }
                }
                var krank = await UOW.Uow._uow
                    .Query<zeiterfkrank>()
                    .Where(
                        x => x.MitarbeiterId == mitarbeiterId &&
                            x.Datum.Value.Date >= von.Date &&
                            x.Datum.Value.Date <= bis.Date)
                    .OrderBy(x => x.Datum)
                    .ToListAsync();

                if (krank != null || krank.Count > 0)
                {
                    foreach (var item in krank)
                    {
                        DateTime datum = Convert.ToDateTime(item.Datum);
                        int month = datum.Month;

                        switch (month)
                        {
                            case 1:
                                ku.K1 += 1;
                                break;
                            case 2:
                                ku.K2 += 1;
                                break;
                            case 3:
                                ku.K3 += 1;
                                break;
                            case 4:
                                ku.K4 += 1;
                                break;
                            case 5:
                                ku.K5 += 1;
                                break;
                            case 6:
                                ku.K6 += 1;
                                break;
                            case 7:
                                ku.K7 += 1;
                                break;
                            case 8:
                                ku.K8 += 1;
                                break;
                            case 9:
                                ku.K9 += 1;
                                break;
                            case 10:
                                ku.K10 += 1;
                                break;
                            case 11:
                                ku.K11 += 1;
                                break;
                            case 12:
                                ku.K12 += 1;
                                break;

                        }
                    }
                }

                if (ku.U1 == 0) { ku.U1 = null; }
                if (ku.K1 == 0) { ku.K1 = null; }
                if (ku.U2 == 0) { ku.U2 = null; }
                if (ku.K2 == 0) { ku.K2 = null; }
                if (ku.U3 == 0) { ku.U3 = null; }
                if (ku.K3 == 0) { ku.K3 = null; }
                if (ku.U4 == 0) { ku.U4 = null; }
                if (ku.K4 == 0) { ku.K4 = null; }
                if (ku.U5 == 0) { ku.U5 = null; }
                if (ku.K5 == 0) { ku.K5 = null; }
                if (ku.U6 == 0) { ku.U6 = null; }
                if (ku.K6 == 0) { ku.K6 = null; }
                if (ku.U7 == 0) { ku.U7 = null; }
                if (ku.K7 == 0) { ku.K7 = null; }
                if (ku.U8 == 0) { ku.U8 = null; }
                if (ku.K8 == 0) { ku.K8 = null; }
                if (ku.U9 == 0) { ku.U9 = null; }
                if (ku.K9 == 0) { ku.K9 = null; }
                if (ku.U10 == 0) { ku.U10 = null; }
                if (ku.K10 == 0) { ku.K10 = null; }
                if (ku.U11 == 0) { ku.U11 = null; }
                if (ku.K11 == 0) { ku.K11 = null; }
                if (ku.U12 == 0) { ku.U12 = null; }
                if (ku.K12 == 0) { ku.K12 = null; }

                return ku;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
