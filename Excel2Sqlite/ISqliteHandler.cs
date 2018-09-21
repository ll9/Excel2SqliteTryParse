using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Sqlite
{
    interface ISqliteHandler
    {
        void ExcecuteQuery(string query);
    }

    public class MockSqliteHandler : ISqliteHandler
    {
        public void ExcecuteQuery(string query)
        {
            Debug.WriteLine("Should execute Query here");
        }
    }
}
