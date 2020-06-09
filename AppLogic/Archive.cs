using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;


namespace AppLogic
{
    static class Archive
    {
        static private string sqlCreateTable = @"CREATE TABLE IF NOT EXISTS $tableName (id INTEGER PRIMARY KEY AUTOINCREMENT, fromName TEXT, fromType TEXT, fromCardID TEXT, fromCardType TEXT, fromBankName TEXT, fromBankID TEXT, toName TEXT, toID TEXT, toCardID TEXT, toCardType TEXT, toBankName TEXT, toBankID TEXT, amount REAL, bankActionResult TEXT);";
        static private string sqlInsertIntoTable = @"INSER INTO $tableName (fromName, fromType, fromCardID, fromCardType, fromBankName, fromBankID, toName, toID, toCardID, toCardType, toBankName, toBankID, amount, bankActionResult) VALUES (fromName, fromType, fromCardID, fromCardType, fromBankName, fromBankID, toName, toID, toCardID, toCardType, toBankName, toBankID, $amount, $bankActionResult);";

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
        /// <param name="tableName">Nazwa tabeli z bazy danych</param>
        /// <param name="record">Rekord do dodania</param>
        /// <exception cref="SqliteException">Wyrzuca SQLiteExceptino</exception>"
        static public void AddRecord(string dbFilePath, string tableName, ArchiveRecord record)
        {
            using (var connection = new SqliteConnection("Data Source=" + dbFilePath))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sqlInsertIntoTable;
                command.Parameters.AddWithValue("$tableName", tableName);
                command.Parameters.AddWithValue("fromName", record.FromName);
                command.Parameters.AddWithValue("fromType", record.FromType);
                command.Parameters.AddWithValue("fromCardID", record.FromCardID);
                command.Parameters.AddWithValue("fromCardType", record.FromCardType);
                command.Parameters.AddWithValue("fromBankName", record.FromBankName);
                command.Parameters.AddWithValue("fromBankID", record.FromBankID);
                command.Parameters.AddWithValue("toName", record.ToName);
                command.Parameters.AddWithValue("toID", record.ToID);
                command.Parameters.AddWithValue("toCardID", record.ToCardID);
                command.Parameters.AddWithValue("toCardType", record.ToCardType);
                command.Parameters.AddWithValue("toBankName", record.ToBankName);
                command.Parameters.AddWithValue("toBankID", record.ToBankID);
                command.Parameters.AddWithValue("$amount", record.Amount);
                command.Parameters.AddWithValue("$bankActionResult", record.Result);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Wykonuje komendę SQLite na bazie danych
        /// </summary>
        /// <param name="dbFilePath">Ściezka do bazy danych</param>
        /// <param name="command">Komenda do wykonania</param>
        /// <returns>Zwraca wynik wykonania query</returns>
        static public List<ArchiveRecord> ExecuteSQLQuery(string dbFilePath, string query)
        {
            List<ArchiveRecord> records = new List<ArchiveRecord>();
            using (var connection = new SqliteConnection("Data Source=" + dbFilePath))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = query;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.FieldCount < 15)
                            continue; //TODO: może leipej wyrzucić błąd ????
                        string fromName = reader.GetString(1);
                        string fromID = reader.GetString(2);
                        string fromType = reader.GetString(3);
                        string fromCardID = reader.GetString(4);
                        string fromCardType = reader.GetString(5);
                        string fromBankName = reader.GetString(6);
                        string fromBankID = reader.GetString(7);
                        string toName = reader.GetString(8);
                        string toID = reader.GetString(9);
                        string toType = reader.GetString(10);
                        string toCardID = reader.GetString(11);
                        string toCardType = reader.GetString(12);
                        string toBankName = reader.GetString(13);
                        string toBankID = reader.GetString(14);
                        float amount = reader.GetFloat(15);
                        BankActionResult bankActionResult = (BankActionResult)int.Parse(reader.GetString(16));

                        ArchiveRecord newRecord = new ArchiveRecord(fromName, fromID, fromType, fromCardID, fromCardType, fromBankName, fromBankID, toName, toID, toType, toCardID, toCardType, toBankName, toBankID, amount, bankActionResult);
                        records.Add(newRecord);
                    }
                }
            }
            return records;
        }
    }
}
