using CRM.Library.Models;
using CRM.Models;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace CRM.Library.Services
{
    public class ShoppingCartService
    {
        private static ShoppingCartService? instance; //singleton stuff 
        private static object instanceLock = new object();

    
        public Payment Payment { get; set; } 

        public ShoppingCart Cart
        {
            get
            {
                if (Current.listofCarts == null || !Current.listofCarts.Any() )
                {
                    var newCart = new ShoppingCart { Id = NextCartId, Contents = new ObservableCollection<Products>() };
                    listofCarts?.Add(newCart);
                    return newCart;
                }
                return listofCarts.First();
            }
        }


        //create a list of shopping carts
        private List<ShoppingCart> listofCarts;
        public ReadOnlyCollection<ShoppingCart> ListOfCarts
        {
            get
            {
                return listofCarts.AsReadOnly();
            }
        }
        private ShoppingCartService() 
        {
            Payment = new Payment();

            listofCarts = new List<ShoppingCart>();
            //start a car so you can select a cart from the shop page without clicking into cart 
            var newCart = new ShoppingCart { Id = NextCartId, Contents = new ObservableCollection<Products>() };
            listofCarts?.Add(newCart);


        }

        public static ShoppingCartService Current //singleton 
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ShoppingCartService();
                    }
                }
                return instance;
            }
        }

        public void AddToCart(Products newProduct) //adds into the current cart 
        {
            if (newProduct == null) return;

            var existingProduct = Current.Cart.Contents.FirstOrDefault(p => p.Id == newProduct.Id);
            var inventoryProduct = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == newProduct.Id);

            if (inventoryProduct == null || inventoryProduct.Quantity < 1)
            {

                return;
            }

            Current.Cart.Contents.Add(newProduct);
            --inventoryProduct.Quantity;
            Subtotal();
   
            
        }

        public void RemoveFromCart(Products product)
{
            var productToRemove = Cart?.Contents?.FirstOrDefault(p => p.Id == product.Id);
            if (productToRemove != null)
            {
                var inventoryProduct = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == product.Id);
                if (inventoryProduct != null)
                {
                    ++inventoryProduct.Quantity;
                }
                Cart?.Contents.Remove(productToRemove);
    }
}

        public decimal? Subtotal() //returns the price after applying the buyOneGetOne offer 
        {
            decimal subtotal = 0;
            var cart = Current.Cart;

            if (cart == null || !cart.Contents.Any())
            {
                return 0;
            }

            var productCounts = new Dictionary<int, int>(); // to keep track of duplicated products

            foreach (var product in cart.Contents)
            {
                if (productCounts.ContainsKey(product.Id))
                {
                    productCounts[product.Id]++;
                }
                else
                {
                    productCounts[product.Id] = 1;
                }

                if (product.BuyoneGetone && productCounts[product.Id] % 2 == 0) // if I have two of the same and it has the offer, then it is df
                {

                    subtotal += 0;
                }
                else
                {
                    subtotal += product.Price;
                }
            }

            cart.TotalPrice = subtotal;
            return cart.TotalPrice; 
        }

       

        public decimal FinalPrice(decimal subtotal) //sets the final price 
        {
            return subtotal + (subtotal * Payment.TaxRate);
        }
        public void ApplyTaxRate(Payment p)
        {
            Current.Cart.TaxConfig.TaxRate = p.TaxRate;
           
        }
        // Add or update a shopping cart
        public ShoppingCart AddOrUpdateCart()
        {
                var newCart = new ShoppingCart { Id = NextCartId ,Contents = new ObservableCollection<Products>() };
                Current.listofCarts.Add(newCart);
                return newCart;
        }

        //set a new Id for the cart 
        private int NextCartId
        {
            get
            {
                if (!listofCarts.Any())
                {
                    return 1;
                }
                return listofCarts.Max(p => p.Id) + 1;
            }
        }

        public void Checkout()
        {
            if (Current.Cart.Contents == null || Current.listofCarts.Count == 0)
            {
                return;
            }

            int amounttoRemove = Current.Cart.Contents.Count; //amount to remove from first car 
            Current.Cart.Contents?.Clear();

            if (Current.listofCarts.Count > 1 && Current.Cart != null && Current.listofCarts != null) //if there is more than one cart, then pass one cart content to the other
            {
                var NextCart = Current?.listofCarts?.FirstOrDefault(p => p.Id == (Current.Cart.Id + 1)); //next cart
                int amountToPass = NextCart.Contents.Count; //amount of products that I am going to pass to the second car 

                //passs from one car to the next one
                for (int i = 0; i < amountToPass; i++)
                {
                    Current.Cart?.Contents.Add(NextCart.Contents.First());
                }


                if (Current.listofCarts != null && (Current.listofCarts.Count != 1 ))
                {
                    Current.Cart.Id = NextCart.Id;
                    Current.listofCarts.Remove(NextCart);

                }
            }
            


        }

        
        public void AddToCartInCart(ShoppingCart cart, Products product) //adds the product in a cart on the lis of carts
        {
            
            var existingCart = Current.listofCarts?.FirstOrDefault(c => c.Id == cart.Id);
            var inventoryProduct = ProductServiceProxy.Current.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existingCart != null && inventoryProduct.Quantity!= null && inventoryProduct.Quantity != 0)
            {

                existingCart.Contents.Add(product);
                --inventoryProduct.Quantity; 
            }
        }

    }

}
