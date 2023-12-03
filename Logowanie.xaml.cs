using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
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
using static Projekt_zaliczenie.App;

namespace Projekt_zaliczenie
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Logowanie : Window
    {
        public string Email { get; set; }
        public Logowanie()
        {
            InitializeComponent();
            DataAcces.InitializeDatabase();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = emailTextBox.Text;
            string haslo = SecureStringToString(passwordTextBox.SecurePassword);

            if (CzyUzytkownikZgodny(email, haslo))
            {
                Email = email;

                MessageBox.Show("Zalogowano pomyślnie!", "Sukces", MessageBoxButton.OK);
                MainWindow s1 = new MainWindow(email);
                s1.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Błędne dane logowania. Spróbuj ponownie.", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CzyUzytkownikZgodny(string email, string haslo)
        {
            return DataAcces.CzyUzytkownikZgodny(email, haslo);
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

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Rejestracja s1 = new Rejestracja();
            s1.Show();
            Close();
        }

        private void LogIn_Off(object sender, EventArgs e)
        {
            MainWindow s1 = new MainWindow();
            s1.Show();
            Close();
        }
    }
}
