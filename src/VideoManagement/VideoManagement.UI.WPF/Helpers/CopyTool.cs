using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoManagement.UI.WPF.Helpers
{
    public static class CopyTool
    {
        public static void CopyToClipboard(object data)
        {
            switch (data)
            {
                case string s:
                    Clipboard.SetText((string)data, TextDataFormat.Text);
                    break;
                default:
                    break;
            }
        }
    }
}
