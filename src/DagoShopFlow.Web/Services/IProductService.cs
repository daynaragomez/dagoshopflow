using DagoShopFlow.Web.Models;

namespace DagoShopFlow.Web.Services;

public interface IProductService
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
    IEnumerable<Product> GetByCategory(string category);
    IEnumerable<Product> Search(string query);
}
