using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Services;

public class ShopSystem : IShopSystem
{
    private readonly List<IShop> _shops = new List<IShop>();
    private readonly List<Product> _products = new List<Product>();

    public IShop AddShop(string name, string address)
    {
        IShop shop = new Shop(name, address);
        _shops.Add(shop);
        return shop;
    }

    public Product AddProduct(string name)
    {
        var product = new Product(name);
        _products.Add(product);
        return product;
    }

    public IShop? FindShop(Guid id)
    {
        return _shops.Find(shop => shop.ShopId == id);
    }

    public void RenameProduct(Product product, string name)
    {
        if (!_products.Contains(product))
        {
            throw ProductException.AbsentException(product.Id);
        }

        product.ChangeName(name);
    }

    public void RestockShop(IShop shop, StockOrder products)
    {
        if (!_shops.Contains(shop))
        {
            throw ShopException.ShopAbcentException(shop.ShopId);
        }

        shop.RestockProducts(products);
    }

    public void ChangeProductPrice(IShop shop, Product product, decimal newPrice)
    {
        if (!_shops.Contains(shop))
        {
            throw ShopException.ShopAbcentException(shop.ShopId);
        }

        shop.ChangePrice(product, newPrice);
    }

    public List<IShop> FindShopsWithProducts(StockOrder products)
    {
        return _shops
            .Where(shop => shop.HasEnoughStock(products, out _))
            .ToList();
    }

    public IShop? FindShopWithLowestPrice(StockOrder products, out decimal price)
    {
        IShop? result = null;
        decimal cheapestPrice = decimal.MaxValue;

        foreach (IShop shop in FindShopsWithProducts(products))
        {
            shop.HasEnoughStock(products, out decimal shopPrice);
            if (shopPrice < cheapestPrice)
            {
                result = shop;
                cheapestPrice = shopPrice;
            }
        }

        price = cheapestPrice == decimal.MaxValue ? 0 : cheapestPrice;
        return result;
    }

    public void BuyProducts(IShop shop, StockOrder products, IBalance customer)
    {
        if (!_shops.Contains(shop))
        {
            throw ShopException.ShopAbcentException(shop.ShopId);
        }

        if (!shop.HasEnoughStock(products, out decimal totalCost))
        {
            throw ShopException.NotEnoughProductsException(shop.ShopId);
        }

        customer.Spend(totalCost);
        shop.BuyProducts(products);
    }
}
