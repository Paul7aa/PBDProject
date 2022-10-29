using PBDProject.Domain.ValidationRules;
using PBDProject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PBDProject.ViewModels
{
    public partial class MainWindowViewModel
    {

        private ICommand _closeDialogHostCommand;
        private ICommand _openAddClientDialogCommand;
        private ICommand _addClientCommand;
        private ICommand _deleteClientCommand;
        private ICommand _openAddProdusDialogCommand;
        private ICommand _addProdusCommand;
        private ICommand _deleteProdusCommand;

        public ICommand CloseDialogHost
        {
            get
            {
                if (_closeDialogHostCommand == null)
                    _closeDialogHostCommand = new RelayCommand((_) => { DialogHostOpen = false; }, 
                        (_) => true);
                return _closeDialogHostCommand;
            }
        }

        public ICommand OpenAddClientDialogCommand
        {
            get
            {
                if (_openAddClientDialogCommand == null)
                    _openAddClientDialogCommand = new RelayCommand((param) => OpenDialogHost(param), 
                        (_) => true);
                return _openAddClientDialogCommand;
            }
        }

        public ICommand AddClientCommand
        {
            get
            {
                if (_addClientCommand == null)
                    _addClientCommand = new RelayCommand((_) => AddClientOnClick(), 
                        (_) => CanExecuteAddClient());
                return _addClientCommand;
            }
        }

        public ICommand DeleteClientCommand
        {
            get
            {
                if (_deleteClientCommand == null)
                    _deleteClientCommand = new RelayCommand((_) => DeleteClientOnClick(), 
                        (_) => SelectedClient!=null);
                return _deleteClientCommand;
            }
        }

        public ICommand OpenAddProdusDialogCommand
        {
            get
            {
                if (_openAddProdusDialogCommand == null)
                    _openAddProdusDialogCommand = new RelayCommand((param) => OpenDialogHost(param),
                        (_) => true);
                return _openAddProdusDialogCommand;
            }
        }

        public ICommand AddProdusCommand
        {
            get
            {
                if (_addProdusCommand == null)
                    _addProdusCommand = new RelayCommand((_) => AddProdusOnClick(),
                        (_) => CanExecuteAddProdus());
                return _addProdusCommand;
            }
        }

        public ICommand DeleteProdusCommand
        {
            get
            {
                if (_deleteProdusCommand == null)
                    _deleteProdusCommand = new RelayCommand((_) => DeleteProdusOnClick(),
                        (_) => SelectedProdus != null);
                return _deleteProdusCommand;
            }
        }
        private void OpenDialogHost(object param)
        {
            string dialogType = param as string;
            AddClientDialogHostOpen = (dialogType == "client") ? true : false;
            AddProdusDialogHostOpen = (dialogType == "produs") ? true : false;
            DialogHostOpen = true;
        }

        private void AddClientOnClick()
        {
            AddClientToTable(NewClientNume, NewClientPrenume, NewClientDataNasterii, NewClientNumarCard);
            NewClientNume = String.Empty;
            NewClientPrenume = String.Empty;
            NewClientNumarCard = String.Empty;
            NewClientDataNasterii = new DateTime(DateTime.Now.Year - 10, DateTime.Now.Month, DateTime.Now.Day);
            DialogHostOpen = false;
        }
        
        private void DeleteClientOnClick()
        {
            bool? result = new CustomMessageBox("Sunteți sigur că doriți să ștergeți clientul? ", MessageType.Confirmation, MessageButtons.YesNo) { Owner = Application.Current.MainWindow }.ShowDialog();
            if (result.HasValue && result == true)
                DeleteClientFromTable();
        }

        private void AddProdusOnClick()
        {
            AddProdusToTable(NewProdus, NewProdusDescriere, NewProdusGarantie, NewProdusStoc, NewProdusValoareUnitara);
            NewProdus = String.Empty;
            NewProdusDescriere = String.Empty;
            NewProdusGarantie = String.Empty;
            NewProdusStoc = String.Empty;
            NewProdusValoareUnitara = String.Empty;
            DialogHostOpen = false;
        }

        private void DeleteProdusOnClick()
        {
            bool? result = new CustomMessageBox("Sunteți sigur că doriți să ștergeți produsul? ", MessageType.Confirmation, MessageButtons.YesNo) { Owner = Application.Current.MainWindow }.ShowDialog();
            if (result.HasValue && result == true)
                DeleteProdusFromTable();
        }

        private bool CanExecuteAddClient()
        {
            return !String.IsNullOrEmpty(NewClientPrenume) && !String.IsNullOrEmpty(NewClientNume) && !String.IsNullOrEmpty(NewClientNumarCard);
        }

        private bool CanExecuteAddProdus()
        {
            return !String.IsNullOrEmpty(NewProdus) && !String.IsNullOrEmpty(NewProdusDescriere) && !String.IsNullOrEmpty(NewProdusGarantie)
                && !String.IsNullOrEmpty(NewProdusStoc) && !String.IsNullOrEmpty(NewProdusValoareUnitara);
        }
    }
}
