using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Sqlite
{
    interface ISqliteHandler
    {
        void ExcecuteQuery(string query);
    }
}
