using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Excel2Sqlite
{
    class SqlRep
    {
        public IList<SqlCellRep> Headers { get; set; } = new List<SqlCellRep>();
        public IList<SqlColumnRep> Columns { get; set; } = new List<SqlColumnRep>();
    }

    public class SqlColumnRep
    {
        public IList<SqlCellRep> Cells { get; set; } = new List<SqlCellRep>();
        public DataType DataType { get; set; }

        public SqlColumnRep(DataType dataType)
        {
            DataType = dataType;
        }
    }

    public class SqlCellRep
    {
        public string Value { get; set; }
        public DataType DataType { get; set; }

        public SqlCellRep(string value, DataType dataType)
        {
            Value = Regex.Replace(value, "[^a-zA-Z0-9_.]+", "");
            DataType = dataType;
        }

        public dynamic GetDynamicValue()
        {
            return DataType.GetDynamicValue(Value);
        }
    }
}
