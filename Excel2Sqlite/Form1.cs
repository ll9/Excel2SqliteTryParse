using ClosedXML.Excel;
using Excel2Sqlite.Models.Sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel2Sqlite
{
    public partial class Form1 : Form
    {
        private const string Tablename = "Features";
        private ISqliteHandler sqliteHandler = new MockSqliteHandler();

        public Form1()
        {
            InitializeComponent();
        }

        private void excel2SqliteButton_Click(object sender, EventArgs e)
        {
            var excelPath = UserInteractionHandler.GetDialogResult<OpenFileDialog>("Excel (*.xlsx) | *.xlsx");
            if (excelPath != null)
            {
                var dbPath = UserInteractionHandler.GetDialogResult<SaveFileDialog>("Sqlite (*.sqlite) | *.sqlite");
                if (dbPath != null)
                {
                    //File.Create(dbPath);
                    var wb = new XLWorkbook(excelPath);

                    var rep = SqlRepConverter.ConvertToSqlRep(wb.Worksheet(1));
                    var adapter = new SqlRepAdapter(rep, new SQLiteConnection($"Data Source={dbPath};"), Tablename);
                    adapter.BuildDatabase();
                    MessageBox.Show("Done");
                }
            }
        }
    }
}
