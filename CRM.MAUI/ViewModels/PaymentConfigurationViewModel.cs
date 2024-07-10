using CRM.Library.Models;
using CRM.Library.Services;
using CRM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRM.MAUI.ViewModels
{
    public class PaymentConfigurationViewModel : INotifyPropertyChanged
    {
        private Payment payment;
        public Payment PaymentConfiguration
        {
            get { return payment; }
            set
            {
                if (payment != value)
                {
                    payment = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public decimal TaxRate // Allows you to use this function to Bind it and display the current TaxRate
        {
            get { return PaymentConfiguration?.TaxRate ?? 0; }
            set
            {
                if (PaymentConfiguration != null && PaymentConfiguration.TaxRate != value)
                {
                    PaymentConfiguration.TaxRate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public void ApplyTaxRate(PaymentConfigurationViewModel? p) // Sets the new TaxRate in the Configuration 
        {
            if (p == null)
            {
                return;
            }
            ShoppingCartService.Current.ApplyTaxRate(p.PaymentConfiguration);
        }

        public ICommand ApplyTaxCommand { get; private set; }

        public PaymentConfigurationViewModel()
        {
            ApplyTaxCommand = new Command(ExecuteApplyTaxCommand);
            PaymentConfiguration = new Payment(); 
        }

        private void ExecuteApplyTaxCommand(object parameter)
        {
            var p = parameter as PaymentConfigurationViewModel;
            if (p?.PaymentConfiguration == null)
            {
                return;
            }

            ShoppingCartService.Current.ApplyTaxRate(p.PaymentConfiguration);
            NotifyPropertyChanged(nameof(CartViewModel.DisplayTax));
            Shell.Current.GoToAsync("//Management");
        }
    }
}
