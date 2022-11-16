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
        private ObservableCollection<VanzareModel> _vanzariList = new ObservableCollection<VanzareModel>();
        private ObservableCollection<RaportClientRowModel> _raportClient = new ObservableCollection<RaportClientRowModel>();
        private ObservableCollection<VanzareModel> _produseGarantiiValide = new ObservableCollection<VanzareModel>();
        private ClientModel _selectedClient;
        private ProdusModel _selectedProdus;
        private VanzareModel _selectedVanzare;

        private Boolean _dialogHostOpen = false;
        private Boolean _addClientDialogHostOpen = false;
        private Boolean _addProdusDialogHostOpen = false;
        private Boolean _addVanzareDialogHostOpen = false;
        private Boolean _raportClientDialogHostOpen = false;
        private Boolean _raportGarantiiDialogHostOpen = false;
        private Boolean _cantitateTotalaVandutaVisible = false;
        private Boolean _cheltuieliTotaleVisible = false;

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

        //new vanzare
        private ClientModel _selectedBuyer = null;
        private ProdusModel _selectedPurchase = null;
        ObservableCollection<PurchaseModel> _purchaseModels = new ObservableCollection<PurchaseModel>();
        private Byte _purchaseCantitate = 0;

        private String _searchTextClient;
        private String _searchTextProdus;
        private String _searchTextVanzare;
        private String _cantitateTotalaVanduta;
        private String _cheltuieliTotale;

        public MainWindowViewModel()
        {
            _sqlConnection = new SqlConnection(_sqlConnectionString);
            _sqlConnection.Open();
            RefreshClientData();
            RefreshProdusData();
            RefreshVanzareData();
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

        public ObservableCollection<VanzareModel> VanzariList
        {
            get
            {
                if (_vanzariList == null)
                    _vanzariList = new ObservableCollection<VanzareModel>();
                return _vanzariList ;
            }
            set
            {
                _vanzariList = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PurchaseModel> PurchaseModels
        {
            get
            {
                if (_purchaseModels == null)
                    _purchaseModels = new ObservableCollection<PurchaseModel>();
                return _purchaseModels;
            }
            set
            {
                _purchaseModels = value;
                OnPropertyChanged();
            }

        }

        public ObservableCollection<RaportClientRowModel> RaportClient
        {
            get
            {
                if (_raportClient == null)
                    _raportClient = new ObservableCollection<RaportClientRowModel>();
                return _raportClient;
            }
            set
            {
                _raportClient = value;
                OnPropertyChanged();
            }

        }

        public ObservableCollection<VanzareModel> ProduseGarantiiValide
        {
            get
            {
                if (_produseGarantiiValide == null)
                    _produseGarantiiValide = new ObservableCollection<VanzareModel>();
                return _produseGarantiiValide;
            }
            set
            {
                _produseGarantiiValide = value;
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

        public VanzareModel SelectedVanzare
        {
            get => _selectedVanzare;
            set
            {
                _selectedVanzare = value;
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

        public Boolean AddVanzareDialogHostOpen
        {
            get => _addVanzareDialogHostOpen;
            set
            {
                _addVanzareDialogHostOpen = value;
                OnPropertyChanged();
            }
        }

        public Boolean RaportClientDialogHostOpen
        {
            get => _raportClientDialogHostOpen;
            set
            {
                _raportClientDialogHostOpen = value;
                OnPropertyChanged();
            }
        }

        public Boolean RaportGarantiiDialogHostOpen
        {
            get => _raportGarantiiDialogHostOpen;
            set
            {
                _raportGarantiiDialogHostOpen = value;
                OnPropertyChanged();
            }
        }

        public Boolean CantitateTotalaVandutaVisible
        {
            get => _cantitateTotalaVandutaVisible;
            set
            {
                _cantitateTotalaVandutaVisible = value;
                OnPropertyChanged();
            }
        }

        public Boolean CheltuieliTotaleVisible
        {
            get => _cheltuieliTotaleVisible;
            set
            {
                _cheltuieliTotaleVisible = value;
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

        public ClientModel SelectedBuyer
        {
            get => _selectedBuyer;
            set
            {
                _selectedBuyer = value;
                OnPropertyChanged();
            }
        }

        public ProdusModel SelectedPurchase
        {
            get => _selectedPurchase;
            set
            {
                _selectedPurchase = value;
                PurchaseCantitate = 0;
                OnPropertyChanged();
            }
        }

        public Byte PurchaseCantitate
        {
            get => _purchaseCantitate;
            set
            {
                _purchaseCantitate = value;
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

        public String SearchTextVanzare
        {
            get => _searchTextVanzare;
            set
            {
                _searchTextVanzare = value;
                RefreshVanzareData();
                if (!String.IsNullOrEmpty(_searchTextVanzare))
                    VanzariList = new ObservableCollection<VanzareModel>(VanzariList
                        .Where(x => x.Produs.ToLower().Contains(_searchTextVanzare.Trim().ToLower())
                        || x.NumeClient.ToLower().Contains(_searchTextVanzare.Trim().ToLower())).ToList());
                OnPropertyChanged();
            }
        }

        public String CantitateTotalaVanduta
        {
            get => _cantitateTotalaVanduta;
            set
            {
                _cantitateTotalaVanduta = value;
                OnPropertyChanged();
            }
        }

        public String CheltuieliTotale
        {
            get => _cheltuieliTotale;
            set
            {
                _cheltuieliTotale = value;
                OnPropertyChanged();
            }
        }
    }
}
