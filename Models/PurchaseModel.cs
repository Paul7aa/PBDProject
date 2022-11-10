using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBDProject.Models
{
    public class PurchaseModel : BaseModel
    {
        private ProdusModel _produs;
        private Byte _cantitate;

        public PurchaseModel(ProdusModel produs, byte cantitate)
        {
            _produs = produs;
            _cantitate = cantitate;
        }

        public ProdusModel Produs
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
    }
}
