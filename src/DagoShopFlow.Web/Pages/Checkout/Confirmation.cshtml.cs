using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DagoShopFlow.Web.Pages.Checkout;

public class ConfirmationModel : PageModel
{
    public string OrderId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Total { get; set; } = string.Empty;

    public IActionResult OnGet()
    {
        if (TempData["OrderId"] is null)
            return RedirectToPage("/Index");

        OrderId = TempData["OrderId"]?.ToString() ?? "";
        CustomerName = TempData["CustomerName"]?.ToString() ?? "";
        Email = TempData["Email"]?.ToString() ?? "";
        Total = TempData["Total"]?.ToString() ?? "";
        return Page();
    }
}
