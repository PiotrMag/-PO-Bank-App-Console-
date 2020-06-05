using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;


namespace AppLogic
{
    static class Archive
    {
        static private string sqlCreateTable = @"CREATE TABLE IF NOT EXISTS $tableName (id INTEGER PRIMARY KEY AUTOINCREMENT, companyName TEXT, companyType TEXT, companyCardID TEXT, companyCardType TEXT, companyBankName TEXT, companyBankID TEXT, clientName TEXT, clientID TEXT, clientCardID TEXT, clientCardType TEXT, clientBankName TEXT, clientBankID TEXT, amount REAL, bankActionResult TEXT);";

        /// <summary>
        /// Sprawdza, czy plik z bazą danych istnieje
        /// </summary>
        /// <param name="dbFilePath">Ściezka do pliku z bazą danych</param>
        /// <returns>Zwraca wartość, czy plik z bazą danych istnieje</returns>
        static public bool CheckIfDBPresent(string dbFilePath)
        {
            return File.Exists(dbFilePath);
        }

        /// <summary>
        /// Tworzy bazę danych SQLite
        /// </summary>
        /// <param name="dbFilePath">Ściezka do utworzenia bazy danych</param>
        /// <param name="tableName">Nazwa tabeli do utworzenia w nowej bazie danych</param>
        /// <exception cref="SqliteException">Wyrzuca wyjątek SQLiteException</exception>
        static public void CreateDBAndTable(string dbFilePath, string tableName)
        {
            using (var connection = new SqliteConnection("Data Source=" + dbFilePath + ";Mode=ReadWriteCreate"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sqlCreateTable;
                command.Parameters.AddWithValue("$tableName", tableName);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Dodaje rekord do bazy danych
        /// </summary>
        /// <param name="dbFilePath">Ściezka do bazy danych</param>
        /// <param name="record">Rekord do dodania</param>
        static public void AddRecord(string dbFilePath, ArchiveRecord record)
        {
            //TODO: dodać kod
        }

        /// <summary>
        /// Wykonuje komendę SQLite na bazie danych
        /// </summary>
        /// <param name="dbFilePath">Ściezka do bazy danych</param>
        /// <param name="command">Komenda do wykonania</param>
        /// <returns>Zwraca listę rekordów, które zwróciło query</returns>
        static public List<ArchiveRecord> ExecuteSQLQuery(string dbFilePath, string command)
        {
            //TODO: dodać kod
        }
    }
}
