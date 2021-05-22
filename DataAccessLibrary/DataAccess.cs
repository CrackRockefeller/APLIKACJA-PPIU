using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.Sqlite;
using Windows.Storage;
using System.IO;

namespace DataAccessLibrary
{
    public static class DataAccess
    {
        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("appDatabase.db", CreationCollisionOption.OpenIfExists);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "appDatabase.db");
            using (SqliteConnection db =
               new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                string tableWydatki = "CREATE TABLE IF NOT EXISTS \"wydatki\" ( `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `kwota` INTEGER NOT NULL, `opis` TEXT, `data` TEXT NOT NULL, `zdjecie_paragonu` BLOB UNIQUE, `uzytkownicy_id` INTEGER, `typWydatku_id` INTEGER, FOREIGN KEY(`uzytkownicy_id`) REFERENCES `uzytkownicy`(`uzytkownicy_id`), FOREIGN KEY(`typWydatku_id`) REFERENCES `wydatki`(`typWydatku_id`) )";
                string tableTyp_wydatku = "CREATE TABLE IF NOT EXISTS \"typ_wydatku\" ( `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `nazwa` TEXT NOT NULL UNIQUE )";
                string tableUzytkowicy = "CREATE TABLE IF NOT EXISTS \"uzytkownicy\" ( `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, `email` TEXT NOT NULL UNIQUE, `imie` TEXT NOT NULL, `haslo` TEXT NOT NULL )";
                SqliteCommand createTable1 = new SqliteCommand(tableWydatki, db);
                SqliteCommand createTable2 = new SqliteCommand(tableTyp_wydatku, db);
                SqliteCommand createTable3 = new SqliteCommand(tableUzytkowicy, db);

                createTable1.ExecuteReader();
                createTable2.ExecuteReader();
                createTable3.ExecuteReader();
            }
        }

        public static List<String> GetData()
        {
            List<String> entries = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "appDatabase.db");
            using (SqliteConnection db = new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                    ("SELECT email from uzytkownicy", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }

                db.Close();
            }

            return entries;
        }
        public static void dodajUzytkownika(string email, string imie, string haslo)
        {
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "appDatabase.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand
                {
                    Connection = db,

                    // Use parameterized query to prevent SQL injection attacks
                    CommandText = "INSERT INTO uzytkownicy(email, imie, haslo) VALUES (@email, @imie, @haslo)"
                };
                insertCommand.Parameters.AddWithValue("@email", email);
                insertCommand.Parameters.AddWithValue("@imie", imie);
                insertCommand.Parameters.AddWithValue("@haslo", haslo);

                insertCommand.ExecuteReader();

                db.Close();
            }
        }

        public static List<String> checkUser(string email, string haslo)
        {
            List<String> entries = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "appDatabase.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                   ($"select exists( select email, haslo from uzytkownicy where email = \"{email}\" and haslo = \"{haslo}\" limit 1) ", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                   entries.Add(query.GetString(0));
                }
                db.Close();
            }
            return entries;
        }
        public static List<String> checkEmail(string email)
        {
            List<String> entries = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "appDatabase.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                   ($"select exists( select email, haslo from uzytkownicy where email = \"{email}\" limit 1) ", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }
                db.Close();
            }
            return entries;
        }
        public static List<String> checkPassword(string email)
        {
            List<String> entries = new List<string>();

            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "appDatabase.db");
            using (SqliteConnection db =
              new SqliteConnection($"Filename={dbpath}"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand
                   ($"select exists( select email, haslo from uzytkownicy where email = \"{email}\" limit 1) ", db);

                SqliteDataReader query = selectCommand.ExecuteReader();

                while (query.Read())
                {
                    entries.Add(query.GetString(0));
                }
                db.Close();
            }
            return entries;
        }
    }
}
