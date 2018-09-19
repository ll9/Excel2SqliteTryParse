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
        public static string GetDialogResult<T>(string filter = null, string initDir = null) where T: FileDialog, new()
        {
            using (var dialog = new T { Filter = filter ?? "", InitialDirectory = initDir ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop) })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
            }
            return null;
        }
    }
}
