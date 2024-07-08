using CRM.Library.Models;
using CRM.Library.Services;
using CRM.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CRM.MAUI.ViewModels
{
    public class CartViewModel : INotifyPropertyChanged
    {

        public ShoppingCart? Model { get; set; }
        //to notify the changes in the cart
        private ShoppingCart cart;

        public ShoppingCart Cart
        {
            get { return ShoppingCartService.Current.Cart; }
            set
            {
                if (cart != value)
                {
                    if (cart != null)
                    {
                        cart.TaxConfig.PropertyChanged -= OnTaxConfigChanged; 
                    }
                   cart = value;

                    if (cart != null)
                    {
                        cart.TaxConfig.PropertyChanged += OnTaxConfigChanged;
                    }

                    NotifyPropertyChanged();
                    NotifyPropertyChanged(nameof(SubtotalPrice));
                    NotifyPropertyChanged(nameof(Disccount));
                    NotifyPropertyChanged(nameof(DisplayTax));
                    NotifyPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        private void OnTaxConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Payment.TaxRate))
            {
                NotifyPropertyChanged(nameof(DisplayTax));
            }
        }



        public List<ProductViewModel> ProductsInCart
        {
            get
            {
                return ShoppingCartService.Current?.Cart?.Contents?.Where(p => p != null)
                    
                    .Select(p => new ProductViewModel(p)).ToList()
                    ?? new List<ProductViewModel>();
            }
        }

        public CartViewModel()
        {
            Cart = ShoppingCartService.Current.Cart;
            Cart.Contents.CollectionChanged += (s, e) =>
            {
                NotifyPropertyChanged(nameof(ProductsInCart));
                NotifyPropertyChanged(nameof(SubtotalPrice));
                NotifyPropertyChanged(nameof(Disccount));
                NotifyPropertyChanged(nameof(TotalPrice));
            };
        }
        public CartViewModel(ShoppingCart p)
        {
            Model = p;
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public decimal? SubtotalPrice //display the  subtotal 
        {
            get
            {
                return ShoppingCartService.Current.Subtotal();
            }
        }
        public decimal? TotalPrice // Display the final total 
        {
            get
            {
                var subtotal = ShoppingCartService.Current.Subtotal();
                var taxRate = ShoppingCartService.Current.Cart.TaxConfig.TaxRate;
                return subtotal * (1 + taxRate/100); // Assuming TaxRate is a porcentage
            }
        }


        public decimal? Disccount // display the discount 
        {
            get
            {
                return -1 * ((ShoppingCartService.Current.Cart.Contents?.Sum(p => p.Price) ?? 0) - (ShoppingCartService.Current.Subtotal()));
            }
        }

        public Payment TaxConfig
        {
            get { return ShoppingCartService.Current.Cart.TaxConfig; }
            set
            {
                if (TaxConfig != value) //if the tax changed then call the new one 
                {
                    if (TaxConfig != null)
                    {
                        TaxConfig.PropertyChanged -= OnTaxConfigChanged; //do not listen to the propoertychanges 
                    }

                    ShoppingCartService.Current.Cart.TaxConfig = value;

                    if (TaxConfig != null)
                    {
                        TaxConfig.PropertyChanged += OnTaxConfigChanged; //listen to the new changes
                    }

                    NotifyPropertyChanged(nameof(TaxConfig));
                    NotifyPropertyChanged(nameof(DisplayTax)); // Notify change in DisplayTax
                }
            }
        }

        public decimal DisplayTax
        {
            get
            {
                return (ShoppingCartService.Current.Cart.TaxConfig.TaxRate)/100;
            }
        }
        public void RefreshProducts()
        {
            
            NotifyPropertyChanged(nameof(Carts));
            NotifyPropertyChanged(nameof(ProductsInCart));
            NotifyPropertyChanged(nameof(TotalPrice));
            NotifyPropertyChanged(nameof(SubtotalPrice));


        }
        //================display the list of carts area=====================//
        public List<CartViewModel> Carts
        {
            get
            {
                return ShoppingCartService.Current.ListOfCarts?.Where(p => p != null)
                    .Select(p => new CartViewModel(p)).ToList()
                    ?? new List<CartViewModel>();
            }
        }

        //gets cart id for the list of carts
        public int CartId
        {
            get { return Model?.Id ?? 0; }

        }


        public string DisplayCartId //display the cartId with the format that I want for the list of carts
        {
            get { return $"🛒# {CartId}"; }
        }


        public int CartIdBill//getts the current cart Id so you can display which cart you are checking out
        {
            get { return ShoppingCartService.Current.Cart.Id; }

        }


        public string DisplayCartIdBill //display the current CartId with the format that I want for the Bill
        {
            get { return $"Current 🛒#: {CartIdBill}"; }
        }


        //=====================ADD AND REMOVE FROM CART=============================//
        //remove from cart
        public void RemoveFromCart(ProductViewModel product)
        {
            if (product?.Model == null)
            {
                return;
            }
            ShoppingCartService.Current.RemoveFromCart(product.Model);
            NotifyPropertyChanged(nameof(ProductsInCart));
            NotifyPropertyChanged(nameof(SubtotalPrice));
            NotifyPropertyChanged(nameof(Disccount));
            NotifyPropertyChanged(nameof(TotalPrice));
        }
        //add cart
        public void AddCart()
        {
            //if(Model == null)
            //{
            //    return;
            //}
            ShoppingCartService.Current.AddOrUpdateCart();
            NotifyPropertyChanged(nameof(Carts));

        }



        //================== CHECKOUT =====================//
        public void Checkout()
        {
            ShoppingCartService.Current.Checkout();
            
            NotifyPropertyChanged(nameof(Carts));
            NotifyPropertyChanged(nameof(CartId));
            NotifyPropertyChanged(nameof(DisplayCartId));
            NotifyPropertyChanged(nameof(CartIdBill));
            NotifyPropertyChanged(nameof(DisplayCartIdBill));
            RefreshProducts();

        }

    }
}
