using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel2Sqlite
{
    /// <summary>
    /// Converts Excel Worksheet to SqlRep
    /// </summary>
    partial class SqlRepConverter
    {

        public static SqlRep ConvertToSqlRep(IXLWorksheet worksheet)
        {
            var sqlRep = new SqlRep();

            foreach (IXLColumn col in worksheet.ColumnsUsed())
            {
                var dataType = GuessType(col);
                var header = new SqlCellRep(col.FirstCellUsed().Value.ToString(), dataType);

                sqlRep.Headers.Add(header);
                sqlRep.Columns.Add(ConvertToSqlColumnRep(col, dataType));
            }
            return sqlRep;
        }

        private static SqlColumnRep ConvertToSqlColumnRep(IXLColumn col, DataType dataType)
        {
            var column = new SqlColumnRep(dataType);
            foreach (var cell in col.CellsUsed().Skip(1))
            {
                column.Cells.Add(new SqlCellRep(cell.Value.ToString(), dataType));
            }
            return column;
        }

        private static DataType GuessType(IXLColumn col)
        {
            var colDistinctValues = col
                .CellsUsed()
                .Skip(1)
                .Select(cell => cell.Value.ToString())
                .Distinct()
                .ToArray();

            return GuessType(colDistinctValues);
        }

        private static DataType GuessType(string[] values)
        {
            foreach (var type in Enum.GetValues(typeof(DataType)).Cast<DataType>())
            {
                int i;
                for (i = 0; i < values.Length; i++)
                {
                    var value = values[i];
                    if (GuessType(value) != type)
                    {
                        break;
                    }
                }
                if (i == values.Length)
                    return type;
            }
            return DataType.System_String;
        }

        private static DataType GuessType(string str)
        {

            bool boolValue;
            Int32 intValue;
            Int64 bigintValue;
            double doubleValue;
            DateTime dateValue;

            // Place checks higher in if-else statement to give higher priority to type.

            if (bool.TryParse(str, out boolValue))
                return DataType.System_Boolean;
            else if (Int32.TryParse(str, out intValue))
                return DataType.System_Int32;
            else if (Int64.TryParse(str, out bigintValue))
                return DataType.System_Int64;
            else if (double.TryParse(str, out doubleValue))
                return DataType.System_Double;
            else if (DateTime.TryParse(str, out dateValue))
                return DataType.System_DateTime;
            else return DataType.System_String;

        }
    }
}
