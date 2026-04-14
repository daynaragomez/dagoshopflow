using DagoShopFlow.Web.Models;
using DagoShopFlow.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DagoShopFlow.Web.Pages.Products;

[ValidateAntiForgeryToken]
public class DetailModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public DetailModel(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }

    public Product? Product { get; set; }

    public void OnGet(int id)
    {
        Product = _productService.GetById(id);
    }

    public IActionResult OnPostAddToCart(int productId, int qty = 1)
    {
        var product = _productService.GetById(productId);
        if (product is not null)
            _cartService.AddItem(product, qty);
        return RedirectToPage("/Cart/Index");
    }
}
