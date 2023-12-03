using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static Projekt_zaliczenie.App;

namespace Projekt_zaliczenie
{
    /// <summary>
    /// Logika interakcji dla klasy StronaOgloszenia.xaml
    /// </summary>
    public partial class StronaOgloszenia : Window
    {
        public string Email { get; set; }
        public int IdOgloszenia { get; set; }
        public string UserEmail { get; set; }
        public StronaOgloszenia(int id, string email)
        {
            InitializeComponent();
            IdOgloszenia = id;
            Email = email;
            Ogloszenia ogloszenie = LoadOgloszenia(IdOgloszenia);

            DataContext = ogloszenie;

            Ogloszenia useremail = GetUserEmailByOgloszenie(IdOgloszenia);
            UserEmail = useremail.Email;
        }

        public StronaOgloszenia(int id)
        {
            InitializeComponent();
            IdOgloszenia = id;
            Email = null;
            buttonOgloszenia.Visibility = Visibility.Collapsed;
            buttonStronaGlowna.Background = Brushes.Black;
            buttonStronaGlowna.Foreground = Brushes.White;
            Ogloszenia ogloszenie = LoadOgloszenia(IdOgloszenia);

            DataContext = ogloszenie;

            Ogloszenia useremail = GetUserEmailByOgloszenie(IdOgloszenia);
            UserEmail = useremail.Email;
        }

        public Ogloszenia GetUserEmailByOgloszenie(int id)
        {
            string dbpath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string query = "SELECT * FROM OgloszeniaTable WHERE Id = @Id";
                var command = new SqliteCommand(query, db);
                command.Parameters.AddWithValue("@Id", id);

                SqliteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int idd = reader.GetInt32(0);
                    string nazwaOgloszenia = reader.GetString(1);
                    string zdjecie = reader.GetString(2);
                    string firma = reader.GetString(3);
                    string nazwastanowiska = reader.GetString(4);
                    string poziomZatrudnienia = reader.GetString(5);
                    string rodzajUmowy = reader.GetString(6);
                    string rodzajPracy = reader.GetString(7);
                    string pracaZdalna = reader.GetString(8);
                    string wynagrodzenie = reader.GetString(9);
                    string dniPracy = reader.GetString(10);
                    string godzinyPracy = reader.GetString(11);
                    string data = reader.GetString(12);
                    string kategoria = reader.GetString(13);
                    string ppisStanowiska = reader.GetString(14);
                    string wymagania = reader.GetString(15);
                    string emailll = reader.GetString(16);



                    Ogloszenia ogloszenia = new Ogloszenia
                    {
                        Id = id,
                        NazwaOgloszenia = nazwaOgloszenia,
                        Zdjecie = zdjecie,
                        Firma = firma,
                        Nazwastanowiska = nazwastanowiska,
                        PoziomZatrudnienia = poziomZatrudnienia,
                        RodzajUmowy = rodzajUmowy,
                        RodzajPracy = rodzajPracy,
                        PracaZdalna = pracaZdalna,
                        Wynagrodzenie = wynagrodzenie,
                        DniPracy = dniPracy,
                        GodzinyPracy = godzinyPracy,
                        DataWygasniecia = data,
                        Kategoria = kategoria,
                        OpisStanowiska = ppisStanowiska,
                        Wymagania = wymagania,
                        Email = emailll,
                    };

                    return ogloszenia;
                }


            }

