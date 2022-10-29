using PBDProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PBDProject.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        private SqlConnection _sqlConnection;
        private SqlCommand _sqlCommand;
        private String _sqlConnectionString = @"Data Source=DESKTOP-IVVHEO0\SQLEXPRESS;Initial Catalog=MagazinElectronice;Integrated Security=True";
        private ObservableCollection<ClientModel> _clientiList = new ObservableCollection<ClientModel>();
        private ObservableCollection<ProdusModel> _produseList = new ObservableCollection<ProdusModel>();
        private ClientModel _selectedClient;
        private ProdusModel _selectedProdus;

        private Boolean _dialogHostOpen = false;
        private Boolean _addClientDialogHostOpen = false;
        private Boolean _addProdusDialogHostOpen = false;

        //new client 
        private String _newClientNume = "";
        private String _newClientPrenume = "";
        private String _newClientNumarCard = "";
        private DateTime _newClientDataNasterii = new DateTime(DateTime.Now.Year - 10, DateTime.Now.Month, DateTime.Now.Day);

        //new produs
        private String _newProdus = "";
        private String _newProdusDescriere = "";
        private String _newProdusGarantie = "";
        private String _newProdusStoc = "";
        private String _newProdusValoareUnitara = "";

        private String _searchTextClient;
        private String _searchTextProdus;
        public MainWindowViewModel()
        {
            _sqlConnection = new SqlConnection(_sqlConnectionString);
            _sqlConnection.Open();
            RefreshClientData();
            RefreshProdusData();
        }

        public ObservableCollection<ClientModel> ClientiList
        {
            get
            {
                if (_clientiList == null)
                    _clientiList = new ObservableCollection<ClientModel>();
                return _clientiList;
            }
            set
            {
                _clientiList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProdusModel> ProduseList
        {
            get
            {
                if (_produseList == null)
                    _produseList = new ObservableCollection<ProdusModel>();
                return _produseList;
            }
            set
            {
                _produseList = value;
                OnPropertyChanged();
            }
        }

        public ClientModel SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged();
            }
        }

        public ProdusModel SelectedProdus
        {
            get => _selectedProdus;
            set
            {
                _selectedProdus = value;
                OnPropertyChanged();
            }
        }

        public Boolean DialogHostOpen
        {
            get => _dialogHostOpen;
            set
            {
                _dialogHostOpen = value;
                OnPropertyChanged();
            }
        }

        public Boolean AddClientDialogHostOpen
        {
            get => _addClientDialogHostOpen;
            set
            {
                _addClientDialogHostOpen = value;
                OnPropertyChanged();
            }
        }

        public Boolean AddProdusDialogHostOpen
        {
            get => _addProdusDialogHostOpen;
            set
            {
                _addProdusDialogHostOpen = value;
                OnPropertyChanged();
            }
        }

        public String NewClientNume
        {
            get => _newClientNume;
            set
            {
                _newClientNume = value;
                OnPropertyChanged();
            }
        }

        public String NewClientPrenume
        {
            get => _newClientPrenume;
            set
            {
                _newClientPrenume = value;
                OnPropertyChanged();
            }
        }

        public String NewClientNumarCard
        {
            get => _newClientNumarCard;
            set
            {
                _newClientNumarCard = value;
                OnPropertyChanged();
            }
        }

        public DateTime NewClientDataNasterii
        {
            get => _newClientDataNasterii;
            set
            {
                _newClientDataNasterii = value;
                OnPropertyChanged();
            }
        }

        public String NewProdus
        {
            get => _newProdus;
            set
            {
                _newProdus = value;
                OnPropertyChanged();
            }
        }
        
        public String NewProdusDescriere
        {
            get => _newProdusDescriere;
            set
            {
                _newProdusDescriere = value;
                OnPropertyChanged();
            }
        }

        public String NewProdusGarantie
        {
            get => _newProdusGarantie;
            set
            {
                _newProdusGarantie = value;
                OnPropertyChanged();
            }
        }

        public String NewProdusStoc
        {
            get => _newProdusStoc;
            set
            {
                _newProdusStoc = value;
                OnPropertyChanged();
            }
        }

        public String NewProdusValoareUnitara
        {
            get => _newProdusValoareUnitara;
            set
            {
                _newProdusValoareUnitara = value;
                OnPropertyChanged();
            }
        }

        public String SearchTextClient
        {
            get => _searchTextClient;
            set
            {
                _searchTextClient = value;
                RefreshClientData();
                if(!String.IsNullOrEmpty(_searchTextClient))
                    ClientiList = new ObservableCollection<ClientModel>(ClientiList
                        .Where(x => x.Nume.ToLower().Contains(_searchTextClient.Trim().ToLower())
                        || x.Prenume.ToLower().Contains(_searchTextClient.Trim().ToLower())).ToList());
                OnPropertyChanged();
            }
        }
        public String SearchTextProdus 
        {
            get => _searchTextProdus;
            set
            {
                _searchTextProdus = value;
                RefreshProdusData();
                if (!String.IsNullOrEmpty(_searchTextProdus))
                    ProduseList = new ObservableCollection<ProdusModel>(ProduseList
                        .Where(x => x.Produs.ToLower().Contains(_searchTextProdus.Trim().ToLower())).ToList());
                OnPropertyChanged();
            }
        }
    }
}
