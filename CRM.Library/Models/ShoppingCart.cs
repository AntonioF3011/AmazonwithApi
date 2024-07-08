using CRM.Library.Models;
using CRM.Library.Services;
using System.Collections.ObjectModel;

namespace CRM.Models
{
    public class ShoppingCart
    {
        
        public int Id{ get; set; }
        public decimal? TotalPrice { get; set; }
        public ObservableCollection<Products> Contents { get; set; }
        public Payment TaxConfig { get; set; }

        public ShoppingCart()
        {
            Contents = new ObservableCollection<Products>();
            TaxConfig = new Payment();
            

        }
       
    }
}
