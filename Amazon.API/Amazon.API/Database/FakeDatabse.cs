using CRM.Models;

namespace Amazon.API.Database
{
    public static class FakeDatabse
    {
        public static int NextProductId
        {
            get
            {
                if (!Products.Any())
                {
                    return 1;
                }

                return Products.Max(p => p.Id) + 1;
            }
        }
        public static List<Products> Products { get; } = new List<Products>()
            {
                new Products { Id = 1, Name = "MacBook Pro", Price = 1750.0M, Quantity = 23, Description = "Intel Core i7 2.6GHz 6-Core (9t" +
                "h Generation) 16GB 26" +
                "66MHz DDR4 RAM | 512GB SSD 16-inch 3072 x 1920 Retina Display AMD Radeon Pro 5300M GPU (4GB GDDR6) P" +
                "3 Color Gamut",
                ImageName = "https://cdn.pixabay.com/photo/2021/06/03/11/06/apple-macbook-pro-6306818_1280.png"},

                new Products { Id = 2, Name = "iPhone X", Price = 1750.0M, Quantity = 4, Description = "Dynamic island. A magical wa" +
                "y to interact with the iPhone. A16 Bionic chip with 5-core GPU",
                ImageName ="https://cdn.pixabay.com/photo/2019/03/23/19/57/smartphone-4076145_1280.png"},

                 new Products { Id = 3, Name = "Lente 43XT", Price = 950.0M, Quantity = 14, Description = "Lente de" +
                " buena calidad mano",
                ImageName ="https://cdn.pixabay.com/photo/2012/04/13/17/00/camera-32871_1280.png"},

                new Products { Id = 4, Name = "Blue chew", Price = 6000.0M, Quantity = 9999, Description = "Not available for legal reasons ",
                ImageName ="https://static.bluechew.com/assets/images/ingredients/sildenafil-sachet.png"}
            };

    }
}
