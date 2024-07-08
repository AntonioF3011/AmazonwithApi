using CRM.Library.Services;
using CRM.Models;

namespace ConsoleApp.Helpers
{
    public class InventoryManager
    {
        private readonly ProductServiceProxy inventorySvc;

        public InventoryManager()
        {
            inventorySvc = ProductServiceProxy.Current;
        }

        public void ManageInventory()
        {
            while (true)
            {
                Console.WriteLine("Inventory Management:");
                Console.WriteLine(@"//=======================\\");
                Console.WriteLine("||1. Create Item         ||");
                Console.WriteLine("||2. Read Items          ||");
                Console.WriteLine("||3. Update Item         ||");
                Console.WriteLine("||4. Delete Item         ||");
                Console.WriteLine("||5. Back to Menu        ||");
                Console.WriteLine(@"\\=======================//");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateItem();
                        break;
                    case "2":
                        ReadItems();
                        break;
                    case "3":
                        UpdateItem();
                        break;
                    case "4":
                        DeleteItem();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        

        private void CreateItem()
        {
            var item = new Products();

            Console.Write("Enter item name: ");
            item.Name = Console.ReadLine();
            Console.Write("Enter item description: ");
            item.Description = Console.ReadLine();
            Console.Write("Enter item price: ");
            item.Price = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter item quantity: ");
            item.Quantity = int.Parse(Console.ReadLine() ?? "0");
            inventorySvc.AddOrUpdate(item);
            Console.WriteLine("Item added !!");
            Console.WriteLine();
        }

        public void ReadItems()
        {
            var items = inventorySvc.Products;
            if (items == null || !items.Any())
            {
                Console.WriteLine("No items in inventory.");
                Console.WriteLine();
                return;
            }
            Console.WriteLine("Items in Inventory:");
            Console.WriteLine(); 

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        private void UpdateItem()
        {
            Console.WriteLine("Update Item");
            Console.WriteLine();
            Console.Write("Enter item ID to update: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            var item = inventorySvc.Products?.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            Console.Write("Enter new item name: ");
            item.Name = Console.ReadLine();
            Console.Write("Enter new item description: ");
            item.Description = Console.ReadLine();
            Console.Write("Enter new item price: ");
            item.Price = double.Parse(Console.ReadLine() ?? "0");
            Console.Write("Enter new item quantity: ");
            item.Quantity = int.Parse(Console.ReadLine() ?? "0");
            inventorySvc.AddOrUpdate(item);
            Console.WriteLine("Item updated successfully.");
            Console.WriteLine();
        }

        private void DeleteItem()
        {
            Console.Write("Enter item ID to delete: ");
            var id = int.Parse(Console.ReadLine() ?? "0");
            inventorySvc.Delete(id);
            Console.WriteLine("Item deleted");
            Console.WriteLine();
        }

        

    }
}
