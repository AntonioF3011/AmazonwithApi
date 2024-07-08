using CRM.Library.Services;
using CRM.Models;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CRM.MAUI.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        public ShopViewModel()
        {
            InventoryQuery = string.Empty;
        }
        public Products? Model { get; set; }

        private string inventoryQuery;
        public string InventoryQuery
        {
            set
            {
                inventoryQuery = value;
                NotifyPropertyChanged();
            }
            get { return inventoryQuery; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public List<ProductViewModel> Products
        {
            get
            {
                return ProductServiceProxy.Current.Products.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList() ?? new List<ProductViewModel>();
            }
        }

        public void RefreshProducts()
        {
            InventoryQuery = string.Empty;
            NotifyPropertyChanged(nameof(Products));
        }

        public void Search()
        {
            NotifyPropertyChanged(nameof(Products));
        }

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ViewProduct(ProductViewModel product) //To see the descrptio of the product 
        {
            if (product?.Model == null)
            {
                return;
            }
            Shell.Current.GoToAsync($"//Description?productId={product.Model.Id}");//this is sending the product Id to ProductiView
        }

        //Add to cart and select cart
        public async Task AddToCart(ProductViewModel product)
        {
            if (product?.Model == null)
            {
                return;
            }
            if (ShoppingCartService.Current.ListOfCarts.Count == 1) //if it is only one then do not show displayAction, only add
            {
                ShoppingCartService.Current.AddToCart(product.Model);
                NotifyPropertyChanged(nameof(ProductsInCart));
                NotifyPropertyChanged(nameof(Products));
                NotifyPropertyChanged(nameof(TotalPrice));
                

            }
            else
            {
                var selectedCart = await SelectCartAsync(); //go to seleccartasync and waits for the selected car 
                if (selectedCart != null)
                {
                    ShoppingCartService.Current.AddToCartInCart(selectedCart.Model, product.Model); //adds the stuff
                    NotifyPropertyChanged(nameof(ProductsInCart));
                    NotifyPropertyChanged(nameof(Products));
                    NotifyPropertyChanged(nameof(TotalPrice));
                    

                }
            }
        }
       
        private async Task<CartViewModel> SelectCartAsync() //selects a cart to add the product
        {
            var cartNames = ShoppingCartService.Current.ListOfCarts.Select(c => $"Cart #{c.Id}").ToArray();
            var selectedCartName = await Application.Current.MainPage.DisplayActionSheet( //display the mini menu to select the cart
                "Select Cart", "Cancel", null, cartNames);

            var selectedCart = ShoppingCartService.Current.ListOfCarts
                .Select(c => new CartViewModel(c))
                .FirstOrDefault(c => $"Cart #{c.CartId}" == selectedCartName);
            return selectedCart;
        }
        
        public List<ProductViewModel> ProductsInCart
        {
            get
            {
                return ShoppingCartService.Current?.Cart?.Contents?.Where(p => p != null)
                    .Where(p => p?.Name?.ToUpper()?.Contains(InventoryQuery.ToUpper()) ?? false)
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public decimal? TotalPrice
        {
            get
            {
                return ShoppingCartService.Current.Subtotal();
            }
        }
        public void RemoveFromCart(ProductViewModel product)
        {
            if (product?.Model == null)
            {
                return;
            }
            ShoppingCartService.Current.RemoveFromCart(product.Model);
            NotifyPropertyChanged(nameof(ProductsInCart));
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(TotalPrice));
           
        }
    }
}
