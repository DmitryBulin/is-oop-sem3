using Shops.Models;

namespace Shops.Entities;

public interface IShop
{
    Guid ShopId { get; }
    string ShopName { get; }
    string ShopAddress { get; }
    IBalance Balance { get; }
    ProductStock? FindProduct(Product product);
    void ChangePrice(Product product, decimal price);
    void RestockProducts(StockOrder products);
    void BuyProducts(StockOrder products);
    bool HasEnoughStock(StockOrder products, out decimal totalCost);
}
