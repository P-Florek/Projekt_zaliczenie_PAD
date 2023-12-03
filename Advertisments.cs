using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_zaliczenie
{
    internal class Advertisments
    {
        public int Id { get; set; }
        public int FirmaId { get; set; }
        public string NazwaStanowiska { get; set; }
        public string PoziomZatrudnienia { get; set; }
        public string RodzajUmowy { get; set; }
        public string Wynagrodzenie { get; set; }
        public string DniPracy { get; set; }
        public string GodzinyPracy { get; set; }
        public string DataWygasniecia { get; set; }
        public string Kategoria { get; set; }
        public string OpisStanowiska { get; set; }
        public string Wymagania { get; set; }
        public string Oferty { get; set; }

        public Advertisments()
        {

        }

    }
}
