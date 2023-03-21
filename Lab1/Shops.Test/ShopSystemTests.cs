using Shops.Entities;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class ShopSystemTests
{
    [Fact]
    public void RestockShop_ProductsAdded()
    {
        Shop shop = TestingConstants.NewTestShop();
        Product firstProduct = TestingConstants.NewTestProduct();
        Product secondProduct = TestingConstants.NewTestProduct();

        StockOrder order = new RestockOrderBuilder()
            .AddProduct(firstProduct, TestingConstants.ProductQuantity, TestingConstants.ProductPrice)
            .AddProduct(secondProduct, TestingConstants.ProductQuantity, TestingConstants.ProductPrice)
            .ToOrder();

        shop.RestockProducts(order);

        Assert.NotNull(shop.FindProduct(firstProduct));
        Assert.NotNull(shop.FindProduct(secondProduct));
        Assert.True(shop.HasEnoughStock(order, out _));
    }

    [Fact]
    public void ChangeProductPrice_PriceChanged()
    {
        Shop shop = TestingConstants.NewTestShop();
        Product product = TestingConstants.NewTestProduct();

        StockOrder order = new RestockOrderBuilder()
            .AddProduct(product, TestingConstants.ProductQuantity, TestingConstants.ProductPrice)
            .ToOrder();

        shop.RestockProducts(order);
        shop.ChangePrice(product, TestingConstants.ProductPrice + 1);

        Assert.Equal(TestingConstants.ProductPrice + 1, shop.FindProduct(product)?.Price);
    }

    [Fact]
    public void FindShopWithCheapestPrice_ShopFoundOrNull()
    {
        var system = new ShopSystem();
        IShop firstShop = system.AddShop(TestingConstants.ShopName, TestingConstants.ShopAddress);
        IShop secondShop = system.AddShop(TestingConstants.ShopName, TestingConstants.ShopAddress);
        Product firstProduct = system.AddProduct(TestingConstants.ProductName);
        Product secondProduct = system.AddProduct(TestingConstants.ProductName);

        firstShop.Balance.Earn(decimal.MaxValue);
        secondShop.Balance.Earn(decimal.MaxValue);

        StockOrder firstRestockOrder = new RestockOrderBuilder()
            .AddProduct(firstProduct, TestingConstants.ProductQuantity, TestingConstants.ProductPrice)
            .ToOrder();

        StockOrder secondRestockOrder = new RestockOrderBuilder()
            .AddProduct(firstProduct, TestingConstants.ProductQuantity, TestingConstants.ProductPrice + 1)
            .AddProduct(secondProduct, TestingConstants.ProductQuantity, TestingConstants.ProductPrice)
            .ToOrder();

        StockOrder firstPurchaseOrder = new PurchaseOrderBuilder()
            .AddProduct(firstProduct, TestingConstants.ProductQuantity)
            .ToOrder();

        StockOrder secondPurchaseOrder = new PurchaseOrderBuilder()
            .AddProduct(secondProduct, TestingConstants.ProductQuantity)
            .ToOrder();

        StockOrder thirdPurchaseOrder = new PurchaseOrderBuilder()
            .AddProduct(secondProduct, TestingConstants.ProductQuantity * 2)
            .ToOrder();

        system.RestockShop(firstShop, firstRestockOrder);
        system.RestockShop(secondShop, secondRestockOrder);

        Assert.Equal(firstShop, system.FindShopWithLowestPrice(firstPurchaseOrder, out _));
        Assert.Equal(secondShop, system.FindShopWithLowestPrice(secondPurchaseOrder, out _));
        Assert.Null(system.FindShopWithLowestPrice(thirdPurchaseOrder, out _));
    }

    [Fact]
    public void BuyProducts_ProductsBoughtMoneyDistributed()
    {
        var system = new ShopSystem();
        var person = new Person();
        IShop shop = system.AddShop(TestingConstants.ShopName, TestingConstants.ShopAddress);
        Product product = system.AddProduct(TestingConstants.ProductName);

        StockOrder order = new RestockOrderBuilder()
            .AddProduct(product, TestingConstants.ProductQuantity, TestingConstants.ProductPrice)
            .ToOrder();

        decimal totalPrice = TestingConstants.ProductQuantity * TestingConstants.ProductPrice;

        person.Balance.Earn(totalPrice);
        shop.Balance.Earn(totalPrice);

        system.RestockShop(shop, order);
        system.BuyProducts(shop, order, person.Balance);

        Assert.Equal(totalPrice, shop.Balance.CurrentBalance);
        Assert.Equal(0, person.Balance.CurrentBalance);
        Assert.False(shop.HasEnoughStock(order, out _));
    }
}
