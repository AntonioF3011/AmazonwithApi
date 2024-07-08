using ConsoleApp.Helpers;
using CRM.Library.Services;
using CRM.Models;
using System.Diagnostics;
using System.Xml.Linq;

namespace Summer2024_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var inventoryManager = new InventoryManager();
            var shopCart = new ShopCart();
            var inventorytSvc = ProductServiceProxy.Current;

            inventorytSvc.AddOrUpdate(
            new Products
            {
                Name = "Enano",
                Description = "Increases Performance",
                Price = 10.54,
                Quantity = 10,
            });

            inventorytSvc.AddOrUpdate(
            new Products
            {
                Name = "Mata todo",
                Description = "Increases Performance",
                Price = 11.00,
                Quantity= 8,
            });
            

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine(@"//=======================\\");
                Console.WriteLine("||1. Inventory Management||");
                Console.WriteLine("||2. Shop                ||");             
                Console.WriteLine("||3. Exit                ||");
                Console.WriteLine(@"\\=======================//");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        inventoryManager.ManageInventory();
                        break;
                    case "2":
                        inventoryManager.ReadItems();
                        shopCart.Shop();
                        break;
                    case "3":
                        return;
                    default:

                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}
