using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_zaliczenie
{
    public class Users
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string DataUrodzenia { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string ZdjProfilowe { get; set; }
        public string Adres { get; set; }
        public string StanowsikoPracy { get; set; }
        public string OpisStanowiskaPracy { get; set; }
        public string PodsumowanieZawodowe { get; set; }
        public string ProfilGitHub { get; set; }
        public string Haslo { get; set; }

        public Users()
        {

        }

        public Users(string imie, string nazwisko, string dataUr, string email, string telefon, string zdjProf, string adres, string stanowsiko, string opistanowiska, string podsumowanie, string profil, string haslo)
        {
            Imie = imie;
            Nazwisko = nazwisko;
            DataUrodzenia = dataUr;
            Email = email;
            Telefon = telefon;
            ZdjProfilowe = zdjProf;
            Adres = adres;
            StanowsikoPracy = stanowsiko;
            OpisStanowiskaPracy = opistanowiska;
            PodsumowanieZawodowe = podsumowanie;
            ProfilGitHub = profil;
            Haslo = haslo;
        }

        public Users(int id, string imie, string nazwisko, string dataUr, string email, string telefon, string zdjProf, string adres, string stanowsiko, string opistanowiska, string podsumowanie, string profil, string haslo)
        {
            Id = id;
            Imie = imie;
            Nazwisko = nazwisko;
            DataUrodzenia = dataUr;
            Email = email;
            Telefon = telefon;
            ZdjProfilowe= zdjProf;
            Adres = adres;
            StanowsikoPracy= stanowsiko;
            OpisStanowiskaPracy= opistanowiska;
            PodsumowanieZawodowe= podsumowanie;
            ProfilGitHub = profil;
            Haslo = haslo;
        }
    }
}
