using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace PBDProject.Models
{
    public class VanzareModel : BaseModel
    {
        private Int32 _idVanzare;
        private Int32 _idProdus;
        private Int32 _idClient;
        private Byte _cantitate;
        private DateTime _dataVanzarii;
        private DateTime _dataExpirarii;

        //extra data
        private String _produs;
        private String _numeClient;

        public VanzareModel(Int32 idVanzare, Int32 idProdus, Int32 idClient, Byte cantitate, DateTime dataVanzarii, DateTime dataExpirarii)
        {
            _idVanzare = idVanzare;
            _idProdus = idProdus;
            _idClient = idClient;
            _cantitate = cantitate;
            _dataVanzarii = dataVanzarii;
            _dataExpirarii = dataExpirarii;
        }

        public VanzareModel(Int32 idProdus, String produs, DateTime dataExpirarii)
        {
            _idProdus = idProdus;
            _produs = produs;
            _dataExpirarii = dataExpirarii; 
        }

        public Int32 IdVanzare
        {
            get => _idVanzare;
            set
            {
                _idVanzare = value;
                OnPropertyChanged();
            }
        }

        public Int32 IdProdus
        {
            get => _idProdus;
            set
            {
                _idProdus = value;
                OnPropertyChanged();
            }
        }

        public Int32 IdClient
        {
            get => _idClient;
            set
            {
                _idClient = value;
                OnPropertyChanged();
            }
        }

        public Byte Cantitate
        {
            get=> _cantitate;
            set
            {
                _cantitate = value;
                OnPropertyChanged();
            }
        }

        public DateTime DataVanzarii
        {
            get => _dataVanzarii; 
            set
            {
                _dataVanzarii = value;
                OnPropertyChanged();
            }
        }

        public DateTime DataExpirarii
        {
            get => _dataExpirarii; 
            set
            {
                _dataExpirarii = value;
                OnPropertyChanged();
            }
        }

        //extra data
        public String Produs
        {
            get => _produs;
            set
            {
                _produs = value;
                OnPropertyChanged();
            }
        }

        public String NumeClient
        {
            get => _numeClient;
            set
            {
                _numeClient = value;
                OnPropertyChanged();
            }
        }
    }
}
