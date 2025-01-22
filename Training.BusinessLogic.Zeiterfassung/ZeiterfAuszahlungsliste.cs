using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class ZeiterfAuszahlungsliste
    {
        public string Name { get; set; }
        public string Stunden { get; set; }
        public string Ueberstunden { get; set; }
        public string Feiertagsstunden { get; set; }
        public string MitarbeiterID { get; set; }
        public bool Eismeister { get; set; }
        public string Zeitmodell { get; set; }
        public string FeiertagsstundenVM { get; set; }
        public string NH3Stunden { get; set; }
        public string NH3Prozent { get; set; }

    }

    public class AuszahlungslisteHeader
    {
        public string Spielstaette { get; set; }
        public string Datum { get; set; }
        public string SummeStunden { get; set; }
        public string SummeAusbezahlt { get; set; }
        public string SummeFT { get; set; }
        public string SummeFTVM { get; set; }
    }
}
