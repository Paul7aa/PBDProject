using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PBDProject.Models
{
    public class RaportClientRowModel: BaseModel
    {
        private String _nume;
        private String _prenume;
        private String _produs;
        private Byte _cantitate;
        private Double _valoareUnitara;
        private Double _valoareTotala;

        public RaportClientRowModel(String Nume, String Prenume, String Produs, Byte Cantitate, Double ValoareUnitara, Double ValoareTotala)
        {
            _nume = Nume;
            _prenume = Prenume;
            _produs = Produs;
            _cantitate = Cantitate;
            _valoareUnitara = ValoareUnitara;
            _valoareTotala = ValoareTotala;
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

        public String Produs
        {
            get => _produs;
            set
            {
                _produs = value;
                OnPropertyChanged();
            }
        }

        public Byte Cantitate
        {
            get => _cantitate;
            set
            {
                _cantitate = value;
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

        public Double ValoareTotala
        {
            get => _valoareTotala;
            set
            {
                _valoareTotala = value;
                OnPropertyChanged();
            }
        }
    }
}
