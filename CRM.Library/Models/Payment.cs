using System;
using System.ComponentModel;

namespace CRM.Library.Models
{
    public class Payment : INotifyPropertyChanged
    {
        private decimal taxRate;
        public decimal TaxRate
        {
            get { return taxRate; }
            set
            {
                if (taxRate != value)
                {
                    taxRate = value;
                    NotifyPropertyChanged(nameof(TaxRate));
                }
            }
        }

        public Payment()
        {
            TaxRate = 7m; // set the Tax to 7 for default 
        }

        public Payment(Payment p)
        {
            TaxRate = p.TaxRate;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
