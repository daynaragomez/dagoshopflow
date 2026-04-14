using DagoShopFlow.Web.Models;

namespace DagoShopFlow.Web.Services;

public interface ICartService
{
    List<CartItem> GetCart();
    void AddItem(Product product, int qty);
    void UpdateQuantity(int productId, int qty);
    void RemoveItem(int productId);
    void Clear();
    int GetItemCount();
    decimal GetTotal();
}
