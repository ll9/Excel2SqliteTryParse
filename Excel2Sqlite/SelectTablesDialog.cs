using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel2Sqlite
{

    public partial class SelectTablesDialog : Form
    {
        SqlRep SqlRep { get; }
        public IList<SelectSqlColumnsViewModel> SelectSqlColumns { get; set; } = new List<SelectSqlColumnsViewModel>();

        public SelectTablesDialog(SqlRep sqlRep)
        {
            InitializeComponent();
            SqlRep = sqlRep;

            foreach (var header in SqlRep.Headers)
            {
                SelectSqlColumnsViewModel item = new SelectSqlColumnsViewModel(header.Value);
                SelectSqlColumns.Add(item);

                var checkBox = new CheckBox();
                checkBox.DataBindings.Add("Checked", item, "IsSelected");
                checkBox.DataBindings.Add("Text", item, "Header");

                flowLayoutPanel1.Controls.Add(checkBox);
                
            }
        }
    }
}
