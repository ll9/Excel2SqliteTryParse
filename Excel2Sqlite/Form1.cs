using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
                    File.Create(dbPath);
                    ExcelHandler.CreateDbFromExcel(excelPath, sqliteHandler);
                    MessageBox.Show("Done");
                }
            }
        }
    }
}
