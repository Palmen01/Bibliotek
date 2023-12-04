using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Bibliotek
{
    internal class Låntagare
    {
        private string namn;
        private long personnummer;
        public List<Bok> lånadeböcker;

        public Låntagare(string Namn, long Personnummer)
        {
            namn = Namn;
            personnummer = Personnummer;
            lånadeböcker = new List<Bok>();
        }

        public string Namn
        {
            get { return namn; }
            set { namn = value; }
        }

        public long Personnummer
        {
            get { return personnummer; }
            set { personnummer = value; }
        }
    }
}
