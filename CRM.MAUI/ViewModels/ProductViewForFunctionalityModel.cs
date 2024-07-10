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

//this view model is to allow the navigation between description views.
//(One pages redirects to the other) 


namespace CRM.MAUI.ViewModels
{
    public class ProductViewForFunctionalityModel : INotifyPropertyChanged
    {
        public Products? Model { get; set; }
        public string? Name //change or edir Name of the product 
        {
            get { return Model?.Name ?? string.Empty; }
            set
            {
                if (Model != null && Model.Name != value)
                {
                    Model.Name = value;
                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(ImageName));
                }
            }
        }

        public string? Description //Allows you to see and change the product description 
        {
            get { return Model?.Description ?? string.Empty; }
            set
            {
                if (Model != null && Model.Description != value)
                {
                    Model.Description = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string? ImageName
        {
            get { return Model?.ImageName ?? string.Empty; }
            set
            {
                if (Model != null && Model.ImageName != value)
                {
                    Model.ImageName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Id
        {
            get { return Model?.Id ?? 0; }
            set
            {
                if (Model != null && Model.Id != value)
                {
                    Model.Id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public decimal MarkDown //set or get the Markdown of a specific product 
        {
            get { return Model?.Markdown ?? 0; }
            set
            {
                if (Model != null && Model.Markdown != value)
                {
                    Model.Markdown = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //BuyoneGetone display
        public bool BuyoneGetone //To define the BuyOneGetOne variable and Display
        {
            get { return Model?.BuyoneGetone ?? false; }
            set
            {
                if (Model != null && Model?.BuyoneGetone != value)
                {
                    Model.BuyoneGetone = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string OfferStatusDescription //To Display the type of offer you are offering  in the Description 
        {
            get
            {
                if (Model?.BuyoneGetone == true)
                {
                    return "Buy One, Get the Second One Free!";
                }
                else
                {
                    return "This product has no offers available at the moment";
                }

            }

        }
    



        public int Quantity //gets the current quantity 
        {
            get { return Model?.Quantity ?? 0; }
            set
            {
                if (Model != null && Model.Quantity != value)
                {
                    Model.Quantity = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string DisplayQuantity //display the quantity with the format that I want
        {
            get { return $"There's only {Model?.Quantity} in stock "; }
        }
        public string DisplayPrice
        {
            get
            {
                if (Model == null) { return string.Empty; }
                return $"{Model.Price:C}";
            }
        }

        public decimal Price
        {
            get { return Model?.Price ?? 0; }
            set
            {
                if (Model != null && Model.Price != value)
                {
                    Model.Price = value;
                    NotifyPropertyChanged();
                }
            }
        }

       
        public ICommand? AddToCartCommand { get; private set; }
       

        public ProductViewForFunctionalityModel()
        {
            Model = new Products();
            SetupCommands();
        }

        public ProductViewForFunctionalityModel(int id)
        {
            Model = ProductServiceProxy.Current?.Products?.FirstOrDefault(p => p.Id == id);
            if (Model == null)
            {
                Model = new Products();
            }
            SetupCommands();
        }

        public ProductViewForFunctionalityModel(Products p)
        {
            Model = p;
            SetupCommands();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

     
        private void ExecuteAddToCart(ProductViewForFunctionalityModel? p)
        {
            if (p?.Model == null)
            {
                return;
            }
            ShoppingCartService.Current.AddToCart(p.Model);
            NotifyPropertyChanged(nameof(Products));
        }

    

      

        public void SetupCommands()
        {
           
          
            AddToCartCommand = new Command((p) => ExecuteAddToCart((p as ProductViewForFunctionalityModel)));
            
        }

        public string? Display
        {
            get { return ToString(); }
        }



        public List<ProductViewForFunctionalityModel> Products
        {
            get
            {
                return ProductServiceProxy.Current.Products.Where(p => p != null)

                    .Select(p => new ProductViewForFunctionalityModel(p)).ToList() ?? new List<ProductViewForFunctionalityModel>();
            }
        }

        public void RefreshProducts()
        {

            NotifyPropertyChanged(nameof(Products));
        }

        public void Search()
        {
            NotifyPropertyChanged(nameof(Products));
        }



        public void ViewProduct(ProductViewForFunctionalityModel product) //To see the descrptio of the product 
        {
            if (product?.Model == null)
            {
                return;
            }

            Shell.Current.GoToAsync($"//Description?productId={product.Model.Id}");//this is sending the product Id to ProductiView
        }

        //Add to cart and select cart
        public async Task AddToCart(ProductViewForFunctionalityModel product)
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
                


            }
            else
            {
                var selectedCart = await SelectCartAsync(); //go to seleccartasync and waits for the selected car 
                if (selectedCart != null)
                {
                    ShoppingCartService.Current.AddToCartInCart(selectedCart.Model, product.Model); //adds the stuff
                    NotifyPropertyChanged(nameof(ProductsInCart));
                    NotifyPropertyChanged(nameof(Products));
                    


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

        public List<ProductViewForFunctionalityModel> ProductsInCart
        {
            get
            {
                return ShoppingCartService.Current?.Cart?.Contents?.Where(p => p != null)

                    .Select(p => new ProductViewForFunctionalityModel(p)).ToList()
                    ?? new List<ProductViewForFunctionalityModel>();
            }
        }

     

    }
}
