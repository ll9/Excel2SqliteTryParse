using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Sqlite
{
    class SqlRep
    {
    }

    public class SqlColumnRep
    {

    }

    public class SqlCellRep
    {
        public string Value { get; set; }
        public DataType DataType { get; set; }

        public dynamic GetDynamicValue()
        {
            return DataType.GetDynamicValue(Value);
        }
    }
}
