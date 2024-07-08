using CRM.Library.Services;
using CRM.Models;

namespace ConsoleApp.Helpers
{
    public class ShopCart
    {
        private readonly ShoppingCartService shoppingCartService;

        public ShopCart()
        {
            shoppingCartService = ShoppingCartService.Current;
        }

        public void Shop()
        {
            while (true)
            {
                Console.WriteLine("Shop:");
                Console.WriteLine(@"//==========================\\");
                Console.WriteLine("||1. Add item to cart       ||");
                Console.WriteLine("||2. Remove item from cart  ||");
                Console.WriteLine("||3. Checkout               ||");
                Console.WriteLine("||4. Back to Menu           ||");
                Console.WriteLine(@"\\==========================//");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddToCart();
                        break;
                    case "2":
                        RemoveFromCart();
                        break;
                    case "3":
                       //
                        return;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private void AddToCart()
        {
            Console.Write("Enter product ID to add: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.Write("Enter quantity: ");
                if (int.TryParse(Console.ReadLine(), out int quantity))
                {
                    var product = new Products { Id = productId, Quantity = quantity };
                    shoppingCartService.AddToCart(product);
                    Console.WriteLine("Product added to cart.");
                }
                else
                {
                    Console.WriteLine("Invalid quantity.");
                }
            }
            else
            {
                Console.WriteLine("Invalid product ID.");
            }
        }

        private void RemoveFromCart()
        {
            Console.Write("Enter product ID to remove: ");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                shoppingCartService.RemoveFromCart(productId);
                Console.WriteLine("Product removed from cart.");
            }
            else
            {
                Console.WriteLine("Invalid product ID.");
            }
        }
    }
}


