using DagoShopFlow.Web.Models;
using DagoShopFlow.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DagoShopFlow.Web.Pages.Checkout;

[ValidateAntiForgeryToken]
public class IndexModel : PageModel
{
    private readonly ICartService _cartService;

    public IndexModel(ICartService cartService)
    {
        _cartService = cartService;
    }

    [BindProperty]
    public Order Order { get; set; } = new();

    public List<CartItem> CartItems { get; set; } = new();
    public decimal Total { get; set; }

    public void OnGet()
    {
        CartItems = _cartService.GetCart();
        Total = _cartService.GetTotal();
    }

    public IActionResult OnPost()
    {
        CartItems = _cartService.GetCart();
        Total = _cartService.GetTotal();

        if (!ModelState.IsValid)
            return Page();

        Order.Items = CartItems;
        Order.Total = Total;
        Order.CreatedAt = DateTime.UtcNow;
        Order.Id = new Random().Next(100000, 999999);

        TempData["OrderId"] = Order.Id;
        TempData["CustomerName"] = Order.CustomerName;
        TempData["Email"] = Order.Email;
        TempData["Total"] = Order.Total.ToString("0.00");

        _cartService.Clear();
        return RedirectToPage("/Checkout/Confirmation");
    }
}
