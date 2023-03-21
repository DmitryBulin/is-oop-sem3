using Shops.Entities;

namespace Shops.Test;

public static class TestingConstants
{
    public const string ShopName = "Test Shop";
    public const string ShopAddress = "Russia, St.Petersburg";
    public const string ProductName = "Test Product";
    public const int ProductPrice = 100;
    public const int ProductQuantity = 2;

    public static Shop NewTestShop()
    {
        var shop = new Shop(ShopName, ShopAddress);
        shop.Balance.Earn(decimal.MaxValue);
        return shop;
    }

    public static Product NewTestProduct()
    {
        return new Product(ProductName);
    }
}
