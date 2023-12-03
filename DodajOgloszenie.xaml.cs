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
    /// Logika interakcji dla klasy DodajOgloszenie.xaml
    /// </summary>
    public partial class DodajOgloszenie : Window
    {
        public string Emaill { get; set; }
        public int IdOgloszenie { get; set; }
        
        public DodajOgloszenie(string email)
        {
            InitializeComponent();
            Emaill = email;
            IdOgloszenie = 0;
        }

        public DodajOgloszenie(int id, string email)
        {
            InitializeComponent();
            IdOgloszenie = id;
            Emaill = email;
            button.Content = "Edytuj";
            ogloszenieTextBox.Content = "Edytuj ogłoszenie";

            Ogloszenia ogloszenie = LoadOgloszenia(IdOgloszenie);

            DataContext = ogloszenie;
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

        private void ButtonDodajOgloszenie_Clicked(object sender, RoutedEventArgs e)
        {
            if(IdOgloszenie == 0)
            {
                Ogloszenia noweOgloszenie = new Ogloszenia()
                {
                    NazwaOgloszenia = TitleTextBox.Text,
                    Zdjecie = ZdjTextBox.Text,
                    Firma = FirmaTextBox.Text,
                    Nazwastanowiska = NazwaStanowiskaTextBox.Text,
                    PoziomZatrudnienia = PoziomZatrudnieniaTextBox.Text,
                    RodzajUmowy = RodzajUmowyTextBox.Text,
                    RodzajPracy = RodzajPracyTextBox.Text,
                    PracaZdalna = PracaZdalnaTextBox.Text,
                    Wynagrodzenie = WynagrodzenieTextBox.Text,
                    DniPracy = DniPracyTextBox.Text,
                    GodzinyPracy = GodzinyPracyTextBox.Text,
                    DataWygasniecia = DataWygasnieciaTextBox.Text,
                    Kategoria = KategoriaTextBox.Text,
                    OpisStanowiska = OpisStanowiskaTextBox.Text,
                    Wymagania = WymaganiaTextBox.Text,
                    Email = Emaill
                };

                DataAcces.AddOgloszenie(noweOgloszenie);

                MessageBox.Show("Ogłoszenie zostało dodane pomyślnie!");

                MojeOgloszenia s1 = new MojeOgloszenia(Emaill);
                s1.Show();
                Close();
            }
            else
            {
                Ogloszenia ogloszenieToUpdate = new Ogloszenia()
                {
                    Id = IdOgloszenie,
                    NazwaOgloszenia = TitleTextBox.Text,
                    Zdjecie = ZdjTextBox.Text,
                    Firma = FirmaTextBox.Text,
                    Nazwastanowiska = NazwaStanowiskaTextBox.Text,
                    PoziomZatrudnienia = PoziomZatrudnieniaTextBox.Text,
                    RodzajUmowy = RodzajUmowyTextBox.Text,
                    RodzajPracy = RodzajPracyTextBox.Text,
                    PracaZdalna = PracaZdalnaTextBox.Text,
                    Wynagrodzenie = WynagrodzenieTextBox.Text,
                    DniPracy = DniPracyTextBox.Text,
                    GodzinyPracy = GodzinyPracyTextBox.Text,
                    DataWygasniecia = DataWygasnieciaTextBox.Text,
                    Kategoria = KategoriaTextBox.Text,
                    OpisStanowiska = OpisStanowiskaTextBox.Text,
                    Wymagania = WymaganiaTextBox.Text,
                    Email = Emaill
                };

                DataAcces.UpdateOgloszenie(ogloszenieToUpdate);
                MessageBox.Show("Ogłoszenie zaktualizowane pomyślnie!");
                MojeOgloszenia ogloszenia = new MojeOgloszenia(Emaill);
                ogloszenia.Show();
                Close();
            }

        }

        private void ButtonCofnij_Clicked(object sender, EventArgs e)
        {
            MojeOgloszenia s1 = new MojeOgloszenia(Emaill);
            s1.Show();
            Close();
        }

        private void Button_Konto(object sender, EventArgs e)
        {
            Konto s1 = new Konto(Emaill);
            s1.Show();
            Close();
        }

        private void Button_StronaGlowna(object sender, EventArgs e)
        {
            MainWindow s1 = new MainWindow(Emaill);
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
    }
}
