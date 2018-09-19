using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Sqlite
{
    class ExcelHandler
    {
        private static SQLiteConnection GetConnection(string connectionString)
        {
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
