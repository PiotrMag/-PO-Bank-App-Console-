using System.IO;
using System.Text;

namespace AppLogic
{
    static class FileHandling
    {
        #region odczyt z pliku
        /// <summary>
        /// Odczytuje zawartość z pliku
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku do odczytu</param>
        /// <returns>Zwraca strumień do czytania. Jeżeli plik nie istnieje zwraca null</returns>
        internal static Stream GetReadingStream(string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            return new StreamReader(filePath).BaseStream;
        }
        #endregion

        #region zapis do pliku
        /// <summary>
        /// Zapisuje dane do pliku
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku do zapisu</param>
        /// <param name="content">Dane do zapisania</param>
        /// <param name="writeEvenIfExists">Czy ma zapisać do pliku, nawet jeżeli istnieje (stara zawartość zostaje usunięta)</param>
        /// <returns>Zwraca true jeżeli zapis się udał, zwraca false, jeżeli się nie udał</returns>
        internal static bool WriteFile(string filePath, string content, bool writeEvenIfExists)
        {
            if (!writeEvenIfExists)
                if (File.Exists(filePath))
                    return false;
            File.WriteAllText(filePath, content, Encoding.UTF8);
            return true;
        }
        #endregion
    }
}
