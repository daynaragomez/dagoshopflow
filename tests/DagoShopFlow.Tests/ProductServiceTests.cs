using DagoShopFlow.Web.Services;
using Xunit;

namespace DagoShopFlow.Tests;

public class ProductServiceTests
{
    private readonly ProductService _service = new();

    [Fact]
    public void GetAll_Returns6Products()
    {
        var products = _service.GetAll();
        Assert.Equal(6, products.Count());
    }

    [Fact]
    public void GetById_ReturnsCorrectProduct()
    {
        var product = _service.GetById(1);
        Assert.NotNull(product);
        Assert.Equal(1, product.Id);
        Assert.Equal("Wireless Headphones", product.Name);
    }

    [Fact]
    public void GetById_WithInvalidId_ReturnsNull()
    {
        var product = _service.GetById(999);
        Assert.Null(product);
    }

    [Fact]
    public void GetByCategory_FiltersCorrectly()
    {
        var electronics = _service.GetByCategory("Electronics").ToList();
        Assert.True(electronics.Count > 0);
        Assert.All(electronics, p => Assert.Equal("Electronics", p.Category));
    }

    [Fact]
    public void Search_ReturnsMatchingProducts()
    {
        var results = _service.Search("headphones").ToList();
        Assert.True(results.Count > 0);
        Assert.Contains(results, p => p.Name.Contains("Headphones", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Search_WithEmptyQuery_ReturnsAll()
    {
        var results = _service.Search("").ToList();
        Assert.Equal(6, results.Count);
    }
}
