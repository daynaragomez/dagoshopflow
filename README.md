# DagoShopFlow

An automation-ready Razor Pages ecommerce demo built with standalone .NET 10.

## Features

- 🛍️ **Product Catalog** — Browse 6 products across Electronics and Apparel categories, with category filtering and keyword search
- 📄 **Product Detail** — View full product description and add to cart with a custom quantity
- 🛒 **Shopping Cart** — Session-based cart; update quantities, remove items, see running total
- 💳 **Checkout** — Simple order form with confirmation page
- 🤖 **Automation-ready** — Every interactive element carries a `data-testid` attribute; Playwright test project included

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Running the app

```bash
dotnet run --project src/DagoShopFlow.Web
# Open http://localhost:5005
```

## Running unit tests

```bash
dotnet test tests/DagoShopFlow.Tests
```

## Running Playwright end-to-end tests

Start the app first, then in a second terminal:

```bash
# Install Playwright browsers (first time only)
pwsh tests/DagoShopFlow.AutoTests/bin/Debug/net10.0/playwright.ps1 install

APP_BASE_URL=http://localhost:5005 dotnet test tests/DagoShopFlow.AutoTests
```

## Project structure

```
DagoShopFlow.slnx
src/
  DagoShopFlow.Web/          # Razor Pages web application (.NET 10)
    Models/                  # Product, CartItem, Order
    Services/                # ProductService (in-memory) + CartService (session)
    Pages/                   # Index, Products/Detail, Cart/Index, Checkout/Index+Confirmation
    wwwroot/css/site.css
tests/
  DagoShopFlow.Tests/        # xUnit unit tests (12 tests)
  DagoShopFlow.AutoTests/    # Playwright end-to-end tests (5 tests)
```

## data-testid reference

| Element | `data-testid` |
|---|---|
| Product card | `product-card-{id}` |
| Add-to-cart button | `add-to-cart-{id}` |
| Cart icon in nav | `cart-icon` |
| Cart item count badge | `cart-count` |
| Cart item row | `cart-item-{productId}` |
| Quantity input (cart) | `qty-input-{productId}` |
| Remove button | `remove-item-{productId}` |
| Search input | `search-input` |
| Checkout name field | `checkout-name` |
| Checkout email field | `checkout-email` |
| Checkout address field | `checkout-address` |
| Checkout city field | `checkout-city` |
| Checkout zip field | `checkout-zip` |
| Submit order button | `submit-order` |
| Order confirmation message | `order-confirmation` |