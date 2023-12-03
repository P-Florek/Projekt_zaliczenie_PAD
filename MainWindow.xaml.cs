using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Projekt_zaliczenie.App;

namespace Projekt_zaliczenie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string UserEmail { get; set; }
        public MainWindow(string email)
        {
            InitializeComponent();
            UserEmail = email;
            LoadOgloszenia();
            
        }
        public MainWindow()
        {
            InitializeComponent();
            LoadOgloszenia();
            UserEmail = null;
            ButtonZaloguj.Content = "Zaloguj się";
            buttonKonto.Visibility = Visibility.Collapsed;
            buttonOgloszenia.Visibility = Visibility.Collapsed;
        }

        private void LoadOgloszenia()
        {
            string dbpath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string query = "SELECT * FROM OgloszeniaTable";
                var command = new SqliteCommand(query, db);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
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

        private void ButtonOgloszenie_Clicked(object sender, RoutedEventArgs e)
        {
            if(UserEmail == null)
            {
                Button button = (Button)sender;
                int ogloszenieid = (int)button.CommandParameter;
                StronaOgloszenia ogloszenie = new StronaOgloszenia(ogloszenieid);
                ogloszenie.Show();
                Close();
            }
            else
            {
                Button button = (Button)sender;
                int ogloszenieid = (int)button.CommandParameter;
                StronaOgloszenia ogloszenie = new StronaOgloszenia(ogloszenieid, UserEmail);
                ogloszenie.Show();
                Close();
            }
        }

        private void Filtry(object sender, RoutedEventArgs e)
        {
            string dbpath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            OgloszeniaList.Items.Clear();

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string selectedRodzajUmowy = (RodzajUmowyText.SelectedItem as ComboBoxItem)?.Content.ToString();
                string selectedPracaZdalna = (PracaZdalnaText.SelectedItem as ComboBoxItem)?.Content.ToString();
                string selectedSortowanie = (SortowanieText.SelectedItem as ComboBoxItem)?.Content.ToString();
                string selectedKategorie = (KategorieText.SelectedItem as ComboBoxItem)?.Content.ToString();

                string query = "SELECT * FROM OgloszeniaTable WHERE 1=1"; // Początkowe zapytanie, aby uprościć późniejsze dodawanie warunków

                if (!string.IsNullOrEmpty(selectedRodzajUmowy))
                {
                    query += $" AND RodzajUmowy = '{selectedRodzajUmowy}'";
                }

                if (!string.IsNullOrEmpty(selectedPracaZdalna))
                {
                    query += $" AND PracaZdalna = '{selectedPracaZdalna}'";
                }

                if (!string.IsNullOrEmpty(selectedKategorie))
                {
                    if(selectedKategorie == "Wszystkie")
                    {

                    }
                    else
                    {
                        query += $" AND Kategoria = '{selectedKategorie}'";
                    }
                }

                if (!string.IsNullOrEmpty(selectedSortowanie))
                {
                    if (selectedSortowanie == "Najdrozsze")
                    {
                        query += " ORDER BY CAST(Wynagrodzenie AS DECIMAL) DESC";
                    }
                    else if (selectedSortowanie == "Najtansze")
                    {
                        query += " ORDER BY CAST(Wynagrodzenie AS DECIMAL) ASC";
                    }
                }

                Console.WriteLine($"Aktualne zapytanie SQL: {query}");

                var command = new SqliteCommand(query, db);

                SqliteDataReader reader = command.ExecuteReader();

                while (reader.Read())
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

        private void ComboBox_PracaZdalna(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_Sortowanie(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_Kategorie(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Boots(object sender, RoutedEventArgs e)
        {
/*            productsContainer.Items.Clear();

            string connectionString = "Data Source=ProjektSklep.db";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Name, Price, image1 FROM Products WHERE Page='Wyprzedaz' AND Type = 'Buty'";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        decimal price = reader.GetDecimal(2);
                        string image = reader.GetString(3);


                        Product product = new Product(id, name, price, image)
                        {
                            Id = id,
                            Name = name,
                            Price = price,
                            Image = image
                        };



                        productsContainer.Items.Add(product);
                    }
                }
            }*/
        }

        private void Button_Bluzy(object sender, RoutedEventArgs e)
        {
/*            productsContainer.Items.Clear();

            string connectionString = "Data Source=ProjektSklep.db";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Name, Price, image1 FROM Products WHERE Page='Wyprzedaz' AND Type = 'Bluzy'";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        decimal price = reader.GetDecimal(2);
                        string image = reader.GetString(3);


                        Product product = new Product(id, name, price, image)
                        {
                            Id = id,
                            Name = name,
                            Price = price,
                            Image = image
                        };



                        productsContainer.Items.Add(product);
                    }
                }
            }*/
        }
        private void Button_Dress(object sender, RoutedEventArgs e)
        {
/*            productsContainer.Items.Clear();

            string connectionString = "Data Source=ProjektSklep.db";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Name, Price, image1 FROM Products WHERE Page='Wyprzedaz' AND Type = 'Spodnie'";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        decimal price = reader.GetDecimal(2);
                        string image = reader.GetString(3);


                        Product product = new Product(id, name, price, image)
                        {
                            Id = id,
                            Name = name,
                            Price = price,
                            Image = image
                        };



                        productsContainer.Items.Add(product);
                    }
                }
            }*/
        }
        private void Button_AscPrice(object sender, RoutedEventArgs e)
        {
/*            productsContainer.Items.Clear();

            string connectionString = "Data Source=ProjektSklep.db";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Name, Price, image1 FROM Products WHERE Page='Wyprzedaz' ORDER BY Price ASC";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        decimal price = reader.GetDecimal(2);
                        string image = reader.GetString(3);


                        Product product = new Product(id, name, price, image)
                        {
                            Id = id,
                            Name = name,
                            Price = price,
                            Image = image
                        };



                        productsContainer.Items.Add(product);
                    }
                }
            }*/
        }
        private void Button_DescPrice(object sender, RoutedEventArgs e)
        {
/*            productsContainer.Items.Clear();

            string connectionString = "Data Source=ProjektSklep.db";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Id, Name, Price, image1 FROM Products WHERE Page='Wyprzedaz' ORDER BY Price DESC";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        decimal price = reader.GetDecimal(2);
                        string image = reader.GetString(3);


                        Product product = new Product(id, name, price, image)
                        {
                            Id = id,
                            Name = name,
                            Price = price,
                            Image = image
                        };



                        productsContainer.Items.Add(product);
                    }
                }
            }*/
        }
        private void Button_All(object sender, RoutedEventArgs e)
        {
/*            productsContainer.Items.Clear();
            LoadProductsFromDatabase();*/
        }

        private void Button_Konto(object sender, EventArgs e)
        {
            Konto s1 = new Konto(UserEmail);
            s1.Show();
            Close();
        }

        private void Button_MojeOgloszenia(object sender, EventArgs e)
        {
            MojeOgloszenia s1 = new MojeOgloszenia(UserEmail);
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
