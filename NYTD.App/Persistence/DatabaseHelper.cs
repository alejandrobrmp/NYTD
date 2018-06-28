using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NYTD.App.Persistence
{
    public class DatabaseHelper
    {
        private static readonly string DBName = "/NYTDDB.db";
        private static readonly string DBPath = AppDomain.CurrentDomain.BaseDirectory + DBName;
        public DatabaseHelper()
        {
            
        }
    }
}
