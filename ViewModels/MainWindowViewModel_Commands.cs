using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PBDProject.ViewModels
{
    public partial class MainWindowViewModel
    {

        private ICommand _closeDialogHostCommand;
        private ICommand _addClientCommand;

        public ICommand CloseDialogHost
        {
            get
            {
                if (_closeDialogHostCommand == null)
                    _closeDialogHostCommand = new RelayCommand((_) => { DialogHostOpen = false; }, (_) => true);
                return _closeDialogHostCommand;
            }
        }

        public ICommand AddClientCommand
        {
            get
            {
                if (_addClientCommand == null)
                    _addClientCommand = new RelayCommand((param) => OpenDialogHost(param), (_) => true);
                return _addClientCommand;
            }
        }

        private void OpenDialogHost(object param)
        {
            string dialogType = param as string;
            AddClientDialogHostOpen = (dialogType == "client")? true : false;
            DialogHostOpen = true;
        }
    }
}
