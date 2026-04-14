// To run these tests, first start the web application:
//   ASPNETCORE_ENVIRONMENT=Development dotnet run --project src/DagoShopFlow.Web
// Then run:
//   dotnet test tests/DagoShopFlow.AutoTests

using Microsoft.Playwright;
using Xunit;

namespace DagoShopFlow.AutoTests;

[Collection("Playwright")]
public class ShopFlowTests : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture _fixture;
    private readonly string _baseUrl;

    public ShopFlowTests(PlaywrightFixture fixture)
    {
        _fixture = fixture;
        _baseUrl = Environment.GetEnvironmentVariable("APP_BASE_URL") ?? "http://localhost:5000";
    }

    private async Task<IPage> NewPageAsync()
    {
        var context = await _fixture.Browser.NewContextAsync();
        return await context.NewPageAsync();
    }

    [Fact(Skip = "Requires live server. Run: dotnet run --project src/DagoShopFlow.Web then dotnet test tests/DagoShopFlow.AutoTests")]
    public async Task Products_AreDisplayed()
    {
        var page = await NewPageAsync();
        await page.GotoAsync(_baseUrl);
        var cards = page.Locator("[data-testid^='product-card-']");
        await cards.First.WaitForAsync();
        Assert.True(await cards.CountAsync() >= 6);
    }

    [Fact(Skip = "Requires live server. Run: dotnet run --project src/DagoShopFlow.Web then dotnet test tests/DagoShopFlow.AutoTests")]
    public async Task AddToCart_UpdatesCartCount()
    {
        var page = await NewPageAsync();
        await page.GotoAsync(_baseUrl);
        var countBefore = await page.Locator("[data-testid='cart-count']").InnerTextAsync();
        await page.Locator("[data-testid='add-to-cart-1']").ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var countAfter = await page.Locator("[data-testid='cart-count']").InnerTextAsync();
        Assert.True(int.Parse(countAfter.Trim()) > int.Parse(countBefore.Trim()));
    }

    [Fact(Skip = "Requires live server. Run: dotnet run --project src/DagoShopFlow.Web then dotnet test tests/DagoShopFlow.AutoTests")]
    public async Task Cart_ShowsAddedProducts()
    {
        var page = await NewPageAsync();
        await page.GotoAsync(_baseUrl);
        await page.Locator("[data-testid='add-to-cart-1']").ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.Locator("[data-testid='cart-icon']").ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var cartItem = page.Locator("[data-testid='cart-item-1']");
        await cartItem.WaitForAsync();
        Assert.True(await cartItem.IsVisibleAsync());
    }

    [Fact(Skip = "Requires live server. Run: dotnet run --project src/DagoShopFlow.Web then dotnet test tests/DagoShopFlow.AutoTests")]
    public async Task Cart_RemoveItem_RemovesFromCart()
    {
        var page = await NewPageAsync();
        await page.GotoAsync(_baseUrl);
        await page.Locator("[data-testid='add-to-cart-1']").ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.GotoAsync($"{_baseUrl}/Cart");
        await page.Locator("[data-testid='remove-item-1']").ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var emptyMsg = page.Locator("[data-testid='cart-empty']");
        await emptyMsg.WaitForAsync();
        Assert.True(await emptyMsg.IsVisibleAsync());
    }

    [Fact(Skip = "Requires live server. Run: dotnet run --project src/DagoShopFlow.Web then dotnet test tests/DagoShopFlow.AutoTests")]
    public async Task Checkout_CompletesOrder()
    {
        var page = await NewPageAsync();
        await page.GotoAsync(_baseUrl);
        await page.Locator("[data-testid='add-to-cart-1']").ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.GotoAsync($"{_baseUrl}/Cart");
        await page.Locator("[data-testid='checkout-btn']").ClickAsync();
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await page.FillAsync("[data-testid='checkout-name']", "Test User");
        await page.FillAsync("[data-testid='checkout-email']", "test@example.com");
        await page.FillAsync("[data-testid='checkout-address']", "123 Test Street");
        await page.FillAsync("[data-testid='checkout-city']", "Test City");
        await page.FillAsync("[data-testid='checkout-zip']", "12345");
        await page.ClickAsync("[data-testid='submit-order']");
        await page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var confirmation = page.Locator("[data-testid='order-confirmation']");
        await confirmation.WaitForAsync();
        Assert.True(await confirmation.IsVisibleAsync());
    }
}
