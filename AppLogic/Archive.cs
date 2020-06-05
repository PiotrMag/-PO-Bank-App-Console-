using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogic
{
    static class Archive
    {
        static public bool CheckIfDBPresent(string dbFilePath)
        {
            return File.Exists(dbFilePath);
        }

        static public void CreateDB(string dbFilePath)
        {
            //TODO: uruchomic komende SQL
        }

        static public void AddRecord(string dbFilePath, ArchiveRecord record)
        {
            //TODO: dodać kod
        }

        static public List<ArchiveRecord> ExecuteSQLCommand(string dbFilePath, string command)
        {
            //TODO: dodać kod
        }
    }
}
