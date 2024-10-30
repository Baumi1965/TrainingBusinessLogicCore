using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BusinessLogic.Gebucht
{
    public class Anzeige
    {
        public int Index { get; set; }
        public String Verband { get; set; }
        public String VName { get; set; }
        public String NName { get; set; }
        public String Datum { get; set; }
        public String Typ { get; set; }
        public String Zutritt { get; set; }
        public String Kuerklasse { get; set; }
        public Double Guthaben { get; set; }
        public string AKader { get; set; }

        public Anzeige(int index, string verband, string vname, string nname, string datum, string typ, string zutritt, string kuerklasse, double guthaben, string akader)
        {
            Index = index;
            Verband = verband;
            VName = vname;
            NName = nname;
            Datum = datum;
            Typ = typ;
            Zutritt = zutritt;
            Kuerklasse = kuerklasse;
            Guthaben = guthaben;
            AKader = akader;
        }
    }
}
