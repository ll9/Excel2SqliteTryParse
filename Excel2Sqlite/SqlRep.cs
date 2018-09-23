using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excel2Sqlite
{
    class SqlRep
    {
        public IList<SqlCellRep> Headers { get; set; }
        public IList<SqlCellRep> Columns { get; set; }
    }

    public class SqlColumnRep
    {
        public IList<SqlCellRep> Cells { get; set; }
        public DataType DataType { get; set; }
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
