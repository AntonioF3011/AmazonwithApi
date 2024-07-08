using CRM.Library.Models;
using CRM.Library.Services;
using System.Windows.Input;

namespace CRM.Models
{
    public class Products
    {
       
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        
        public decimal Markdown { get; set; }
        
        public bool BuyoneGetone { get; set; }
        public string? ImageName { get; set; }
        
        public Products()
        {
           
        }
        public Products(Products p)
        {
            Name = p.Name;
            Description = p.Description;
            Price = p.Price;
            Id = p.Id;
            Quantity = p.Quantity;
            BuyoneGetone = p.BuyoneGetone;
            ImageName = p.ImageName;
    }

    }
}

