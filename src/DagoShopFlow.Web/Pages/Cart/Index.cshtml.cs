using DagoShopFlow.Web.Models;
using DagoShopFlow.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DagoShopFlow.Web.Pages.Cart;

[ValidateAntiForgeryToken]
public class IndexModel : PageModel
{
    private readonly ICartService _cartService;

    public IndexModel(ICartService cartService)
    {
        _cartService = cartService;
    }

    public List<CartItem> CartItems { get; set; } = new();
    public decimal Total { get; set; }

    public void OnGet()
    {
        CartItems = _cartService.GetCart();
        Total = _cartService.GetTotal();
    }

    public IActionResult OnPostUpdateQuantity(int productId, int qty)
    {
        _cartService.UpdateQuantity(productId, qty);
        return RedirectToPage();
    }

    public IActionResult OnPostRemoveItem(int productId)
    {
        _cartService.RemoveItem(productId);
        return RedirectToPage();
    }
}
