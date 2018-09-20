using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
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

        public static void TestParse(string path)
        {
            var workBook = new XLWorkbook(path);
            var workSheet = workBook.Worksheet(1);


            foreach (var col in workSheet.ColumnsUsed())
            {
                var colDistinctValues = col
                    .CellsUsed()
                    .Skip(1)
                    .Select(cell => cell.Value)
                    .Distinct()
                    .ToArray();
            }
        }
    }
}
