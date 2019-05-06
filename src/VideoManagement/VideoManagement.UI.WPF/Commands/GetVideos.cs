using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VideoManagement.Business;

namespace VideoManagement.UI.WPF.Commands
{
    public class GetVideos : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private VideoMgmtService videoMgmtService;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            videoMgmtService.Get();
        }
    }
}
