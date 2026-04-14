using System.Text.Json;
using DagoShopFlow.Web.Models;
using Microsoft.AspNetCore.Http;

namespace DagoShopFlow.Web.Services;

public class CartService : ICartService
{
    private const string CartKey = "ShoppingCart";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext!.Session;

    public List<CartItem> GetCart()
    {
        var json = Session.GetString(CartKey);
        return json is null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(json)!;
    }

    private void SaveCart(List<CartItem> cart) =>
        Session.SetString(CartKey, JsonSerializer.Serialize(cart));

    public void AddItem(Product product, int qty)
    {
        var cart = GetCart();
        var existing = cart.FirstOrDefault(i => i.ProductId == product.Id);
        if (existing is not null)
        {
            existing.Quantity += qty;
        }
        else
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = qty,
                ImageUrl = product.ImageUrl
            });
        }
        SaveCart(cart);
    }

    public void UpdateQuantity(int productId, int qty)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            if (qty <= 0)
                cart.Remove(item);
            else
                item.Quantity = qty;
        }
        SaveCart(cart);
    }

    public void RemoveItem(int productId)
    {
        var cart = GetCart();
        cart.RemoveAll(i => i.ProductId == productId);
        SaveCart(cart);
    }

    public void Clear()
    {
        Session.Remove(CartKey);
    }

    public int GetItemCount() => GetCart().Sum(i => i.Quantity);

    public decimal GetTotal() => GetCart().Sum(i => i.Price * i.Quantity);
}
