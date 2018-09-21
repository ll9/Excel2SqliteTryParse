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
    partial class ExcelHandler
    {
        private static DataTable LoadExcelDataTable(string path)
        {
            using (var workBook = new XLWorkbook(path))
            {
                using (var workSheet = workBook.Worksheet(1))
                {
                    DataTable dataTable = new DataTable();

                    //Loop through the Worksheet rows.
                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dataTable.Columns.Add(cell.Value.ToString());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dataTable.Rows.Add();
                            int i = 0;

                            foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                            {
                                dataTable.Rows[dataTable.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                        }
                    }
                    return dataTable;
                }
            }
        }

        public static void CreateDbFromExcel(string path, ISqliteHandler sqliteHandler)
        {
            var workBook = new XLWorkbook(path);
            var workSheet = workBook.Worksheet(1);


            var createTableQuery = GetCreateTableQuery(workSheet);
            sqliteHandler.ExcecuteQuery(createTableQuery);

            var insertQuery = GetInsertQuery();
        }

        private static string GetInsertQuery()
        {
            throw new NotImplementedException();
        }

        private static string GetCreateTableQuery(IXLWorksheet workSheet)
        {
            var createStatement = "CREATE TABLE FEATURES";
            foreach (var col in workSheet.ColumnsUsed())
            {
                var guessedType = GuessType(col);
            }
            var columnDeclarations = workSheet.ColumnsUsed()
                .Select(col => 
                    Regex.Replace(col.FirstCellUsed().Value.ToString(), "[^0-9a-zA-Z]+", "") + 
                    " " + 
                    GuessType(col).GetSqlDataType())
                .Aggregate((current, next) => $"{current}, {next}");

            return string.Format("{0} ({1})", createStatement, columnDeclarations);
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
