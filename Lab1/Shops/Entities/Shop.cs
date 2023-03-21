using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop : IShop
{
    private readonly List<ProductStock> _products = new List<ProductStock>();
    private readonly ShopStock _stock = new ShopStock();

    public Shop(string name, string address)
    {
        ShopId = Guid.NewGuid();
        Balance = new BankAccount();
        ShopAddress = address;
        ChangeName(name);
    }

    public Guid ShopId { get; }
    public IBalance Balance { get; }
    public string ShopName { get; private set; } = string.Empty;
    public string ShopAddress { get; }

    public static bool IsValidName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        return true;
    }

    public void ChangeName(string name)
    {
        if (!IsValidName(name))
        {
            throw ShopException.InvalidNameException(name);
        }

        ShopName = name;
    }

    public ProductStock? FindProduct(Product product)
    {
        return _stock.FindProduct(product);
    }

    public void ChangePrice(Product product, decimal price)
    {
        _stock.ChangePrice(product, price);
    }

    public void RestockProducts(StockOrder products)
    {
        decimal totalCost = products.Stocks.Sum(stock => stock.TotalPrice);
        Balance.Spend(totalCost);

        _stock.RestockProducts(products);
    }

    public void BuyProducts(StockOrder products)
    {
        _stock.BuyProducts(products, out decimal totalIncome);
        Balance.Earn(totalIncome);
    }

    public bool HasEnoughStock(StockOrder products, out decimal totalCost)
    {
        return _stock.HasEnoughStock(products, out totalCost);
    }
}
