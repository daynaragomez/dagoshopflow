using DagoShopFlow.Web.Models;

namespace DagoShopFlow.Web.Services;

public class ProductService : IProductService
{
    private readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Wireless Headphones", Description = "Premium noise-cancelling wireless headphones with 30-hour battery life and comfortable over-ear design.", Price = 79.99m, ImageUrl = "https://placehold.co/400x300?text=Headphones", Category = "Electronics", Stock = 15 },
        new Product { Id = 2, Name = "Mechanical Keyboard", Description = "Compact TKL mechanical keyboard with RGB backlighting and tactile blue switches for satisfying typing.", Price = 59.99m, ImageUrl = "https://placehold.co/400x300?text=Keyboard", Category = "Electronics", Stock = 10 },
        new Product { Id = 3, Name = "USB-C Hub", Description = "7-in-1 USB-C hub with HDMI 4K, 3 USB 3.0 ports, SD card reader, and 100W PD charging.", Price = 34.99m, ImageUrl = "https://placehold.co/400x300?text=USB+Hub", Category = "Electronics", Stock = 25 },
        new Product { Id = 4, Name = "Classic T-Shirt", Description = "100% organic cotton unisex t-shirt. Pre-shrunk, comfortable fit, available in multiple sizes.", Price = 19.99m, ImageUrl = "https://placehold.co/400x300?text=T-Shirt", Category = "Apparel", Stock = 50 },
        new Product { Id = 5, Name = "Hooded Sweatshirt", Description = "Warm fleece-lined hoodie with kangaroo pocket and adjustable drawstring. Perfect for all seasons.", Price = 44.99m, ImageUrl = "https://placehold.co/400x300?text=Hoodie", Category = "Apparel", Stock = 30 },
        new Product { Id = 6, Name = "Running Sneakers", Description = "Lightweight breathable running shoes with cushioned sole and reflective accents for night runs.", Price = 89.99m, ImageUrl = "https://placehold.co/400x300?text=Sneakers", Category = "Apparel", Stock = 20 },
    };

    public IEnumerable<Product> GetAll() => _products;

    public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public IEnumerable<Product> GetByCategory(string category) =>
        _products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<Product> Search(string query) =>
        string.IsNullOrWhiteSpace(query)
            ? _products
            : _products.Where(p =>
                p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                p.Description.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Contains(query, StringComparison.OrdinalIgnoreCase));
}
