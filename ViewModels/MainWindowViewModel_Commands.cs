using PBDProject.Domain.ValidationRules;
using PBDProject.Models;
using PBDProject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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
        private ICommand _openAddProdusDialogCommand;
        private ICommand _openAddVanzareDialogCommand;
        private ICommand _addClientCommand;
        private ICommand _addProdusCommand;
        private ICommand _addVanzareCommand;
        private ICommand _deleteClientCommand;
        private ICommand _deleteProdusCommand;
        private ICommand _deleteVanzareCommand;
        private ICommand _addSelectedPurchaseCommand;
        private ICommand _removePurchaseCommand;
        private ICommand _raportPersoanaCommand;

        public ICommand CloseDialogHost
        {
            get
            {
                if (_closeDialogHostCommand == null)
                    _closeDialogHostCommand = new RelayCommand((_) => { DialogHostOpen = false; RefreshAllData(); },
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

        public ICommand OpenAddVanzareDialogCommand
        {
            get
            {
                if (_openAddVanzareDialogCommand == null)
                    _openAddVanzareDialogCommand = new RelayCommand((param) => OpenDialogHost(param),
                        (_) => true);
                return _openAddVanzareDialogCommand;
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
        public ICommand AddVanzareCommand
        {
            get
            {
                if (_addVanzareCommand == null)
                    _addVanzareCommand = new RelayCommand((_) => AddVanzareOnClick(),
                        (_) => CanExecuteAddVanzare());
                return _addVanzareCommand;
            }
        }
        public ICommand DeleteClientCommand
        {
            get
            {
                if (_deleteClientCommand == null)
                    _deleteClientCommand = new RelayCommand((_) => DeleteClientOnClick(),
                        (_) => SelectedClient != null);
                return _deleteClientCommand;
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

        public ICommand DeleteVanzareCommand
        {
            get
            {
                if (_deleteVanzareCommand == null)
                    _deleteVanzareCommand = new RelayCommand((_) => DeleteVanzareOnClick(),
                        (_) => SelectedVanzare != null);
                return _deleteVanzareCommand;
            }
        }

        public ICommand AddSelectedPurchaseCommand
        {
            get
            {
                if (_addSelectedPurchaseCommand == null)
                    _addSelectedPurchaseCommand = new RelayCommand((_) => AddSelectedPurchaseOnClick(),
                        (_) => SelectedPurchase != null);
                return _addSelectedPurchaseCommand;
            }
        }

        public ICommand RemovePurchaseCommand
        {
            get
            {
                if (_removePurchaseCommand == null)
                    _removePurchaseCommand = new RelayCommand((param) => RemoveSelectedPurchaseOnClick(param),
                        (_) => true);
                return _removePurchaseCommand;
            }
        }

        public ICommand RaportPersoanaCommand
        {
            get
            {
                if (_raportPersoanaCommand == null)
                    _raportPersoanaCommand = new RelayCommand((_) => RaportPersoanaOnClick(),
                        (_) => SelectedClient != null);
                return _raportPersoanaCommand;
            }
        }

        private void OpenDialogHost(object param)
        {
            try
            {
                string dialogType = param as string;
                AddClientDialogHostOpen = (dialogType == "client") ? true : false;
                AddProdusDialogHostOpen = (dialogType == "produs") ? true : false;
                AddVanzareDialogHostOpen = (dialogType == "vanzare") ? true : false;
                RaportClientDialogHostOpen = (dialogType == "raportclient") ? true : false;
                DialogHostOpen = true;
            }
            catch (Exception ex)
            {
                ShowError(MethodBase.GetCurrentMethod().Name + "Error: " + ex.Message);
            }
        }

        private void AddClientOnClick()
        {
            try
            {
                AddClientToTable(NewClientNume, NewClientPrenume, NewClientDataNasterii, NewClientNumarCard);
                NewClientNume = String.Empty;
                NewClientPrenume = String.Empty;
                NewClientNumarCard = String.Empty;
                NewClientDataNasterii = new DateTime(DateTime.Now.Year - 10, DateTime.Now.Month, DateTime.Now.Day);
                DialogHostOpen = false;
            }
            catch (Exception ex)
            {
                ShowError(MethodBase.GetCurrentMethod().Name + "Error: " + ex.Message);
            }
        }

        private void AddProdusOnClick()
        {
            try
            {
                AddProdusToTable(NewProdus, NewProdusDescriere, NewProdusGarantie, NewProdusStoc, NewProdusValoareUnitara);
                NewProdus = String.Empty;
                NewProdusDescriere = String.Empty;
                NewProdusGarantie = String.Empty;
                NewProdusStoc = String.Empty;
                NewProdusValoareUnitara = String.Empty;
                DialogHostOpen = false;
            }
            catch (Exception ex)
            {
                ShowError(MethodBase.GetCurrentMethod().Name + "Error: " + ex.Message);
            }
        }

        private void AddSelectedPurchaseOnClick()
        {
            try
            {
                if (PurchaseCantitate <= 0 || PurchaseCantitate > SelectedPurchase.Stoc)
                {
                    ShowError("Cantitate invalidă. Cantitatea maximă este " + SelectedPurchase.Stoc + "!");
                    return;
                }
                PurchaseModels.Add(new PurchaseModel(SelectedPurchase, PurchaseCantitate));
                ProduseList.Remove(SelectedPurchase);
            }
            catch (Exception ex)
            {
                ShowError(MethodBase.GetCurrentMethod().Name + "Error: " + ex.Message);
            }
        }


        private void RemoveSelectedPurchaseOnClick(object param)
        {
            try
            {
                ProduseList.Add(PurchaseModels.Where(x => x.Produs.IdProdus == int.Parse(param.ToString()))?.FirstOrDefault()?.Produs);
                PurchaseModels.Remove(PurchaseModels.Where(x => x.Produs.IdProdus == int.Parse(param.ToString()))?.FirstOrDefault());
            }
            catch (Exception ex)
            {
                ShowError(MethodBase.GetCurrentMethod().Name + "Error: " + ex.Message);
            }
        }

        private void AddVanzareOnClick()
        {
            try
            {
                AddVanzareToTable(SelectedBuyer);
                SelectedBuyer = null;
                PurchaseModels.Clear();
                DialogHostOpen = false;
            }
            catch (Exception ex)
            {
                ShowError(MethodBase.GetCurrentMethod().Name + "Error: " + ex.Message);
            }
        }

        private void DeleteClientOnClick()
        {
            try
            {
                bool? result = new CustomMessageBox("Sunteți sigur că doriți să ștergeți clientul? ", MessageType.Confirmation, MessageButtons.YesNo) { Owner = Application.Current.MainWindow }.ShowDialog();
                if (result.HasValue && result == true)
                    DeleteClientFromTable();
            }
            catch (Exception ex)
            {
                ShowError(MethodBase.GetCurrentMethod().Name + "Error: " + ex.Message);
            }
        }

        private void DeleteProdusOnClick()
        {
            try
            {
                bool? result = new CustomMessageBox("Sunteți sigur că doriți să ștergeți produsul? ", MessageType.Confirmation, MessageButtons.YesNo) { Owner = Application.Current.MainWindow }.ShowDialog();
                if (result.HasValue && result == true)
                    DeleteProdusFromTable();
            }
            catch (Exception ex)
            {
                ShowError(MethodBase.GetCurrentMethod().Name + "Error: " + ex.Message);
            }
        }

        private void DeleteVanzareOnClick()
        {
            try
            {
                bool? result = new CustomMessageBox("Sunteți sigur că doriți să ștergeți vânzarea? ", MessageType.Confirmation, MessageButtons.YesNo) { Owner = Application.Current.MainWindow }.ShowDialog();
                if (result.HasValue && result == true)
                    DeleteVanzareFromTable();
            }
            catch (Exception ex)
            {
                ShowError(MethodBase.GetCurrentMethod().Name + "Error: " + ex.Message);
            }
        }

        public void RaportPersoanaOnClick()
        {
            GenerateClientReport();
            OpenDialogHost("raportclient");
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

        private bool CanExecuteAddVanzare()
        {
            return PurchaseModels.Count != 0 && SelectedBuyer != null;
        }
    }
}
