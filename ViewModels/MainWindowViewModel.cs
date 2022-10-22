using PBDProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBDProject.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        private SqlConnection _sqlConnection;
        private SqlCommand _sqlCommand;
        private String _sqlConnectionString = @"Data Source=DESKTOP-IVVHEO0\SQLEXPRESS;Initial Catalog=MagazinElectronice;Integrated Security=True";
        private List<ClientModel> _clientiList = new List<ClientModel>();

        private Boolean _dialogHostOpen = false;
        private Boolean _addClientDialogHostOpen = false;

        private String _newClientNume;
        private String _newClientPrenume;
        private String _newClientNumarCard;
        private DateTime _newClientDataNasterii = DateTime.Now;
        public MainWindowViewModel()
        {
            _sqlConnection = new SqlConnection(_sqlConnectionString);
            _sqlConnection.Open();
            InitializeData();
        }

        public List<ClientModel> ClientiList
        {
            get
            {
                if (_clientiList == null)
                    _clientiList = new List<ClientModel>();
                return _clientiList;
            }
            set
            {
                _clientiList = value;
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
                //_newClientNume = String.Empty;
                //_newClientPrenume = String.Empty;
                //_newClientNumarCard = String.Empty;
                //_newClientDataNasterii = DateTime.Now;
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
            get => _newClientNume;
            set
            {
                _newClientNume = value;
                OnPropertyChanged();
            }
        }

        public String NewClientNumarCard
        {
            get => _newClientNume;
            set
            {
                _newClientNume = value;
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

    }
}
