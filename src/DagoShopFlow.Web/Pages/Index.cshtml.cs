using DagoShopFlow.Web.Models;
using DagoShopFlow.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DagoShopFlow.Web.Pages;

[ValidateAntiForgeryToken]
public class IndexModel : PageModel
{
    private readonly IProductService _productService;
    private readonly ICartService _cartService;

    public IndexModel(IProductService productService, ICartService cartService)
    {
        _productService = productService;
        _cartService = cartService;
    }

    public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
    public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();
    public string? SelectedCategory { get; set; }
    public string? SearchQuery { get; set; }

    public void OnGet(string? category, string? search)
    {
        SelectedCategory = category;
        SearchQuery = search;
        Categories = _productService.GetAll().Select(p => p.Category).Distinct().OrderBy(c => c);

        if (!string.IsNullOrWhiteSpace(search))
            Products = _productService.Search(search);
        else if (!string.IsNullOrWhiteSpace(category))
            Products = _productService.GetByCategory(category);
        else
            Products = _productService.GetAll();
    }

    public IActionResult OnPostAddToCart(int productId, int qty = 1)
    {
        var product = _productService.GetById(productId);
        if (product is not null)
            _cartService.AddItem(product, qty);
        return RedirectToPage();
    }
}
