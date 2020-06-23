using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;


namespace AppLogic
{
    static class Archive
    {
        #region pola
        static readonly string sqlCreateTable = @"CREATE TABLE transactions (id INTEGER PRIMARY KEY AUTOINCREMENT, fromName TEXT, fromID TEXT, fromType TEXT, fromCardID TEXT, fromCardType TEXT, fromBankName TEXT, fromBankID TEXT, toName TEXT, toID TEXT, toType TEXT, toCardID TEXT, toCardType TEXT, toBankName TEXT, toBankID TEXT, amount REAL, bankActionResult TEXT);";
        static readonly string sqlInsertIntoTable = @"INSERT INTO transactions (fromName, fromID, fromType, fromCardID, fromCardType, fromBankName, fromBankID, toName, toID, toType, toCardID, toCardType, toBankName, toBankID, amount, bankActionResult) VALUES (@fromName, @fromID, @fromType, @fromCardID, @fromCardType, @fromBankName, @fromBankID, @toName, @toID, @toType, @toCardID, @toCardType, @toBankName, @toBankID, @amount, @bankActionResult);";
        internal static readonly string tableName = "transactions";
        #endregion

        #region metody
        /// <summary>
        /// Sprawdza, czy plik z bazą danych istnieje
        /// </summary>
        /// <param name="dbFilePath">Ściezka do pliku z bazą danych</param>
        /// <returns>Zwraca informację, czy plik z bazą danych istnieje</returns>
        internal static bool CheckIfDBPresent(string dbFilePath)
        {
            return File.Exists(dbFilePath);
        }

        /// <summary>
        /// Tworzy bazę danych SQLite zawierającą logi transakcji
        /// </summary>
        /// <param name="dbFilePath">Ściezka do utworzenia bazy danych</param>
        /// <exception cref="SqliteException">Wyrzuca wyjątek SQLiteException</exception>
        internal static void CreateDBAndTable(string dbFilePath)
        {
            using (var connection = new SQLiteConnection("Data Source=" + dbFilePath + ";Mode=ReadWriteCreate;Version=3"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sqlCreateTable;
                int res = command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Dodaje rekord do bazy danych
        /// </summary>
        /// <param name="dbFilePath">Ściezka do bazy danych</param>
        /// <param name="record">Rekord do dodania</param>
        /// <exception cref="SqliteException">Wyrzuca SQLiteExceptino</exception>"
        internal static void AddRecord(string dbFilePath, ArchiveRecord record)
        {
            using (var connection = new SQLiteConnection("Data Source=" + dbFilePath))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sqlInsertIntoTable;
                command.Parameters.AddWithValue("@fromName", record.FromName);
                command.Parameters.AddWithValue("@fromID", record.FromID);
                command.Parameters.AddWithValue("@fromType", record.FromType);
                command.Parameters.AddWithValue("@fromCardID", record.FromCardID);
                command.Parameters.AddWithValue("@fromCardType", record.FromCardType);
                command.Parameters.AddWithValue("@fromBankName", record.FromBankName);
                command.Parameters.AddWithValue("@fromBankID", record.FromBankID);
                command.Parameters.AddWithValue("@toName", record.ToName);
                command.Parameters.AddWithValue("@toID", record.ToID);
                command.Parameters.AddWithValue("@toType", record.ToType);
                command.Parameters.AddWithValue("@toCardID", record.ToCardID);
                command.Parameters.AddWithValue("@toCardType", record.ToCardType);
                command.Parameters.AddWithValue("@toBankName", record.ToBankName);
                command.Parameters.AddWithValue("@toBankID", record.ToBankID);
                command.Parameters.AddWithValue("@amount", record.Amount);
                command.Parameters.AddWithValue("@bankActionResult", record.Result);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Wykonuje zapytanie SQLite na bazie danych
        /// </summary>
        /// <param name="dbFilePath">Ścieżka do bazy danych</param>
        /// <param name="command">Komenda do wykonania</param>
        /// <returns>Zwraca wynik wykonania zapytania</returns>
        internal static List<ArchiveRecord> ExecuteSQLQuery(string dbFilePath, string query)
        {
            try
            {
                List<ArchiveRecord> records = new List<ArchiveRecord>();
                using (var connection = new SQLiteConnection("Data Source=" + dbFilePath))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = query;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.FieldCount < 15)
                                continue;
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

                            ArchiveRecord newRecord = new ArchiveRecord(fromName, fromID, fromType, fromCardID, fromCardType, fromBankName, fromBankID, toName, toID, toType, toCardID, toCardType, toBankName, toBankID, (decimal)amount, bankActionResult);
                            records.Add(newRecord);
                        }
                    }
                }
                return records;
            }
            catch(SQLiteException ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
