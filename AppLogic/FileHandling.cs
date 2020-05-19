using System;
using System.IO;

namespace AppLogic
{
    public static class FileHandling
    {
        /// <summary>
        /// Odczytuje zawartość z pliku
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku do odczytu</param>
        /// <returns>Zwraca strumień do czytania. Jeżeli plik nie istnieje zwraca null</returns>
        public static Stream GetReadingStream(string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            return new StreamReader(filePath).BaseStream;
        }

        /// <summary>
        /// Zapisuje dane do pliku
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku do zapisu</param>
        /// <param name="content">Dane do zapisania</param>
        /// <param name="writeEvenIfExists">Czy ma zapisać do pliku, nawet jeżeli istnieje (stara zawartość zostaje usunięta)</param>
        /// <returns>Zwraca true jeżeli zapis się udał, zwraca false, jeżeli się nie udał</returns>
        public static bool WriteFile(string filePath, string content, bool writeEvenIfExists)
        {
            if (!writeEvenIfExists)
                if (File.Exists(filePath))
                    return false;
            File.WriteAllText(filePath, content);
            return true;
        }
    }
}
