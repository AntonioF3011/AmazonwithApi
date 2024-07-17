using Amazon.API.Database;
using CRM.Library.DTO;
using CRM.Models;

namespace Amazon.API.EC
{
    public class InventoryEC
    {
        
        public InventoryEC(){
            
        }
        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return FakeDatabse.Products.Take(100).Select(p => new ProductDTO(p));
        }

        public async Task<ProductDTO?> Delete(int id)
        {
            var productToDelete = FakeDatabse.Products.FirstOrDefault(p => p.Id == id);
            if (productToDelete == null)
            {
                return null;
            }
            FakeDatabse.Products.Remove(productToDelete);
            return new ProductDTO(productToDelete);
        }

        public async Task<ProductDTO> AddorUpdate(ProductDTO p)
        {
            if (p == null)
            {
                throw new ArgumentNullException(nameof(p));
            }

            if (p.Id == 0)
            {
                p.Id = FakeDatabse.NextProductId;
                FakeDatabse.Products.Add(new Products(p));
            }
            else
            {
                var existingProduct = FakeDatabse.Products.FirstOrDefault(pr => pr.Id == p.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = p.Name;
                    existingProduct.Description = p.Description;
                    existingProduct.Quantity = p.Quantity;
                    existingProduct.Price = p.Price;
                    existingProduct.BuyoneGetone = p.BuyoneGetone;
                    existingProduct.Markdown = p.Markdown;

                }

                else
                {
                    FakeDatabse.Products.Add(new Products(p));
                }
            }

            return p;

            
        }
    }
}
