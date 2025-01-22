using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfArbeitsleistung
    {
        public string MitarbeiterID { get; set; }
        public string Name { get; set; }
        public string StdAnwesend { get; set; }
        public string Urlaub { get; set; }
        public string Krank { get; set; }
        public string Feiertag { get; set; }
        public string Ueberstunden { get; set; }
        public string StdAnwesendVM { get; set; }
        public string UrlaubVM { get; set; }
        public string KrankVM { get; set; }
        public string FeiertagVM { get;set; }
        public string UeberstundenVM { get; set; }
    }

    public class ArbeitsleistungHeader
    {
        public string Spielstaette { get; set; }
        public string Datum { get; set; }
        public string SummeStunden { get; set; }
        public string SummeAusbezahlt { get; set; }
        public string SummeFT { get; set; }
        public string SummeFTVM { get; set; }
        public string DatumVM { get; set; }
        public string SummeUrlaub { get; set; }
        public string SummeUrlaubVM { get; set; }
        public string SummeAusbezahltVM { get; set; }
        public string SummeStundenVM { get; set; }
        public string Krank { get; set; }
        public string KrankVM { get; set; }

    }
}
