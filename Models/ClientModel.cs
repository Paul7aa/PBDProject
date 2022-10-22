using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBDProject.Models
{
    public class ClientModel:BaseModel
    {
        public Int32 _idClient;
        public String _nume;
        public String _prenume;
        public DateTime _dataNasterii;
        public String _numarCard;

        public ClientModel(Int32 idClient, string nume, string prenume, DateTime dataNasterii, string numarCard)
        {
            _idClient = idClient;
            _nume = nume;
            _prenume = prenume;
            _dataNasterii = dataNasterii;
            _numarCard = numarCard;
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
        public String Nume
        {
            get => _nume;
            set
            {
                _nume = value;
                OnPropertyChanged();
            }
        }
        public String Prenume
        {
            get => _prenume;
            set
            {
                _prenume = value;
                OnPropertyChanged();
            }
        }
        public DateTime DataNasterii
        {
            get => _dataNasterii;
            set
            {
                _dataNasterii = value;
                OnPropertyChanged();
            }
        }
        public String NumarCard
        {
            get => _numarCard;
            set
            {
                _numarCard = value;
                OnPropertyChanged();
            }
        }
    }
}