            return null; // Zwróć null, jeśli użytkownik o podanym emailu nie istnieje
        }

        public Users GetUserByEmail(string email)
        {
            string dbpath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string query = "SELECT * FROM UsersTable WHERE Email = @Email";
                var command = new SqliteCommand(query, db);
                command.Parameters.AddWithValue("@Email", email);

                SqliteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int idd = reader.GetInt32(0);
                    string imie = reader.GetString(1);
                    string nazwisko = reader.GetString(2);
                    string dataUrodzenia = reader.GetString(3);
                    string emaill = reader.GetString(4);
                    string telefon = reader.GetString(5);
                    string zdjProfilowe = reader.GetString(6);
                    string adres = reader.GetString(7);
                    string stanowsikoPracy = reader.GetString(8);
                    string opisStanowiskaPracy = reader.GetString(9);
                    string podsumowanieZawodowe = reader.GetString(10);
                    string profilGitHub = reader.GetString(11);
                    string haslo = reader.GetString(12);

                    Users user = new Users(idd, imie, nazwisko, dataUrodzenia, emaill, telefon, zdjProfilowe, adres, stanowsikoPracy, opisStanowiskaPracy, podsumowanieZawodowe, profilGitHub, haslo)
                    {
                        Id = idd,
                        Imie = imie,
                        Nazwisko = nazwisko,
                        DataUrodzenia = dataUrodzenia,
                        Email = emaill,
                        Telefon = telefon,
                        ZdjProfilowe = zdjProfilowe,
                        Adres = adres,
                        StanowsikoPracy = stanowsikoPracy,
                        OpisStanowiskaPracy = opisStanowiskaPracy,
                        PodsumowanieZawodowe = podsumowanieZawodowe,
                        ProfilGitHub = profilGitHub,
                        Haslo = haslo
                        // Dodaj inne pola, jeśli istnieją
                    };

                    return user;
                }


            }

            return null; // Zwróć null, jeśli użytkownik o podanym emailu nie istnieje
        }

        public Ogloszenia LoadOgloszenia(int id)
        {
            string dbpath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string query = "SELECT * FROM OgloszeniaTable WHERE Id = @Id";
                var command = new SqliteCommand(query, db);
                command.Parameters.AddWithValue("@Id", id);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int idd = reader.GetInt32(0);
                    string nazwaOgloszenia = reader.GetString(1);
                    string zdjecie = reader.GetString(2);
                    string firma = reader.GetString(3);
                    string nazwastanowiska = reader.GetString(4);
                    string poziomZatrudnienia = reader.GetString(5);
                    string rodzajUmowy = reader.GetString(6);
                    string rodzajPracy = reader.GetString(7);
                    string pracaZdalna = reader.GetString(8);
                    string wynagrodzenie = reader.GetString(9);
                    string dniPracy = reader.GetString(10);
                    string godzinyPracy = reader.GetString(11);
                    string data = reader.GetString(12);
                    string kategoria = reader.GetString(13);
                    string ppisStanowiska = reader.GetString(14);
                    string wymagania = reader.GetString(15);
                    string emailll = reader.GetString(16);



                    Ogloszenia ogloszenia = new Ogloszenia
                    {
                        Id = idd,
                        NazwaOgloszenia = nazwaOgloszenia,
                        Zdjecie = zdjecie,
                        Firma = firma,
                        Nazwastanowiska = nazwastanowiska,
                        PoziomZatrudnienia = poziomZatrudnienia,
                        RodzajUmowy = rodzajUmowy,
                        RodzajPracy = rodzajPracy,
                        PracaZdalna = pracaZdalna,
                        Wynagrodzenie = wynagrodzenie,
                        DniPracy = dniPracy,
                        GodzinyPracy = godzinyPracy,
                        DataWygasniecia = data,
                        Kategoria = kategoria,
                        OpisStanowiska = ppisStanowiska,
                        Wymagania = wymagania,
                        Email = emailll
                    };


                    return ogloszenia;
                }
            }
            return null;
        }


        private void Button_Informacje(object sender, EventArgs e )
        {
            Users user = GetUserByEmail(UserEmail);
            MessageBox.Show($"Użytkownik: {user.Imie} {user.Nazwisko}\nEmail: {user.Email}\nTelefon: {user.Telefon}", "Informacje o użytkowniku", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Konto(object sender, EventArgs e)
        {
            Konto s1 = new Konto(Email);
            s1.Show();
            Close();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            AppSession.IsUserLoggedIn = false;
            AppSession.LoggedInUserEmail = null;

            Logowanie s1 = new Logowanie();
            s1.Show();
            Close();
        }

        private void Button_MojeOgloszenia(object sender, EventArgs e)
        {
            if(Email == null)
            {
                MainWindow s1 = new MainWindow();
                s1.Show();
                Close();
            }
            else
            {
                MojeOgloszenia s1 = new MojeOgloszenia(Email);
                s1.Show();
                Close();
            }
        }

        private void Button_StronaGlowna(object sender, EventArgs e)
        {
            MainWindow s1 = new MainWindow(Email);
            s1.Show();
            Close();
        }
    }

}
