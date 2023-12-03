using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Runtime.InteropServices;
using System.Security;
using System.Xml.Linq;

namespace Projekt_zaliczenie
{
    /// <summary>
    /// Logika interakcji dla klasy Rejestracja.xaml
    /// </summary>
    public partial class Rejestracja : Window
    {
        public Rejestracja()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Users nowyUzytkownik = new Users
            {
                Imie = imieTextBox.Text,
                Nazwisko = nazwiskoTextBox.Text,
                DataUrodzenia = DataurodzeniaTextBox.Text,
                Email = emailTextBox.Text,
                Telefon = telefonTextBox.Text,
                ZdjProfilowe = ProfiloweTextBox.Text,
                Adres = AdresTextBox.Text,
                StanowsikoPracy = StanowiskoTextBox.Text,
                OpisStanowiskaPracy = OpisTextBox.Text,
                PodsumowanieZawodowe = PodsumowanieTextBox.Text,
                ProfilGitHub = GitHubTextBox.Text,
                Haslo = SecureStringToString(paswordTextBox.SecurePassword)
            };

            if (string.IsNullOrWhiteSpace(imieTextBox.Text) || string.IsNullOrWhiteSpace(nazwiskoTextBox.Text) || string.IsNullOrWhiteSpace(DataurodzeniaTextBox.Text) || string.IsNullOrWhiteSpace(emailTextBox.Text) || string.IsNullOrWhiteSpace(telefonTextBox.Text) || string.IsNullOrWhiteSpace(ProfiloweTextBox.Text) || string.IsNullOrWhiteSpace(AdresTextBox.Text) || string.IsNullOrWhiteSpace(StanowiskoTextBox.Text) || string.IsNullOrWhiteSpace(OpisTextBox.Text) || string.IsNullOrWhiteSpace(PodsumowanieTextBox.Text) || string.IsNullOrWhiteSpace(GitHubTextBox.Text) || string.IsNullOrWhiteSpace(SecureStringToString(paswordTextBox.SecurePassword)))
            {
                MessageBox.Show("Uzupełnij wszystkie pola !");
                return;
            }
            if (!Regex.IsMatch(emailTextBox.Text, @"^[^\s]+$") || !Regex.IsMatch(imieTextBox.Text, @"^[^\s]+$") || !Regex.IsMatch(nazwiskoTextBox.Text, @"^[^\s]+$") || !Regex.IsMatch(SecureStringToString(paswordTextBox.SecurePassword), @"^[^\s]+$"))
            {
                MessageBox.Show("Nie wolno wpisywać spacji !");
                return;
            }
            if (!Regex.IsMatch(emailTextBox.Text, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
            {
                MessageBox.Show("email jest niepoprawny !");
                return;
            }

            DataAcces.AddUser(nowyUzytkownik);

            MessageBox.Show("Użytkownik dodany pomyślnie!", "Sukces", MessageBoxButton.OK);

            Logowanie s1 = new Logowanie();
            s1.Show();
            Close();
        }

        private string SecureStringToString(SecureString secureString)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
