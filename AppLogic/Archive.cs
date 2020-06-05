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
        static private string sqlCreateTable = @"CREATE TABLE IF NOT EXISTS $tableName (id INTEGER PRIMARY KEY AUTOINCREMENT, companyName TEXT, companyType TEXT, companyCardID TEXT, companyCardType TEXT, companyBankName TEXT, companyBankID TEXT, clientName TEXT, clientID TEXT, clientCardID TEXT, clientCardType TEXT, clientBankName TEXT, clientBankID TEXT, amount REAL, bankActionResult TEXT);";
        static private string sqlInsertIntoTable = @"INSER INTO $tableName (companyName, companyType, companyCardID, companyCardType, companyBankName, companyBankID, clientName, clientID, clientCardID, clientCardType, clientBankName, clientBankID, amount, bankActionResult) VALUES ($companyName, $companyType, $companyCardID, $companyCardType, $companyBankName, $companyBankID, $clientName, $clientID, $clientCardID, $clientCardType, $clientBankName, $clientBankID, $amount, $bankActionResult);";

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
                command.Parameters.AddWithValue("$companyName", record.CompanyName);
                command.Parameters.AddWithValue("$companyType", record.CompanyType);
                command.Parameters.AddWithValue("$companyCardID", record.CompanyCardID);
                command.Parameters.AddWithValue("$companyCardType", record.CompanyCardType);
                command.Parameters.AddWithValue("$companyBankName", record.CompanyBankName);
                command.Parameters.AddWithValue("$companyBankID", record.CompanyBankID);
                command.Parameters.AddWithValue("$clientName", record.ClientName);
                command.Parameters.AddWithValue("$clientID", record.ClientID);
                command.Parameters.AddWithValue("$clientCardID", record.ClientCardID);
                command.Parameters.AddWithValue("$clientCardType", record.ClientCardType);
                command.Parameters.AddWithValue("$clientBankName", record.ClientBankName);
                command.Parameters.AddWithValue("$clientBankID", record.ClientBankID);
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
        static public List<ArchiveRecord> ExecuteSQLQuery(string dbFilePath, string tableName, string query)
        {
            List<ArchiveRecord> records = new List<ArchiveRecord>();
            using (var connection = new SqliteConnection("Data Source=" + dbFilePath))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sqlInsertIntoTable;
                command.ExecuteNonQuery();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.FieldCount < 15)
                            continue; //TODO: może leipej wyrzucić błąd ????
                        string companyName = reader.GetString(1);
                        string companyType = reader.GetString(2);
                        string companyCardID = reader.GetString(3);
                        string companyCardType = reader.GetString(4);
                        string companyBankName = reader.GetString(5);
                        string companyBankID = reader.GetString(6);
                        string clientName = reader.GetString(7);
                        string clientID = reader.GetString(8);
                        string clientCardID = reader.GetString(9);
                        string clientCardType = reader.GetString(10);
                        string clientBankName = reader.GetString(11);
                        string clientBankID = reader.GetString(12);
                        float amount = reader.GetFloat(13);
                        BankActionResult bankActionResult = (BankActionResult)int.Parse(reader.GetString(14));

                        ArchiveRecord newRecord = new ArchiveRecord(companyName, companyType, companyCardID, companyCardType, companyBankName, companyBankID, clientName, clientID, clientCardID, clientCardType, clientBankName, clientBankID, amount, bankActionResult);
                        records.Add(newRecord);
                    }
                }
            }
            return records;
        }
    }
}
