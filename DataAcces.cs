using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_zaliczenie
{
    class DataAcces
    {
        public static void InitializeDatabase()
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string tableCommand = @"CREATE TABLE IF NOT EXISTS UsersTable (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Imie TEXT,
                    Nazwisko TEXT,
                    DataUrodzenia TEXT,
                    Email TEXT,
                    Telefon TEXT,
                    ZdjProfilowe TEXT,
                    Adres TEXT,
                    StanowsikoPracy TEXT,
                    OpisStanowiskaPracy TEXT,
                    PodsumowanieZawodowe TEXT,
                    ProfilGitHub TEXT,
                    Haslo TEXT
                )";

                var createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string tableCommand = @"CREATE TABLE IF NOT EXISTS OgloszeniaTable (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    NazwaOgloszenia TEXT,
                    Zdjecie TEXT,
                    Firma TEXT,
                    Nazwastanowiska TEXT,
                    PoziomZatrudnienia TEXT,
                    RodzajUmowy TEXT,
                    RodzajPracy TEXT,
                    PracaZdalna TEXT,
                    Wynagrodzenie TEXT,
                    DniPracy TEXT,
                    GodzinyPracy TEXT,
                    DataWygasniecia TEXT,
                    Kategoria TEXT,
                    OpisStanowiska TEXT,
                    Wymagania TEXT,
                    Email TEXT
                )";

                var createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }

        public static bool CzyUzytkownikZgodny(string email, string haslo)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string query = "SELECT COUNT(*) FROM UsersTable WHERE Email = @Email AND Haslo = @Haslo";
                var command = new SqliteCommand(query, db);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Haslo", haslo);

                int userCount = Convert.ToInt32(command.ExecuteScalar());

                return userCount > 0;
            }
        }

        public static void AddUser(Users user)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string insertCommand = @"
                INSERT INTO UsersTable (
                    Imie, Nazwisko, DataUrodzenia, Email, Telefon, ZdjProfilowe, Adres,
                    StanowsikoPracy, OpisStanowiskaPracy, PodsumowanieZawodowe, ProfilGitHub, Haslo
                )
                VALUES (
                    @Imie, @Nazwisko, @DataUrodzenia, @Email, @Telefon, @ZdjProfilowe, @Adres,
                    @StanowsikoPracy, @OpisStanowiskaPracy, @PodsumowanieZawodowe, @ProfilGitHub, @Haslo
                )";

                var insertUser = new SqliteCommand(insertCommand, db);
                insertUser.Parameters.AddWithValue("@Imie", user.Imie);
                insertUser.Parameters.AddWithValue("@Nazwisko", user.Nazwisko);
                insertUser.Parameters.AddWithValue("@DataUrodzenia", user.DataUrodzenia);
                insertUser.Parameters.AddWithValue("@Email", user.Email);
                insertUser.Parameters.AddWithValue("@Telefon", user.Telefon);
                insertUser.Parameters.AddWithValue("@ZdjProfilowe", user.ZdjProfilowe);
                insertUser.Parameters.AddWithValue("@Adres", user.Adres);
                insertUser.Parameters.AddWithValue("@StanowsikoPracy", user.StanowsikoPracy);
                insertUser.Parameters.AddWithValue("@OpisStanowiskaPracy", user.OpisStanowiskaPracy);
                insertUser.Parameters.AddWithValue("@PodsumowanieZawodowe", user.PodsumowanieZawodowe);
                insertUser.Parameters.AddWithValue("@ProfilGitHub", user.ProfilGitHub);
                insertUser.Parameters.AddWithValue("@Haslo", user.Haslo);

                insertUser.ExecuteReader();
            }
        }

        public static void AddOgloszenie(Ogloszenia ogloszenie)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string insertCommand = @"
                INSERT INTO OgloszeniaTable (
                    NazwaOgloszenia, Zdjecie, Firma, Nazwastanowiska, PoziomZatrudnienia, RodzajUmowy, RodzajPracy, PracaZdalna,
                    Wynagrodzenie, DniPracy, GodzinyPracy, DataWygasniecia, Kategoria, OpisStanowiska, Wymagania, Email
                )
                VALUES (
                    @NazwaOgloszenia, @Zdjecie, @Firma, @Nazwastanowiska, @PoziomZatrudnienia, @RodzajUmowy, @RodzajPracy, @PracaZdalna,
                    @Wynagrodzenie, @DniPracy, @GodzinyPracy, @DataWygasniecia, @Kategoria, @OpisStanowiska, @Wymagania, @Email
                )";

                var insertUser = new SqliteCommand(insertCommand, db);
                insertUser.Parameters.AddWithValue("@NazwaOgloszenia", ogloszenie.NazwaOgloszenia);
                insertUser.Parameters.AddWithValue("@Zdjecie", ogloszenie.Zdjecie);
                insertUser.Parameters.AddWithValue("@Firma", ogloszenie.Firma);
                insertUser.Parameters.AddWithValue("@Nazwastanowiska", ogloszenie.Nazwastanowiska);
                insertUser.Parameters.AddWithValue("@PoziomZatrudnienia", ogloszenie.PoziomZatrudnienia);
                insertUser.Parameters.AddWithValue("@RodzajUmowy", ogloszenie.RodzajUmowy);
                insertUser.Parameters.AddWithValue("@RodzajPracy", ogloszenie.RodzajPracy);
                insertUser.Parameters.AddWithValue("@PracaZdalna", ogloszenie.PracaZdalna);
                insertUser.Parameters.AddWithValue("@Wynagrodzenie", ogloszenie.Wynagrodzenie);
                insertUser.Parameters.AddWithValue("@DniPracy", ogloszenie.DniPracy);
                insertUser.Parameters.AddWithValue("@GodzinyPracy", ogloszenie.GodzinyPracy);
                insertUser.Parameters.AddWithValue("@DataWygasniecia", ogloszenie.DataWygasniecia);
                insertUser.Parameters.AddWithValue("@Kategoria", ogloszenie.Kategoria);
                insertUser.Parameters.AddWithValue("@OpisStanowiska", ogloszenie.OpisStanowiska);
                insertUser.Parameters.AddWithValue("@Wymagania", ogloszenie.Wymagania);
                insertUser.Parameters.AddWithValue("@Email", ogloszenie.Email);



                insertUser.ExecuteReader();
            }
        }

        public static void UpdateUser(Users user)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string updateCommand = @"
                UPDATE UsersTable
                SET
                    Imie = @Imie,
                    Nazwisko = @Nazwisko,
                    DataUrodzenia = @DataUrodzenia,
                    Email = @Email,
                    Telefon = @Telefon,
                    ZdjProfilowe = @ZdjProfilowe,
                    Adres = @Adres,
                    StanowsikoPracy = @StanowsikoPracy,
                    OpisStanowiskaPracy = @OpisStanowiskaPracy,
                    PodsumowanieZawodowe = @PodsumowanieZawodowe,
                    ProfilGitHub = @ProfilGitHub,
                    Haslo = @Haslo
                WHERE Email = @Email";

                var updateUser = new SqliteCommand(updateCommand, db);
                updateUser.Parameters.AddWithValue("@Id", user.Id);
                updateUser.Parameters.AddWithValue("@Imie", user.Imie);
                updateUser.Parameters.AddWithValue("@Nazwisko", user.Nazwisko);
                updateUser.Parameters.AddWithValue("@DataUrodzenia", user.DataUrodzenia);
                updateUser.Parameters.AddWithValue("@Email", user.Email);
                updateUser.Parameters.AddWithValue("@Telefon", user.Telefon);
                updateUser.Parameters.AddWithValue("@ZdjProfilowe", user.ZdjProfilowe);
                updateUser.Parameters.AddWithValue("@Adres", user.Adres);
                updateUser.Parameters.AddWithValue("@StanowsikoPracy", user.StanowsikoPracy);
                updateUser.Parameters.AddWithValue("@OpisStanowiskaPracy", user.OpisStanowiskaPracy);
                updateUser.Parameters.AddWithValue("@PodsumowanieZawodowe", user.PodsumowanieZawodowe);
                updateUser.Parameters.AddWithValue("@ProfilGitHub", user.ProfilGitHub);
                updateUser.Parameters.AddWithValue("@Haslo", user.Haslo);

                updateUser.ExecuteReader();
            }
        }

        public static void UpdateOgloszenie(Ogloszenia ogloszenie)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string updateCommand = @"
            UPDATE OgloszeniaTable
            SET
                NazwaOgloszenia = @NazwaOgloszenia,
                Zdjecie = @Zdjecie,
                Firma = @Firma,
                Nazwastanowiska = @Nazwastanowiska,
                PoziomZatrudnienia = @PoziomZatrudnienia,
                RodzajUmowy = @RodzajUmowy,
                RodzajPracy = @RodzajPracy,
                PracaZdalna = @PracaZdalna,
                Wynagrodzenie = @Wynagrodzenie,
                DniPracy = @DniPracy,
                GodzinyPracy = @GodzinyPracy,
                DataWygasniecia = @DataWygasniecia,
                Kategoria = @Kategoria,
                OpisStanowiska = @OpisStanowiska,
                Wymagania = @Wymagania,
                Email = @Email
            WHERE Id = @Id";

                var updateOgloszenie = new SqliteCommand(updateCommand, db);
                updateOgloszenie.Parameters.AddWithValue("@Id", ogloszenie.Id);
                updateOgloszenie.Parameters.AddWithValue("@NazwaOgloszenia", ogloszenie.NazwaOgloszenia);
                updateOgloszenie.Parameters.AddWithValue("@Zdjecie", ogloszenie.Zdjecie);
                updateOgloszenie.Parameters.AddWithValue("@Firma", ogloszenie.Firma);
                updateOgloszenie.Parameters.AddWithValue("@Nazwastanowiska", ogloszenie.Nazwastanowiska);
                updateOgloszenie.Parameters.AddWithValue("@PoziomZatrudnienia", ogloszenie.PoziomZatrudnienia);
                updateOgloszenie.Parameters.AddWithValue("@RodzajUmowy", ogloszenie.RodzajUmowy);
                updateOgloszenie.Parameters.AddWithValue("@RodzajPracy", ogloszenie.RodzajPracy);
                updateOgloszenie.Parameters.AddWithValue("@PracaZdalna", ogloszenie.PracaZdalna);
                updateOgloszenie.Parameters.AddWithValue("@Wynagrodzenie", ogloszenie.Wynagrodzenie);
                updateOgloszenie.Parameters.AddWithValue("@DniPracy", ogloszenie.DniPracy);
                updateOgloszenie.Parameters.AddWithValue("@GodzinyPracy", ogloszenie.GodzinyPracy);
                updateOgloszenie.Parameters.AddWithValue("@DataWygasniecia", ogloszenie.DataWygasniecia);
                updateOgloszenie.Parameters.AddWithValue("@Kategoria", ogloszenie.Kategoria);
                updateOgloszenie.Parameters.AddWithValue("@OpisStanowiska", ogloszenie.OpisStanowiska);
                updateOgloszenie.Parameters.AddWithValue("@Wymagania", ogloszenie.Wymagania);
                updateOgloszenie.Parameters.AddWithValue("@Email", ogloszenie.Email);

                updateOgloszenie.ExecuteReader();
            }
        }

        public static void DeleteUser(string email)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string deleteCommand = "DELETE FROM UsersTable WHERE Email = @Email";

                var deleteUser = new SqliteCommand(deleteCommand, db);
                deleteUser.Parameters.AddWithValue("@Email", email);

                deleteUser.ExecuteReader();
            }
        }

        public static void DeleteOgloszenie(int id)
        {
            string dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyComputer), "PanelOgloszeniowy.db");

            using (var db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string deleteCommand = "DELETE FROM OgloszeniaTable WHERE Id = @Id";

                var deleteUser = new SqliteCommand(deleteCommand, db);
                deleteUser.Parameters.AddWithValue("@Id", id);

                deleteUser.ExecuteReader();
            }
        }
    }
}

