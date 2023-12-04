using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotek
{
    internal class Bok
    {
        private string titel;
        private string författare;
        private bool utlånad;

        public Bok(string bokTitel, string BokFörfattare)
        {
            titel = bokTitel;
            författare = BokFörfattare;
            utlånad = false;
        }

        public string Titel
        {
            get { return titel; }
            set { titel = value; }
        }
        public string Författare
        {
            get { return författare; }
            set { författare = value; }
        }
        public bool Utlånad
        {
            get { return utlånad; }
            set { utlånad = value; }
        }
    }
}
