using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BusinessLogic.Zeiterfassung
{
    public class Feiertag : IComparable<Feiertag>
    {
        private bool isFix;
        private DateTime datum;
        private string name;

        public Feiertag(bool isFix, DateTime datum, string name)
        {
            this.IsFix = isFix;
            this.Datum = datum;
            this.Name = name;

        }

        /// <summary>
        /// Beschreibung: 
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        /// <summary>
        /// Beschreibung: 
        /// </summary>
        public DateTime Datum
        {
            get
            {
                return datum;
            }
            set
            {
                datum = value;
            }
        }


        /// <summary>
        /// Beschreibung: 
        /// </summary>
        public bool IsFix
        {
            get
            {
                return isFix;
            }
            set
            {
                isFix = value;
            }
        }


        #region IComparable<Feiertag> Member

        public int CompareTo(Feiertag other)
        {
            return this.datum.Date.CompareTo(other.datum.Date);
        }

        #endregion
    }
}
