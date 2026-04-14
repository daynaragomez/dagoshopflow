using DagoShopFlow.Web.Models;
using DagoShopFlow.Web.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace DagoShopFlow.Tests;

public class CartServiceTests
{
    private readonly CartService _cartService;

    public CartServiceTests()
    {
        var session = new InMemorySession();
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(c => c.Session).Returns(session);

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

        _cartService = new CartService(httpContextAccessorMock.Object);
    }

    private static Product MakeProduct(int id = 1, string name = "Test Product", decimal price = 10m) =>
        new() { Id = id, Name = name, Price = price, ImageUrl = "", Category = "Test", Stock = 100 };

    [Fact]
    public void AddItem_AddsToCart()
    {
        _cartService.AddItem(MakeProduct(), 1);
        Assert.Single(_cartService.GetCart());
    }

    [Fact]
    public void AddItem_SameProduct_IncreasesQuantity()
    {
        _cartService.AddItem(MakeProduct(), 1);
        _cartService.AddItem(MakeProduct(), 2);
        Assert.Single(_cartService.GetCart());
        Assert.Equal(3, _cartService.GetCart()[0].Quantity);
    }

    [Fact]
    public void UpdateQuantity_UpdatesCorrectly()
    {
        _cartService.AddItem(MakeProduct(), 1);
        _cartService.UpdateQuantity(1, 5);
        Assert.Equal(5, _cartService.GetCart()[0].Quantity);
    }

    [Fact]
    public void RemoveItem_RemovesFromCart()
    {
        _cartService.AddItem(MakeProduct(), 1);
        _cartService.RemoveItem(1);
        Assert.Empty(_cartService.GetCart());
    }

    [Fact]
    public void GetTotal_CalculatesCorrectly()
    {
        _cartService.AddItem(MakeProduct(1, "A", 10m), 2);
        _cartService.AddItem(MakeProduct(2, "B", 5m), 3);
        Assert.Equal(35m, _cartService.GetTotal());
    }

    [Fact]
    public void Clear_EmptiesCart()
    {
        _cartService.AddItem(MakeProduct(), 1);
        _cartService.Clear();
        Assert.Empty(_cartService.GetCart());
    }

    private sealed class InMemorySession : ISession
    {
        private readonly Dictionary<string, byte[]> _store = new();
        public bool IsAvailable => true;
        public string Id => "test-session";
        public IEnumerable<string> Keys => _store.Keys;
        public void Clear() => _store.Clear();
        public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
        public void Remove(string key) => _store.Remove(key);
        public void Set(string key, byte[] value) => _store[key] = value;
        public bool TryGetValue(string key, out byte[] value)
        {
            var found = _store.TryGetValue(key, out var v);
            value = v!;
            return found;
        }
    }
}
