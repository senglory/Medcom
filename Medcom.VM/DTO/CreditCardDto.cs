using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medcom.VM.DTO
{
    public class CreditCardDto : BaseObjDto
    {
        private string _number;
        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged();
            }
        }

        DateTime? _expDate;
        public DateTime? ExpDate
        {
            get { return _expDate; }
            set
            {
                _expDate = value;
                OnPropertyChanged();
            }
        }

    }
}
