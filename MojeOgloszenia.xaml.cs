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
using System.IO;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Navigation;
using static Projekt_zaliczenie.App;
using System.Security.Cryptography.X509Certificates;

namespace Projekt_zaliczenie
{
    /// <summary>
    /// Logika interakcji dla klasy MojeOgloszenia.xaml
    /// </summary>
    public partial class MojeOgloszenia : Window
    {
        public string Email { get; set; }
        public string IdOgloszenia { get; set; }

        public MojeOgloszenia(string email)
        {
            InitializeComponent();
            Email = email;
            LoadOgloszenia(Email);
        }

        private void LoadOgloszenia(string email)
        {
            string dbpath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string query = "SELECT * FROM OgloszeniaTable WHERE Email = @Email";
                var command = new SqliteCommand(query, db);
                command.Parameters.AddWithValue("@Email", email);

                SqliteDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    int id = reader.GetInt32(0);
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


                    OgloszeniaList.Items.Add(ogloszenia);
                }
            }
        }

        private void ButtonDodajOgloszenie_Clicked(object sender, RoutedEventArgs e)
        {
            DodajOgloszenie s1 = new DodajOgloszenie(Email);
            s1.Show();
            Close();
        }

        private void ButtonEdytacjaOgloszenia_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int ogloszenieid = (int)button.CommandParameter;
            DodajOgloszenie ogloszenie = new DodajOgloszenie(ogloszenieid, Email);
            ogloszenie.Show();
            Close();
        }

        private void ButtonOgloszenie_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int ogloszenieid = (int)button.CommandParameter;
            StronaOgloszenia ogloszenie = new StronaOgloszenia(ogloszenieid, Email);
            ogloszenie.Show();
            Close();
        }

        private void ButtonUsuniecieOgloszenia_Clicked(Object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int productId = (int)button.CommandParameter;
            DataAcces.DeleteOgloszenie(productId);
            MessageBox.Show("Ogłoszenie zostało usunięte pomyślnie!");
            MojeOgloszenia s1 = new MojeOgloszenia(Email);
            s1.Show();
            Close();
        }

        private void Button_Konto(object sender, EventArgs e)
        {
            Konto s1 = new Konto(Email);
            s1.Show();
            Close();
        }

        private void Button_StronaGlowna(object sender, EventArgs e)
        {
            MainWindow s1 = new MainWindow(Email);
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
