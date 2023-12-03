using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_zaliczenie
{
    public class Ogloszenia
    {
        public int Id { get; set; }
        public string NazwaOgloszenia { get; set; }

        public string Zdjecie { get; set; }
        public string Firma { get; set; }
        public string Nazwastanowiska { get; set; }
        public string PoziomZatrudnienia { get; set; }
        public string RodzajUmowy { get; set; }
        public string RodzajPracy { get; set; }
        public string PracaZdalna { get; set; }
        public string Wynagrodzenie { get; set; }
        public string DniPracy { get; set; }
        public string GodzinyPracy { get; set; }
        public string DataWygasniecia { get; set; }
        public string Kategoria { get; set; }
        public string OpisStanowiska { get; set; }
        public string Wymagania { get; set; }
        public string Email { get; set; }


        public Ogloszenia()     
        { 
            
        }
    }
}
