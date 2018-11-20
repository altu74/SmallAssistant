using System;
using System.IO;

namespace SmallAssistant.Droid
{
    public class AndroidDBPath: IDBPath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), filename);
        }
    }
}