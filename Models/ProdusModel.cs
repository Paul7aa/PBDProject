using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBDProject.Models
{
    public class ProdusModel : BaseModel
    {
        Int32 _idProdus;
        String _produs;
        String _descriere;
        Byte _garantie;
        Byte _stoc;
        Double _valoareUnitara;
        
        public ProdusModel(Int32 idProdus, string produs, string descriere, Byte garantie, Byte stoc, Double valoareUnitara)
        {
            _idProdus = idProdus;
            _produs = produs;
            _descriere = descriere;
            _garantie = garantie;
            _stoc = stoc;
            _valoareUnitara = valoareUnitara;
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
        public String Produs
        {
            get => _produs;
            set
            {
                _produs = value;
                OnPropertyChanged();
            }
        }
        public String Descriere
        {
            get => _descriere;
            set
            {
                _descriere = value;
                OnPropertyChanged();
            }
        }
        public Byte Garantie
        {
            get => _garantie;
            set
            {
                _garantie = value;
                OnPropertyChanged();
            }
        }
        public Byte Stoc
        {
            get => _stoc;
            set
            {
                _stoc = value;
                OnPropertyChanged();
            }
        }
        public Double ValoareUnitara
        {
            get => _valoareUnitara;
            set
            {
                _valoareUnitara = value;
                OnPropertyChanged();
            }
        }
        
    }
}
