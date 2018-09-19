using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Excel2Sqlite
{
    class UserInteractionHandler
    {
        /// <summary>
        /// Opens Dialog
        /// User selects file
        /// </summary>
        /// <returns>Path of selected file or null if canceled</returns>
        public static string GetDialogResult()
        {
            using (var dialog = GetDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
            }
            return null;
        }

        private static OpenFileDialog GetDialog()
        {
            return new OpenFileDialog
            {
                Filter = "Excel file (*.xlsx) | *.xlsx",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
        }
    }
}
