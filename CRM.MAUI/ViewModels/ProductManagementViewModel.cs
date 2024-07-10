using CRM.Library.Services;
using CRM.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CRM.MAUI.ViewModels
{
    public class ProductManagementViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<ProductViewModel> Products
        {
            get
            {
                return ProductServiceProxy.Current.Products?.Where(p => p != null)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public ProductViewModel SelectedProduct { get; set; }
        public ProductManagementViewModel()
        {

        }

        public void RefreshProducts()
        {
            NotifyPropertyChanged(nameof(Products));
        }
        public void UpdateProduct()
        {
            if (SelectedProduct?.Model == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Product?productId={SelectedProduct.Model.Id}"); //this is sending the product Id 
            ProductServiceProxy.Current.AddOrUpdate(SelectedProduct.Model);
        }

        //2.esto lo replicaste para el delete, es como el update de arriba, aquí creste el deleteC
        public void DeleteProducts()
        {
            if (SelectedProduct?.Model == null)
            {
                return;
            }
            ProductServiceProxy.Current.Delete(SelectedProduct.Model.Id);
            RefreshProducts();
        }
    }
}
