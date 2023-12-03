using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Projekt_zaliczenie
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static class AppSession
        {
            public static bool IsUserLoggedIn { get; set; }
            public static string LoggedInUserEmail { get; set; }
        }
    }
}
