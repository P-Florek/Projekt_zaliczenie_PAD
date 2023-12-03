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

namespace Projekt_zaliczenie
{
    /// <summary>
    /// Logika interakcji dla klasy EdytacjaDanych.xaml
    /// </summary>
    public partial class EdytacjaDanych : Window
    {
        public string Email { get; set; }
        public EdytacjaDanych(string email)
        {
            InitializeComponent();
            Email = email;

            Users user = GetUserByEmail(Email);

            DataContext = user;
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
                    int id = reader.GetInt32(0);
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

                    Users user = new Users(id, imie, nazwisko, dataUrodzenia, email, telefon, zdjProfilowe, adres, stanowsikoPracy, opisStanowiskaPracy, podsumowanieZawodowe, profilGitHub, haslo)
                    {
                        Id = id,
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

        private void ButtonEdytacjaDanych_Clicked(object sender, EventArgs e)
        {
            Users userToUpdate = new Users()
            {
                Imie = imieTextBox.Text,
                Nazwisko = nazwiskoTextBox.Text,
                DataUrodzenia = DataurodzeniaTextBox.Text,
                Email = emailTextBox.Text,
                Telefon= telefonTextBox.Text,
                ZdjProfilowe = ProfiloweTextBox.Text,
                Adres = AdresTextBox.Text,
                StanowsikoPracy = StanowiskoTextBox.Text,
                OpisStanowiskaPracy = OpisTextBox.Text,
                PodsumowanieZawodowe = PodsumowanieTextBox.Text,
                ProfilGitHub = GitHubTextBox.Text,
                Haslo = paswordTextBox.Text
            };

            DataAcces.UpdateUser(userToUpdate);
            MessageBox.Show("Użytkownik zaktualizowany pomyślnie!");
            Konto s1 = new Konto(Email);
            s1.Show();
            Close();
        }

        private void ButtonCofnij_Clicked(object sender, RoutedEventArgs e)
        {
            Konto s1 = new Konto(Email);
            s1.Show();
            Close();
        }
    }
}
